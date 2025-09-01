using Api.Model;



namespace Api.Data.Seed;

public class InitializeBookDatabase : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        //проверка наличия данных и добавление, если их нет
        using var session = store.LightweightSession();
        if (!await session.Query<Book>().AnyAsync())
        {
            session.Store<Book>(InitialData.Books);
            await session.SaveChangesAsync(cancellation);
        }

    }
}