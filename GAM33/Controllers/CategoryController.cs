using AutoMapper;
using GAM33.Dtos;
using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAM33.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenaricRepo<Category> _repo;
        private readonly IMapper _mapper;

        public CategoryController(IGenaricRepo<Category> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAll()
        {
            var Category = await _repo.GetAllAsync();
            var map = _mapper.Map<IReadOnlyList<CategoryDto>>(Category);
            return Ok(map);
        }
    }
}
