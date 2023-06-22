using PharmCompany.DataControllers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Xml.Linq;

namespace PharmCompany
{
    public class Product: ITableInterface
    {
        SqlConnection connection;
        public Product(SqlConnection _connection)
        {
            connection = _connection;
        }

        public void Create() 
        {
            string sql = "INSERT INTO Product (name) VALUES (@name)";           
            SqlCommand command = new SqlCommand(sql, connection);
            Console.WriteLine("Название:");
            string name = Console.ReadLine().ToString();
          

            command.Parameters.Add(new SqlParameter("@name", name));  
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Товар добавлен");
            }
            catch
            {
                Console.WriteLine("Не удалось добавить");
            }
        }

        ////!!каскадное удаление в БД!!
        public void Delete()
        {
            string sql = "DELETE FROM Product WHERE id = @id";

            SqlCommand command = new SqlCommand(sql, connection);

            Console.WriteLine("Код:");
            int id = Convert.ToInt32(Console.ReadLine());

            command.Parameters.Add(new SqlParameter("@id", id));

            try
            {
                if(command.ExecuteNonQuery()>=1)
                    Console.WriteLine("Товар удален");
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
            string sql = "SELECT id, name FROM Product";          
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {                
                var query = command.ExecuteReader();
                if (query.HasRows)
                {
                    Console.WriteLine("Код|Название");
                    while (query.Read())
                    {
                        Console.WriteLine(query.GetValue(0) + "|" + query.GetValue(1));
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

        public void ShowPharmacyAmount()
        {
            string sql = "SELECT pr.Id, pr.Name, SUM(b.Amount) FROM Product as pr " +
                "INNER join Batch as b ON b.Id_Product=pr.Id " +
                "INNER join Storage as s ON b.Id_Storage = s.Id " +
                "INNER join Pharmacy as ph ON s.Id_Pharmacy = ph.Id " +
                "WHERE ph.Id = @id GROUP BY pr.Id, pr.Name";
            Console.WriteLine("Код Аптеки:");
            int id = Convert.ToInt32(Console.ReadLine());      
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@id", id));
            try
            {
                SqlDataReader query = command.ExecuteReader();
                if (query.HasRows) 
                {
                    Console.WriteLine("Код|Название|Кол-во");
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
