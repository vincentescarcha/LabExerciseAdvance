using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public class PersonRepository
    {
        private readonly List<Person> _persons;
        public static CityRepository CityRepo = new CityRepository();
        private int IdCount = 1;

        public PersonRepository()
        {
            _persons = new List<Person>();
        }
        public List<Person> GetList
        {
            get
            {
                return _persons;
            }
        }
        public Person GetSpecific(int id)
        {
            return _persons.SingleOrDefault(x => x.ID == id);
        }
        public void LoadFromCSV(string fileName)
        {
            string fileUrl = Common.CheckCurrentDirectory(fileName);

            using (var reader = new StreamReader(fileUrl))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split('|');
                    Add(values);
                }
            }
        }
        public void Validate(string[] values)
        {
            string message = "";
            if (values.Length < 6)
            {
                message = "Invalid Input";
                throw new Exception(message);
            }
            if (!Regex.IsMatch(values[0], @"^[a-zA-Z\ ]+$"))
            {
                message += "First Name should contain Alpha characters only\n";
            }
            if (!Regex.IsMatch(values[1], @"^[a-zA-Z\ ]+$"))
            {
                message += "Last Name should contain Alpha characters only\n";
            }

            try
            {
                Common.ParseDate(values[2]);
            }
            catch
            {
                message += "Date Of Birth should be in correct format\n";
            }

            if (values[3].ToLower() != "male" && values[3].ToLower() != "female")
            {
                message += "Gender can only be Male or Female\n";
            }

            if (message != "")
            {
                throw new Exception(message);
            }
        }
        public void Add(string[] values)
        {
            var age = Common.CalculateAge(Common.ParseDate(values[2]));

            if (age >= 11)
                AddAs<Adult>(values);
            else if (age >= 2 && age < 11)
                AddAs<Child>(values);
            else
                AddAs<Infant>(values);
        }
        public void AddAs<T>(string[] values) where T : Person, new ()
        {
            T person = new T
            {
                ID = IdCount,
                FirstName = values[0],
                LastName = values[1],

                DateOfBirth = Common.ParseDate(values[2]),

                Gender = (Gender)Enum.Parse(typeof(Gender), values[3]),
                Status = (Status)Enum.Parse(typeof(Status), values[4]),

                CityId = GetCityIdByName(values[5])
            };

            if (typeof(T) == typeof(Adult))
            {
                Type Adult = person.GetType();
                PropertyInfo jobTitle = Adult.GetProperty("JobTitle");
                jobTitle.SetValue(person,values[6]);
            }
            else if (typeof(T) == typeof(Child))
            {
                Type Child = person.GetType();
                PropertyInfo school = Child.GetProperty("School");
                school.SetValue(person, values[6]);
                PropertyInfo level = Child.GetProperty("Level");
                level.SetValue(person, values[7]);
            }
            else if (typeof(T) == typeof(Infant))
            {
                Type Infant = person.GetType();
                PropertyInfo favoriteFood = Infant.GetProperty("FavoriteFood");
                favoriteFood.SetValue(person, values[6]);
                PropertyInfo favoriteMilk = Infant.GetProperty("FavoriteMilk");
                favoriteMilk.SetValue(person, values[7]);
            }
            _persons.Add(person);
            IdCount++;
        }
        public int GetCityIdByName(string cityName)
        {
            return CityRepo.GetList.SingleOrDefault(c => c.Name == cityName).ID;
        }
        //public void Clear()
        //{
        //    IdCount = 1;
        //    _persons.Clear();
        //}
    }
}
