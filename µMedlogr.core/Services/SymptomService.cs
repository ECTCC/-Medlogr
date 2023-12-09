﻿using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;

namespace µMedlogr.core.Services;
internal class SymptomService(µMedlogrContext medlogrContext) : IEntityService<SymptomType>, ISymptomService {
    private readonly µMedlogrContext _medlogrContext = medlogrContext;
    #region EntityService
    public async Task<bool> Delete(SymptomType entity) {
        _medlogrContext.SymptomTypes.Remove(entity);
        return await _medlogrContext.SaveChangesAsync() > 0;
    }

    public async Task<SymptomType?> Find(int key) {
        return await _medlogrContext.SymptomTypes.FindAsync(key);
    }

    public async Task<IEnumerable<SymptomType>> GetAll() {
        return [.. _medlogrContext.SymptomTypes.Select(x => new SymptomType() {Id = x.Id, Name=x.Name })];
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
    #endregion
}
