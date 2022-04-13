using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// View Class
namespace Mobile_Phone_Database
{
    internal class PhoneWriter
    {
        public static void WritePhonesToScreen(List<Phone> phones)
        {
            foreach(Phone phone in phones)
            {
                Console.WriteLine(phone);
            }
        }
    }
}
