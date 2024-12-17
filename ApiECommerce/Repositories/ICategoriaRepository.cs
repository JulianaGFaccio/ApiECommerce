using ApiECormmerce.Entities;

namespace ApiECormmerce.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetCategorias();
    }
}
