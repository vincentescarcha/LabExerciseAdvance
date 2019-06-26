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

namespace LabExerciseAdvance
{
    public class Program
    {
        public static PersonRepository PersonRepo = new PersonRepository();
        public static CityRepository CityRepo = new CityRepository();
        public static VotersRegistration<Person> Voters = new VotersRegistration<Person>();
        public static SchoolRegistration<Person> School = new SchoolRegistration<Person>();
        public static DayCareRegistration<Person> DayCare = new DayCareRegistration<Person>();
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

            DisplayTable(persons);
        }


        public static void DisplayTable(List<Person> persons)
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
            Console.WriteLine("(R)egister  |  (U)nregister  |  (S)how Registered |  Searc(h) |  (E)xport");
            string answer = Console.ReadLine().Trim();
            if (answer.Length > 0)
            {
                switch (answer[0].ToString().ToLower())
                {
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
                        Voters.RegisterPerson(PersonRepo.GetSpecific(1));
                        Voters.RegisterPerson(PersonRepo.GetSpecific(2));
                        School.RegisterPerson(PersonRepo.GetSpecific(3));
                        DayCare.RegisterPerson(PersonRepo.GetSpecific(4));
                        Voters.RegisterPerson(PersonRepo.GetSpecific(5));
                        Voters.RegisterPerson(PersonRepo.GetSpecific(6));
                        School.RegisterPerson(PersonRepo.GetSpecific(7));
                        School.RegisterPerson(PersonRepo.GetSpecific(8));
                        Voters.RegisterPerson(PersonRepo.GetSpecific(9));
                        Voters.RegisterPerson(PersonRepo.GetSpecific(10));
                        break;
                    default:
                        ShowError("Error: Invalid Answer");
                        break;
                }
            }
            AskFunction();
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
        public static void AskPersonRegister(string registrationString, IRegistration<Person> registration)
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
        public static void RegisterPerson(int Id, IRegistration<Person> registration)
        {
            //get person, validate then register
            var person = PersonRepo.GetSpecific(Id);
            try
            {
                if (person == null)
                {
                    throw new Exception("No Record Found");
                }
                registration.RegisterPerson(person);
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
        public static void AskPersonUnregister(string registrationString, IRegistration<Person> registration)
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
        public static void UnregisterPerson(int Id, IRegistration<Person> registration)
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
        public static void AskGroupForShowining(string registrationString, IRegistration<Person> registration)
        {
            Console.WriteLine("\nGroup by?");
            Console.WriteLine("(C)ity |  (P)rovince  |  (R)egion");
            string answer = Console.ReadLine().Trim();
            if (answer.Length > 0)
            {
                switch (answer[0].ToString().ToLower())
                {
                    case "c":
                        ShowRegistration(registrationString, registration, "CityName");
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
        public static void ShowRegistration(string registrationString, IRegistration<Person> registration, string groupBy = "")
        {
            var registeredPersons = registration.GetRegisteredPersons();
            var cities = CityRepo.GetList;
            
            ShowMessage("\n\t" + registrationString + " Registration Group By " + groupBy );

            if (registeredPersons.Count == 0)
            {
                ShowError("\n" +registrationString + " Registration is Empty");
                return;
            }

            var joinPersonsCities = registeredPersons.Join(cities, p => p.CityId, c => c.ID,
                                            (p, c) => new PersonView {
                                                ID =p.ID,
                                                FirstName = p.FirstName,
                                                LastName = p.LastName,
                                                DateOfBirth = p.DateOfBirth,
                                                Gender = p.Gender,
                                                Status = p.Status,
                                                CityName = c.Name,
                                                Province = c.Province,
                                                Region = c.Region
                                            });

            var propertyInfo = typeof(PersonView).GetProperty(groupBy);

            var groupedPersons = joinPersonsCities.GroupBy(x => propertyInfo.GetValue(x, null).ToString(),
                                        StringComparer.InvariantCultureIgnoreCase);
            foreach (var groupedPerson in groupedPersons)
            {
                ShowMessage(groupedPerson.Key);
                DisplayGroupedTable(groupedPerson.ToList());
                Console.WriteLine();
            }
        }
        public static void DisplayGroupedTable(List<PersonView> persons)
        {
            ConsoleTable table = new ConsoleTable("ID", "First Name", "Last Name", "Date of Birth", "Gender",
                                                        "Status", "City", "Province", "Region");
            foreach (var person in persons)
            {
                table.AddRow(person.ID, person.FirstName, person.LastName, person.DateOfBirth.ToString("MMM dd, yyyy"),
                             person.Gender, person.Status, person.CityName, person.Province, person.Region);
            }
            table.Write();
        }


        public static void AskSearchAgain()
        {
            Console.WriteLine("Do you want to Search again? (y)/(n)");
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
        public static void SearchPersonRegistered(IRegistration<Person> registration, string registrationString)
        {
            Console.Write("\nSearch For: ");
            string searchKey = Console.ReadLine().Trim();

            List<Person> personList = registration.SearchRegisteredPersons(searchKey);

            Console.WriteLine();
            Console.WriteLine($"Searching for \"{searchKey}\" in "+ registrationString);

            DisplayTable(personList);

            Console.WriteLine();
        }


        public static void ExportToXML()
        {
            ExportToXML(Voters, "Voters");
            ExportToXML(School, "School");
            ExportToXML(DayCare, "DayCare");
            ShowMessage("Export Success!");
            //Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory);
        }
        public static void ExportToXML(IRegistration<Person> registration, string registrationString)
        {
            var registeredPersons = registration.GetRegisteredPersons();
            var cities = CityRepo.GetList;
            var joinPersonsCities = registeredPersons.Join(cities, p => p.CityId, c => c.ID,
                                            (p, c) => new PersonView
                                            {
                                                ID = p.ID,
                                                FirstName = p.FirstName,
                                                LastName = p.LastName,
                                                DateOfBirth = p.DateOfBirth,
                                                Age = p.Age,
                                                Gender = p.Gender,
                                                Status = p.Status,
                                                PersonType = p.GetType().Name,
                                                CityName = c.Name,
                                                Province = c.Province,
                                                Region = c.Region
                                            });

            var documentNode = new XDocument();
            var personsNode = new XElement("Persons");
            foreach (var person in joinPersonsCities)
            {
                var personNode = new XElement("Person",
                        new XAttribute("Id", person.ID),
                        new XAttribute("FirstName", person.FirstName),
                        new XAttribute("LastName", person.LastName),
                        new XAttribute("PersonType", person.PersonType),
                        new XAttribute("DateOfBirth", person.DateOfBirth.ToString("MMM dd, yyyy")),
                        new XAttribute("Age", person.Age),
                        new XAttribute("Gender", person.Gender),
                        new XAttribute("Status", person.Status),
                        new XAttribute("City", person.CityName),
                        new XAttribute("Province", person.Province),
                        new XAttribute("Region", person.Region)

                    );
                personsNode.Add(personNode);
            }
            documentNode.Add(personsNode);
            documentNode.Save(registrationString+".xml");
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
