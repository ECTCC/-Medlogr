using µMedlogr.core.Models;

namespace µMedlogr.core.Interfaces;
internal interface IHealthRecordService {
    public Task<HealthRecord> GetHealthRecordById(int id);
    public Task<HealthRecord> GetHealthRecordByName(string name);
    public Task<bool> SaveHealthRecord(HealthRecord record);
    public Task<bool> AddTemperatureDataToHealthRecord(HealthRecord record, TemperatureData data);
    public Task<bool> AddSymptomMeasurementToHealthRecord(HealthRecord record, SymptomMeasurement measurement);
}
