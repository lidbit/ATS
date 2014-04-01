using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ats.Model;
using System.Data;
using System.Data.SqlClient;

namespace RemoteDAL
{
    public static class QuestionDAL
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ToString();

        public static List<Question> GetQuestions(int testid)
        {
            var questions = new List<Question>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "GetQuestions";

                    sqlCommand.Parameters.AddWithValue("@TestId", testid);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            questions.Add(new Question()
                            {
                                Id = Tools.ConvertToInt(sqlDataReader["questionid"]),
                                Quest = Tools.ConvertToString(sqlDataReader["questionstring"]),
                                Answer = Tools.ConvertToString(sqlDataReader["answer"]),
                                Type = Tools.ConvertToString(sqlDataReader["type"]),
                                TestId = Tools.ConvertToInt(sqlDataReader["testid"])
                            });
                        }
                    }
                }
            }
            return questions;
        }

        public static int AddQuestion(int testid, String question, String type, String answer)
        {
            int result = -1;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "AddQuestion";

                    sqlCommand.Parameters.AddWithValue("@TestId", testid);
                    sqlCommand.Parameters.AddWithValue("@Question", question);
                    sqlCommand.Parameters.AddWithValue("@Type", type);
                    sqlCommand.Parameters.AddWithValue("@Answer", answer);

                    result = sqlCommand.ExecuteNonQuery();
                }
            }
            return result;
        }
        public static int UpdateQuestion(int questionid, string questionstring, string answer)
        {
            int result;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "UpdateQuestion";

                    sqlCommand.Parameters.AddWithValue("@Questionstring", questionstring);
                    sqlCommand.Parameters.AddWithValue("@Answer", answer);
                    sqlCommand.Parameters.AddWithValue("@QuestionId", questionid);
                    result = sqlCommand.ExecuteNonQuery();
                }
            }
            return result;
        }

        public static int DeleteQuestion(int questionid)
        {
            int result = -1;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "DeleteQuestion";
                    sqlCommand.Parameters.AddWithValue("@QuestionId", questionid);
                    result = sqlCommand.ExecuteNonQuery();
                }
            }
            return result;
        }

        public static List<Question> GetQuestionsAll()
        {
            var questions = new List<Question>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "GetQuestionsAll";

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            questions.Add(new Question()
                            {
                                Id = Tools.ConvertToInt(sqlDataReader["questionid"]),
                                Quest = Tools.ConvertToString(sqlDataReader["questionstring"]),
                                Answer = Tools.ConvertToString(sqlDataReader["answer"]),
                                Type = Tools.ConvertToString(sqlDataReader["type"]),
                                TestId = Tools.ConvertToInt(sqlDataReader["testid"])
                            });
                        }
                    }
                }
            }
            return questions;
        }
    }
}
