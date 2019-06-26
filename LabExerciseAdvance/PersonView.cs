using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public class PersonView
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Status Status { get; set; }
        public Gender Gender { get; set; }
        public string CityName { get; set; }
        public string Province { get; set; }
        public string Region { get; set; }
        public string PersonType { get; set; }
        public int Age { get; set; }
    }
}
