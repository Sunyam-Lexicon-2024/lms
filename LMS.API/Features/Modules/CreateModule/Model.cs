//public class Request
//{
//    public CreateModuleBaseModel CreateModuleBaseModel { get; set; }
//}

public class Response
{
    public string Message { get; set; }
}

public class CreateModuleModel
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int? ParentId { get; set; }
    public int? CourseId { get; set; }
}

