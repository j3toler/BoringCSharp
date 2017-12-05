using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSProject
{
    class Staff
    {
        private float hourlyRate
        {
            get
            {
                return hourlyRate;
            }
            set
            {
                if (value < 0)
                {
                    hourlyRate = 0;
                } else
                {
                    hourlyRate = value;
                }
            }
        }
        private int hWorked
        {
            get
            {
                return hWorked;
            }
            set
            {
                if ( hWorked < 0 )
            {
                hWorked = 0;
            } else
            {
                hWorked = value;
            }
            }
        }

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
            return "Name: " + NameOfStaff + ";\nType: " + "Staff" + ";\nHourly Rate: " + hourlyRate + ";\nHours Worked: " + HoursWorked + ";\nBase Pay: " + BasicPay + "\nTotal Pay: " + TotalPay + ";";
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
            return "Name: " + NameOfStaff + ";\nType: " + "Manager" + ";\nHourly Rate: " + managerHourlyRate + ";\nHours Worked: " + HoursWorked + ";\nBase Pay: " + BasicPay + ";\nAllowance: " + Allowance + ";\nTotal Pay: " + TotalPay + ";";
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
            return "Name: " + NameOfStaff + ";\nType: " + "Admin" + ";\nHourly Rate: " + adminHourlyRate + ";\nHours Worked: " + HoursWorked + ";\nOvertime Rate:" + overtimeRate + ";\nBase Pay: " + BasicPay +  ";\nTotal Pay: " + TotalPay + ";";
        }

    }

    class FileReader
    {

    }
    class PaySlip
    {

    }


    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
