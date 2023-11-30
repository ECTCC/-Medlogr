using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace µMedlogr.core.Services
{
    public class PersonPage
    {
        //Might be needed?
        public static List<string> CreateGenderList()
        {
            List<string> genderlist = new List<string>();
            genderlist.AddRange(new List<string> { "Man", "Kvinna", "Vill ej Ange" });
            return genderlist;
        }
        public static List<string> CreateAllergiesList()
        {
            List<string> allergieslist = new List<string>();
            allergieslist.AddRange(new List<string> {"Pollen","Glutten","Laktos","Jordnötter","Skaldjur","Ägg","Vete","Selleri"});

            return allergieslist;
        }
        public static List<string> ReturnSameListOrAddStringNoAllergy(List<string>inputlist)
        {
            if (inputlist.Count == 0)
            {
                inputlist.Add("Inga allergier");
                return inputlist;
            }
            return inputlist;
        }
        //Might be needed?
        public static TimeSpan GetAge(DateOnly birthdate)
        {
            var birthDateTime = new DateTime(birthdate.Year, birthdate.Month, birthdate.Day);
            var today = DateTime.Now;
            var age = today - birthDateTime;
            return age;
        }
    }
}
