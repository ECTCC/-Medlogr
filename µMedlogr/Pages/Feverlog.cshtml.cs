using µMedlogr.core.Exceptions;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace µMedlogr.Pages;
// TODO: Until sonarlint can understand primary constructros fully, keep this disabled 
#pragma warning disable S3604
public class AddTemperatureModel(HealthRecordService healthRecordService) : PageModel {
    public ChartJs Chart { get; set; } = null!;
    public string ChartJson { get; set; } = null!;
    public List<TemperatureData> Temperatures { get; set; } = [];
    public string Nickname { get; set; } = "";
    public int HealthRecordId { get; set; }
    [BindProperty]
    [Range(35, 45, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public float MeasuredTemperature { get; set; }
    [BindProperty]
    public string? Notes { get; set; }

    public async void OnGet(int healthRecordId) {
        var healthRecord = await healthRecordService.GetHealthRecordById(healthRecordId);
        Nickname = healthRecord?.Person?.NickName ?? "Anonym person";
        Temperatures = healthRecord?.Temperatures.ToList() ?? []; 
        Temperatures.Sort((y, x) => x.TimeOfMeasurement.CompareTo(y.TimeOfMeasurement));
        var temps = Temperatures.Take(10).Select(x => (int)Math.Round(x.Measurement)).ToList();
        var times = Temperatures.Take(10).Select(x => x.TimeOfMeasurement).ToList();
        HealthRecordId = healthRecordId;

        var chartData = $$"""
        {
            type: 'line',
            responsive: true,
            data:
            {
                labels: ['{{times.ElementAtOrDefault(9)}}', '{{times.ElementAtOrDefault(8)}}', '{{times.ElementAtOrDefault(7)}}', '{{times.ElementAtOrDefault(6)}}', '{{times.ElementAtOrDefault(5)}}', '{{times.ElementAtOrDefault(4)}}', '{{times.ElementAtOrDefault(3)}}', '{{times.ElementAtOrDefault(2)}}', '{{times.ElementAtOrDefault(1)}}', '{{times.ElementAtOrDefault(0)}}'],
                datasets: [{
                    label: 'Kroppstemperatur °',
                    data: [{{temps.ElementAtOrDefault(9)}},{{temps.ElementAtOrDefault(8)}},{{temps.ElementAtOrDefault(7)}},{{temps.ElementAtOrDefault(6)}},{{temps.ElementAtOrDefault(5)}},{{temps.ElementAtOrDefault(4)}},{{temps.ElementAtOrDefault(3)}},{{temps.ElementAtOrDefault(2)}},{{temps.ElementAtOrDefault(1)}},{{temps.ElementAtOrDefault(0)}}],
                    backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                        ],
                    borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                        ],
                    borderWidth: 1
                }]
            },
            options:
            {
                scales:
                {
                    yAxes: [{
                        ticks:
                        {
                            beginAtZero: false
                        },
                        min: 30,
                        max: 47
                    }]
                }
            }
        }
        """;

        Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
        ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore,
        });
    }

    public async Task<IActionResult> OnPost(int healthRecordId) {
        if (!ModelState.IsValid) {
            TempData["Error"] = "Modal";
            TempData["Message"] = "Kontrollera angivna data";
            return Page();
        }
        var healthRecord = await healthRecordService.GetHealthRecordById(healthRecordId);
        var temperatureData = new TemperatureData() { Measurement = MeasuredTemperature, Comments=Notes, TimeOfMeasurement = DateTime.Now };

        if(healthRecord is null) {
            TempData["Error"] = "Modal";
            TempData["Message"] = "Tekniskt fel. Kunde inte hitta loggen, kontakta sidans administratör";
            return Page();
        }
        try {
            await healthRecordService.AddTemperatureDataToHealthRecord(healthRecord, temperatureData);
        } catch (TemperatureOutOfRangeException) {
            TempData["Error"] = "Modal";
            TempData["Message"] = "Kunde inte lägga till temperaturmätning";
            return Page();
        }
        return RedirectToPage("/Feverlog", new { healthRecordId });
    }
}
#pragma warning restore S3604
#region Chart conversion classes
public class ChartJs {
    public string type { get; set; }
    public int duration { get; set; }
    public string easing { get; set; }
    public bool responsive { get; set; }
    public Title title { get; set; }
    public bool lazy { get; set; }
    public Data data { get; set; }
    public Options options { get; set; }
}
public class Options {
    public Scales scales { get; set; }
}
public class Scales {
    public yAxes[] yAxes { get; set; }
    public xAxes[] xAxes { get; set; }
}
public class Ticks {
    public bool beginAtZero { get; set; }
}
public class Title {
    public bool display { get; set; }
    public string text { get; set; }
}
public class xAxes {
    public string id { get; set; }
    public bool display { get; set; }
    public string type { get; set; }
    public Ticks ticks { get; set; }
}
public class yAxes {
    public string id { get; set; }
    public bool display { get; set; }
    public string type { get; set; }
    public Ticks ticks { get; set; }
}
public class Data {
    public string[] labels { get; set; }
    public Dataset[] datasets { get; set; }
}
public class Dataset {
    public string label { get; set; }
    public int[] data { get; set; }
    public string[] backgroundColor { get; set; }
    public string[] borderColor { get; set; }
    public int borderWidth { get; set; }
    public string yAxisID { get; set; }
    public string xAxisID { get; set; }
}
#endregion