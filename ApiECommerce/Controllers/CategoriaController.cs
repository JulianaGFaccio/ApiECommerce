using ApiECormmerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiECormmerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            this.categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categoria = await categoriaRepository.GetCategorias();
            return Ok(categoria);
        }
            
    }
}
