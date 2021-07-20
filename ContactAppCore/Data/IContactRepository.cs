using ContactAppCore.Data.Models;
using System;
using System.Threading.Tasks;

namespace ContactAppCore.Data
{
    public interface IContactRepository
    {
        int Create<T>(T item) where T : BaseDataItem;

        Task<int> CreateAsync<T>(T item) where T : BaseDataItem;

        int Delete<T>(T item);

        Task<int> DeleteAsync<T>(T item);

        int MakeActive<T>(T item, bool active) where T : BaseDataItem;

        Task<int> MakeActiveAsync<T>(T item, bool active) where T : BaseDataItem;

        T Read<T>(Func<ContactContext, T> work);

        Task<T> ReadAsync<T>(Func<ContactContext, T> work);

        int Update<T>(T item) where T : BaseDataItem;

        Task<int> UpdateAsync<T>(T item) where T : BaseDataItem;
    }
}