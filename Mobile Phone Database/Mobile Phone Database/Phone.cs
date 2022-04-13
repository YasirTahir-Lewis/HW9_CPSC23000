using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Model Class
namespace Mobile_Phone_Database
{
    internal class Phone
    {
        public int PhoneId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string OperatingSystem { get; set; }
        public float Price { get; set; }

        public Phone()
        {
            PhoneId = 0;
            Name = "A phone";
            Brand = "A brand";
            OperatingSystem = "An operating system";
            Price = 0;
        }

        public Phone(int phoneId, string name, string brand, string operatingSystem, float price)
        {
            PhoneId = phoneId;
            Name = name;
            Brand = brand;
            OperatingSystem = operatingSystem;
            Price = price;
        }

        public override string ToString()
        {
            return String.Format("PhoneId={0}, Name={1}, Brand={2}, Operating System={3}, Price={4}", PhoneId, Name, Brand, OperatingSystem, Price);
        }

    }
}
