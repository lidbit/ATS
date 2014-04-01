using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Ats.Model;

namespace RemoteDAL
{
    public class TestDAL
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ToString();

        public static void ImportTest(int testid, string name, string type, int timelimit)
        {
            int result;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "ImportTest";

                    sqlCommand.Parameters.AddWithValue("TestId",testid);
                    sqlCommand.Parameters.AddWithValue("@Name", name);
                    sqlCommand.Parameters.AddWithValue("@Timelimit", timelimit);
                    sqlCommand.Parameters.AddWithValue("@Type", type);

                    result = sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public static Test[] GetTests()
        {
            List<Test> tests = new List<Test>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "GetTests";


                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            tests.Add(new Test()
                            {
                                Id = Tools.ConvertToInt(sqlDataReader["testid"]),
                                Name = Tools.ConvertToString(sqlDataReader["name"]),
                                TestType = Tools.ConvertToString(sqlDataReader["type"]),
                                TimeLimit = Tools.ConvertToInt(sqlDataReader["timelimit"])
                            });
                        }
                    }
                }
            }
            return tests.ToArray<Test>();
        }

        public static int DeleteTest(int testid)
        {
            int result = -1;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "DeleteTest";
                    sqlCommand.Parameters.AddWithValue("@TestId",testid);

                    result = sqlCommand.ExecuteNonQuery();
                }
            }
            return result;
        }

        public static Test GetTest(int testid)
        {
            Test test = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "GetTest";
                    sqlCommand.Parameters.AddWithValue("@TestId", testid);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            test = new Test()
                            {
                                Id = Tools.ConvertToInt(sqlDataReader["testid"]),
                                Name = Tools.ConvertToString(sqlDataReader["name"]),
                                TestType = Tools.ConvertToString(sqlDataReader["type"]),
                                TimeLimit = Tools.ConvertToInt(sqlDataReader["timelimit"])
                            };
                        }
                    }
                }
            }
            return test;
        }

        public static int SetTestTimelimit(int testid, int timelimit)
        {
            int result = -1;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "Test_SetTimeLimit";
                    sqlCommand.Parameters.AddWithValue("@TestId", testid);
                    sqlCommand.Parameters.AddWithValue("@Timelimit", timelimit);

                    result = sqlCommand.ExecuteNonQuery();
                }
            }
            return result;
        }

        public static List<string> GetTestTypes()
        {
            List<string> testTypes = new List<string>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "GetTestTypes";

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            testTypes.Add(Tools.ConvertToString(sqlDataReader["type"]));
                        }
                    }
                }
            }
            return testTypes;
        }

        public static int AddTest(string name, string type, int timelimit)
        {
            int testid;
            int result = -1;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "AddTest";

                    sqlCommand.Parameters.AddWithValue("@Name", name);
                    sqlCommand.Parameters.AddWithValue("@Timelimit", timelimit);
                    sqlCommand.Parameters.AddWithValue("@Type", type);

                    Tools.AddOutputParam(sqlCommand, "@TestIdOut", SqlDbType.Int);

                    result = sqlCommand.ExecuteNonQuery();

                    testid = Tools.ConvertToInt(sqlCommand.Parameters["@TestIdOut"]);
                }
            }
            return testid;
        }
    }
}
