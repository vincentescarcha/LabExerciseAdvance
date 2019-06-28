using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace LabExerciseAdvance
{
    public class Program
    {
        public static PersonRepository PersonRepo = new PersonRepository();
        public static CityRepository CityRepo = new CityRepository();
        public static IRegistration<Adult> Voters = new VotersRegistration<Adult>();
        public static IRegistration<Child> School = new SchoolRegistration<Child>();
        public static IRegistration<Infant> DayCare = new DayCareRegistration<Infant>();
        public static void Main(string[] args)
        {
            LoadPersonFromCSV();
            ShowMessage("Person Loaded");
            AskFunction();

            Console.ReadKey();
        }

        public static void LoadPersonFromCSV()
        {
            List<Person> persons = new List<Person>();
            bool tryAgain = true;
            while (tryAgain)
            {
                try
                {
                    Console.WriteLine("\nPlease insert file name to load");
                    string fileName = Console.ReadLine();
                    PersonRepo.LoadFromCSV(fileName);
                    persons = PersonRepo.GetList;
                    tryAgain = false;
                }
                catch (Exception ex)
                {
                    //PersonRepo.Clear();
                    ShowError(ex.Message);
                }
            }

            DisplayPerson(persons);
        }


        public static void DisplayPerson(List<Person> persons)
        {
            ConsoleTable table = new ConsoleTable("ID", "First Name", "Last Name", "Date of Birth", "Gender", "Status","   ", "   ");
            foreach (var person in persons)
            {
                if (person is Adult)
                {
                    table.AddRow(person.ID, person.FirstName, person.LastName, person.DateOfBirth.ToString("MMM dd, yyyy"), person.Gender, person.Status,
                         ((Adult)person).JobTitle, "");
                }
                else if (person is Child)
                {
                    table.AddRow(person.ID, person.FirstName, person.LastName, person.DateOfBirth.ToString("MMM dd, yyyy"), person.Gender, person.Status,
                         ((Child)person).School, ((Child)person).Level);
                }
                else if (person is Infant)
                {
                    table.AddRow(person.ID, person.FirstName, person.LastName, person.DateOfBirth.ToString("MMM dd, yyyy"), person.Gender, person.Status,
                         ((Infant)person).FavoriteFood, ((Infant)person).FavoriteMilk);
                }
            }
            table.Write();
        }


        public static void AskFunction()
        {
            Console.WriteLine("\nWhat do you want to do next?");
            Console.WriteLine("(A)dd Person  |  Show (P)erson |  (R)egister  |  (U)nregister  |  " +
                "(S)how Registered |  Searc(h) |  (E)xport");
            string answer = Console.ReadLine().Trim();
            if (answer.Length > 0)
            {
                switch (answer[0].ToString().ToLower())
                {
                    case "a":
                        InputPerson();
                        break;
                    case "p":
                        PersonView();
                        break;
                    case "r":
                        AskTypeForRegistration();
                        break;
                    case "u":
                        AskTypeForUnregistration();
                        break;
                    case "s":
                        AskTypeForShowing();
                        break;
                    case "h":
                        AskTypeForSearching();
                        break;
                    case "e":
                        ExportToXML();
                        break;
                    case "v":
                        PersonRepo.GetSpecific(1).TryParseTo(typeof(Adult), out Adult person1);
                        Voters.RegisterPerson(person1);
                        PersonRepo.GetSpecific(2).TryParseTo(typeof(Adult), out Adult person2);
                        Voters.RegisterPerson(person2);
                        PersonRepo.GetSpecific(3).TryParseTo(typeof(Child), out Child person3);
                        School.RegisterPerson(person3);
                        PersonRepo.GetSpecific(4).TryParseTo(typeof(Infant), out Infant person4);
                        DayCare.RegisterPerson(person4);
                        PersonRepo.GetSpecific(5).TryParseTo(typeof(Adult), out Adult person5);
                        Voters.RegisterPerson(person5);
                        PersonRepo.GetSpecific(6).TryParseTo(typeof(Adult), out Adult person6);
                        Voters.RegisterPerson(person6);
                        PersonRepo.GetSpecific(7).TryParseTo(typeof(Child), out Child person7);
                        School.RegisterPerson(person7);
                        PersonRepo.GetSpecific(8).TryParseTo(typeof(Child), out Child person8);
                        School.RegisterPerson(person8);
                        PersonRepo.GetSpecific(9).TryParseTo(typeof(Adult), out Adult person9);
                        Voters.RegisterPerson(person9);
                        PersonRepo.GetSpecific(10).TryParseTo(typeof(Adult), out Adult person10);
                        Voters.RegisterPerson(person10);
                        break;
                    default:
                        ShowError("Error: Invalid Answer");
                        break;
                }
            }
            AskFunction();
        }


        public static void InputPerson()
        {
            Console.WriteLine("\nPlease input formated Person:");
            string input = Console.ReadLine().Trim();
            ProcessInputPerson(input);
        }
        public static void ProcessInputPerson(string input)
        {
            string[] values = input.Split('|');

            try
            {
                PersonRepo.Validate(values);
                PersonRepo.Add(values);
                ShowMessage("Successfully Added! ID: "+PersonRepo.GetList.Last().ID);
            }
            catch (Exception ex)
            {
                ShowError("Error:\n" + ex.Message);
            }
            AskInputPersonAgain();
        }
        public static void AskInputPersonAgain()
        {
            Console.WriteLine("\nDo you want to Add again? (y)/(n)");
            string answer = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(answer) && answer.Trim()[0].ToString().ToLower() == "y")
            {
                InputPerson();
            }
            else if (!string.IsNullOrEmpty(answer) && answer.Trim()[0].ToString().ToLower() == "n")
            {
                AskFunction();
            }
            else
            {
                ShowError("Error: Invalid answer\n");
                AskInputPersonAgain();
            }
        }


        public static void PersonView()
        {
            var personList = PersonRepo.GetList;
            ShowMessage("\nShowing List of Persons");
            DisplayPerson(personList);
        }


        public static void AskRegisterAgain()
        {
            Console.WriteLine("\nDo you want to Register again? (y)/(n)");
            string answer = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(answer) && answer.Trim()[0].ToString().ToLower() == "y")
            {
                AskTypeForRegistration();
            }
            else if (!string.IsNullOrEmpty(answer) && answer.Trim()[0].ToString().ToLower() == "n")
            {
                AskFunction();
            }
            else
            {
                ShowError("Error: Invalid Answer");
                AskRegisterAgain();
            }
        }
        public static void AskTypeForRegistration()
        {
            Console.WriteLine("\nWhat Registration do you want to Register?");
            Console.WriteLine("(V)oters  |  (S)chool  |  (D)ay Care");
            string answer = Console.ReadLine().Trim();
            if (answer.Length > 0)
            {
                switch (answer[0].ToString().ToLower())
                {
                    case "v":
                        AskPersonRegister("Voters", Voters);
                        break;
                    case "s":
                        AskPersonRegister("School", School);
                        break;
                    case "d":
                        AskPersonRegister("DayCare", DayCare);
                        break;
                    default:
                        ShowError("Error: Invalid Answer");
                        break;
                }
                AskRegisterAgain();
            }
            else
            {
                AskTypeForRegistration();
            }
        }
        public static void AskPersonRegister<T>(string registrationString, IRegistration<T> registration) where T : Person
        {
            Console.WriteLine("\nPlease enter ID you want to Register in " + registrationString + " registration");
            if (int.TryParse(Console.ReadLine().Trim(), out int answer) && answer != 0)
            {
                RegisterPerson(answer, registration);
            }
            else
            {
                ShowError("Error: Invalid ID");
                AskRegisterAgain();
            }
        }
        public static void RegisterPerson<T>(int Id, IRegistration<T> registration) where T : Person
        {
            //get person, validate then register
            try
            {
                Person person = PersonRepo.GetSpecific(Id);
                if (person == null)
                {
                    throw new Exception("No Record Found");
                }

                if (!person.TryParseTo(typeof(T), out T convertedPerson))
                {
                    throw new Exception("Person is not "+typeof(T).Name);
                }
                registration.RegisterPerson(convertedPerson);
                ShowMessage("Person Registered!");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }

            AskRegisterAgain();
        }


        public static void AskUnregisterAgain()
        {
            Console.WriteLine("\nDo you want to Unregister again? (y)/(n)");
            string answer = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(answer) && answer.Trim()[0].ToString().ToLower() == "y")
            {
                AskTypeForUnregistration();
            }
            else if (!string.IsNullOrEmpty(answer) && answer.Trim()[0].ToString().ToLower() == "n")
            {
                AskFunction();
            }
            else
            {
                ShowError("Error: Invalid Answer");
                AskUnregisterAgain();
            }
        }
        public static void AskTypeForUnregistration()
        {
            Console.WriteLine("\nWhat Registration do you want to Unregister?");
            Console.WriteLine("(V)oters  |  (S)chool  |  (D)ay Care");
            string answer = Console.ReadLine().Trim();
            if (answer.Length > 0)
            {
                switch (answer[0].ToString().ToLower())
                {
                    case "v":
                        AskPersonUnregister("Voters",Voters);
                        break;
                    case "s":
                        AskPersonUnregister("School", School);
                        break;
                    case "d":
                        AskPersonUnregister("DayCare", DayCare);
                        break;
                    default:
                        ShowError("Error: Invalid Answer");
                        break;
                }
                AskUnregisterAgain();
            }
            else
            {
                AskTypeForUnregistration();
            }
        }
        public static void AskPersonUnregister<T>(string registrationString, IRegistration<T> registration) where T : Person
        {
            Console.WriteLine("\nPlease enter ID you want to Unregister in " + registrationString + " registration");
            if (int.TryParse(Console.ReadLine().Trim(), out int answer) && answer != 0)
            {
                UnregisterPerson(answer, registration);
            }
            else
            {
                ShowError("Error: Invalid ID");
                AskRegisterAgain();
            }
        }
        public static void UnregisterPerson<T>(int Id, IRegistration<T> registration) where T : Person
        {
            //get person, validate then register
            var person = PersonRepo.GetSpecific(Id);
            try
            {
                if (person == null)
                {
                    throw new Exception("No Record Found");
                }
                registration.UnregisterPerson(person.ID);
                ShowMessage("Person Unregistered!");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }

            AskUnregisterAgain();
        }


        public static void AskShowAgain()
        {
            Console.WriteLine("\nDo you want to View Registrations again? (y)/(n)");
            string answer = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(answer) && answer.Trim()[0].ToString().ToLower() == "y")
            {
                AskTypeForShowing();
            }
            else if (!string.IsNullOrEmpty(answer) && answer.Trim()[0].ToString().ToLower() == "n")
            {
                AskFunction();
            }
            else
            {
                ShowError("Error: Invalid Answer");
                AskShowAgain();
            }
        }
        public static void AskTypeForShowing()
        {
            Console.WriteLine("\nWhat Registration do you want to view?");
            Console.WriteLine("(V)oters  |  (S)chool  |  (D)ay Care ");
            string answer = Console.ReadLine().Trim();
            if (answer.Length > 0)
            {
                switch (answer[0].ToString().ToLower())
                {
                    case "v":
                        AskGroupForShowining("Voters", Voters);
                        break;
                    case "s":
                        AskGroupForShowining("School", School);
                        break;
                    case "d":
                        AskGroupForShowining("DayCare", DayCare);
                        break;
                    default:
                        ShowError("Error: Invalid Answer");
                        break;
                }
                AskShowAgain();
            }
            else
            {
                AskTypeForShowing();
            }
        }
        public static void AskGroupForShowining<T>(string registrationString, IRegistration<T> registration) where T : Person
        {
            Console.WriteLine("\nGroup by?");
            Console.WriteLine("(C)ity |  (P)rovince  |  (R)egion");
            string answer = Console.ReadLine().Trim();
            if (answer.Length > 0)
            {
                switch (answer[0].ToString().ToLower())
                {
                    case "c":
                        ShowRegistration(registrationString, registration, "City");
                        break;
                    case "p":
                        ShowRegistration(registrationString, registration, "Province");
                        break;
                    case "r":
                        ShowRegistration(registrationString, registration, "Region");
                        break;
                    default:
                        ShowError("Error: Invalid Answer");
                        break;
                }
                AskShowAgain();
            }
            else
            {
                AskGroupForShowining(registrationString,registration);
            }
        }
        public static void ShowRegistration<T>(string registrationString, IRegistration<T> registration, string groupBy = "") 
                                where T : Person
        {
            var registeredPersons = registration.GetRegisteredPersons();
            
            ShowMessage("\n\t" + registrationString + " Registration Group By " + groupBy );

            if (registeredPersons.Count == 0)
            {
                ShowError("\n" +registrationString + " Registration is Empty");
                return;
            }

            var joinPersonsCities = registeredPersons.Cast<Person>().ToPersonView();

            var groupedPersons = joinPersonsCities.Group(groupBy);

            foreach (var groupedPerson in groupedPersons)
            {
                ShowMessage(groupedPerson.Key);
                DisplayPersonView(groupedPerson.ToList());
                Console.WriteLine();
            }
        }
        public static void DisplayPersonView(List<PersonView> persons)
        {
            ConsoleTable table = new ConsoleTable("ID", "First Name", "Last Name", "Age", "Gender",
                                                        "Status", "City", "Province", "Region");
            foreach (var person in persons)
            {
                table.AddRow(person.ID, person.FirstName, person.LastName, person.Age,
                             person.Gender, person.Status, person.City, person.Province, person.Region);
            }
            table.Write();
        }


        public static void AskSearchAgain()
        {
            Console.WriteLine("\nDo you want to Search again? (y)/(n)");
            string answer = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(answer) && answer.Trim()[0].ToString().ToLower() == "y")
            {
                AskTypeForSearching();
            }
            else if (!string.IsNullOrEmpty(answer) && answer.Trim()[0].ToString().ToLower() == "n")
            {
                AskFunction();
            }
            else
            {
                ShowError("Error: Invalid answer\n");
                AskSearchAgain();
            }
        }
        public static void AskTypeForSearching()
        {
            Console.WriteLine();
            Console.WriteLine("In what Registration do you want to Search?");
            Console.WriteLine("(V)oters  |  (S)chool  |  (D)ay Care");
            string answer = Console.ReadLine().Trim();
            if (answer.Length > 0)
            {
                switch (answer[0].ToString().ToLower())
                {
                    case "v":
                        SearchPersonRegistered(Voters, "Voters");
                        break;
                    case "s":
                        SearchPersonRegistered(School, "School");
                        break;
                    case "d":
                        SearchPersonRegistered(DayCare, "DayCare");
                        break;
                    default:
                        ShowError("Error: Function not found\n");
                        break;
                }
                AskSearchAgain();
            }
            else
            {
                AskTypeForSearching();
            }
        }
        public static void SearchPersonRegistered<T>(IRegistration<T> registration, string registrationString)
            where T : Person
        {
            Console.Write("\nSearch For: ");
            string searchKey = Console.ReadLine().Trim();


            Console.WriteLine("\n Search In:");
            Console.WriteLine("(E)veryWhere  |  (F)irst Name  |  (L)ast Name  |  (A)ge Range  |  (G)ender  \n  (S)tatus  |" +
                "  (C)ity  |  (P)rovince  |  (R)egion");

            string answer = Console.ReadLine().Trim();
            if (answer.Length > 0)
            {
                switch (answer[0].ToString().ToLower())
                {
                    case "e":
                        SearchPersonRegistered(registration, registrationString, searchKey, "");
                        break;
                    case "f":
                        SearchPersonRegistered(registration, registrationString, searchKey, "FirstName");
                        break;
                    case "l":
                        SearchPersonRegistered(registration, registrationString, searchKey, "LastName");
                        break;
                    case "a":
                        try
                        {
                            ValidateSearchKey(searchKey);
                            SearchPersonRegistered(registration, registrationString, searchKey, "Age Range");
                        }
                        catch (Exception ex)
                        {
                            ShowError(ex.Message);
                        }
                        break;
                    case "g":
                        SearchPersonRegistered(registration, registrationString, searchKey, "Gender");
                        break;
                    case "s":
                        SearchPersonRegistered(registration, registrationString, searchKey, "Status");
                        break;
                    case "c":
                        SearchPersonRegistered(registration, registrationString, searchKey, "City");
                        break;
                    case "p":
                        SearchPersonRegistered(registration, registrationString, searchKey, "Province");
                        break;
                    case "r":
                        SearchPersonRegistered(registration, registrationString, searchKey, "Region");
                        break;
                    default:
                        ShowError("Error: Function not found\n");
                        break;
                }
                AskSearchAgain();
            }
            else
            {
                SearchPersonRegistered(registration, registrationString);
            }
        }

        public static void ValidateSearchKey(string AgeRange)
        {
            if (!Regex.IsMatch(AgeRange, @"^\d+\-\d+$"))
            {
                throw new Exception("Search of Age Range doesn't match on specific format: N-N");
            }
            var split = AgeRange.Split('-');
            if (Convert.ToInt32(split[0]) > Convert.ToInt32(split[1]))
            {
                throw new Exception("Age Range left side is greater than right");
            }
        }

        public static void SearchPersonRegistered<T>(IRegistration<T> registration, 
            string registrationString, string key, string field) where T : Person
        {
            ShowMessage($"\nSearching for \"{key}\" in {field} at {registrationString} Registration");
            var personList = registration.SearchRegisteredPersons(key, field);
            DisplayPersonView(personList);
        }

        public static void ExportToXML()
        {
            ExportToXML(Voters, "Voters");
            ExportToXML(School, "School");
            ExportToXML(DayCare, "DayCare");
            ShowMessage("Export Success!");
            //Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory);
        }
        public static void ExportToXML<T>(IRegistration<T> registration, string registrationString) where T : Person
        {
            var registeredPersons = registration.GetRegisteredPersons();
            var joinPersonsCities = registeredPersons.Cast<Person>().ToPersonView();

            var documentNode = new XDocument();
            var personsNode = new XElement("Persons");
            foreach (var person in joinPersonsCities)
            {
                var personNode = new XElement("Person",
                        new XAttribute("Id", person.ID),
                        new XAttribute("FirstName", person.FirstName),
                        new XAttribute("LastName", person.LastName),
                        new XAttribute("PersonType", person.PersonType),
                        new XAttribute("DateOfBirth", person.DateOfBirth),
                        new XAttribute("Age", person.Age),
                        new XAttribute("Gender", person.Gender),
                        new XAttribute("Status", person.Status),
                        new XAttribute("City", person.City),
                        new XAttribute("Province", person.Province),
                        new XAttribute("Region", person.Region)

                    );
                personsNode.Add(personNode);
            }
            documentNode.Add(personsNode);
            documentNode.Save(registrationString + ".xml");
        }

        

        public static void ShowError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void ShowMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
