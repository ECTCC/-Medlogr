using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;

namespace µMedlogr.core.Services;
internal class SymptomService(µMedlogrContext medlogrContext) : IEntityService<SymptomType> {
    private readonly µMedlogrContext _medlogrContext = medlogrContext;
    public bool Delete(SymptomType entity) {
        _medlogrContext.SymptomTypes.Remove(entity);
        return _medlogrContext.SaveChanges() > 0;
    }

    public SymptomType? Find(int key) {
        return _medlogrContext.SymptomTypes.Find(key);
    }

    public IEnumerable<SymptomType> GetAll() {
        return _medlogrContext.SymptomTypes.AsEnumerable();
    }

    public bool SaveAll(IEnumerable<SymptomType> values) {

        throw new NotImplementedException();
    }

    public bool Update(SymptomType entity) {


        throw new NotImplementedException();
    }
}
