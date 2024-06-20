namespace Users.Students.GetModulesForStudent
{
    public class Request
    {
        public int StudentId { get; set; }
    }

    public class Response
    {
        public IEnumerable<Module> Modules { get; set; } = [];
    }
}