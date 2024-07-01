namespace Documents.GetDocumentById;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure() => Get("/documents/{UserId}/get-document-by-id/{Id}");

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        DocumentBaseModel teacherDoc;
        IDbContextFactory<LmsDbContext>? contextFactory = TryResolve<IDbContextFactory<LmsDbContext>>();

        if (contextFactory is null)
        {
            ThrowError("Could not resolve DbContextFactory Service");
            return;
        }

        using var context = contextFactory.CreateDbContext();
        var tDoc = await context.Documents
            .OfType<Document>()
            .Include(d => d.User)
            .Where(d => d.UploaderId == req.UserId && d.Id == req.Id)
            .FirstOrDefaultAsync(ct);
        
        if (tDoc is null)
        {
            await SendNotFoundAsync(cancellation: ct);
            return;
        }

        teacherDoc = Map.FromEntity(tDoc);
        await SendAsync(new Response { Document = teacherDoc }, cancellation: ct);
    }
}