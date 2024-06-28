namespace Courses.Modules.Activities.CreateActivity;

public class Mapper : Mapper<Request, Response, ModuleActivity>
{

    public override ModuleActivity ToEntity(Request req)
    {
        return new ModuleActivity()
        {
            Name = req.Name,
            Description = req.Description,
            Type = req.Type,
            StartDate = req.StartDate,
            EndDate = req.EndDate,
            ParentId = req.ParentId,
        };
    }

    public override Response FromEntity(ModuleActivity ma)
    {
        return new Response()
        {
            ActivityId = ma.Id,
            Name = ma.Name,
        };
    }
}