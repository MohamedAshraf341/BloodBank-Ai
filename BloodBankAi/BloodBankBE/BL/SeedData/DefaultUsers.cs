﻿using BL.Dtos;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;

namespace DAL.Data.SeedData
{
    public static class DefaultUsers
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            if (await userManager.Users.AnyAsync()) return;
            string AppDirectory = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName);
            string path = $"{AppDirectory}\\bloodbankAPI\\BL\\SeedData\\JsonData\\UserSeed.json";
            var jsonData = File.ReadAllText(path);
            var addUserDto = JsonSerializer.Deserialize<List<AddUsersDto>> (jsonData);

            foreach (var addUser in addUserDto)
            {
                
                var CheckUser = await userManager.FindByEmailAsync(addUser.Email);
                if (CheckUser == null)
                {
                    var user = new ApplicationUser
                    {
                        Name = addUser.Name,
                        UserName = addUser.UserName.ToLower(),
                        Email = addUser.Email,
                        DateOfBirth = DateTime.ParseExact(addUser.DateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Available = addUser.Available,
                        BloodGroup = addUser.BloodGroup,
                        Gender = addUser.Gender,
                        PhoneNumber = addUser.PhoneNumber,
                        LastActive = DateTime.ParseExact(addUser.LastActive, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    };


                    if (user.UserName == "admin")
                    {
                        var passwordAdmin = "Admin@2023";
                        await userManager.CreateAsync(user, passwordAdmin);
                        await userManager.AddToRolesAsync(user, new[] { Roles.Member.ToString(), Roles.Admin.ToString() });
                    }
                    else if (user.UserName == "salma")
                    {
                        var passwordModerator = "Moderator@2023";
                        await userManager.CreateAsync(user, passwordModerator);
                        await userManager.AddToRolesAsync(user, new[] { Roles.Member.ToString(), Roles.Moderator.ToString() });
                    }
                    else
                    {
                        var passwordMember = "Member@2023";
                        await userManager.CreateAsync(user, passwordMember);
                        await userManager.AddToRoleAsync(user,Roles.Member.ToString() );
                    }

                    var address = new Address
                    {
                        UserId= user.Id,
                        Country=addUser.Address.Country,
                        Area=addUser.Address.Area,
                        City=addUser.Address.State,
                        Government=addUser.Address.City
                    };
                    await context.Addresses.AddAsync(address);
                    await context.SaveChangesAsync();
                }
            }


        }
    }
}