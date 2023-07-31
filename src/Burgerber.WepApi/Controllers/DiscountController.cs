using Burgerber.DataAccess.Utils;
using Burgerber.Service.Dtos.Discounts;
using Burgerber.Service.Interfeces.Discounts;
using Burgerber.Service.Validators.Dtos.Discounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Burgerber.WepApi.Controllers;

[Route("api/discount")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _service;
    private readonly int maxPageSize = 30;

    public DiscountController(IDiscountService service)
    {
        this._service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));


    [HttpGet("{discountId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(long discountId)
    => Ok(await _service.GetByIdAsync(discountId));


    [HttpGet("count")]
    [AllowAnonymous]
    public async Task<IActionResult> CountAsync()
       => Ok(await _service.CountAsync());


    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] DiscountCreateDto dto)
    {
        var createValidator = new DiscountCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreatAsync(dto));
        else return BadRequest(result.Errors);
    }


    [HttpPut("{discountId}")]
    public async Task<IActionResult> UpdateAsync(long discountId, [FromForm] DiscountUpdateDto dto)
    {
        var updateValidator = new DiscountUpdateValidator();
        var result = updateValidator.Validate(dto);
        if (result.IsValid) return Ok(await _service.UpdateAsync(discountId, dto));
        else return BadRequest(result.Errors);
    }


    [HttpDelete("{discountId}")]
    public async Task<IActionResult> DeleteAsync(long discountId)
        => Ok(await _service.DeleteAsync(discountId));
}
