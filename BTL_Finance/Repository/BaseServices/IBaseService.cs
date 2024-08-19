using BTL_Finance.Models.GenericModels;

namespace Repository.BaseService
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task<bool> Insert(T entity);
        Task<bool> Update(T entity);
        Task<bool> Remove(Guid id);
    }
}
