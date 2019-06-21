using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertPublisherApp
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


            InsertNewPublisher(connection, "Litera", 6);

            Console.ReadKey();
        }

        private static void InsertNewPublisher(SqlConnection connection, string name,
    int publisherId)
        {
            try
            {
                connection.Open();

                var selectString = @"
                        INSERT INTO [dbo].[Publisher]
                                   ([name]
                                   ,[PublisherId])
                             VALUES
                                   (@name
                                   ,@publisherId);
                SELECT CAST(scope_identity() AS int)";

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };

                SqlParameter nameSqlParameter = new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "name",
                    Value = name
                };

                SqlParameter publisherIdSqlParameter = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "publisherId",
                    Value = publisherId
                };

                command.Parameters.Add(nameSqlParameter);
                command.Parameters.Add(publisherIdSqlParameter);

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

