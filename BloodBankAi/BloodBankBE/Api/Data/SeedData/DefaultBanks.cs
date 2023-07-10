using Api.Const;
using Api.Data.Entities;
using Api.Data.Entities.Identity;
using Api.Dto.Bank;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Api.Data.SeedData
{
    public static class DefaultBanks
    {
        public static async Task SeedBanksAsync(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            if (await context.Banks.AnyAsync()) return;
            string AppDirectory = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName);
            string path = $"{AppDirectory}\\BloodBankBE\\Api\\Data\\JsonData\\BankSeed.json";
            var jsonData = File.ReadAllText(path);
            var addBankDto = JsonSerializer.Deserialize<List<SeedBanksDto>>(jsonData);

            var bankModerator1 = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == "bankmoderator");
            var bankModerator2 = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == "yara");

            if (addBankDto == null) return;

            var i = 0;
            foreach (var addBank in addBankDto)
            {
                var CheckBank=context.Banks.Where(b=>b.Name == addBank.Name).FirstOrDefault();
                if (CheckBank == null)
                {
                    var address = new Address
                    {
                        Country = addBank.Address.Country,
                        Area = addBank.Address.Area,
                        City = addBank.Address.State,
                        Government = addBank.Address.City,
                    };
                    //add bank
                    var bank = new Bank
                    {
                        Name = addBank.Name,
                        Email = addBank.Email,
                        Website = addBank.Website,
                        PhoneNumber = addBank.PhoneNumber,
                        LastUpdated = DateTime.Now,
                        Address= address,
                    };
                    await context.Banks.AddAsync(bank);
                    await context.SaveChangesAsync();

                    //add blood groups of bank
                    foreach (var b in addBank.BloodGroups)
                    {
                        var bloodGroup = new BloodGroup
                        {
                            Group = b.Group,
                            Value = b.Value,
                            BankId=bank.Id,
                        };
                        await context.BloodGroups.AddAsync(bloodGroup);
                        await context.SaveChangesAsync();
                    }

                    //add moderator of group
                    var userName = addBank.Moderators.First().User.UserName.ToLower();
                    var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                    if (user == null)
                        throw new Exception($"{userName} not found!");
                    var Moderators = new List<Moderator>
                    {
                        new() { BankId = bank.Id, UserId = user.Id, Type = Roles.BankAdmin.ToString() },
                        new() { BankId = bank.Id, UserId = i % 2 == 0 ? bankModerator1.Id : bankModerator2.Id,Type = Roles.BankModerator.ToString()}
                    };
                    await context.Moderators.AddRangeAsync(Moderators);
                    await context.SaveChangesAsync();


                    await userManager.AddToRoleAsync(user, Roles.BankAdmin.ToString());
                    i++;
                }
                await userManager.AddToRoleAsync(bankModerator1, Roles.BankModerator.ToString());
                await userManager.AddToRoleAsync(bankModerator2, Roles.BankModerator.ToString());

            }


        }
    }
}
