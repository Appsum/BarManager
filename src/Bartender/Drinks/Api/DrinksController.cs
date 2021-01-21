using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Bartender.Api.Drinks.Api.Models;
using Bartender.Api.Drinks.Domain;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bartender.Api.Drinks.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrinksController : ControllerBase
    {
        [HttpGet] // GET api/drinks
        [ProducesResponseType(typeof(ICollection<Drink>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            await Task.CompletedTask;

            return Ok();
        }

        [HttpGet("{id}")] // GET api/drinks/{id}
        [ProducesResponseType(typeof(Drink), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest($"Id may not be {Guid.Empty}");
            }

            await Task.CompletedTask;

            return Ok();
        }

        [HttpPost] // POST api/drinks
        [ProducesResponseType(typeof(Drink), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateDrinkDto createDrinkDto)
        {
            await Task.CompletedTask;

            return Ok();
        }

        [HttpPut("{id}")] // PUT api/drinks/{id}
        [ProducesResponseType(typeof(Drink), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Rename([FromRoute] Guid id, [FromBody] RenameDrinkDto renameDrinkDto)
        {
            if (id == Guid.Empty)
            {
                return BadRequest($"Id may not be {Guid.Empty}");
            }

            await Task.CompletedTask;

            return Ok();
        }

        [HttpPost("order")] // POST api/drinks/order
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> OrderDrinks([FromBody] OrderDrinksDto orderDrinksDto)
        {
            await Task.CompletedTask;

            return Ok();
        }

        [HttpDelete("{id}")] // DELETE api/drinks/{id}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest($"Id may not be {Guid.Empty}");
            }

            await Task.CompletedTask;

            return Ok();
        }
    }
}