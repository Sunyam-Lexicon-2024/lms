namespace Users.Students.GetModulesForStudent;

public class Mapper : Mapper<Request, Response, IEnumerable<Module>>
{
    public override Response FromEntity(IEnumerable<Module> modules)
    {
        return new Response()
        {
            Modules = modules
        };
    }
}