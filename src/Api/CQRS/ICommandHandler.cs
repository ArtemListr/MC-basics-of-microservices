namespace Api.CQRS;

// обработчик "переваривает" запрос/команду и выдают результат, если это соответствует условиям

// обработчик команд без возвращения результата (интерфейс)
public interface ICommandHandler<in TCommand>
    : ICommandHandler<TCommand, Unit> //Unit используется, если ничего не нужно возвращать
    where TCommand : ICommand<Unit> //TCommand обязательно реализует ICommand<Unit>
{ }

// обработчик команд с возвращением результата (интерфейс)
public interface ICommandHandler <in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand: ICommand<TResponse> //TCommand обязательно реализует ICommand<TResponse>
    where TResponse: notnull //ответ обязательно что-нибудь содержит
{}