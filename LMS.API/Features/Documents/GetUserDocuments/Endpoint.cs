namespace Documents.GetUserDocuments;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Get("/documents/get-user-documents/{UserId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        IEnumerable<Document> teacherDocs;
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
            return;
        }

        using var context = contextFactory.CreateDbContext();
        teacherDocs = await context.Documents
            .OfType<Document>()
            .Include(d => d.User)
            .Where(d => d.UploaderId == req.UserId)
            .ToListAsync(ct);

        var docModels = teacherDocs.Select(Map.FromEntity).ToList();

        await SendAsync(new Response { Documents = docModels }, cancellation: ct);
    }
}