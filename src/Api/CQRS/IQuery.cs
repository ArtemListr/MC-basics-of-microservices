namespace Api.CQRS;

// запросы отвечают за чтение (получение) данных, не приводят к изменению системы
// ожидаем получить ответ: коллекция чего-либо, иногда статус-код
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull //TResponse не может быть пустым
{ }