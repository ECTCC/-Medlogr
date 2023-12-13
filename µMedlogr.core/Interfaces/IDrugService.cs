using µMedlogr.core.Models;

namespace µMedlogr.core.Interfaces;
public interface IDrugService {
    public Task<IEnumerable<Drug>> GetAllDrugs();
    public Task<IEnumerable<Drug>> FindRange(IEnumerable<int> ids);
}
