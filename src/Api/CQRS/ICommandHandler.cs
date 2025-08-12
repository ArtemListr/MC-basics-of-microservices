using System.Windows.Input;


namespace Api.CQRS;

// обработчик команд без возвращения результата (интерфейс)
public interface ICommandHandler <in TCommand>
    : ICommandHandler<TCommand, Unit> //Unit используется, если ничего не нужно возвращать
    where TCommand: ICommand<Unit> //TCommand обязательно реализует ...
{}

// обработчик команд с возвращением результата (интерфейс)
public interface ICommandHandler <in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand: ICommand<TResponse> //TCommand обязательно реализует ...
    where TResponse: notnull //ответ обязательно что-нибудь содержит
{}