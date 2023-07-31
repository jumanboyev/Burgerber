using Burgerber.DataAccess.Utils;
using Burgerber.Service.Dtos.Products;
using Burgerber.Service.Interfeces.Products;
using Burgerber.Service.Validators.Dtos.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Burgerber.WepApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        private readonly int MaxPageSize = 30;


        public ProductController(IProductService productService)
        {
            this._productService=productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] ProductCreateDto dto)
        {
                var createValidator = new ProductCreateValidator();
                var result = createValidator.Validate(dto);
                if (result.IsValid) return Ok(await _productService.CreateAsync(dto));
                else return BadRequest(result.Errors);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long productId)
        {
            return Ok(await _productService.DeleteAsync(productId));
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
            =>Ok(await _productService.CountAsync());

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _productService.GetAllAsync(new PaginationParams(page, MaxPageSize)));

        [HttpGet("{productId}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _productService.GetByIdAsync(id));


        [HttpPut("{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdatedAsync(long id, [FromForm] ProductUpdateDto dto)
        {
            return Ok(await _productService.UpdateAsync(id, dto));
        }
    }
}
