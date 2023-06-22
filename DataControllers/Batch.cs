using PharmCompany.DataControllers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Xml.Linq;

namespace PharmCompany
{
    public class Batch : ITableInterface
    {
        SqlConnection connection;
        public Batch(SqlConnection _connection)
        {
            connection = _connection;
        }

        public void Create() 
        {            
            string sql = "INSERT INTO Batch (Id_Product, Id_Storage,amount) VALUES (@id_p, @id_s, @amount)";
            SqlCommand command = new SqlCommand(sql, connection);

            Console.WriteLine("Код товара:");
            int id_p = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Код склада:");
            int id_s = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Кол-во:");
            int amount = Convert.ToInt32(Console.ReadLine());

            command.Parameters.Add(new SqlParameter("@id_p", id_p));
            command.Parameters.Add(new SqlParameter("@id_s", id_s));
            command.Parameters.Add(new SqlParameter("@amount", amount));
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Партия добавлена");
            }
            catch
            {
                Console.WriteLine("Не удалось добавить");
            }
        }

        
        public void Delete()
        {
            string sql = "DELETE FROM Batch WHERE id = @id";

            SqlCommand command = new SqlCommand(sql, connection);

            Console.WriteLine("Код:");
            int id = Convert.ToInt32(Console.ReadLine());

            command.Parameters.Add(new SqlParameter("@id", id));

            try
            {
                if(command.ExecuteNonQuery()>=1)
                    Console.WriteLine("Партия удалена");
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
            string sql = "SELECT id, Id_Product, Id_Storage, amount FROM Batch";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                var query = command.ExecuteReader();
                if (query.HasRows)
                {
                    Console.WriteLine("Код|Код товара|Код склада|Кол-во");
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
