namespace Api.Books.UpdateBook;
using Api.CQRS;
using Api.Model;
using Api.Exceptions;


public record UpdateBookComand( //обновляемая книга
    Guid Id,
    string Title,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    List<string> Category
): ICommand<UpdateBookResult>;

public record UpdateBookResult(bool IsSucces);

public class UpdateBookCommandHandler(IDocumentSession session)
:ICommandHandler<UpdateBookComand, UpdateBookResult>
{
    public async Task<UpdateBookResult> Handle(

        UpdateBookComand command,
        CancellationToken cancellationToken)
    {
        var book = await session.LoadAsync<Book>(command.Id, cancellationToken);

        if (book is null)
        {
            throw new BookNotFoundException(command.Id);
        }

        command.Adapt(book);
        session.Update(book);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateBookResult(true);

    }

}