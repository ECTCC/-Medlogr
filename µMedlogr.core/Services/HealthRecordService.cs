using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core.Services;
public class HealthRecordService(µMedlogrContext context) : IEntityService<HealthRecord>, IHealthRecordService {
    #region EntityService
    public Task<bool> Delete(HealthRecord entity) {
        throw new NotImplementedException();
    }

    public Task<HealthRecord?> Find(int key) {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<HealthRecord>> GetAll() {
        throw new NotImplementedException();
    }

    public Task<bool> SaveAll(IEnumerable<HealthRecord> values) {
        throw new NotImplementedException();
    }

    public Task<bool> Update(HealthRecord entity) {
        throw new NotImplementedException();
    }
    #endregion
    #region HealthRecordService
    public Task<bool> AddSymptomMeasurementToHealthRecord(HealthRecord record, SymptomMeasurement measurement) {
        throw new NotImplementedException();
    }

    public Task<bool> AddTemperatureDataToHealthRecord(HealthRecord record, TemperatureData data) {
        throw new NotImplementedException();
    }

    public async Task<HealthRecord?> GetHealthRecordById(int id) {
        return await context.HealthRecords
            .Where(x => x.Id == id)
            .Include(x => x.Events)
            .Include(x => x.Entries)
            .Include(x => x.Temperatures)
            .FirstOrDefaultAsync();
    }

    public Task<HealthRecord> GetHealthRecordByAppUserId(string appUserId) {
        throw new NotImplementedException();
    }

    public Task<bool> SaveHealthRecord(HealthRecord record) {
        throw new NotImplementedException();
    }

    public Task<bool> AddEventToHealthRecord(Event theEvent) {
        throw new NotImplementedException();
    }
    #endregion

}
