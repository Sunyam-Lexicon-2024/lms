using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LMS.Data.DbContexts;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace LMS.Client.Stores;

public class LmsUserStore(LmsDbContext context) : UserStore<User, IdentityRole, LmsDbContext>(context)
{
    public override async Task<IdentityResult> CreateAsync(User user, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);

        if (user is Student student)
        {
            Context.Add(student);
        }
        else if (user is Teacher teacher)
        {
            Context.Add(teacher);
        }
        else
        {
            Context.Add(user);
        }

        await SaveChanges(ct);
        return IdentityResult.Success;
    }
}