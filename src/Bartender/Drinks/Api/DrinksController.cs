using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Bartender.Drinks.Api.Models;
using Bartender.Drinks.Application.Commands;
using Bartender.Drinks.Application.Queries;
using Bartender.Drinks.Domain;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bartender.Drinks.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrinksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DrinksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet] // GET api/drinks
        [ProducesResponseType(typeof(ICollection<Drink>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyCollection<Drink> drinks = await _mediator.Send(new GetAllDrinks());
            return Ok(drinks);
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

            Drink drink = await _mediator.Send(new GetDrinkById(id));
            return Ok(drink);
        }

        [HttpPost] // POST api/drinks
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateDrinkDto createDrinkDto)
        {
            await _mediator.Send(new CreateDrink(createDrinkDto.Name));
            return Ok();
        }

        [HttpPut("{id}")] // PUT api/drinks/{id}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Rename([FromRoute] Guid id, [FromBody] RenameDrinkDto renameDrinkDto)
        {
            if (id == Guid.Empty)
            {
                return BadRequest($"Id may not be {Guid.Empty}");
            }

            await _mediator.Send(new RenameDrink(id, renameDrinkDto.NewName));
            return Ok();
        }

        [HttpPost("order")] // POST api/drinks/order
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> OrderDrinks([FromBody] OrderDrinksDto orderDrinksDto)
        {
            await _mediator.Send(new OrderDrinks(orderDrinksDto.DrinksOrder));

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

            await _mediator.Send(new DeleteDrink(id));

            return Ok();
        }
    }
}