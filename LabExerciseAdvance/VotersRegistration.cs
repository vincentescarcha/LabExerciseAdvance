using System;
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

        public List<PersonView> SearchRegisteredPersons(string FirstName, string LastName, string Gender, string Status, string City, string Province, string Region)
        {
            List<PersonView> _tempView = new List<PersonView>();

            if (FirstName != "")
            {
                List<PersonView> test = GetRegisteredPersons().ToPersonView().
                    Where(x => x.FirstName.IndexOf(FirstName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                _tempView.AddRange(CheckIfExist(test, _tempView));
            }
            if (LastName != "")
            {
                List<PersonView> test = GetRegisteredPersons().ToPersonView().
                    Where(x => x.LastName.IndexOf(LastName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                _tempView.AddRange(CheckIfExist(test, _tempView));
            }
            if (Gender != "")
            {
                List<PersonView> test = GetRegisteredPersons().ToPersonView().
                    Where(x => x.Gender.IndexOf(Gender, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                _tempView.AddRange(CheckIfExist(test, _tempView));
            }
            if (Status != "")
            {
                List<PersonView> test = GetRegisteredPersons().ToPersonView().
                    Where(x => x.Status.IndexOf(Status, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                _tempView.AddRange(CheckIfExist(test, _tempView));
            }
            if (City != "")
            {
                List<PersonView> test = GetRegisteredPersons().ToPersonView().
                    Where(x => x.City.IndexOf(City, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                _tempView.AddRange(CheckIfExist(test, _tempView));
            }
            if (Province != "")
            {
                List<PersonView> test = GetRegisteredPersons().ToPersonView().
                    Where(x => x.Province.IndexOf(Province, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                _tempView.AddRange(CheckIfExist(test, _tempView));
            }
            if (Region != "")
            {
                List<PersonView> test = GetRegisteredPersons().ToPersonView().
                    Where(x => x.Region.IndexOf(Region, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                _tempView.AddRange(CheckIfExist(test, _tempView));
            }
            return _tempView;
        }

        private List<PersonView> CheckIfExist(List<PersonView> _tempPersonView, List<PersonView> actualPersonView)
        {
            List<int> ids = actualPersonView.Select(x => x.ID).ToList();
            List<PersonView> toBeAddedToList = _tempPersonView.Where(x => !ids.Contains(x.ID)).ToList();

            return toBeAddedToList;
        }
    }
}
