using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudBookApp
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

            //InsertNewBook(connection, "Amintiri din copilarie", 2, 2019, 100);
            //int bookid = Int32.Parse(Console.ReadLine());
            //UpdateBook(connection, 1000, bookid);
            //int deletebookid = Int32.Parse(Console.ReadLine());
            //DeleteBook(connection, deletebookid);
            int selectbookid = Int32.Parse(Console.ReadLine());
            SelectBook(connection, selectbookid);


            Console.ReadKey();

        }

        private static void InsertNewBook(SqlConnection connection, string title,
           int publisherId, int year, int price)
        {
            try
            {
                connection.Open();

                var selectString = @"
                        INSERT INTO [dbo].[Book]
                                   ([Title]
                                   ,[PublisherId]
                                   ,[Year]
                                   ,[Price])
                             VALUES
                                   (@title
                                   ,@publisherId
                                   ,@year
                                   ,@price);
                SELECT CAST(scope_identity() AS int)";

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };

                SqlParameter titleSqlParameter = new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "title",
                    Value = title
                };

                SqlParameter publisherIdSqlParameter = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "publisherId",
                    Value = publisherId
                };

                SqlParameter yearParameter = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "year",
                    Value = year
                };

                SqlParameter priceParameter = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "price",
                    Value = price
                };

                command.Parameters.Add(titleSqlParameter);
                command.Parameters.Add(publisherIdSqlParameter);
                command.Parameters.Add(yearParameter);
                command.Parameters.Add(priceParameter);

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

        private static void UpdateBook(SqlConnection connection, int price, int bookid)
        {
            try
            {
                connection.Open();

                var selectString = @"
                        UPDATE [dbo].[Book]
                                 SET  Price=@Price
                             WHERE
                                   BookId=@bookid;
                SELECT CAST(scope_identity() AS int)";

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };

                SqlParameter priceSqlParameter = new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "price",
                    Value = price
                };

                SqlParameter boookIdSqlParameter = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "bookid",
                    Value = bookid
                };



                command.Parameters.Add(priceSqlParameter);
                command.Parameters.Add(boookIdSqlParameter);


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

        private static void DeleteBook(SqlConnection connection, int bookid)
        {
            try
            {
                connection.Open();

                var selectString = @"
                        DELETE FROM [dbo].[Book]
                             WHERE
                                   BookId=@bookid;
                SELECT CAST(scope_identity() AS int)";

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };


                SqlParameter boookIdSqlParameter = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "bookid",
                    Value = bookid
                };


                command.Parameters.Add(boookIdSqlParameter);


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

        private static void SelectBook(SqlConnection connection, int bookid)
        {
            try
            {
                connection.Open();

                var selectString = "select * from Book";
                selectString += string.Format($" where bookid = @bookid;" +
                                              $"SELECT CAST(scope_identity() AS int)");

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };

                SqlParameter nameParameter = new SqlParameter();
                nameParameter.DbType = DbType.String;
                nameParameter.ParameterName = "bookid";

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
