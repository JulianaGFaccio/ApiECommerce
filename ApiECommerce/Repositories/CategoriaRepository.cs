using ApiECommerce.Context;
using ApiECormmerce.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiECormmerce.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext dbContext;

        public CategoriaRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await dbContext.Categorias.ToListAsync();
        }

    }
}
