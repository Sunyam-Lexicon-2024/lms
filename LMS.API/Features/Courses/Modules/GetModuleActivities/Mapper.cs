namespace Courses.Modules.GetModuleActivities;

public class Mapper : ResponseMapper<ModuleActivityBaseModel, ModuleActivity>
{
    public override ModuleActivityBaseModel FromEntity(ModuleActivity ma)
    {
        return new()
        {
            ActivityId = ma.Id,
            Name = ma.Name,
            Description = ma.Description,
            StartDate = ma.StartDate,
            EndDate = ma.EndDate,
        };
    }
}