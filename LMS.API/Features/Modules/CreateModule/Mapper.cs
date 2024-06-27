namespace LMS.API.Features.Modules.CreateModule
{
    public class Mapper : ResponseMapper<CourseElement, CreateModuleBaseModel>
    {
        public override CreateModuleBaseModel ToEntity(CourseElement ce)
        {
            return new CreateModuleBaseModel()
            {
                Name = ce.Name,
                StartDate = ce.StartDate,
                EndDate = ce.EndDate,
                ParentId = ce.ParentId
            };
        }
    }
}
