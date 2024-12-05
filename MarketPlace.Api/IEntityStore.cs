using MarketPlace.Framework;

namespace MarketPlace.Api;

public interface IEntityStore
{
    /// <summary>
    /// Loads an entity by id
    /// </summary>
    Task<T> Load<T>(string entityId) where T : Entity;
    /// <summary>
    /// Persists an entity
    /// </summary>
    Task Save<T>(T entity) where T : Entity;
    /// <summary>
    /// Check if entity with a given id already exists
    /// <typeparam name="T">Entity type</typeparam>
    Task<bool> Exists<T>(string entityId);
}
