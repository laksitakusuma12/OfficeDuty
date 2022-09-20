using Microsoft.Data.SqlClient;
using System;

namespace MCCDTS
{
    class Program
    {
        SqlConnection sqlConnection;

        /*
         * Data Source
         * Initial Catalog
         * User ID
         * Password
         */
        string connectionString = "Data Source=LAPTOP-DIV8R0C0\\MSSQLSERVER02;Initial Catalog=OFFICE;User ID=laksita;Password=123;TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            Program program = new Program();
            program.GetAll();
            program.Get(1);

            MCCDTS.Models.Employee employee = new MCCDTS.Models.Employee()
            {
                Name = "Tina",
                JobdeskId = 7
            };
            program.Insert(employee);

            MCCDTS.Models.Employee employee_update = new MCCDTS.Models.Employee()
            {
                Name = "Willy",
                Id = 1
            };
            program.Update(employee_update);

            MCCDTS.Models.Employee employee_delete = new MCCDTS.Models.Employee()
            {
                Id = 8
            };
            program.Delete(employee_delete);
            program.GetAll();
        }

        void GetAll()
        {
            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select Employee.name, JobdeskTable.Jobdesk from Employee join JobdeskTable on Employee.JobdeskId = JobdeskTable.Id";

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine("Name : " + sqlDataReader[0]);
                            Console.WriteLine("Jobdesk : " + sqlDataReader[1]);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void Get(int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select " +
                                     "Employee.name, JobdeskTable.Jobdesk " +
                                     "from Employee join JobdeskTable " +
                                     "on Employee.JobdeskId = JobdeskTable.Id " +
                                     "where Employee.id = @id";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@id";
            sqlParameter.Value = id;

            sqlCommand.Parameters.Add(sqlParameter);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine("Name : " + sqlDataReader[0]);
                            Console.WriteLine("Jobdesk : " + sqlDataReader[1]);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void Insert(MCCDTS.Models.Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText =
                        "INSERT INTO Employee " +
                        "(Name, JobdeskId) " +
                        "VALUES (@employeeName, @jobdeskid)";

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@employeeName";
                    sqlParameter.Value = employee.Name;

                    SqlParameter sqlParameter1 = new SqlParameter();
                    sqlParameter1.ParameterName = "@jobdeskid";
                    sqlParameter1.Value = employee.JobdeskId;

                    sqlCommand.Parameters.Add(sqlParameter);
                    sqlCommand.Parameters.Add(sqlParameter1);

                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }

        void Update(MCCDTS.Models.Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = employee.Id;
                sqlCommand.Parameters.Add(sqlParameter);

                SqlParameter sqlParameter1 = new SqlParameter();
                sqlParameter1.ParameterName = "@name";
                sqlParameter1.Value = employee.Name;
                sqlCommand.Parameters.Add(sqlParameter1);

                try
                {
                    sqlCommand.CommandText =
                        "UPDATE Employee " +
                        "SET Name = @name WHERE Id=@id ";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }

        void Delete(MCCDTS.Models.Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = employee.Id;
                sqlCommand.Parameters.Add(sqlParameter);

                try
                {
                    sqlCommand.CommandText =
                        "DELETE FROM Employee " +
                        "WHERE Id = @id ";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }

    }

}
