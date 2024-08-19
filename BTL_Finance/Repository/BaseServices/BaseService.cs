using BTL_Finance.Models.Context;
using BTL_Finance.Models.GenericModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Repository.BaseService
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext dContext;

        public BaseService(ApplicationDbContext dContext)
        {
            this.dContext = dContext;
        }
        public async Task<T?> GetById(Guid id)
        {
            var item = await dContext.Set<T>().FindAsync(id);

            if (item == null)
            {
                return default(T); // Or return a specific "Not Found" value if needed
            }

            return item;
        }

        public async Task<List<T>> GetAll()
        {
            return await dContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<bool> Insert(T entity)
        {
            var item = await dContext.Set<T>().FindAsync(entity.ID);

            if (item is not null) throw new DuplicateNameException();

            dContext.Set<T>().Add(entity);
            await dContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Remove(Guid id)
        {
            var entity = await dContext.Set<T>().FindAsync(id) ?? throw new KeyNotFoundException();

            dContext.Set<T>().Remove(entity);

            await dContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(T entity)
        {
            var item = await dContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.ID == entity.ID) ?? throw new KeyNotFoundException();

            dContext.Update(entity);

            await dContext.SaveChangesAsync();

            return true;
        }

    }
}
