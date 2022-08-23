using Model.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CharactersAREN.Integration.Tests.Helpers
{
    public static class DatabaseSeeder
    {
        public static async Task InitializeDbForTests(CharacterContext context)
        {
            context.Users.AddRange(new User { Id = 1, AuthId = Guid.NewGuid().ToString() }) ;
            context.SkillCategory.AddRange(new SkillCategory { Id = 1, Name = "Ingenieur" });
            context.Skills.AddRange(new Skill { Id = 1, Name = "Blauwdrukken maken", Level = 1, SkillCategoryId = 1, Description = "Kek blauwdrukken maken" });
            context.Careers.AddRange(new Career { Id = 1, Name = "Ingenieur", SkillId = 1 });
            context.Characters.AddRange(new Character { Id = 1, CareerId = 1, IcName = "Jenna", UserId = 1, Skills = context.Skills.ToList() }, new Character { Id = 55, CareerId = 1, IcName = "Petra", UserId = 1 });

            await context.SaveChangesAsync();
        }

        public static async Task ResetDbForTests(CharacterContext context)
        {
            context.Characters.RemoveRange(context.Characters);
            context.Careers.RemoveRange(context.Careers);
            context.Skills.RemoveRange(context.Skills);
            context.SkillCategory.RemoveRange(context.SkillCategory);
            context.Users.RemoveRange(context.Users);

            await context.SaveChangesAsync();

            await InitializeDbForTests(context);
        }
    }
}
