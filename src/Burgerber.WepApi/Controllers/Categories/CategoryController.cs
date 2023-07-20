using Burgerber.DataAccess.Utils;
using Burgerber.Service.Dtos.Categories;
using Burgerber.Service.Interfeces.Categories;
using Burgerber.Service.Validators.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Burgerber.WepApi.Controllers.Categories
{
    [Route("api/category")]
    [ApiController]
    public  class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly int MaxPageSize = 30;


        public CategoryController(ICategoryService service)
        {
            this._service=service;

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CategoryCreateDto dto)
        {
            var createValidator=new CategoryCreateValidator();
            var result =createValidator.Validate(dto);
            if(result.IsValid) return Ok(await _service.CreateAsync(dto));
            else return BadRequest(result.Errors);

        }


        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateAsync(long categoryId, [FromForm] CategoryUpdateDto dto)
            => Ok(await _service.UpdateAsync(categoryId, dto));


        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long categorieId)
            =>Ok(await _service.DeleteAsync(categorieId));


        [HttpGet("count")]
        public async Task<IActionResult> Countasync()
            =>Ok(await _service.CountAsync());


        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, MaxPageSize)));


        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetByIdAsync(long categoryId)
            =>Ok(await _service.GetByIdAsync(categoryId));

    }
}
