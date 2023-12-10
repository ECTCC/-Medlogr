using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace µMedlogr.Controllers;

public class ModalController(HealthRecordService healthRecordService, DrugService drugService) : Controller {

    [HttpPost, Route("AddEvent")]
    [ActionName("AddEvent")]
    public async Task<IActionResult> AddEventAsync([FromForm] int healthrecordId, [FromForm] string title, [FromForm] string description, [FromForm] int[] drugId) {
        if (healthrecordId != 0 && title != null && description != null) {
            ICollection<Drug> drugs = (await drugService.FindRange(drugId)).ToList();
            var newEvent = new Event() {Title = title, Description=description, NotedAt=DateTime.Now, AdministeredMedicines=drugs };
            await healthRecordService.AddEventToHealthRecord(newEvent);
        }
        return Redirect(Request.Headers["Referer"]);
    }
}
