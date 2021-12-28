using Imdb.Identity.Enteties;
using Microsoft.AspNetCore.Identity;

namespace Imdb.Identity
{
    public static class UserCreator
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {

            var applicationUsers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    FirstName = "UserOne",
                    LastName = "UserFirst",
                    UserName = "user_one",
                    Email = "userone@test.com",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    FirstName = "UserTwo",
                    LastName = "UserSecond",
                    UserName = "user_two",
                    Email = "usertwo@test.com",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    FirstName = "UserThree",
                    LastName = "UserThird",
                    UserName = "user_three",
                    Email = "userthree@test.com",
                    EmailConfirmed = true
                }
        };

            foreach (var applicationUser in applicationUsers)
            {
                var user = await userManager.FindByEmailAsync(applicationUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(applicationUser, "MistralTask2022!");
                }
            }




        }
    }
}
