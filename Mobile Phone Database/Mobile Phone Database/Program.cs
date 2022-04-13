using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;

namespace Mobile_Phone_Database
{
    internal class Program
    {
        public static List<Phone> GetPhonesFromDatabase(DbCommand cmd)
        {
            List<Phone> results = new List<Phone>();
            cmd.CommandText = "select * from Phones";
            Phone phone;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    phone = new Phone(Convert.ToInt32(reader["PhoneId"]), Convert.ToString(reader["Name"]), Convert.ToString(reader["Brand"]), Convert.ToString(reader["Operating System"]), Convert.ToSingle(reader["Price"]));
                    results.Add(phone);
                }
            }
            return results;
        }

        public static int GetNextPhoneId(List<Phone> phones)
        {
            int maxId = 0;
            foreach (Phone phone in phones)
            {
                if(phone.PhoneId > maxId)
                {
                    maxId = phone.PhoneId;
                }
            }
            return maxId + 1;
        }

        static void Main(string[] args)
        {
            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            List<Phone> phones;

            // create the connection between this application and the database
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
            using (DbConnection conn = factory.CreateConnection())
            {
                if (conn == null)
                {
                    Console.WriteLine("Could not connect to the database");
                    Console.ReadLine();
                    return;
                }
                conn.ConnectionString = connectionString;
                conn.Open();

                // now ask conn to create a mouse piece
                DbCommand cmd = conn.CreateCommand();

                phones = GetPhonesFromDatabase(cmd);

                Console.WriteLine("Welcome to the phone database!");
                Console.WriteLine("Here are all the phones currently in the database: ");
                PhoneWriter.WritePhonesToScreen(phones);

                int selection = 0;
                while (selection != 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Choose what would you like to do in the database (Enter the option number): ");
                    Console.WriteLine("1. Insert a new phone");
                    Console.WriteLine("2. Delete a phone from the database");
                    Console.WriteLine("3. Quit");

                    selection = Convert.ToInt32(Console.ReadLine());

                    if (selection == 1)
                    {
                        // insert a new record. Ask the user for new phone and then insert it into the table
                        Console.WriteLine();
                        Console.Write("Enter the name of a phone: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter the phone brand: ");
                        string brand = Console.ReadLine();
                        Console.Write("Enter the phone's operating system: ");
                        string os = Console.ReadLine();
                        Console.Write("Enter its price: ");
                        double price = Convert.ToDouble(Console.ReadLine());

                        int newId = GetNextPhoneId(phones);

                        string query = String.Format("insert into Phones values ({0}, '{1}', '{2}', '{3}', {4:C})", newId, name, brand, os, price);
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();

                        phones = GetPhonesFromDatabase(cmd);
                        Console.WriteLine("Here are the results after adding a new phone:");
                        PhoneWriter.WritePhonesToScreen(phones);
                    } else if(selection == 2)
                    {
                        Console.WriteLine();
                        Console.Write("Enter the phone id to remove: ");
                        int deleteThis = Convert.ToInt32(Console.ReadLine());
                        cmd.CommandText = String.Format("delete from Phones where PhoneId='{0}'", deleteThis);
                        cmd.ExecuteNonQuery();
                        phones = GetPhonesFromDatabase(cmd);
                        Console.WriteLine("Here are the results after deleting that phone:");
                        PhoneWriter.WritePhonesToScreen(phones);
                    }
                    
                }
                Console.WriteLine();
                Console.WriteLine("Thank you from using our application!");
                
            }
        }
    }
}
