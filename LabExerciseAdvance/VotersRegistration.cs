﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public class VotersRegistration<T> : IRegistration<T> where T : Person
    {
        public List<T> RegisteredPersons { get; set; }
        public VotersRegistration()
        {
            RegisteredPersons = new List<T>();
        }

        public List<T> GetRegisteredPersons()
        {
            return RegisteredPersons.OrderBy(p => p.ID).ToList();
        }

        public bool IsPersonRegistered(T Person)
        {
            return RegisteredPersons.Any(p => p.ID == Person.ID);
        }

        public bool IsPersonValid(T Person)
        {
            if (!(Person is Adult))
            {
                throw new Exception("Person is not an Adult");
            }
            if (IsPersonRegistered(Person))
            {
                throw new Exception("Person is Already Registered");
            }
            if (Person.Age <= 16)
            {
                throw new Exception("Person Age is Minor to Register");
            }
            return true;
        }

        public void RegisterPerson(T Person) 
        {
            IsPersonValid(Person);
            RegisteredPersons.Add(Person);
        }

        public void UnregisterPerson(int personId)
        {
            T person = GetRegisteredPersons().SingleOrDefault(p => p.ID == personId);
            if (person == null || !IsPersonRegistered(person))
            {
                throw new Exception("Person is not Registered");
            }
            
            RegisteredPersons.Remove(person);
        }

        public List<T> SearchRegisteredPersons(string FirstName, string LastName, string Gender, string Status, string City, string Province, string Region)
        {

            return (from person in GetRegisteredPersons()

                    join city in Program.CityRepo.GetList
                        on person.CityId equals city.ID

                    where (FirstName == "" || (person.FirstName.IndexOf(FirstName, StringComparison.OrdinalIgnoreCase) >= 0)) &&

                        (LastName == "" || (person.LastName.IndexOf(LastName, StringComparison.OrdinalIgnoreCase) >= 0)) &&

                        (Gender == "" || (person.Gender.ToString().IndexOf(Gender, StringComparison.OrdinalIgnoreCase) >= 0)) &&

                        (Status == "" || (person.Status.ToString().IndexOf(Status, StringComparison.OrdinalIgnoreCase) >= 0)) &&

                        (City == "" || (city.Name.IndexOf(City, StringComparison.OrdinalIgnoreCase) >= 0)) &&

                        (Province == "" || (city.Province.IndexOf(Province, StringComparison.OrdinalIgnoreCase) >= 0)) &&

                        (Region == "" || (city.Region.IndexOf(Region, StringComparison.OrdinalIgnoreCase) >= 0))

                    select person).ToList();
        }
    }
}
