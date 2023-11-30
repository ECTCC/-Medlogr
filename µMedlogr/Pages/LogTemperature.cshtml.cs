using µMedlogr.core;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace µMedlogr.Pages; 
public class LogTemperatureModel : PageModel
{
    private readonly EntityManager _entityManager;
    private readonly µMedlogrContext _context;
    public List<TemperatureData> Temperatures { get; set; } = [];
    public string Nickname { get; set; }
    [BindProperty]
    [Range(30, 45, ErrorMessage ="Value for {0} must be between {1} and {2}")]
    public float NewTemperature { get; set; }
    [BindProperty]
    public string Notes { get; set; }
    public LogTemperatureModel(EntityManager entityManager, µMedlogrContext context) {
        _entityManager = entityManager;
        _context = context;
    }
    public void OnGet(int healthRecordId)
    {
        Nickname = _context.HealthRecords.Find(healthRecordId)?.Record?.NickName??"Anonym person";
        Temperatures =  _entityManager.GetTemperatureDataByHealthRecordId(healthRecordId).ToList();
    }
}
