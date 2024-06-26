namespace Courses.GetCourseModules;

public class Mapper : ResponseMapper<ModuleBaseModel, Module>
{
    public override ModuleBaseModel FromEntity(Module m)
    {
        return new ModuleBaseModel()
        {
            ModuleId = m.Id,
            Name = m.Name,
            StartDate = m.StartDate,
            EndDate = m.EndDate,
        };
    }
}