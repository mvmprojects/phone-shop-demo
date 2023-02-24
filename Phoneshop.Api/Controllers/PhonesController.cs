using Microsoft.AspNetCore.Mvc;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Phoneshop.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private IPhoneService _phoneService;
        const int MAX_PAGE_SIZE = 20;

        public PhonesController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        [HttpGet]
        public async Task<IEnumerable<Phone>> GetPhones(string? query = null)
        {
            return await _phoneService.SearchAsync(query);

            //List<Phone> awaitedList = await _phoneService.SearchAsync(query);
            //// mapping not currently needed...
            //List<Phone> resultList = new() { };

            //foreach (var item in awaitedList)
            //{
            //    resultList.Add(item);
            //}

            //return Ok(awaitedList);
        }

        // test me
        [HttpGet]
        [Route("filter")]
        public async Task<IEnumerable<Phone>> SearchOrFilterPhones(
            string? filter,
            string? searchQuery,
            int pageNumber = 1,
            int pageSize = 10)
        {
            if (pageSize > MAX_PAGE_SIZE) pageSize = MAX_PAGE_SIZE;

            var (entities, paginationMetaData) = await _phoneService.SearchOrFilterAsync(
                filter,
                searchQuery,
                pageNumber,
                pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetaData));

            return entities;

            //return Ok(entities);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();

            Phone awaitedPhone = await _phoneService.GetPhoneAsync(id);

            if (awaitedPhone == null) return NotFound();

            return Ok(awaitedPhone);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Phone phone)
        {
            // temp
            if (string.IsNullOrWhiteSpace(phone.Type) ||
                string.IsNullOrWhiteSpace(phone.Brand?.Name))
            {
                return BadRequest();
            }

            Phone awaitedPhone = await _phoneService.CreatePhoneAsync(phone);

            if (awaitedPhone == null || awaitedPhone.Id == 0) return StatusCode(500);

            return CreatedAtAction(nameof(Create), awaitedPhone);
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }

    //public record PhoneRecord(string Type, string Brand, string Description, int Stock, decimal Price);
}
