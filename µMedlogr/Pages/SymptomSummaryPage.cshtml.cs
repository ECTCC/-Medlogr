using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using µMedlogr.core.Services;
using µMedlogr.core;
using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.Pages
{
    public class SymptomSummaryPageModel(EntityManager entityManager, µMedlogrContext context) : PageModel
    {
        private readonly EntityManager _entityManager = entityManager;
        private readonly µMedlogrContext _context = context;

        public HealthRecord HealthRecord { get; set; }
        public async Task OnGetAsync(int personId)
        {
            //var CurrentHealthRecord = await _context.HealthRecords
            //  .Include(x => x.PersonId == personId)
            //   .FirstOrDefaultAsync();
            

            //var CurrentPerson = await _context.People
            //    .Include(p => p.HealthRecord)
            //    .ThenInclude(hr => hr.CurrentSymptoms)
            //    .SingleOrDefaultAsync();
            //CurrentHealthRecord = CurrentPerson.HealthRecord;
        }
    }
}
