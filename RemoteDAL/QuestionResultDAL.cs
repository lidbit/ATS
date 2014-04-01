using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RemoteDAL
{
    public class QuestionResultDAL
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ToString();

        public static int AddQuestionResult(int testid, int questionid, string useranswer, string answer, int correct, int responsetime, int result)
        {
            int questionResultId;
            int Queryresult = -1;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "AddQuestionResult";

                    sqlCommand.Parameters.AddWithValue("@TestId", testid);
                    sqlCommand.Parameters.AddWithValue("@QuestionId", questionid);
                    sqlCommand.Parameters.AddWithValue("@UserAnswer", useranswer);
                    sqlCommand.Parameters.AddWithValue("@Answer", answer);
                    sqlCommand.Parameters.AddWithValue("@Correct", correct);
                    sqlCommand.Parameters.AddWithValue("@ResponseTime", responsetime);
                    sqlCommand.Parameters.AddWithValue("@Result", result);

                    Tools.AddOutputParam(sqlCommand, "@QuestionResultId", SqlDbType.Int);

                    Queryresult = sqlCommand.ExecuteNonQuery();

                    questionResultId = Tools.ConvertToInt(sqlCommand.Parameters["@TestIdOut"]);
                }
            }
            return questionResultId;
        }
    }
}
