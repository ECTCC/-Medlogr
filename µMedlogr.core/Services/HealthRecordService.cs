using µMedlogr.core.Exceptions;
using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace µMedlogr.core.Services;
public class HealthRecordService(µMedlogrContext context) : IHealthRecordService {
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
    public async Task<bool> AddSymptomMeasurementToHealthRecord(HealthRecord record, HealthRecordEntry entry) {
        ArgumentNullException.ThrowIfNull(record);
        ArgumentNullException.ThrowIfNull(entry);
        record.Entries.Add(entry);
        context.Update(record);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddTemperatureDataToHealthRecord(HealthRecord record, TemperatureData data) {
        ArgumentNullException.ThrowIfNull(record);
        ArgumentNullException.ThrowIfNull(data);
        TemperatureOutOfRangeException.ThrowIfEqual(false, IsValidTemperature(data.Measurement));
        record.Temperatures.Add(data);
        context.Update(record);
        return await context.SaveChangesAsync() > 0;
    }


    public async Task<HealthRecord?> GetHealthRecordById(int id) {
        return context.HealthRecords
            .Include(x => x.Events)
            .ThenInclude(x => x.AdministeredMedicines)
            .Include(x => x.Entries)
            .Include(x => x.Temperatures)
            .Where(x => x.Id == id)
            .FirstOrDefault();
    }
    public async Task<List<HealthRecordEntry>> GetHealthRecordEntriesByHealthRekordId(int healthRecordId)
    {
        var healthRekordentries = await context.HealthRecords
            .Where(hr => hr.Id == healthRecordId)
            .SelectMany(hr => hr.Entries)
            .Include(entry => entry.Measurements)
            .ToListAsync();
        return healthRekordentries;
    }

    public async Task<HealthRecord?> GetHealthRecordByAppUserId(string appUserId) {
        var user = context.AppUsers
            .Where(x => x.Id == appUserId)
            .Select(x => x.Me);
        HealthRecord? record = null;
        if (user.Any()) {
            // user should never be null under given conditions 
            record = await user.Select(user => user!.HealthRecord).Include(x => x.Person).Include(x => x.Events).Include(x => x.Temperatures).FirstOrDefaultAsync();
        }
        return record;
    }

    public async Task<bool> SaveHealthRecord(HealthRecord record) {
        if(record is null || !IsValidHealthRecord(record)) {
            return false;
        }
        context.HealthRecords.Add(record);
        return (await context.SaveChangesAsync()) > 0;
    }

    public async Task<bool> AddEventToHealthRecord(Event @event, int healthRecordId) {
        HealthRecord? record = context.HealthRecords.Find(healthRecordId);
        if (record is not null && IsValidEvent(@event)) {
            record.Events.Add(@event);
            context.Update(record);
            return (await context.SaveChangesAsync()) > 0;
        }
        return false;
    }
    #endregion

    private static bool IsValidEvent(Event @event) {
        var timeSpan = new TimeSpan(DateTime.Now.Ticks - @event.NotedAt.Ticks);
        if (@event is null || timeSpan.Ticks < 0 || timeSpan.TotalMilliseconds > 5000 ) { 
            return false; 
        }
        return true;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidHealthRecord(HealthRecord healthRecord) {
        return healthRecord.Id <= 0;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidTemperature(float measurement) {
        return measurement >= 35 && measurement <= 45;
    }
}
