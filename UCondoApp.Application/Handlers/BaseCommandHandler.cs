using UCondoApp.Domain.Services.Notifications.Interfaces;
using UCondoApp.Infra.Data.Uow.Interfaces;

namespace UCondoApp.Application.Handlers;

public abstract class BaseCommandHandler
{
    protected readonly INotificationHandler NotificationHandler;

    protected BaseCommandHandler(INotificationHandler notificationHandler)
    {
        NotificationHandler = notificationHandler;
    }

    protected void AddError(string errorMessage)
    {
        NotificationHandler.AddNotification(errorMessage);
    }

    protected async Task<bool> Commit(IContextUnitOfWork uow, string message)
    {
        if (!await uow.SaveEntitiesAsync())
        {
            AddError(message);
            return false;
        }

        return true;
    }

    protected async Task<bool> Commit(IContextUnitOfWork uow)
    {
        return await Commit(uow, DefaultCommitErrorMessageLog).ConfigureAwait(continueOnCapturedContext: false);
    }


    public const string DefaultCommitErrorMessageLog = "Houve um erro ao tentar efetuar a persistência na base de dados.";
}
