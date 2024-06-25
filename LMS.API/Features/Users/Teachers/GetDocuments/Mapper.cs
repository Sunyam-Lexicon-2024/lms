namespace Users.Teachers.GetDocuments;

public class Mapper : ResponseMapper<DocumentBaseModel, Document>
{
    public override DocumentBaseModel FromEntity(Document d)
    {
        return new DocumentBaseModel()
        {
            Name = d.Name,
            Url = d.Url,
            Description = d.Description,
            UploadedAt = d.UploadedAt,
            UserId = d.UploaderId,
        };
    }
}
