using PharmCompany.DataControllers;
using System;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace PharmCompany
{
    internal class Program
    {
        private static void Main()
        {           
            Console.WriteLine("1-Товары 2-Аптеки 3-Склады 4-Партии\n5-Количество товара во всех складах аптеки\n6-ВЫХОД");
            Console.WriteLine("Введите цифру 1-5:");
            int pick=0;
            try
            {
                pick = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Main();
            }
            
            if (pick > 0 && pick < 7)
            {                
                ITableInterface table = null;                
                switch (pick)
                {
                    case 1:
                        Console.WriteLine("Товары:");
                        table = new Product(DBconnect()); 
                        break;
                    case 2:
                        Console.WriteLine("Аптеки:");
                        table = new Pharmacy(DBconnect());
                        break;
                    case 3:
                        Console.WriteLine("Склады:");
                        table = new Storage(DBconnect());
                        break;
                    case 4:
                        Console.WriteLine("Партии:");
                        table = new Batch(DBconnect());
                        break;
                    case 5:
                        Console.WriteLine("Количество товара во всех складах аптеки:");
                        Product product = new Product(DBconnect());
                        product.ShowPharmacyAmount();
                        DBconnect().Close();
                        Main();
                        break;
                    case 6:
                        DBconnect().Close();
                        Environment.Exit(0);
                        break;
                }
                Operation(table);
            }
            else
                Main();       
        }

        private static void Operation(ITableInterface table)
        {             
            Console.WriteLine("1-Создать 2-Удалить 3-Просмотр");
            int pick = Convert.ToInt32(Console.ReadLine());
            switch (pick)
            {
                case 1:
                    table.Create();
                    break;
                case 2:
                    table.Delete();
                    break;
                case 3:
                    table.Show();
                    break;
            }
            DBconnect().Close();
            Main();
        }

        

        public static SqlConnection DBconnect()
        {
            string connectionString = @"Server=DESKTOP-55T53UI\SQLEXPRESS;Database=pharmCompDB;Integrated Security=SSPI;Encrypt=false";
            SqlConnection connection = new SqlConnection(connectionString);            
            connection.Open();
            return connection;            
        }

    }
}
