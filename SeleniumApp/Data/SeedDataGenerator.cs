using Microsoft.EntityFrameworkCore;
using SeleniumApp.Models;

namespace SeleniumApp.Data
{
    public class SeedDataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SeleniumAppDbContext(serviceProvider.GetRequiredService<DbContextOptions<SeleniumAppDbContext>>()))
            {
                // Check any etudiant exists 
                if (context.Etudiants.Any())
                {
                    return; // Data already exists no need to generate
                }

                context.Etudiants.AddRange(
                     new Etudiant
                     {
                         Nom = "John",
                         Prenom = "Doe",
                         Email = "john@example.com",
                         Sexe = "Homme",
                         DateNais = DateTime.Now.AddYears(-25)
                     },
                    new Etudiant
                    {
                        Nom = "Alice",
                        Prenom = "Johnson",
                        Email = "alice@example.com",
                        Sexe = "Femme",
                        DateNais = DateTime.Now.AddYears(-23)
                    },
                    new Etudiant
                    {
                        Nom = "Bob",
                        Prenom = "Smith",
                        Email = "bob@example.com",
                        Sexe = "Homme",
                        DateNais = DateTime.Now.AddYears(-22)
                    },
                    new Etudiant
                    {
                        Nom = "Eva",
                        Prenom = "Williams",
                        Email = "eva@example.com",
                        Sexe = "Femme",
                        DateNais = DateTime.Now.AddYears(-21)
                    },
                    new Etudiant
                    {
                        Nom = "Michael",
                        Prenom = "Brown",
                        Email = "michael@example.com",
                        Sexe = "Homme",
                        DateNais = DateTime.Now.AddYears(-20)
                    }
                );
                context.SaveChanges();

            }
        }
    }
}