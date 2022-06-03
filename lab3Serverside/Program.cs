using System.Collections;
namespace Lab3
{
    class Program
    {
        public static ArrayList people = new ArrayList();
        static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                DisplayMenu();
                Console.WriteLine("Enter choice: ");
                choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                MenuOption(choice);
            } while (choice != 0);

            //Method to display Menu
            static void DisplayMenu()
            {
                Console.WriteLine(" ----- Menu ----- ");
                Console.WriteLine("1. Create a Person");
                Console.WriteLine("2. Select a Person");
                Console.WriteLine("3. Remove a Person");
                Console.WriteLine("4. Display Random Last Name");
                Console.WriteLine("5. Display Random SSN");
                Console.WriteLine("6. Display Random Phone Number");
                Console.WriteLine("0. Exit");
                Console.WriteLine(" ---------------- ");
            }

            // Method to choose from menu
            static void MenuOption(int option)
            {
                switch (option)
                {
                    case 1:
                        CreatePerson();
                        break;
                    case 2:
                        ViewAll();
                        break;
                    case 3:
                        Remove();
                        break;
                    case 4:
                        RandomL();
                        break;
                    case 5:
                        RandomSSN();
                        break;
                    case 6:
                        RandomPhone();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Invalid Menu choice. Try Again");
                        break;
                }
            }
        }
        public static void CreatePerson()
        {
            Console.WriteLine("How Many People: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < choice; i++)
            {
                people.Add(new Person());
            }

            Console.WriteLine($"\nAdded {choice} People");
        }

        public static void ViewAll()
        {
            if (people.Count == 0)
            {
                people.Add(new Person());
                Console.WriteLine(people[0]);
            }
            else
            {
                for (int i = 0; i < people.Count; i++)
                {
                    Console.WriteLine(people[i]);
                }
            }

        }

        public static void Remove()
        {
            if (people.Count == 0)
            {
                Console.WriteLine("Currently no people to remove.");
            }
            else
            {
                for (int i = 0; i < people.Count; i++)
                {
                    Console.WriteLine("Index " + i + ": \n" + people[i]);
                }

                Console.WriteLine("Which Person would you like to remove?");
                int choice = Convert.ToInt32(Console.ReadLine());

                if (people[choice] == null)
                {
                    Console.WriteLine("Nobody's There...");
                }
                else if (people[choice] != null)
                {
                    people.Remove(people[choice]);
                    Console.WriteLine($"Got rid of Person {choice}");
                }

            }
        }
        public static void RandomL()
        {
            Person randomL = new Person();
            Console.WriteLine(randomL.LastName);
        }

        public static void RandomSSN()
        {
            Person randomSSN = new Person();
            Console.WriteLine(randomSSN.SSN);
        }

        public static void RandomPhone()
        {
            Person randomPhone = new Person();
            Console.WriteLine(randomPhone.PhoneNumber);
        }
    }
    class Person
    {
        public static string[] _arrayOfFirstNames = new string[] { "John", "Colby", "Joe", "Kate", "Emilee", "Melissa", "Mark", "Brain", "Will", "Cole" };
        public Dependent[] _dependents;

        public string FirstName { get; init; }
        //Init prevents the code from being changed once it is initialized and does not allow code outside of the initilizer to change the code
        public string LastName { get; init; }
        public DateTime BirthDate { get; init; }
        public SSN SSN { get; init; }
        public PhoneNumber PhoneNumber { get; init; }

        public string SetFirstName()
        {
            Random rnd = new Random();
            int index = rnd.Next(_arrayOfFirstNames.Length);
            return _arrayOfFirstNames[index];
        }

        public string SetLastName()
        {
            Random rnd = new Random();
            Array lastNames = Enum.GetValues(typeof(LastName));
            var last = (LastName?)lastNames.GetValue(rnd.Next(lastNames.Length));
            return last.ToString();
        }
        public DateTime SetBirhtDate()
        {
            Random rnd = new Random();
            DateTime old = new DateTime(2040, 6, 1);
            DateTime young = new DateTime(2102, 6, 1);
            int range = (young - old).Days;
            return old.AddDays(rnd.Next(range));
        }

        public Person()
        {
            FirstName = SetFirstName();
            
            LastName = SetLastName();

            BirthDate = SetBirhtDate();

            this.SSN = new SSN();

            this.PhoneNumber = new PhoneNumber();

            _dependents = new Dependent[10];
        }

        public int Age()
        {
            var today = DateTime.Today;

            var age = today.Year - BirthDate.Year;

            return age;

        }

        public void AddDependent()
        {
            if (_dependents[0] == null)
            {
                _dependents[0] = new Dependent();
            }
            while (_dependents[0] != null)
            {
                for (int i = 1; i < _dependents.Length; i++)
                {
                    if (_dependents[i] == null)
                    {
                        _dependents[i] = new Dependent();
                        return;
                    }
                }
            }
        }
    }

    class Dependent : Person
    {
        public Dependent() : base()
        {
            Random rnd = new Random();
            BirthDate = DateTime.Now.AddYears(rnd.Next(11));
        }

    }
    class SSN
    {
        //Valid SSN Research:
        //https://primepay.com/blog/how-determine-valid-social-security-number#:~:text=An%20invalid%20SSN%20is%20one,four%20digits%20as%20%E2%80%9C0000.%E2%80%9D
        public string Number
        {
            get; init;
        }

        public SSN()
        {
            Random rnd = new Random();
            int I900 = rnd.Next(900, 1000);
            string Over900 = Convert.ToString(I900);

            string[] invalid1group = { "000", "666", Over900 };
            string invalid2 = "00";
            string invalid3 = "0000";

            int index = rnd.Next(0, invalid1group.Length);
            string invalid1 = invalid1group[index];

            string[] invalidSSN = { invalid1, invalid2, invalid3 };
            int Index = rnd.Next(0, invalidSSN.Length);

            var SSNFull = new System.Text.StringBuilder();

            switch (Index)
            {
                case 0:
                    SSNFull.Append(invalid1);
                    for (int i = 0; i < 6; i++)
                    {
                        SSNFull.Append(rnd.Next(0, 10));
                    }
                    break;
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        SSNFull.Append(rnd.Next(0, 10));
                    }
                    SSNFull.Append(invalid2);
                    for (int i = 0; i < 4; i++)
                    {
                        SSNFull.Append(rnd.Next(0, 10));
                    }
                    break;
                case 2:
                    for (int i = 0; i < 5; i++)
                    {
                        SSNFull.Append(rnd.Next(0, 10));
                    }
                    SSNFull.Append(invalid3);
                    break;
                default:
                    Console.WriteLine("No Index???");
                    break;
            }

            Number = SSNFull.ToString();
        }

        public override string ToString()
        {
            string fixedSSN = Number;
            fixedSSN = fixedSSN.Insert(3, "-");
            fixedSSN = fixedSSN.Insert(6, "-");
            return fixedSSN;
        }

    }

    class PhoneNumber
    {
        public PhoneNumber()
        {
            Random rnd = new Random();
            long number = rnd.NextInt64(2000000000, 9999999999);

            string phone = number.ToString();
            string area = phone.Substring(0, 3);
            string major = phone.Substring(3, 3);
            string minor = phone.Substring(6);
            string formatted = string.Format("{0}-{1}-{2}", area, major, minor);
        }
    }
    enum LastName
    {
        Kledis,
        Martin,
        Harper,
        Wrenn,
        Kisler
    }
}