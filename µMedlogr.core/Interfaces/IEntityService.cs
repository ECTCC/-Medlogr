using µMedlogr.core.Models;

namespace µMedlogr.core.Interfaces;
public interface IEntityService<T> where T : Entity {
    public IEnumerable<T> GetAll();
    public bool SaveAll(IEnumerable<T> values);
    public bool Delete(T entity);
    public bool Update(T entity);
    public T? Find(int key);
}


