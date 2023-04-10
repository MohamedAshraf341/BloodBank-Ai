using Api.Data.Entities;
using Api.Dto;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Api.Data.SeedData
{
    public static class DefaultGovernrates
    {
         

        public static async Task SeedGovernratesAsync( ApplicationDbContext context)
        {
            if (await context.Governorates.AnyAsync()) return;
            string AppDirectory = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName);
            string path = $"{AppDirectory}\\BackEnd\\Api\\Data\\JsonData\\GovernorateSeed.json";
            var data = File.ReadAllText(path);
            var Governorates = JsonSerializer.Deserialize<ListGovernorates>(data);
            if (Governorates == null) return;
            foreach(var Govern in Governorates.AddGovernoratesDto)
            {
                var CheckGovernrate = context.Governorates.Where(G => G.ArabicName == Govern.governorate_name_ar && G.EnglishName == Govern.governorate_name_en).FirstOrDefault();
                if(CheckGovernrate == null)
                {
                    var Gov = new Governorate
                    {
                        ArabicName = Govern.governorate_name_ar,
                        EnglishName = Govern.governorate_name_en
                    };
                    await context.Governorates.AddAsync(Gov);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
