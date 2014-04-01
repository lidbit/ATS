using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Ats.Model;

namespace RemoteDAL
{
    public class TestResultDAL
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ToString();

        public static int AddTestResult(int userid, int testid, int timetaken, int correct, int total, string testname)
        {
            int testResultId;
            int QueryResult = -1;
            int totalQuestions = RemoteDAL.QuestionDAL.GetQuestions(testid).Count;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "AddTestResult";

                    sqlCommand.Parameters.AddWithValue("@UserId", userid);
                    sqlCommand.Parameters.AddWithValue("@TestId", testid);
                    sqlCommand.Parameters.AddWithValue("@TimeTaken", timetaken);
                    sqlCommand.Parameters.AddWithValue("@Correct", correct);
                    sqlCommand.Parameters.AddWithValue("@Total", totalQuestions);
                    sqlCommand.Parameters.AddWithValue("@TestName", testname);

                    Tools.AddOutputParam(sqlCommand,"@TestResultId",SqlDbType.Int);

                    QueryResult = sqlCommand.ExecuteNonQuery();
                    int value;
                    Int32.TryParse((Convert.ToString(sqlCommand.Parameters["@TestResultId"]) ?? "").Trim(), out value);
                    testResultId = value;
                }
            }
            return testResultId;
        }

        public static TestResult GetTestResult(int testresultid)
        {
            TestResult testResult = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "GetTestResult";

                    sqlCommand.Parameters.AddWithValue("@TestResultId", testresultid);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            DateTime value;
                            DateTime.TryParse((Convert.ToString(sqlDataReader["date"]) ?? "").Trim(), out value);
                            testResult = new TestResult()
                            {
                                UserId = Tools.ConvertToInt(sqlDataReader["userid"]),
                                TestId = Tools.ConvertToInt(sqlDataReader["testid"]),
                                DateTaken = value,
                                TimeTaken = Tools.ConvertToInt(sqlDataReader["timetaken"]),
                                Score = Tools.ConvertToInt(sqlDataReader["correct"]),
                                TotalQuestions = Tools.ConvertToInt(sqlDataReader["total"]),
                                Description = Tools.ConvertToString(sqlDataReader["testname"])
                            };
                        }
                    }
                }
            }
            return testResult;
        }

        public static object GetTestResults()
        {
            List<TestResult> testResults = new List<TestResult>();

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
                            DateTime value;
                            DateTime.TryParse((Convert.ToString(sqlDataReader["date"]) ?? "").Trim(), out value);
                            testResults.Add(new TestResult()
                            {
                                UserId = Tools.ConvertToInt(sqlDataReader["userid"]),
                                TestId = Tools.ConvertToInt(sqlDataReader["testid"]),
                                DateTaken = value,
                                TimeTaken = Tools.ConvertToInt(sqlDataReader["timetaken"]),
                                Score = Tools.ConvertToInt(sqlDataReader["correct"]),
                                TotalQuestions = Tools.ConvertToInt(sqlDataReader["total"]),
                                Description = Tools.ConvertToString(sqlDataReader["testname"])
                            });
                        }
                    }
                }
            }
            return testResults.ToArray<TestResult>();
        }
    }
}
