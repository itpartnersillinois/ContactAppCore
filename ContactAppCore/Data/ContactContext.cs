using ContactAppCore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public DbSet<Log> Logs { get; set; }

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
            var people = new List<Person>
            {
                new Person { Title = "jonker@illinois.edu", Id = -1, AreaId = null, OfficeId = null, IsActive = true, IsFullAdmin = true, LastUpdated = DateTime.Now },
                new Person { Title = "rbwatson@illinois.edu", Id = -2, AreaId = null, OfficeId = null, IsActive = true, IsFullAdmin = true, LastUpdated = DateTime.Now }
            };
            modelBuilder.Entity<Person>().HasData(people);
            Debug.WriteLine($"{id} context finishing initial setup.");
        }
    }
}