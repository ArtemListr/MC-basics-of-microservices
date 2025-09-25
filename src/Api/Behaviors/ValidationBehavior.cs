using Api.CQRS;
using FluentValidation;

namespace Api.Behaviors;


//отработка пустых полей для книги
public class ValidationBehavior<TRequest, TResponse> //что приходит и уходит
(IEnumerable<IValidator<TRequest>> validators) //принимает коллекцию валидаторов
: IPipelineBehavior<TRequest, TResponse>
where TRequest : ICommand<TResponse> //запрос обяхательно должен быть в иерархии TCommand
{
    public async Task<TResponse> Handle( //метод handle выполняется каждый раз, когда комманда проходит через пайплайн Mediatr
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        //контекст валидации, связанный с текущим запросом
        var context = new ValidationContext<TRequest>(request);

        //для каждого валидатора выполняется асинхронная валидация и сбор результатов в массив
        var validationResult = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        //поиск ошибок в полученных результатах
        //если хотя бы один с ошибкой - программа добавляет его в набор ошибок
        var failures = validationResult
        .Where(r => r.Errors.Any())
        .SelectMany(r => r.Errors)
        .ToList();

        //исключение на ошибку валидации, которую (-ые) программа нашла выше
        if (failures.Any())
        {
            throw new Exception("Ошибка валидации");
        }

        //если ошибки нет, продолжаем проверку
        return await next();
    }
}