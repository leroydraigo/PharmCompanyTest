using PharmCompany.DataControllers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Xml.Linq;

namespace PharmCompany
{
    public class Storage : ITableInterface
    {
        SqlConnection connection;
        public Storage(SqlConnection _connection)
        {
            connection = _connection;
        }

        public void Create() 
        {            
            string sql = "INSERT INTO Storage (Id_Pharmacy, name) VALUES (@id_p, @name)";
            SqlCommand command = new SqlCommand(sql, connection);

            Console.WriteLine("Код Аптеки:");
            string id_p = Console.ReadLine().ToString();
            Console.WriteLine("Название:");
            string name = Console.ReadLine().ToString();

            command.Parameters.Add(new SqlParameter("@id_p", id_p));
            command.Parameters.Add(new SqlParameter("@name", name));           
            
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Склад добавлен");
            }
            catch
            {
                Console.WriteLine("Не удалось добавить");
            }
        }

        //!!каскадное удаление в БД!!
        public void Delete()
        {
            string sql = "DELETE FROM Storage WHERE id = @id";

            SqlCommand command = new SqlCommand(sql, connection);

            Console.WriteLine("Код:");
            int id = Convert.ToInt32(Console.ReadLine());

            command.Parameters.Add(new SqlParameter("@id", id));

            try
            {
                if(command.ExecuteNonQuery()>=1)
                    Console.WriteLine("Склад удален");
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
            string sql = "SELECT id, Id_Pharmacy, Name FROM Storage";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                var query = command.ExecuteReader();
                if (query.HasRows)
                {
                    Console.WriteLine("Код|Код Аптеки|Название");
                    while (query.Read())
                    {
                        Console.WriteLine(query.GetValue(0) + "|" + query.GetValue(1) + "|" + query.GetValue(2));
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
