using AutoMapper;
using GAM33.Dtos;
using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Interfaces;
using Gma33.Core.Specfication.ProductSpec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAM33.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenaricRepo<Product> _repo;
        private readonly IMapper _mapper;

        public ProductController(IGenaricRepo<Product> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllAsync([FromQuery] ProductSpecPrams prams)
        {
            var spec = new ProductWithCategorySpec(prams);
            var SpecCount = new ProductWithCategoryAndBrandPaginationCountSpec(prams);
            var Products = await _repo.GetAllWithSpecAsync(spec); 

            var mapp = _mapper.Map<IReadOnlyList<ProductDto>>(Products);

            var count = await _repo.getCountAsync(SpecCount);



            return Ok(new PaginationDto<ProductDto>(prams.PageIndex,prams.pagesize,mapp,count));
        }
    }
}
