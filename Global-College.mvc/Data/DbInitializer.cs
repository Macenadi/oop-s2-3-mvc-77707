using Global_College.domain.Models.Administrator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Global_College.mvc.Data
{
    public static class DbInitializer
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            string[] roles = { "Administrator", "Faculty", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Admin
            var adminEmail = "admin@college.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, "Admin123!");
                await userManager.AddToRoleAsync(adminUser, "Administrator");
            }

            // Faculty
            var facultyEmail = "faculty1@college.com";
            var facultyUser = await userManager.FindByEmailAsync(facultyEmail);
            if (facultyUser == null)
            {
                facultyUser = new IdentityUser
                {
                    UserName = facultyEmail,
                    Email = facultyEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(facultyUser, "Faculty123!");
                await userManager.AddToRoleAsync(facultyUser, "Faculty");
            }

            // Student 1
            var student1Email = "student1@college.com";
            var student1User = await userManager.FindByEmailAsync(student1Email);
            if (student1User == null)
            {
                student1User = new IdentityUser
                {
                    UserName = student1Email,
                    Email = student1Email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(student1User, "Student123!");
                await userManager.AddToRoleAsync(student1User, "Student");
            }

            // Student 2
            var student2Email = "student2@college.com";
            var student2User = await userManager.FindByEmailAsync(student2Email);
            if (student2User == null)
            {
                student2User = new IdentityUser
                {
                    UserName = student2Email,
                    Email = student2Email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(student2User, "Student123!");
                await userManager.AddToRoleAsync(student2User, "Student");
            }

            // Link FacultyProfile
            var facultyProfile = await context.FacultyProfiles.FirstOrDefaultAsync();
            if (facultyProfile != null)
            {
                facultyProfile.IdentityUserId = facultyUser!.Id;
            }

            // Link StudentProfiles
            var studentProfile1 = await context.StudentProfiles.OrderBy(s => s.Id).FirstOrDefaultAsync();
            var studentProfile2 = await context.StudentProfiles.OrderBy(s => s.Id).Skip(1).FirstOrDefaultAsync();

            if (studentProfile1 != null)
            {
                studentProfile1.IdentityUserId = student1User!.Id;
                studentProfile1.Email = student1Email;
                studentProfile1.FullName = "Student One";
            }

            if (studentProfile2 != null)
            {
                studentProfile2.IdentityUserId = student2User!.Id;
                studentProfile2.Email = student2Email;
                studentProfile2.FullName = "Student Two";
            }

            await context.SaveChangesAsync();
        }
    }
}