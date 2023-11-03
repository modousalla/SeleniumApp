using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SeleniumApp.Controllers;
using SeleniumApp.Models;
using Xunit;
using System.Threading.Tasks;

namespace SeleniumAppTest
{
    public class EtudiantTest
    {

        [Fact]
        public async Task CreateEtudiantTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SeleniumAppDbContext>()
                .UseInMemoryDatabase(databaseName: "SeleniumAppDB")
                .Options;

            using var context = new SeleniumAppDbContext(options);
            var controller = new EtudiantsController(context);

            // Initialisez le modèle de l'étudiant avec des valeurs valides
            var etudiant = new Etudiant
            {
                Nom = "John",
                Prenom = "Doe",
                Email = "john@example.com",
                Sexe = "Homme",
                DateNais = DateTime.Now.AddYears(-25)
            };

            // Act
            var result = await controller.Create(etudiant) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            // Vérifiez si l'étudiant a été ajouté à la base de données
            var etudiantInDatabase = await context.Etudiants.FirstOrDefaultAsync();
            Assert.NotNull(etudiantInDatabase);
            Assert.Equal("John", etudiantInDatabase.Nom);
            Assert.Equal("Doe", etudiantInDatabase.Prenom);
        }

        [Fact]
        public async Task DeleteEtudiantTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SeleniumAppDbContext>()
                .UseInMemoryDatabase(databaseName: "SeleniumAppDB")
                .Options;

            using var context = new SeleniumAppDbContext(options);
            var controller = new EtudiantsController(context);

            // Initialisez le modèle de l'étudiant avec des valeurs valides
            var etudiant = new Etudiant
            {
                Nom = "John",
                Prenom = "Doe",
                Email = "john@example.com",
                Sexe = "Homme",
                DateNais = DateTime.Now.AddYears(-25)
            };

            // Ajoutez l'étudiant à la base de données
            await controller.Create(etudiant);

            // Récupérez l'ID de l'étudiant ajouté
            int etudiantId = etudiant.Id;

            // Act
            var result = await controller.Delete(etudiantId) as ViewResult;

            // Assert
            Assert.NotNull(result);

            // Vérifiez si l'étudiant a été correctement récupéré pour la suppression
            var etudiantToDelete = result.Model as Etudiant;
            Assert.NotNull(etudiantToDelete);

            // Supprimez l'étudiant
            var deleteResult = await controller.DeleteConfirmed(etudiantId) as RedirectToActionResult;
            Assert.NotNull(deleteResult);
            Assert.Equal("Index", deleteResult.ActionName);

            // Vérifiez que l'étudiant a été supprimé de la base de données
            var etudiantInDatabase = await context.Etudiants.FirstOrDefaultAsync(e => e.Id == etudiantId);
            Assert.Null(etudiantInDatabase);
        }


    }


}

