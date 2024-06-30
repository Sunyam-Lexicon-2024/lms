namespace LMS.API.Features.Courses.Modules.CreateModule;

public class Mapper : Mapper<Request, Response, Module>
{
    public override Module ToEntity(Request req)
    {
        return new Module()
        {
            Name = req.Name,
            Description = req.Description,
            ParentId = req.ParentId,
            StartDate = req.StartDate,
            EndDate = req.EndDate,
        };
    }

    public override Response FromEntity(Module m)
    {
        return new Response()
        {
            ModuleId = m.Id,
            Name = m.Name
        };
    }
}
