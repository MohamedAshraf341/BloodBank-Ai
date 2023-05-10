using Api.Data.Entities;
using Api.Dto;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Api.Data.SeedData
{
    public static class DefaultCities
    {
        public static async Task SeedCitiesAsync(ApplicationDbContext context)
        {
            if (await context.Cities.AnyAsync()) return;
            string AppDirectory = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName);
            string path = $"{AppDirectory}\\BloodBankBE\\Api\\Data\\JsonData\\CitySeed.json";
            var data = File.ReadAllText(path);
            var Cities = JsonSerializer.Deserialize<ListCities>(data);
            if (Cities == null) return;
            foreach (var city in Cities.AddCityDto)
            {
                var CheckGovernrate = context.Cities.Where(G => G.ArabicName == city.city_name_ar && G.EnglishName == city.city_name_en).FirstOrDefault();
                if (CheckGovernrate == null)
                {
                    var Cit = new City
                    {
                        GovernorateId = int.Parse(city.governorate_id),
                        ArabicName = city.city_name_ar,
                        EnglishName = city.city_name_en
                        
                    };
                    await context.Cities.AddAsync(Cit);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
