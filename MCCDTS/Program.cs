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
            program.GetAllDepartement();
            program.GetAllJobdeskTable();
            program.GetAllEmployee();
            program.Get(1);

            //EMPLOYEE
            MCCDTS.Models.Employee employee_insert = new MCCDTS.Models.Employee()
            {
                Name = "Devi",
                JobdeskId = 6
            };
            program.InsertEmployee(employee_insert);

            MCCDTS.Models.Employee employee_update = new MCCDTS.Models.Employee()
            {
                Name = "Demian",
                Id = 2
            };
            program.UpdateEmployee(employee_update);

            MCCDTS.Models.Employee employee_delete = new MCCDTS.Models.Employee()
            {
                Id = 5
            };
            program.DeleteEmployee(employee_delete);

            //JOBDESKTABLE
            MCCDTS.Models.JobdeskTable jobdesk_insert = new MCCDTS.Models.JobdeskTable()
            {
                Id = 8,
                Jobdesk = "Maintain Machine",
                DepartementId = 11
            };
            program.InsertJobdeskTable(jobdesk_insert);

            MCCDTS.Models.JobdeskTable jobdesk_update = new MCCDTS.Models.JobdeskTable()
            {
                Id = 1,
                Jobdesk = "Meeting with Client"
            };
            program.UpdateJobdeskTable(jobdesk_update);

            MCCDTS.Models.JobdeskTable jobdesk_delete = new MCCDTS.Models.JobdeskTable()
            {
                Id = 7
            };
            program.DeleteJobdeskTable(jobdesk_delete);

            //DEPARTEMENT
            MCCDTS.Models.DepartementTable departement_insert = new MCCDTS.Models.DepartementTable()
            {
                Departement = "Information & Technology"
            };
            program.InsertDepartementTable(departement_insert);

            MCCDTS.Models.DepartementTable departement_update = new MCCDTS.Models.DepartementTable()
            {
                Id = 11,
                Departement = "General Affair"
            };
            program.UpdateDepartementTable(departement_update);

            MCCDTS.Models.DepartementTable departement_delete = new MCCDTS.Models.DepartementTable()
            {
                Id = 6
            };
            program.DeleteDepartementTable(departement_delete);
        }

        void GetAll()
        {
            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select Employee.name, JobdeskTable.Jobdesk, Departement.Departement " +
                "from Employee " +
                "inner join JobdeskTable " +
                "on Employee.JobdeskId = JobdeskTable.Id " +
                "inner join Departement " +
                "on Jobdesktable.DepartementId = Departement.Id ";

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
                            Console.WriteLine("Departement : " + sqlDataReader[2]);
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

        void GetAllDepartement()
        {
            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select * from Departement ";

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine("Id : " + sqlDataReader[0]);
                            Console.WriteLine("Departement : " + sqlDataReader[1]);
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

        void GetAllJobdeskTable()
        {
            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select * from Jobdesktable ";

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine("Id : " + sqlDataReader[0]);
                            Console.WriteLine("Jobdesk : " + sqlDataReader[1]);
                            Console.WriteLine("DepartementId : " + sqlDataReader[2]);
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

        void GetAllEmployee()
        {
            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select * from Employee ";

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine("Id : " + sqlDataReader[0]);
                            Console.WriteLine("Name : " + sqlDataReader[1]);
                            Console.WriteLine("JobdeskId : " + sqlDataReader[2]);
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

        //EMPLOYEE
        void InsertEmployee(MCCDTS.Models.Employee employee)
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
                        "VALUES (@name, @jobdeskid)";

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@name";
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

        void UpdateEmployee(MCCDTS.Models.Employee employee)
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

        void DeleteEmployee(MCCDTS.Models.Employee employee)
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

        //JOBDESKTABLE
        void InsertJobdeskTable(MCCDTS.Models.JobdeskTable jobdeskTable)
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
                        "INSERT INTO JobdeskTable " +
                        "(Id, Jobdesk, DepartementId) " +
                        "VALUES (@id, @jobdesk, @departementid) ";

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@id";
                    sqlParameter.Value = jobdeskTable.Id;

                    SqlParameter sqlParameter1 = new SqlParameter();
                    sqlParameter1.ParameterName = "@jobdesk";
                    sqlParameter1.Value = jobdeskTable.Jobdesk;

                    SqlParameter sqlParameter2 = new SqlParameter();
                    sqlParameter2.ParameterName = "@departementid";
                    sqlParameter2.Value = jobdeskTable.DepartementId;

                    sqlCommand.Parameters.Add(sqlParameter);
                    sqlCommand.Parameters.Add(sqlParameter1);
                    sqlCommand.Parameters.Add(sqlParameter2);

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

        void UpdateJobdeskTable(MCCDTS.Models.JobdeskTable jobdeskTable)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = jobdeskTable.Id;
                sqlCommand.Parameters.Add(sqlParameter);

                SqlParameter sqlParameter1 = new SqlParameter();
                sqlParameter1.ParameterName = "@jobdesk";
                sqlParameter1.Value = jobdeskTable.Jobdesk;
                sqlCommand.Parameters.Add(sqlParameter1);

                try
                {
                    sqlCommand.CommandText =
                        "UPDATE JobdeskTable " +
                        "SET Jobdesk = @jobdesk WHERE Id=@id ";
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

        void DeleteJobdeskTable(MCCDTS.Models.JobdeskTable jobdeskTable)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = jobdeskTable.Id;
                sqlCommand.Parameters.Add(sqlParameter);

                try
                {
                    sqlCommand.CommandText =
                        "DELETE FROM JobdeskTable " +
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

        //DEPARTEMENT
        void InsertDepartementTable(MCCDTS.Models.DepartementTable departementTable)
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
                        "INSERT INTO Departement " +
                        "(Departement) " +
                        "VALUES (@departement) ";

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@departement";
                    sqlParameter.Value = departementTable.Departement;

                    //sqlCommand.Parameters.Add(sqlParameter);
                    sqlCommand.Parameters.Add(sqlParameter);

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

        void UpdateDepartementTable(MCCDTS.Models.DepartementTable departementTable)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = departementTable.Id;
                sqlCommand.Parameters.Add(sqlParameter);

                SqlParameter sqlParameter1 = new SqlParameter();
                sqlParameter1.ParameterName = "@departement";
                sqlParameter1.Value = departementTable.Departement;
                sqlCommand.Parameters.Add(sqlParameter1);

                try
                {
                    sqlCommand.CommandText =
                        "UPDATE Departement " +
                        "SET Departement = @departement WHERE Id=@id ";
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

        void DeleteDepartementTable(MCCDTS.Models.DepartementTable departementTable)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = departementTable.Id;
                sqlCommand.Parameters.Add(sqlParameter);

                try
                {
                    sqlCommand.CommandText =
                        "DELETE FROM Departement " +
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
