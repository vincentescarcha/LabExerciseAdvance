using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public class Person
    {
        public Person()
        {

        }
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age {
            get
            {
                return Common.CalculateAge(DateOfBirth);
            }
        }
        public Status Status { get; set; }
        public Gender Gender { get; set; }
        public int CityId { get; set; }

    }
    public enum Gender
    {
        Male,Female
    }
    public enum Status
    {
        Single,Married
    }
}
