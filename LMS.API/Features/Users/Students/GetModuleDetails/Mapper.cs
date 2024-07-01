namespace Users.Students.GetModuleDetails
{
    public class Mapper : ResponseMapper<ModuleDetailsModel, CourseElement>
    {
        public override ModuleDetailsModel FromEntity(CourseElement m)
        {
            return new ModuleDetailsModel()
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                StartDate = m.StartDate.ToDateTime(TimeOnly.MinValue),
                EndDate = m.EndDate.ToDateTime(TimeOnly.MinValue),
                ParentId = m.ParentId,
              
            };
        }
    }
}
