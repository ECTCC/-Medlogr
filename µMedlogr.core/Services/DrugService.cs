using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core.Services;
public class DrugService(µMedlogrContext context) : IEntityService<Drug>, IDrugService{
    #region EntityService
    public async Task<bool> Delete(Drug entity) {
        context.Drugs.Remove(entity);
        return context.SaveChangesAsync().Result > 0;
    }

    public async Task<Drug?> Find(int key) {
        return await context.Drugs.FindAsync(key);
    }
    /// <summary>
    /// Gets all drugs with only id and name properties
    /// </summary>
    /// <returns>A list of drugs</returns>
    public async Task<IEnumerable<Drug>> GetAll() {
        return await context.Drugs
            .Select(x => new Drug() { 
                Id = x.Id, 
                Name = x.Name, 
                ActiveSubstance = null! })
            .ToListAsync();
    }

    public async Task<bool> SaveAll(IEnumerable<Drug> values) {
        bool canSave = false;
        if (canSave) {
            context.AddRange(values);
            return context.SaveChanges() > 0;
        }
        return false;
    }

    public async Task<bool> Update(Drug entity) {
        context.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }
    #endregion
    #region DrugService
    public async Task<IEnumerable<Drug>> GetAllDrugs() {
        return await GetAll();
    }

    public async Task<IEnumerable<Drug>> FindRange(IEnumerable<int> ids) {
        return await context.Drugs.Where(x => ids.Contains(x.Id)).ToListAsync();
    }
    #endregion
}
