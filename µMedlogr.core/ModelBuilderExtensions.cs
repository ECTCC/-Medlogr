using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using µMedlogr.core.Models;

namespace µMedlogr.core;
public static class ModelBuilderExtensions {
    public static void Seed(this ModelBuilder builder) {
        #region Drugs
        builder.Entity<Drug>().HasData(
            new Drug() {
                Id = 1,
                Name = "Ipren",
                Form = Enums.Form.Tablet,
                ActiveSubstance = "Kokain",
                Effects = [Enums.Effect.Analgesic]
            },
            new Drug() {
                Id = 2,
                Name = "Treo",
                Form = Enums.Form.Tablet,
                ActiveSubstance = "MDMA",
                Effects = [Enums.Effect.Analgesic, Enums.Effect.Anti_Inflammatory, Enums.Effect.Antipyretic]
            },
            new Drug() {
                Id = 3,
                Name = "Viagra",
                Form = Enums.Form.Tablet,
                ActiveSubstance = "Secret",
                Effects = [Enums.Effect.Other]
            },
            new Drug() {
                Id = 4,
                Name = "Amoxicillin",
                Form = Enums.Form.Liquid,
                ActiveSubstance = "Kokain",
                Effects = [Enums.Effect.Antibiotic]
            },
            new Drug() {
                Id = 5,
                Name = "Thomas Energy Supplement",
                Form = Enums.Form.Injection,
                ActiveSubstance = "alpha-methylphenethylamine",
                Effects = [Enums.Effect.Analgesic, Enums.Effect.Other]
            }
            );
        #endregion
        #region SymptomTypes
        builder.Entity<SymptomType>().HasData(
            new SymptomType() { Id = 1, Name = "Snuva" },
            new SymptomType() { Id = 2, Name = "Hosta" },
            new SymptomType() { Id = 3, Name = "Feber" },
            new SymptomType() { Id = 4, Name = "Huvudvärk" },
            new SymptomType() { Id = 5, Name = "Låg Energi" },
            new SymptomType() { Id = 6, Name = "Nedsatt prestationsförmåga" }
            );
        #endregion
        #region Persons
        builder.Entity<Person>().HasData(
            new List<Person>() {
                new Person() {
                    Id=1,
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                    NickName = "Totte",
                    WeightInKg = 55
                }
            }
            )
        ;
        #endregion
        #region AppUsers
        var password = "Tester";
        var passwordHasher = new PasswordHasher<IdentityUser>();

        var firstUser = new AppUser {
            UserName = "Test",
            Email = "Test@Test.com",
            EmailConfirmed = true,
        };
        firstUser.NormalizedUserName = firstUser.UserName.ToUpper();
        firstUser.NormalizedEmail = firstUser.Email.ToUpper();
        firstUser.PasswordHash = passwordHasher.HashPassword(firstUser, password);

        builder.Entity<AppUser>().HasData(
            new List<AppUser>() {
                firstUser
            }
            );

        #endregion
    }
}
