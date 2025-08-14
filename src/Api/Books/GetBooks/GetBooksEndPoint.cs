using Api.Books.GetBooks;
using Api.Model;
using Carter;

namespace Catalog.Api.Books.GetBooks;

public record GetBooksResponse(IEnumerable<Book>Books);
public class GetBooksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/books", async (ISender sender) =>
        {
            GetBooksResult result = await sender.Send(new GetBooksQuery());
            GetBooksResponse response = result.Adapt<GetBooksResponse>();
            return Results.Ok(response);

        });
    }
}