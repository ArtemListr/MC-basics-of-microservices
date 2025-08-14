using Api.CQRS;
using Api.Model;

namespace Api.Books.GetBooks;

public record GetBooksQuery(): IQuery<GetBooksResult>;

public record GetBooksResult(IEnumerable<Book>Books);

public class GetBooksQueryHandler(IDocumentSession session)
 : IQueryHandler<GetBooksQuery, GetBooksResult>
{
    public async Task<GetBooksResult> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await session.Query<Book>().ToListAsync(cancellationToken);
        return new GetBooksResult(books);
    }
}