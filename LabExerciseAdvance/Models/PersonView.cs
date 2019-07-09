using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance.Model
{
    public class PersonView 
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Region { get; set; }
        public string PersonType { get; set; }
    }
}
