namespace LMS.API.Features.Modules.CreateModule
{
    public class Mapper : ResponseMapper<CreateModuleBaseModel, CourseElement>
    {
        public override CreateModuleBaseModel FromEntity(CourseElement ce)
        {
            return new CreateModuleBaseModel()
            {
                Name = ce.Name,
                StartDate = ce.StartDate,
                EndDate = ce.EndDate,
                CourseId = ce.ParentId
            };
        }
    }
}
