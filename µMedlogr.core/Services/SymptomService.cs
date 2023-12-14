using Microsoft.EntityFrameworkCore;
using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;

namespace µMedlogr.core.Services;
internal class SymptomService(µMedlogrContext medlogrContext) : ISymptomService {
    #region EntityService
    public async Task<bool> Delete(SymptomType entity) {
        medlogrContext.SymptomTypes.Remove(entity);
        return await medlogrContext.SaveChangesAsync() > 0;
    }

    public async Task<SymptomType?> Find(int key) {
        return await medlogrContext.SymptomTypes.FindAsync(key);
    }

    public async Task<IEnumerable<SymptomType>> GetAll() {
        return await medlogrContext.SymptomTypes.ToListAsync();
    }


    public async Task<bool> SaveAll(IEnumerable<SymptomType> values) {

        throw new NotImplementedException();
    }

    public async Task<bool> Update(SymptomType entity) {


        throw new NotImplementedException();
    }
    #endregion
    #region SymptomService
    public async Task<IEnumerable<SymptomType>> GetAllSymptoms() {
        return await GetAll();
    }

    public async Task<SymptomType?> FindSymptom(int id) {
        return medlogrContext.SymptomTypes.Find(id);
    }
    #endregion
}
