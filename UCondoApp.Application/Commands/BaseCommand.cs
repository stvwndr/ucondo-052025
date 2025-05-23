using MediatR;
using UCondoApp.Domain.Consts;

namespace UCondoApp.Application.Commands;

public abstract class BaseCommand<TResponse> : IRequest<TResponse>
{
    public Guid TransactionId { get; private set; }
    public DateTime Timestamp { get; private set; }

    protected BaseCommand()
    {
        TransactionId = Guid.NewGuid();
        Timestamp = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, UCondoAppDomainConst.DefaultTimeZoneId);
    }
}
