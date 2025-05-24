using MediatR;
using Microsoft.AspNetCore.Mvc;
using UCondoApp.Application.Commands.Requests;
using UCondoApp.Application.Queries.Requests;
using UCondoApp.Domain.Services.Notifications.Interfaces;

namespace UCondoApp.Api.Controllers;

[ApiController]
[Route("api/accounts-chart")]
public class AccountsChartController : ControllerBase
{
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll(
            [FromServices] INotificationHandler notificationHandler,
            [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new GetAllAccountsChartRequestQuery());

        if (!notificationHandler.HasNotifications)
        {
            return Ok(response);
        }

        return BadRequest(notificationHandler.NotificationResponse);
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByPartialName(
        [FromQuery] string partialName,
        [FromServices] INotificationHandler notificationHandler,
        [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new GetAllAccountsChartsByPartialNameRequestQuery
        {
            PartialName = partialName
        });

        if (!notificationHandler.HasNotifications)
        {
            if (response == null) return NotFound();

            return Ok(response);
        }

        return BadRequest(notificationHandler.NotificationResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateAccountsChartRequestCommand command,
        [FromServices] INotificationHandler notificationHandler,
        [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(command);

        if (!notificationHandler.HasNotifications)
        {
            return Created("accountschart", response);
        }

        return BadRequest(notificationHandler.NotificationResponse);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(
    [FromRoute] Guid id,
    [FromServices] INotificationHandler notificationHandler,
    [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new DeleteAccountsChartRequestCommand
        {
            Id = id
        });

        if (!notificationHandler.HasNotifications)
        {
            return Ok();
        }

        return BadRequest(notificationHandler.NotificationResponse);
    }
}
