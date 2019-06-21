using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeletePublisherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=Week09.2;Integrated Security=True;";

            SqlConnection connection = new SqlConnection
            {
                ConnectionString = connectionString
            };

            int selectpublisherid = Int32.Parse(Console.ReadLine());
            SelectPublisher(connection, selectpublisherid);

            Console.ReadKey();
        }

        private static void SelectPublisher(SqlConnection connection, int publisherid)
        {
            try
            {
                connection.Open();

                var selectString = "select * from Publisher";
                selectString += string.Format($" where publisherid = @publisherid;" +
                                              $"SELECT CAST(scope_identity() AS int)");

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };

                SqlParameter nameParameter = new SqlParameter();
                nameParameter.DbType = DbType.String;
                nameParameter.ParameterName = "publisherid";

                command.Parameters.Add(nameParameter);

                var returnedValue = command.ExecuteScalar();

                Console.WriteLine(returnedValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
