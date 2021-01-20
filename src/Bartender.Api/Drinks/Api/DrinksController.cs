using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using BarManager.Api.Drinks.Domain;

using Bartender.Api.Drinks.Api.Models;

using Microsoft.AspNetCore.Mvc;

namespace Bartender.Api.Drinks.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrinksController : ControllerBase
    {
        [HttpGet] // GET api/drinks
        [Produces(typeof(ICollection<Drink>))]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            await Task.CompletedTask;

            return Ok();
        }

        [HttpGet("{id}")] // GET api/drinks/{id}
        [Produces(typeof(Drink))]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
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
        [Produces(typeof(Drink))]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateDrinkDto createDrinkDto)
        {
            await Task.CompletedTask;

            return Ok();
        }

        [HttpPut("{id}")] // PUT api/drinks/{id}
        [Produces(typeof(Drink))]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
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
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> OrderDrink([FromBody] OrderDrinkDto orderDrinkDto)
        { 
            await Task.CompletedTask;

            return Ok();
        }

        [HttpDelete("{id}")] // DELETE api/drinks/{id}
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
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