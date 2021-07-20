using ContactAppCore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAppCore.Data
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDbContextFactory<ContactContext> factory;

        public ContactRepository(IDbContextFactory<ContactContext> factory)
        {
            this.factory = factory;
        }

        public int Create<T>(T item) where T : BaseDataItem
        {
            using var context = factory.CreateDbContext();
            item.LastUpdated = DateTime.Now;
            context.Add(item);
            return context.SaveChanges();
        }

        public async Task<int> CreateAsync<T>(T item) where T : BaseDataItem
        {
            using var context = factory.CreateDbContext();
            item.LastUpdated = DateTime.Now;
            context.Add(item);
            return await context.SaveChangesAsync();
        }

        public int Delete<T>(T item)
        {
            using var context = factory.CreateDbContext();
            context.Remove(item);
            return context.SaveChanges();
        }

        public async Task<int> DeleteAsync<T>(T item)
        {
            using var context = factory.CreateDbContext();
            context.Remove(item);
            return await context.SaveChangesAsync();
        }

        public int MakeActive<T>(T item, bool active) where T : BaseDataItem
        {
            using var context = factory.CreateDbContext();
            item.LastUpdated = DateTime.Now;
            item.IsActive = active;
            context.Update(item);
            return context.SaveChanges();
        }

        public async Task<int> MakeActiveAsync<T>(T item, bool active) where T : BaseDataItem
        {
            using var context = factory.CreateDbContext();
            item.LastUpdated = DateTime.Now;
            item.IsActive = active;
            context.Update(item);
            return await context.SaveChangesAsync();
        }

        public T Read<T>(Func<ContactContext, T> work)
        {
            using var context = factory.CreateDbContext();
            return work(context);
        }

        public async Task<T> ReadAsync<T>(Func<ContactContext, T> work)
        {
            using var context = factory.CreateDbContext();
            return await Task.Run(() => work(context));
        }

        public int Update<T>(T item) where T : BaseDataItem
        {
            using var context = factory.CreateDbContext();
            item.LastUpdated = DateTime.Now;
            context.Update(item);
            return context.SaveChanges();
        }

        public async Task<int> UpdateAsync<T>(T item) where T : BaseDataItem
        {
            using var context = factory.CreateDbContext();
            item.LastUpdated = DateTime.Now;
            context.Update(item);
            return await context.SaveChangesAsync();
        }
    }
}