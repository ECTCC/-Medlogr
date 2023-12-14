using µMedlogr.core.Models;

namespace µMedlogr.core.Interfaces;
public interface IHealthRecordService {
    public Task<HealthRecord?> GetHealthRecordById(int id);
    public Task<HealthRecord?> GetHealthRecordByAppUserId(string appUserId);
    public Task<List<HealthRecordEntry>> GetHealthRecordEntriesByHealthRekordId(int healthRecordId);
    public Task<bool> SaveHealthRecord(HealthRecord record);
    public Task<bool> AddTemperatureDataToHealthRecord(HealthRecord record, TemperatureData data);
    public Task<bool> AddSymptomMeasurementToHealthRecord(HealthRecord record, HealthRecordEntry entry);
    public Task<bool> AddEventToHealthRecord(Event @event, int healthRecordId);
}
