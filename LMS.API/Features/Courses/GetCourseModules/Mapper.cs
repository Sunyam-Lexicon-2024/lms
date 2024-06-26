namespace Courses.GetCourseModules;

public class Mapper : ResponseMapper<ModuleModel, Module>
{
    public override ModuleModel FromEntity(Module m)
    {
        return new ModuleModel()
        {
            Name = m.Name,
            StartDate = m.StartDate,
            EndDate = m.EndDate,
        };
    }
}