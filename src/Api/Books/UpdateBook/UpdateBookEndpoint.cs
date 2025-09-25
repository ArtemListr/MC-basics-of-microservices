namespace Api.Books.UpdateBook;


public record UpdateBookRequest(
    Guid Id,
    string Title,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    List<string> Category
);

public record UpdateBookResponse(bool IsSucces);

public class UpdateBookEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/books", async (
            UpdateBookRequest request,
            ISender sender
         ) =>
         {
             var command = request.Adapt<UpdateBookComand>();
             var result = await sender.Send(command);
             var response = result.Adapt<UpdateBookResponse>();
             return Results.Ok(response);
         });
    }

}