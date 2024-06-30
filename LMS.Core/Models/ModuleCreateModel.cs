namespace LMS.Core.Models
{
    public class ModuleCreateModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int ParentId { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
    }
}
 