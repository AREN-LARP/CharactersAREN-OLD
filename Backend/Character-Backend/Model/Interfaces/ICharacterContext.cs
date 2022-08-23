using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface ICharacterContext
    {
        DbSet<Character> Characters { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<Career> Careers { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        EntityEntry Entry(object entity);
    }
}
