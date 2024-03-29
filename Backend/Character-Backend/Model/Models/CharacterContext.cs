﻿using Microsoft.EntityFrameworkCore;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class CharacterContext : DbContext, ICharacterContext
    {
        public CharacterContext(DbContextOptions<CharacterContext> options) : base(options)
        {
            //Database.Migrate();
        }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Career> Careers { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<SkillCategory> SkillCategory { get; set; }
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasMany(c => c.Skills).WithMany(s => s.Characters);
            modelBuilder.Entity<Event>().HasMany(e => e.Characters).WithMany(c => c.Events);
            modelBuilder.Entity<Character>().HasOne(c => c.Career).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Career>().HasOne(c => c.Skill);
        }
    }
}
