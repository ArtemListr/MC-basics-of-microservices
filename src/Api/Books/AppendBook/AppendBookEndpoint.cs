using Api.Books.AppendBook;
using FluentValidation;

namespace AppendBook;

public record AppendBookRequest( //новая книга
    string Title,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    List<string> Category
);

//Реализация валидатора для новой книги
public class AppendBookCommandValidator : AbstractValidator<AppendBookComand>
{
    public AppendBookCommandValidator()
    {
        RuleFor(item => item.Title).NotEmpty().WithMessage("Title не может быть пустым");
        RuleFor(item => item.Price).GreaterThan(0).WithMessage("Price должен быть больше нуля");
        RuleFor(item => item.Description).NotEmpty().WithMessage("Description не может быть пустым");
        RuleFor(item => item.Name).NotEmpty().WithMessage("Name не может быть пустым");
    }
}

public record AppendBookResponse(Guid Id); //формат


public class AppendBookEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/books", async (
            AppendBookRequest request,
            ISender sender
         ) =>
         {
             var command = request.Adapt<AppendBookComand>();
             var result = await sender.Send(command);
             var response = result.Adapt<AppendBookResponse>();
             return Results.Ok(response);
         });
    }
}