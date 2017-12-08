using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSProject
{
    class Staff
    {
        private float hourlyRate;
        private int hWorked;

        public float TotalPay { get; protected set; }
        public float BasicPay { get; private set; }
        public string NameOfStaff { get; private set; }
        public int HoursWorked
        {
            get
            {
                return hWorked;
            }
            set
            {
                if (value < 0)
                {
                    hWorked = 0;
                } else
                {
                    hWorked = value;
                }
            }
        }

        public virtual void CalculatePay()
        {
            Console.WriteLine("Calculating pay...");

            BasicPay = hWorked * hourlyRate;
            TotalPay = BasicPay;
        }

        public Staff(string name, float rate)
        {
            NameOfStaff = name;
            hourlyRate = rate;
        }
        public override string ToString()
        {
            return "Name: " + NameOfStaff + ";\nType: " + "Staff" + ";\nHourly Rate: " + hourlyRate + ";\nHours Worked: " + HoursWorked + ";\nBase Pay: $" + BasicPay + "\nTotal Pay: $" + TotalPay + ";";
        }
    }

    class Manager : Staff
    {
        private const float managerHourlyRate = 50;

        public int Allowance { get; private set; }

        public Manager(string name) : base(name, managerHourlyRate) { }

        public override void CalculatePay()
        {
            base.CalculatePay();
            if (HoursWorked > 80)
            {
                Allowance = 1000;
                TotalPay = BasicPay + Allowance;
            }
        }

        public override string ToString()
        {
            return "Name: " + NameOfStaff + ";\nType: " + "Manager" + ";\nHourly Rate: " + managerHourlyRate + ";\nHours Worked: " + HoursWorked + ";\nBase Pay: $" + BasicPay + ";\nAllowance: $" + Allowance + ";\nTotal Pay: $" + TotalPay + ";";
        }

    }

    class Admin : Staff
    {
        private const float overtimeRate = 15.5F;
        private const float adminHourlyRate = 30;

        public float Overtime { get; private set; }

        public Admin(string name) : base(name, adminHourlyRate) { }

        public override void CalculatePay()
        {
            base.CalculatePay();
            if (HoursWorked > 160)
            {
                Overtime = overtimeRate * (160 - HoursWorked);
                TotalPay += Overtime;
            }
        }

        public override string ToString()
        {
            return "Name: " + NameOfStaff + ";\nType: " + "Admin" + ";\nHourly Rate: " + adminHourlyRate + ";\nHours Worked: " + HoursWorked + ";\nOvertime Rate: " + overtimeRate + ";\nBase Pay: $" + BasicPay +  ";\nTotal Pay: $" + TotalPay + ";";
        }

    }

    class FileReader
    {
        public List<Staff> ReadFile()
        {
            List<Staff> myStaff = new List<Staff>();
            string[] result = new string[2];
            string path = "staff.txt";
            string[] seperator = { ", " };
            if (File.Exists(path))
            {
                Console.WriteLine("Reading Contents of '{0}'...", path);
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.EndOfStream != true)
                    {
                        result = sr.ReadLine().Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                        switch (result[1])
                        {
                            case "Manager":
                                myStaff.Add(new Manager(result[0]));    // I had to look up the fact that I needed to add 'new'
                                break;
                            case "Admin":
                                myStaff.Add(new Admin(result[0]));      // I had to look up the fact that I needed to add 'new'
                                break;
                            default:
                                Console.WriteLine("Error! Incorrect Staff Type.");
                                break;

                        }
                    }
                    sr.Close();
                }
            } else
            {
                Console.WriteLine("Error! Could not find '{0}' in current working directory", path);
            }
            return myStaff;
        }
    }
    class PaySlip
    {
        private int month;
        private int year;

        enum MonthsOfYear
        {
            JAN = 1, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC
        }

        public PaySlip(int payMonth, int payYear)
        {
            month = payMonth;
            year = payYear;
        }

        public void GeneratePaySlip(List<Staff> myStaff)
        {
            string breakline = "==========================";
            string path;
            
            foreach (Staff f in myStaff)
            {
                path = f.NameOfStaff + ".txt";

                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine("PAYSLIP FOR {0} {1}", (MonthsOfYear) month, year);
                    sw.WriteLine(breakline);

                    sw.WriteLine("Name of Staff: {0}", f.NameOfStaff);

                    sw.WriteLine("Hours Worked: {0}\n", f.HoursWorked);

                    sw.Write("Basic Pay: {0:C}", f.BasicPay);

                    if (f.GetType() == typeof(Manager))
                    {
                        sw.WriteLine("Allowance: {0:C}", ((Manager)f).Allowance);
                    } else if (f.GetType() == typeof(Admin))
                    {
                        sw.WriteLine("Overtime Pay: {0:C}", ((Admin)f).Overtime);
                    }

                    sw.WriteLine("\n{0}\nTotal Pay: {1:C}\n{2}", breakline, f.TotalPay, breakline);
                    sw.Close();
                }
            }
        }

        public void GenerateSummary(List<Staff> myStaff)
        {
            string path = "summary.txt";

            var result =
                from staff in myStaff
                where (staff.HoursWorked) < 10
                orderby staff.NameOfStaff ascending
                select new { staff.NameOfStaff, staff.HoursWorked };

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Staff with less than 10 Working Hours");
                foreach (var staff in result)
                {
                    sw.WriteLine("Name of Staff: {0}, Hours Worked: {1}", staff.NameOfStaff, staff.HoursWorked );
                }
                sw.Close();
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Staff> myStaff = new List<Staff>();
            FileReader fr = new FileReader();
            int month = 0;
            int year = 0;

            while (year == 0)
            {
                Console.Write("\nPlease enter the year: ");
                try
                {
                    year = Convert.ToInt32(Console.ReadLine());
                } catch (FormatException)
                {
                    Console.WriteLine("Sorry, that number sucks...");
                }
            }

            while (month == 0)
            {
                Console.Write("\nPlease enter the month: ");
                try
                {
                    month = Convert.ToInt32(Console.ReadLine());
                    if (month < 1 || month > 12)
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Sorry, that number sucks...");
                    month = 0;
                }
            }

            myStaff = fr.ReadFile();

            for (int i = 0; i < myStaff.Count; i++)
            {
                try
                {
                    Console.Write("\nPlease enter the number of Hours Worked by {0}: ", myStaff[i].NameOfStaff);
                    myStaff[i].HoursWorked = Convert.ToInt32(Console.ReadLine());
                    myStaff[i].CalculatePay();
                    Console.WriteLine(myStaff[i].ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("EXCEPTION: {0}", e);
                    i--;
                }
            }

            PaySlip ps = new PaySlip(month, year);
            Console.WriteLine("Generating Summary...");
            ps.GenerateSummary(myStaff);
            Console.WriteLine("======DONE======");
            Console.Read();

        }
    }
}
