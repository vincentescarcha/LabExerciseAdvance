# LabExerciseAdvance 
Create a base class called Person with the following properties:
- Id (int)
- First Name (string)
- Last Name (string)
- Date of Birth (DateTime)
- Gender (enum Male / Female)
- Status (enum Single / Married)
- CityId (string)

---

Then create a class called Adult which inherits Person and has the following additional properties:
- Employed (bool)
- Job Title (string)

---

Create a class called Child which inherits Person and has the following additional properties:
- School (string)
- Level (string)

---

Create a class called Infant which inherits Person and has the following additional properties:
- Favorite Food (string)
- Favorite Milk (string)

---

Create an interface class called IRegistration<T> with the following:
Properties:
- List<T> RegisteredPersons

Methods:
- RegisterPerson(T Person)
- UnregisterPerson(int personId)
- GetRegisteredPersons
- SearchRegisteredPersons(First Name, Last Name, Age Range, Gender, Status, City, Province, Region)
- IsPersonRegistered(T Person)
- IsPersonValid(T Person)

---

Create a class called VotersRegistration<T> that will implement interface class IRegistration
- Only Adults can be registered

---

Create a class called SchoolRegistration<T> that will implement interface class IRegistration
- Only Children can be registered

---

Create a class called DayCareRegistration<T> that will implement interface class IRegistration
- Only Infants can be registered

---

Create a class called City with the following properties:
- Id (int)
- Name (string)
- Province (string)
- Region (string)

---

Create a console program that will allow the user to do the following:
- Load list of persons (via CSV file)
- Select type of registration: Voters Registration, School Registration, Day Care Registration
	- Then once a registration type is selected, have the following options:
		- Select a Person (from the loaded CSV file)
		- Add Person
		- Unregister Person
		- Display list of Registered Persons (By City, By Province, By Region)
		- Search Registered Person
		- Generate file containing list of persons (via XML file). Make the filename unique per registration type.
- Make sure you cannot register the same person and will throw a validation message
- Make sure to implement the correct class with registration type class (e.g. Adult class for VotersRegistration<Adult>)
- Make sure to implement a validation message if the criteria is not met (e.g. Adult with age 16 cannot be registered in Voters Registration)
- Make sure to use LINQ queries
- Make sure to apply the correct concepts from the programming paradigms course

---

Data for Persons.csv

John|Doe|19800101|Male|Married|Las Pinas|Store Manager<br>
Jane|Doe|19800202|Female|Married|Las Pinas|Housewife<br>
James|Doe|20100303|Male|Single|Las Pinas|St. James School|Grade 1<br>
June|Doe|20190404|Male|Single|Las Pinas|Cerelac|Nido<br>
Jake|Smith|19800505|Male|Married|Binan|Househusband<br>
Jill|Smith|19800606|Female|Married|Binan|Marketing Manager<br>
Jet|Smith|20100707|Male|Single|Binan|St. John School|Grade 2<br>
Jacklyn|Smith|20100808|Male|Single|Binan|St. Peter School|Grade 3<br>
Jack|Johnson|19800808|Male|Married|Bacoor|Bank Manager<br>
Jennifer|Johnson|19801010|Female|Married|Bacoor|Store Clerk II<br>

---

Data for Cities.csv

Las Pinas|None|NCR<br>
Makati|None|NCR<br>
Quezon City|None|NCR<br>
Binan|Laguna|Region IV<br>
Santa Rosa|Laguna|Region IV<br>
Bacoor|Cavite|Region IV<br>
Tagaytay|Cavite|Region IV<br>
San Fernando|La Union|Region I<br>
Bauang|La Union|Region I<br>

---

Format for xml file:

< Persons >
< Person Id="" FirstName="" LastName="" PersonType="" DateOfBirth="" Age="" Gender="" Status="" City="" Province="" Region="" />
</ Persons >

