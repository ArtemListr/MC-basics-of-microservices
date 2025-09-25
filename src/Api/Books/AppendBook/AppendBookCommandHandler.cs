using Api.CQRS;
using Api.Model;

namespace Api.Books.AppendBook;

public record AppendBookComand( //новая книга
    string Title,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    List<string> Category
): ICommand<AppendBookResult>; //встраиваем команду в общую иерархию команд

public record AppendBookResult(Guid Id); //формат



public class AppendBookCommandHandler(IDocumentSession session)
    :ICommandHandler<AppendBookComand, AppendBookResult>
{
    public async Task<AppendBookResult> Handle(
        AppendBookComand command, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Title = command.Title,
            Name = command.Name,
            Description = command.Description,
            ImageUrl = command.ImageUrl,
            Price = command.Price,
            Category = command.Category
        };

        session.Store(book);
        await session.SaveChangesAsync(cancellationToken);

        return new AppendBookResult(book.Id);
    }

}