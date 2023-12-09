using µMedlogr.core.Models;

namespace µMedlogr.core.Interfaces;
internal interface IDrugService {
    public Task<IEnumerable<Drug>> GetAllDrugs();
}
