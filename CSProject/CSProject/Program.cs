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

        public float TotalPay { get; private set; }
        public float BasicPay { get; private set; }
        public string NameOfStaff { get; private set; }

        void CalculatePay()
        {

        }
    }

    class Manager : Staff
    {

    }

    class Admin : Staff
    {

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
