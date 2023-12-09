using µMedlogr.core.Models;

namespace µMedlogr.core.Interfaces;
/// <summary>
/// Represents a Service over people, Ensuring person business rules
/// </summary>
internal interface IPersonService {
    public Task<Person?> GetAppUsersPersonById(string userId);
    public Task<Person?> FindPerson(int personId);
    public Task<bool> SavePerson(Person person);
    public Task<bool> UpdatePerson(Person person);
    public Task<bool> DeletePerson(Person person);
}
