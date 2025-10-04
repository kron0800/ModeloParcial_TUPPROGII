using TwitterCloneApi.Models;

namespace TwitterCloneApi.Repositories
{
    public interface ITweetRepository
    {
        IQueryable<Tweet> GetQueryable();
        Task<IEnumerable<Tweet>> GetByFiltersAsync(IQueryable<Tweet> query);
        Task<Tweet?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Tweet entity);
        //Task<bool> DeleteAsync(Tweet entity); 
        // No hace falta usar un metodo delete si ya tenemos el update para hacer cambios en la bd.
        // Nomas cambiamos estado en el service y lo pasamos por el update
        Task<Tweet> CreateAsync(Tweet entity);
    }
}
