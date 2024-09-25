using System.Security.Principal;
using FromSingapore.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FromSingapore.WebApi.Extensions;

public static class DatabaseExtensions
{
    public static IQueryable<AppUser> WhereUserFromIdentity(this DbSet<AppUser> db, IIdentity? identity)
    {
        ArgumentNullException.ThrowIfNull(identity);
        return db.Where(u => u.Email == identity.Name);
    }
}