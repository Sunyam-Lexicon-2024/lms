using LMS.Core.Models;

namespace Users.Teachers.GetDocuments;

public class Response
{
    public IEnumerable<DocumentBaseModel> Documents { get; set; }
}

public class Request
{
    public string UserId { get; set; }
}
