using Bogus;
using Microsoft.AspNetCore.Hosting;
using RnM.Api.DB;
using RnM.Api.Models;
using Microsoft.AspNetCore.Hosting;

namespace RnM.Api
{
    public static class DataSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {

                RnMContext context = scope.ServiceProvider.GetService<RnMContext>();

                context?.Database.EnsureCreated();


                if (app.ApplicationServices.GetService< Microsoft.AspNetCore.Hosting.IHostingEnvironment>().IsDevelopment())
                {
                    var locations = new Faker<Location>()
                        .RuleFor(l => l.Name, f => f.Address.State())
                        .RuleFor(l => l.Type, f => f.Address.StreetName())
                        .RuleFor(l => l.Dimension, f => f.Random.Int(0, 5).ToString())
                        .RuleFor(l => l.Created, f => f.Date.Past())
                        .RuleFor(l => l.Url, f => string.Empty)
                        .Generate(5);

                    context.Location.AddRange(locations);
                    context.SaveChanges();
                    var episodes = new Faker<Episode>()
                        .RuleFor(e => e.Name, f => f.Address.State())
                        .RuleFor(e => e.AirDate, f => f.Date.Past())
                        .RuleFor(e => e.EpisodeCode, f => f.Lorem.Word())
                        .RuleFor(e => e.Created, f => f.Date.Past())
                        .Generate(5);

                    context.Episodes.AddRange(episodes);
                    context.SaveChanges();

                    var characters = new Faker<Character>()
                        .RuleFor(c => c.Name, f => f.Person.FirstName)
                        .RuleFor(c => c.Status, f => f.PickRandom<CharacterStatus>())
                        .RuleFor(c => c.Species, f => f.Lorem.Word())
                        .RuleFor(c => c.Type, f => f.Lorem.Word())
                        .RuleFor(c => c.Gender, f => f.PickRandom<CharacterGender>())
                        .RuleFor(c => c.Origin, f => f.Address.StreetName())
                        .RuleFor(c => c.Location, f => context.Location.First())
                        .RuleFor(c => c.Image, f => f.Person.Avatar.ToLower())
                        .RuleFor(c => c.Url, string.Empty)
                        .RuleFor(c => c.Created, f => f.Date.Past())
                        .Generate(10);


                    foreach(var character in characters){
                        character.Episodes.Add(context.Episodes.First());
                    }

                    context.Characters.AddRange(characters);
                    context.SaveChanges();
                }
           
            }
             
        }
    }
}
