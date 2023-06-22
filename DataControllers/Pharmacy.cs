using PharmCompany.DataControllers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Xml.Linq;

namespace PharmCompany
{
    public class Pharmacy: ITableInterface
    {
        SqlConnection connection;
        public Pharmacy(SqlConnection _connection)
        {
            connection = _connection;
        }

        public void Create() 
        {            
            string sql = "INSERT INTO Pharmacy (name, address, phone) VALUES (@name, @address, @phone)";
            SqlCommand command = new SqlCommand(sql, connection);

            Console.WriteLine("Название:");
            string name = Console.ReadLine().ToString();
            Console.WriteLine("Адресс:");
            string address = Console.ReadLine().ToString();
            Console.WriteLine("Телефон:");
            string phone = Console.ReadLine().ToString();

            command.Parameters.Add(new SqlParameter("@name", name));
            command.Parameters.Add(new SqlParameter("@address", address));
            command.Parameters.Add(new SqlParameter("@phone", phone));
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Аптека добавлена");
            }
            catch
            {
                Console.WriteLine("Не удалось добавить");
            }
        }

        ////!!каскадное удаление в БД!!
        public void Delete()
        {
            string sql = "DELETE FROM Pharmacy WHERE id = @id";

            SqlCommand command = new SqlCommand(sql, connection);

            Console.WriteLine("Код:");
            int id = Convert.ToInt32(Console.ReadLine());

            command.Parameters.Add(new SqlParameter("@id", id));

            try
            {
                if(command.ExecuteNonQuery()>=1)
                    Console.WriteLine("Аптека удалена");
                else
                    Console.WriteLine("Не удалось удалить");
            }
            catch
            {
                Console.WriteLine("Не удалось удалить");
            }
        }

        public void Show()
        {
            string sql = "SELECT id, name, address, phone FROM Pharmacy";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                var query = command.ExecuteReader();
                if (query.HasRows)
                {
                    Console.WriteLine("Код|Название|Адресс|Телефон");
                    while (query.Read())
                    {
                        Console.WriteLine(query.GetValue(0) + "|" + query.GetValue(1) + "|" + query.GetValue(2) + "|" + query.GetValue(3));
                    }
                }
                else
                    Console.WriteLine("Пусто");
            }
            catch
            {
                Console.WriteLine("Ошибка вывода");
            }
        }

    }
}
