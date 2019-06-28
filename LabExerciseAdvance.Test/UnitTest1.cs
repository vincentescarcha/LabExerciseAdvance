using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabExerciseAdvance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabExerciseAdvance.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PersonRepoLoadFromCSVTest()
        {
            PersonRepository personRepo = new PersonRepository();
            personRepo.LoadFromCSV("persons.csv");

            Assert.AreEqual(10, personRepo.GetList.Count);
        }
        [TestMethod]
        public void PersonRepoLoadFromCSVTest2()
        {
            PersonRepository personRepo = new PersonRepository();
            personRepo.LoadFromCSV("persons.csv");

            Assert.AreEqual("John", personRepo.GetSpecific(1).FirstName);
        }
        [TestMethod]
        public void PersonRepoConvertToTest()
        {
            string[] values = new[] { "John", "Doe", "19800101", "Male", "Married", "Las Pinas", "Store Manager" };
            PersonRepository personRepo = new PersonRepository();
            personRepo.AddAs<Adult>(values);

            Assert.AreEqual(typeof(Adult), personRepo.GetSpecific(1).GetType());
        }
        [TestMethod]
        public void PersonRepoGetCityIdByNameTest()
        {
            PersonRepository personRepo = new PersonRepository();
            var result = personRepo.GetCityIdByName("Bacoor");

            Assert.AreEqual(6, result);
        }
        [TestMethod]
        public void CityRepoNewInstanceTest()
        {
            CityRepository cityRepo = new CityRepository();

            Assert.AreEqual(9, cityRepo.GetList.Count);
        }
        [TestMethod]
        public void CityRepoLoadMoreCities()
        {
            CityRepository cityRepo = new CityRepository();
            cityRepo.LoadFromCSV("cItiEs.cSv");
            cityRepo.LoadFromCSV("CITIES.CSV");

            Assert.AreEqual(27, cityRepo.GetList.Count);
        }
        [TestMethod]
        public void CityRepoAddCities()
        {
            CityRepository cityRepo = new CityRepository();
            cityRepo.Add(new[]{ "Caloocan", "None", "NCR" });

            Assert.AreEqual(10, cityRepo.GetList.Count);
        }


        public static List<Person> GetAllPerson()
        {
            PersonRepository PersonRepo = new PersonRepository();
            PersonRepo.LoadFromCSV("persons.csv");
            return PersonRepo.GetList;
        }


        [TestMethod]
        public void SchoolAddPerson()
        {
            SchoolRegistration<Person> school = new SchoolRegistration<Person>();
            school.RegisterPerson(GetAllPerson()[2]);

            Assert.AreEqual(1,school.GetRegisteredPersons().Count);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Person is not a child")]
        public void SchoolAddInvalidPerson()
        {
            SchoolRegistration<Person> school = new SchoolRegistration<Person>();
            school.RegisterPerson(GetAllPerson()[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Person is not a child")]
        public void SchoolIsValidTestWithInvalidPerson()
        {
            SchoolRegistration<Person> school = new SchoolRegistration<Person>();
            school.IsPersonValid(GetAllPerson()[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Person is Already Registered")]
        public void SchoolAddTestWithDuplicatePerson()
        {
            SchoolRegistration<Person> school = new SchoolRegistration<Person>();
            school.RegisterPerson(GetAllPerson()[0]);
            school.RegisterPerson(GetAllPerson()[0]);
        }
        [TestMethod]
        public void SchoolTestIsPersonRegistered()
        {
            SchoolRegistration<Person> school = new SchoolRegistration<Person>();
            school.RegisterPerson(GetAllPerson()[2]);

            Assert.AreEqual(true,school.IsPersonRegistered(GetAllPerson()[2]));
        }
        [TestMethod]
        public void SchoolSearchRegisteredPerson()
        {
            SchoolRegistration<Person> school = new SchoolRegistration<Person>();
            school.RegisterPerson(GetAllPerson()[2]);
            school.RegisterPerson(GetAllPerson()[6]);
            school.RegisterPerson(GetAllPerson()[7]);

            var result1 = school.SearchRegisteredPersons("ASD","").Count;
            Assert.AreEqual(0, result1);

            var person = school.SearchRegisteredPersons("Jet","FirstName")[0];

            Assert.AreEqual("Smith",person.LastName);
            Assert.AreEqual("Jet", person.FirstName);
        }
        [TestMethod]
        public void SchoolUnregisterPerson()
        {
            SchoolRegistration<Person> school = new SchoolRegistration<Person>();
            school.RegisterPerson(GetAllPerson()[2]);
            school.RegisterPerson(GetAllPerson()[6]);
            school.RegisterPerson(GetAllPerson()[7]);

            Assert.AreEqual(3, school.GetRegisteredPersons().Count);

            school.UnregisterPerson(3);

            Assert.AreEqual(2, school.GetRegisteredPersons().Count);
        }


        [TestMethod]
        public void VotersAddPerson()
        {
            SchoolRegistration<Person> school = new SchoolRegistration<Person>();
            school.RegisterPerson(GetAllPerson()[2]);

            Assert.AreEqual(1, school.GetRegisteredPersons().Count);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Person is not an Adult")]
        public void VotersAddInvalidPerson()
        {
            VotersRegistration<Person> Voters = new VotersRegistration<Person>();
            Voters.RegisterPerson(GetAllPerson()[2]);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Person is not an Adult")]
        public void VotersIsValidTestWithInvalidPerson()
        {
            VotersRegistration<Person> Voters = new VotersRegistration<Person>();
            Voters.IsPersonValid(GetAllPerson()[3]);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Person is Already Registered")]
        public void VotersAddTestWithDuplicatePerson()
        {
            VotersRegistration<Person> Voters = new VotersRegistration<Person>();
            Voters.RegisterPerson(GetAllPerson()[0]);
            Voters.RegisterPerson(GetAllPerson()[0]);
        }
        [TestMethod]
        public void VotersTestIsPersonRegistered()
        {
            VotersRegistration<Person> Voters = new VotersRegistration<Person>();
            Voters.RegisterPerson(GetAllPerson()[4]);

            Assert.AreEqual(true, Voters.IsPersonRegistered(GetAllPerson()[4]));
        }
        [TestMethod]
        public void VotersSearchRegisteredPerson()
        {
            VotersRegistration<Person> Voters = new VotersRegistration<Person>();
            Voters.RegisterPerson(GetAllPerson()[0]);
            Voters.RegisterPerson(GetAllPerson()[1]);
            Voters.RegisterPerson(GetAllPerson()[4]);

            var result1 = Voters.SearchRegisteredPersons("ASD","").Count;
            Assert.AreEqual(0, result1);

            var person = Voters.SearchRegisteredPersons("Jake","FirstName")[0];

            Assert.AreEqual("Smith", person.LastName);
            Assert.AreEqual("Jake", person.FirstName);
        }
        [TestMethod]
        public void VotersUnregisterPerson()
        {
            VotersRegistration<Person> Voters = new VotersRegistration<Person>();
            Voters.RegisterPerson(GetAllPerson()[0]);
            Voters.RegisterPerson(GetAllPerson()[1]);
            Voters.RegisterPerson(GetAllPerson()[4]);

            Assert.AreEqual(3, Voters.GetRegisteredPersons().Count);

            Voters.UnregisterPerson(1);

            Assert.AreEqual(2, Voters.GetRegisteredPersons().Count);
        }


        [TestMethod]
        public void DayCareAddPerson()
        {
            DayCareRegistration<Person> DayCare = new DayCareRegistration<Person>();
            DayCare.RegisterPerson(GetAllPerson()[3]);

            Assert.AreEqual(1, DayCare.GetRegisteredPersons().Count);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Person is not an Infant")]
        public void DayCareAddInvalidPerson()
        {
            DayCareRegistration<Person> DayCare = new DayCareRegistration<Person>();
            DayCare.RegisterPerson(GetAllPerson()[1]);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Person is not an Infant")]
        public void DayCareIsValidTestWithInvalidPerson()
        {
            DayCareRegistration<Person> DayCare = new DayCareRegistration<Person>();
            DayCare.IsPersonValid(GetAllPerson()[1]);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Person is Already Registered")]
        public void DayCareAddTestWithDuplicatePerson()
        {
            DayCareRegistration<Person> DayCare = new DayCareRegistration<Person>();
            DayCare.RegisterPerson(GetAllPerson()[3]);
            DayCare.RegisterPerson(GetAllPerson()[3]);
        }
        [TestMethod]
        public void DayCareTestIsPersonRegistered()
        {
            DayCareRegistration<Person> DayCare = new DayCareRegistration<Person>();
            DayCare.RegisterPerson(GetAllPerson()[3]);

            Assert.AreEqual(true, DayCare.IsPersonRegistered(GetAllPerson()[3]));
        }
        [TestMethod]
        public void DayCareSearchRegisteredPerson()
        {
            DayCareRegistration<Person> DayCare = new DayCareRegistration<Person>();
            DayCare.RegisterPerson(GetAllPerson()[3]);

            List<Person> x = new List<Person>();

            var result1 = DayCare.SearchRegisteredPersons("ASD","").Count;
            Assert.AreEqual(0, result1);

            var result2 = DayCare.SearchRegisteredPersons("June","FirstName").Count;
            Assert.AreEqual(1, result2);
        }
        [TestMethod]
        public void DayCareUnregisterPerson()
        {
            DayCareRegistration<Person> DayCare = new DayCareRegistration<Person>();
            DayCare.RegisterPerson(GetAllPerson()[3]);

            Assert.AreEqual(1, DayCare.GetRegisteredPersons().Count);

            DayCare.UnregisterPerson(4);

            Assert.AreEqual(0, DayCare.GetRegisteredPersons().Count);
        }


        [TestMethod]
        public void CommonCalculateAgeTestValid()
        {
            //Arrange
            DateTime date = new DateTime(2000, 4, 20);
            int expected = 19;
            //Act
            int actual = Common.CalculateAge(date);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
