using System.Windows.Input;


namespace Api.CQRS;

// команда без возвращения результата (интерфейс)
public interface ICommand : ICommand<Unit>
{

}

// команда с возвращением результата (интерфейс)
public interface ICommand<out TResponse> : IRequest<TResponse>
{

}