using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core.Services;
public class DrugService(µMedlogrContext context) : IEntityService<Drug>, IDrugService{
    private readonly µMedlogrContext _context = context;
    #region EntityService
    public async Task<bool> Delete(Drug entity) {
        _context.Drugs.Remove(entity);
        return _context.SaveChangesAsync().Result > 0;
    }

    public async Task<Drug?> Find(int key) {
        return await _context.Drugs.FindAsync(key);
    }
    /// <summary>
    /// Gets all drugs with only id and name properties
    /// </summary>
    /// <returns>A list of drugs</returns>
    public async Task<IEnumerable<Drug>> GetAll() {
        return await _context.Drugs
            .Select(x => new Drug() { 
                Id = x.Id, 
                Name = x.Name, 
                ActiveSubstance = null! })
            .ToListAsync();
    }

    public async Task<bool> SaveAll(IEnumerable<Drug> values) {
        bool canSave = false;
        if (canSave) {
            _context.AddRange(values);
            return _context.SaveChanges() > 0;
        }
        return false;
    }

    public async Task<bool> Update(Drug entity) {
        _context.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }
    #endregion
    #region DrugService
    public async Task<IEnumerable<Drug>> GetAllDrugs() {
        return await GetAll();
    }
    #endregion
}
