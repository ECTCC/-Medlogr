using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using µMedlogr.core.Enums;

namespace µMedlogr.Pages
{
    public class IndexModel : PageModel
    {
        public List<string> Severitys { get; set; }
        public IndexModel()
        {

        }
        public void OnGet()
        {
            Severitys = Enum.GetValues(typeof(Severity))
    .Cast<Severity>()
    .Select(v => v.ToString())
    .ToList();
            var a = 0;

        }
    }
}
