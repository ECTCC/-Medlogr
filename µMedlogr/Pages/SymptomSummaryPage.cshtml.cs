using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using µMedlogr.core.Services;
using µMedlogr.core;
using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace µMedlogr.Pages
{
    public class SymptomSummaryPageModel(EntityManager entityManager, µMedlogrContext context) : PageModel
    {
        private readonly EntityManager _entityManager = entityManager;
        private readonly µMedlogrContext _context = context;

        public Person Person { get; set; }

        public HealthRecord HealthRecord { get; set; }
        public async Task OnGetAsync(int personId =2)
        {
            var currentHealthRecord = await _context.HealthRecords
              .Where(x => x.PersonId == personId)
              .FirstOrDefaultAsync();
            HealthRecord = currentHealthRecord;

            var currentPerson = await _context.People
                .Where(p=>p.Id == personId)
                .FirstOrDefaultAsync();
            Person = currentPerson;
           
        }
    }
}
