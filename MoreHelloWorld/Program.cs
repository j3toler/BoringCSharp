using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreHelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            int exitCode = 0;
            List<Staff> StaffList = new List<Staff>();
            do
            {
                Console.WriteLine("\n\nThank you for choosing this program.\nPlease choose and option...");
                Console.WriteLine("\t1. Create a new member\n\t2. View all Staff\n\t3. Delete a Staff member\n\t4. Edit a Staff Member\n\t5. Exit...");
                Console.Write("Choice: ");

                int userChoice = 0;
                string name;
                float hours;
                int indexChoice;
                int editChoice;
                float newFloatValue;

                try
                {
                    userChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    userChoice = 0;
                    exitCode = 1;
                }
                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("You have chosen to Create a new Member...");
                        Console.Write("Please enter a name: ");
                        name = Console.ReadLine();
                        Console.Write("Please enter hours worked: ");
                        try
                        {
                            hours = Convert.ToSingle(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Exiting...");
                            Console.Read();
                            exitCode = 1;
                            break;
                        }
                        Console.WriteLine("You have chosen the name \"{0}\", with a total of {1:F2} hours worked", name, hours);
                        Staff newStaff = new Staff(name);
                        newStaff.HoursWorked = hours;
                        StaffList.Add(newStaff);
                        break;
                    case 2:
                        Console.WriteLine("You have chosen to View All Staff...");
                        if (StaffList.Count > 0)
                        {
                            ShowAllStaff(StaffList);
                        }
                        else
                        {
                            Console.WriteLine("There are 0 Staff...");
                        }
                        break;
                    case 3:
                        Console.WriteLine("You have chosen to Delete a Staff Member...");
                        if (StaffList.Count == 0)
                        {
                            Console.WriteLine("There are 0 Staff so far...");
                            break;
                        }
                        indexChoice = MakeChoiceForStaff();
                        if (indexChoice == -1)
                        {
                            exitCode = 1;
                            break;
                        } else
                        {
                            Console.WriteLine("You have chosen {0}.", StaffList[indexChoice].Name);
                            StaffList.RemoveAt(indexChoice);
                        }
                        break;
                    case 4:
                        Console.WriteLine("You have chosen to Edit a Staff Member...");
                        if (StaffList.Count == 0)
                        {
                            Console.WriteLine("There are 0 Staff so far...");
                            break;
                        }
                        indexChoice = MakeChoiceForStaff();
                        if (indexChoice == -1)
                        {
                            exitCode = 1;
                            break;
                        }
                        Console.WriteLine("You have chosen {0}.", StaffList[indexChoice].Name);
                        Console.WriteLine("What would you like to change?");
                        Console.WriteLine("\t1. Hours Worked\n\t2. Hourly Rate\n\t3. Nothing...");
                        Console.Write("Choice: ");
                        editChoice = Convert.ToInt32(Console.ReadLine());
                        switch (editChoice)
                        {
                            case 1:
                                Console.Write("You have chosen to edit {0}'s hours.\nPlease Enter a new number: ", StaffList[indexChoice].Name);
                                newFloatValue = Convert.ToSingle(Console.ReadLine());
                                StaffList[indexChoice].HoursWorked = newFloatValue;
                                break;
                            case 2:
                                Console.WriteLine("You have chosen to edit {0}'s pay rate", StaffList[indexChoice].Name);
                                break;
                            default:
                                Console.WriteLine("You have chosen to do nothing...");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Exiting...");
                        Console.Read();
                        exitCode = 1;
                        break;
                }
            } while (exitCode == 0);

            void ShowAllStaff(List<Staff> staffList)
            {
                Console.WriteLine("There are {0} Staff:", staffList.Count);
                for (int i = 0; i < StaffList.Count; i++)
                {
                    Console.WriteLine("{0}. {1}\t{2}F:2 Hrs at {3}:F2/Hr.", i+1, StaffList[i].Name, StaffList[i].HoursWorked, StaffList[i].Rate);
                }
            }

            int MakeChoiceForStaff()
            {
                string showListVal;
                bool showList;
                int indexChoice;

                Console.Write("Do you need to see the current list? (y,n) ");
                showListVal = Console.ReadLine();
                showList = String.Equals(showListVal, "y", StringComparison.OrdinalIgnoreCase);
                if (showList)
                {
                    ShowAllStaff(StaffList);
                }
                Console.Write("Please select an index: ");
                try
                {
                    indexChoice = Convert.ToInt32(Console.ReadLine());
                    indexChoice--;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                } if (indexChoice < StaffList.Count && indexChoice >= 0)
                {
                    return indexChoice;
                } else
                {
                    return -1;
                }
            }

        }
    }

    class Staff
    {
        private string NameOfStaff;
        private float hWorked;
        private const float hourlyRate = 30;

        public Staff (string name)
        {
            NameOfStaff = name;
            Console.WriteLine("\n{0} Created\n--------------------------", NameOfStaff);
        }

        public Staff(string firstName, string lastName)
        {
            NameOfStaff = firstName + " " + lastName;
            Console.WriteLine("\n{0} Created\n--------------------------", NameOfStaff);
        }

        public string Name
        {
            get
            {
                return NameOfStaff;
            }
            set
            {
                if (value is string)
                {
                    NameOfStaff = value;
                } else
                {
                    Console.WriteLine("Something went wrong...");
                }
            }
        }

        public float HoursWorked
        {
            get
            {
                return hWorked;
            }
            set
            {
                if (value > 0)
                {
                    hWorked = value;
                }
                else
                {
                    hWorked = 0;
                }
            }
        }

        public float Rate
        {
            get
            {
                return hourlyRate;
            }
            set
            {
                if (value > 0)
                {
                    hourlyRate = value;
                }
                else
                {
                    hWorked = 0;
                }
            }
        }

        public void PrintMessage()
        {
            Console.WriteLine("Calculating Pay...");
        }

        public float CalculatePay()
        {
            PrintMessage();
            float StaffPay;
            StaffPay = hWorked * hourlyRate;
            if (hWorked > 0)
            {
                return StaffPay;
            } else
            {
                return 0;
            }
        }

        public float CalculatePay(float bonus, float allowance)
        {
            PrintMessage();
            float StaffPay;
            StaffPay = hWorked * hourlyRate + bonus + allowance;
            if (hWorked > 0)
            {
                return StaffPay;
            }
            else
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return "Name of Staff = " + NameOfStaff + ", Hourly Rate = " + hourlyRate + ", Hours Worked = " + hWorked;
        }
    }
}
