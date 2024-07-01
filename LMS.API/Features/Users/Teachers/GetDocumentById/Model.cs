using LMS.Core.Models;
namespace Users.Teachers.GetDocumentById;

public class Response
{
    public DocumentBaseModel Document { get; set; }
}

public class Request
{
    public string UserId { get; set; }
    public int Id { get; set; }
}
