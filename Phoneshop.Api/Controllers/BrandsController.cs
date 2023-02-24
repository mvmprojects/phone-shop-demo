using Microsoft.AspNetCore.Mvc;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System.Threading.Tasks;

namespace Phoneshop.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private IBrandService _brandservice;

        public BrandsController(IBrandService brandService)
        {
            _brandservice = brandService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();

            Brand awaitedBrand = await _brandservice.GetBrandAsync(id);

            if (awaitedBrand == null) return NotFound();

            BrandRecord record = MapBrandRecord(awaitedBrand);

            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandRecord record)
        {
            Brand awaitedBrand = await _brandservice
                .CreateBrandAsync(record.Name);

            if (awaitedBrand == null) return StatusCode(500);

            BrandRecord result = MapBrandRecord(awaitedBrand);

            return CreatedAtAction(nameof(Create), result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _brandservice.DeleteBrandAsync(id);
            return Ok();
        }

        private BrandRecord MapBrandRecord(Brand brand)
        {
            return new BrandRecord(
                brand.Name
            );
        }
    }

    public record BrandRecord(string Name);
}
