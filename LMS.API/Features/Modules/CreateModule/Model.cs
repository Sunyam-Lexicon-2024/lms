public class Request
{
    public CreateModuleBaseModel CreateModuleBaseModel { get; set; }
}

public class Response
{
    //
}

public class CreateModuleBaseModel
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int ParentId { get; set; }
}

