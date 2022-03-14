using APIProject.Model;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly EmployeeDBContext _context;
        public GenericRepository(EmployeeDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> AllAsync()
        => await _context.Set<T>().ToListAsync();

        public async Task<T> CreateAsync(T entity)
        {  
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<T> GetByIdAsync(string id)
        => await _context.Set<T>().FirstOrDefaultAsync();

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
