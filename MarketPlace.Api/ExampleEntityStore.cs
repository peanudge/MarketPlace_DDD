
using MarketPlace.Framework;

namespace MarketPlace.Api;

public class ExampleEntityStore : IEntityStore
{
    public Task<bool> Exists<T>(string entityId)
    {
        throw new NotImplementedException();
    }

    public Task<T> Load<T>(string entityId) where T : Entity
    {
        throw new NotImplementedException();
    }

    public Task Save<T>(T entity) where T : Entity
    {
        throw new NotImplementedException();
    }
}
