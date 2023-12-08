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
        return _context.Drugs.AsEnumerable();
    }

    public bool SaveAll(IEnumerable<Drug> values) {
        bool canSave = false;
        if (canSave) {
            _context.AddRange(values);
            return _context.SaveChanges() > 0;
        }
        return false;
    }

    public bool Update(Drug entity) {
        _context.Update(entity);
        return _context.SaveChanges() > 0;
    }
}
