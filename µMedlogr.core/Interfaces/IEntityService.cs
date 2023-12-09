using µMedlogr.core.Models;

namespace µMedlogr.core.Interfaces;
/// <summary>
/// Handles the store and retrieval of instances of any entity under given business rules
/// </summary>
/// <typeparam name="T">The type of entities handled</typeparam>
public interface IEntityService<T> where T : Entity {
    /// <summary>
    /// Gets all saved Instances of the Entity 
    /// </summary>
    /// <returns>An IEnumerable of entities</returns>
    public Task<IEnumerable<T>> GetAll();
    /// <summary>
    /// Saves many entities after performing checks that the entities conform to business rules <br>
    /// Will save in one chunk, if function fails nothing is saved
    /// </summary>
    /// <param name="values">Entities to save</param>
    /// <returns>True iff all entities could be saved, otherwise false</returns>
    public Task<bool> SaveAll(IEnumerable<T> values);
    /// <summary>
    /// Attemts to remove an entity instance
    /// </summary>
    /// <param name="entity">The entity to remove</param>
    /// <returns>True iff either entity was successfully removed or if entity was not saved, otherwise false</returns>
    public Task<bool> Delete(T entity);
    /// <summary>
    /// Updates the entity values after performing checks if the changes confirm to business rules
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <returns></returns>
    public Task<bool> Update(T entity);
    /// <summary>
    /// Finds an instance 
    /// </summary>
    /// <param name="key">The integer key value to search for</param>
    /// <returns>Iff found the instance, otherwise null</returns>
    public Task<T?> Find(int key);
}


