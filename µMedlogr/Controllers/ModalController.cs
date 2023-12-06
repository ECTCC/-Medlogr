using Microsoft.AspNetCore.Mvc;
using System.Web;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;

namespace µMedlogr.Controllers;
//[ApiController]
//[Route("[controller]")]
public class ModalController(EntityManager entityManager) : Controller {
    private readonly EntityManager _entityManager = entityManager;

    [HttpPost, Route("AddEvent")]
    [ActionName("AddEvent")]
    public async Task<IActionResult> AddEventAsync([FromForm] int healthrecordId, [FromForm] string title, [FromForm] string description, [FromForm] string drugsIdJson) {
        var a = Request.QueryString;
        if (healthrecordId != 0 && title != null && description != null) {

            var drugIds = new List<int>();
            if (drugsIdJson is not null && drugsIdJson != String.Empty) {
                var deserializedDrugs = JsonSerializer.Deserialize<List<int>>(drugsIdJson);
                if (!deserializedDrugs.IsNullOrEmpty()) {
                    drugIds = deserializedDrugs;
                }
            }
            Event newEvent = _entityManager.CreateEvent(title, description, DateTime.Now, drugIds);
            //Save Entity
        }
        return Redirect(Request.Headers["Referer"]);
    }
}
