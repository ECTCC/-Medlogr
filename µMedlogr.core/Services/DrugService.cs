using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;

namespace µMedlogr.core.Services;
public class DrugService(µMedlogrContext context) : IEntityService<Drug> {
    private readonly µMedlogrContext _context = context;
    public bool Delete(Drug entity) {
        _context.Drugs.Remove(entity);
        return _context.SaveChanges() > 0;
    }

    public Drug? Find(int key) {
        return _context.Drugs.Find(key);
    }

    public IEnumerable<Drug> GetAll() {
        throw new NotImplementedException();
    }

    public bool SaveAll(IEnumerable<Drug> values) {
        throw new NotImplementedException();
    }

    public bool Update(Drug entity) {
        throw new NotImplementedException();
    }
}
