using MediatR;
using Microsoft.AspNetCore.Mvc;
using UCondoApp.Application.Commands.Requests;
using UCondoApp.Application.Commands.Responses;
using UCondoApp.Application.Queries.Requests;
using UCondoApp.Application.Queries.Responses;
using UCondoApp.Domain.Services.Notifications.Interfaces;
using UCondoApp.Domain.Services.Notifications.Messages;

namespace UCondoApp.Api.Controllers;

[ApiController]
[Route("api/accounts-chart")]
public class AccountsChartController : ControllerBase
{
    [HttpGet()]
    [ProducesResponseType(typeof(IList<GetAllAccountsChartResponseQuery>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotificationErrorMessage), StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(typeof(GetAllAccountsChartResponseQuery), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(NotificationErrorMessage), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByPartialName(
        [FromQuery] GetAllAccountsChartByPartialNameRequestQuery command,
        [FromServices] INotificationHandler notificationHandler,
        [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(command);

        if (!notificationHandler.HasNotifications)
        {
            if (response == null) return NotFound();

            return Ok(response);
        }

        return BadRequest(notificationHandler.NotificationResponse);
    }


    [HttpGet("parent")]
    [ProducesResponseType(typeof(GetAllAccountsChartResponseQuery), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotificationErrorMessage), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetParent(
        [FromServices] INotificationHandler notificationHandler,
        [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new GetAllParentAccountsChartRequestQuery());

        if (!notificationHandler.HasNotifications)
        {
            return Ok(response);
        }

        return BadRequest(notificationHandler.NotificationResponse);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateAccountChartResponseCommand), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(NotificationErrorMessage), StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(typeof(NotificationErrorMessage), StatusCodes.Status400BadRequest)]
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

    [HttpGet("next-code")]
    [ProducesResponseType(typeof(GetNextCodeResponseQuery), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotificationErrorMessage), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetNextCode(
        [FromQuery] string? parent,
        [FromServices] INotificationHandler notificationHandler,
        [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new GetNextCodeRequestQuery
        {
            ParentCode = parent
        });

        if (!notificationHandler.HasNotifications)
        {
            return Ok(response);
        }

        return BadRequest(notificationHandler.NotificationResponse);
    }
}
