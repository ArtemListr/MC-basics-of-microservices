namespace Api.CQRS;

// обработчик "переваривает" запрос/команду и выдают результат, если это соответствует условиям

public interface IQueryHandler<in TQuery, TResponse>
: IRequestHandler<TQuery, TResponse> //встраиваем в общую иерархию
where TQuery : IQuery<TResponse> //все запросы должны реализовывать TResponse
where TResponse : notnull //TResponse не может быть пустым
{ }