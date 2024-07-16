using TMS_Project.Data;
using TMS_Project.Models;

namespace TMS_Project
{
    public static class DbInitializer
    {
        public static void Initialize(TMSContext context)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return; 
            }

            var roles = new Role[]
            {
            new Role { RoleName = "Admin" },
            new Role { RoleName = "Manager" },
            new Role { RoleName = "TeamMember" }
            };

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }

            context.SaveChanges();
        }

    }
}
