using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace µMedlogr.Controllers;

public class ModalController(EntityManager entityManager) : Controller {
    private readonly EntityManager _entityManager = entityManager;

    [HttpPost, Route("AddEvent")]
    [ActionName("AddEvent")]
    public async Task<IActionResult> AddEventAsync([FromForm] int healthrecordId, [FromForm] string title, [FromForm] string description, [FromForm] string drugsIdJson) {
        if (healthrecordId != 0 && title != null && description != null) {

            var drugIds = new List<int>();
            if (drugsIdJson is not null && drugsIdJson != String.Empty) {
                var deserializedDrugs = JsonSerializer.Deserialize<List<int>>(drugsIdJson);
                if (!deserializedDrugs.IsNullOrEmpty()) {
                    drugIds = deserializedDrugs;
                }
            }
            Event newEvent = _entityManager.CreateEvent(title, description, DateTime.Now, drugIds);
            bool success = _entityManager.SaveEntity<Drug>();
        }
        return Redirect(Request.Headers["Referer"]);
    }
}
