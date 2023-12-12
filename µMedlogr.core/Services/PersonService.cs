using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core.Services;
public class PersonService(µMedlogrContext context) : IEntityService<Person>, IPersonService {
    #region EntityService
    public Task<bool> Delete(Person entity) {
        if (entity == null) {
            return Task.FromResult(false);
        }
        return Task.FromResult(true);
    }

    public Task<Person?> Find(int key) {
        if(key <= 0) {
            return Task.FromResult<Person?>(null);
        }
        return Task.FromResult<Person?>(null);
    }

    public Task<IEnumerable<Person>> GetAll() {
        return Task.Run(() => context.People.AsEnumerable());
    }

    public Task<bool> SaveAll(IEnumerable<Person> values) {
        context.AddRange(values);
        return Task.Run(() => context.SaveChanges() > 0);
    }

    public Task<bool> Update(Person entity) {
        if (entity is null || entity.Id <= 0) {
            return Task.FromResult(false);
        }
        context.Update<Person>(entity);
        return Task.Run(() => context.SaveChanges() > 0);
    }
    #endregion
    #region PersonService
    public async Task<bool> DeletePerson(Person person) {
        context.People.Remove(person);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Person?> FindPerson(int personId) {
        if(personId <= 0) {
            return null;
        }
        return await context.People
            .Where(x => x.Id == personId)
            .Include(x => x.CareGivers)
            .Include(x => x.HealthRecord)
            .FirstOrDefaultAsync();
    }

    public async Task<Person?> GetAppUsersPersonById(string userId) {
        if(userId is null) {
            return null;
        }
        /*
        IQueryable<AppUser> appUser = context.AppUsers
            .Include(x => x.Me)
            .ThenInclude(x => x.CareGivers)
            .Include(x => x.Me)
            .ThenInclude(x => x.HealthRecord)
            .Where(x => x.Id == userId);
        return await appUser.Select(x => x.Me).FirstOrDefaultAsync();
        */
        return await context.AppUsers
            .Where(x => x.Id == userId)
            .Include(x => x.Me)
            .ThenInclude(x => x.CareGivers)
            .Include(x => x.Me)
            .ThenInclude(x => x.HealthRecord)
            .Select(x => x.Me)
            .FirstOrDefaultAsync();

    }

    public async Task<bool> SavePerson(Person person) {
        if (person is not null && HasValidData(person) && person.Id == 0) {
            context.People.Add(person);
            return await context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<bool> UpdatePerson(Person person) {
        if (person is not null && HasValidData(person) && ExistsInDatabase<Person>(person.Id)) {
            context.People.Update(person);
            return await context.SaveChangesAsync() > 0;
        }
        return false;
    }
    #endregion

    private static bool HasValidData(Person person) {
        bool hasReasonableAge = person.DateOfBirth > new DateOnly(1920, 1, 1) && person.DateOfBirth < DateOnly.FromDateTime(DateTime.Now);
        bool hasReasonableWeight = person.WeightInKg > 2 && person.WeightInKg < 200;
        return hasReasonableAge && hasReasonableWeight;
    }
    private bool ExistsInDatabase<T>(int entityId) where T: Entity => context.Find<T>(entityId) is not null;

    public async Task<AppUser?> GetAppUserWithRelationsById(string userId) => 
        await context.AppUsers
        .Include(x => x.Me)
        .ThenInclude(x => x.HealthRecord)
        .Include(x => x.PeopleInCareOf)
        .ThenInclude(x => x.HealthRecord)
        .Where(x => x.Id.Equals(userId))
        .FirstOrDefaultAsync();

    public async Task<bool> UpdateAppUser(AppUser user) {
        context.AppUsers.Update(user);
        return await context.SaveChangesAsync() > 0;
    }
}
