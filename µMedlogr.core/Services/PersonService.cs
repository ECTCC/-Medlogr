using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;

namespace µMedlogr.core.Services;
public class PersonService(µMedlogrContext context) : IEntityService<Person>, IPersonService {
    private readonly µMedlogrContext _context = context;
    #region EntityService
    public Task<bool> Delete(Person entity) {
        throw new NotImplementedException();
    }

    public Task<Person?> Find(int key) {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Person>> GetAll() {
        throw new NotImplementedException();
    }

    public Task<bool> SaveAll(IEnumerable<Person> values) {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Person entity) {
        throw new NotImplementedException();
    }
    #endregion
    #region PersonService
    public Task<bool> DeletePerson(Person person) {
        throw new NotImplementedException();
    }

    public Task<Person?> FindPerson(int personId) {
        throw new NotImplementedException();
    }

    public Task<Person?> GetAppUsersMePersonById(string userId) {
        throw new NotImplementedException();
    }

    public Task<bool> SavePerson(Person person) {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePerson(Person person) {
        throw new NotImplementedException();
    }
    #endregion
}
