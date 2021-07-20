using ContactAppCore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAppCore.Data
{
    public class ContactContext : DbContext
    {
        private Guid id;

        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
            id = Guid.NewGuid();
            Debug.WriteLine($"{id} context created.");
        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<ExternalLink> ExternalLinks { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public override void Dispose()
        {
            Debug.WriteLine($"{id} context disposed.");
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            Debug.WriteLine($"{id} context disposed async.");
            return base.DisposeAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Debug.WriteLine($"{id} context starting initial setup.");
            modelBuilder.Entity<Person>().HasData(new Person { Title = "jonker@illinois.edu", Id = -1, AreaId = null, OfficeId = null, IsActive = true, IsFullAdmin = true, LastUpdated = DateTime.Now });
            Debug.WriteLine($"{id} context finishing initial setup.");
        }
    }
}