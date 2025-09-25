using Api.Books.AppendBook;

namespace AppendBook;

public record AppendBookRequest( //новая книга
    string Title,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    List<string> Category
);


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