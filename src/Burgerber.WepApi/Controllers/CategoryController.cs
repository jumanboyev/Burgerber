using Burgerber.DataAccess.Utils;
using Burgerber.Service.Dtos.Categories;
using Burgerber.Service.Dtos.Products;
using Burgerber.Service.Interfeces.Categories;
using Burgerber.Service.Validators.Dtos.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Burgerber.WepApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly int MaxPageSize = 30;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateAsync([FromForm] CategoryCreateDto dto)
        {
            var createValidator = new CategoryCreateValidator();
            var result = createValidator.Validate(dto);
            if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            else return BadRequest(result.Errors);

        }

        [HttpPut("{categoryId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long categoryId, [FromForm] CategoryUpdateDto dto)
            => Ok(await _service.UpdateAsync(categoryId, dto));


        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long categorieId)
            => Ok(await _service.DeleteAsync(categorieId));


        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, MaxPageSize)));


        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long categoryId)
            => Ok(await _service.GetByIdAsync(categoryId));

        [HttpGet("count")]
        [AllowAnonymous]
        public async Task<IActionResult> CountAsync()
            => Ok(await _service.CountAsync());

        
    }
}
