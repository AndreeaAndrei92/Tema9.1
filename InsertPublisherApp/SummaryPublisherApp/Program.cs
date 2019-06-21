using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryPublisherApp
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

            //Selects(connection);
            //SelectsTopPublisher(connection);
            //SelectsBookPerPublisher(connection);
            SelectsTotalPricePerPublisher(connection);
            Console.ReadKey();
        }

        private static void Selects(SqlConnection connection)
        {
            try
            {
                connection.Open();

                var selectString = "select count(*) from Publisher";

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };


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

        private static void SelectsTopPublisher(SqlConnection connection)
        {
            try
            {
                connection.Open();

                var selectString = "select top 10 name, publisherid from Publisher";

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };

                SqlDataReader returnedValue = command.ExecuteReader();


                while (returnedValue.Read())
                {
                    ReadSingleRow((IDataRecord)returnedValue);
                }

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

        private static void ReadSingleRow(IDataRecord record)
        {
            Console.WriteLine(String.Format("{0}, {1}", record[0], record[1]));
        }


        private static void SelectsBookPerPublisher(SqlConnection connection)
        {
            try
            {
                connection.Open();

                var selectString = "select name, count(bookid) from book b inner join publisher p on b.publisherid = p.publisherid group by name";

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };

                SqlDataReader returnedValue = command.ExecuteReader();


                while (returnedValue.Read())
                {
                    ReadSingleRow((IDataRecord)returnedValue);
                }

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


        private static void SelectsTotalPricePerPublisher(SqlConnection connection)
        {
            try
            {
                connection.Open();

                var selectString = "select name, sum(price) from book b inner join publisher p on b.publisherid = p.publisherid group by name";

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };

                SqlDataReader returnedValue = command.ExecuteReader();


                while (returnedValue.Read())
                {
                    ReadSingleRow((IDataRecord)returnedValue);
                }

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

