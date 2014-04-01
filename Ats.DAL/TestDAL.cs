using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using foundation;
using Ats.Model;

namespace Ats.DAL
{
    public class TestDAL
    {
        private static IDatabase<DbDataReader,DbConnection> dbmanager;
        public static List<Test> GetTests()
        {
            List<Test> tests = new List<Test>();

            dbmanager = dbManager.Instance();

            dbmanager.executeQuery("select * from tests;");

            if (dbmanager.Reader != null)
            {
                while (dbmanager.Reader.Read())
                {
                    tests.Add(new Test
                    {
                        Id = dbmanager.Reader.GetInt32(0),
                        Name = dbmanager.Reader.GetString(1),
                        TestType = dbmanager.Reader.GetString(2),
                        TimeLimit = dbmanager.Reader.GetInt32(3)
                    });
                }
            }
            return tests;
        }

        public static Test GetTest(int testid)
        {
            string name = String.Empty;
            string type = String.Empty;
            int timelimit = 0;
            dbmanager = dbManager.Instance();

            string query = "SELECT * from tests WHERE testid=" + testid + ";";

            dbmanager.executeQuery(query);

            while (dbmanager.Reader.Read())
            {
                name = dbmanager.Reader.GetString(1);
                type = dbmanager.Reader.GetString(2);
                timelimit = dbmanager.Reader.GetInt32(3);
            }
            return new Test
            {
                Name = name,
                TestType = type,
                TimeLimit = timelimit
            };
        }

        public static void AddTest(string name, string type, int timelimit)
        {
            dbmanager = dbManager.Instance();

            dbmanager.beginTransaction();
            dbmanager.setCommandText("INSERT INTO tests(name,type,timelimit) VALUES(@pName,@pType,@pTimelimit)");
            dbmanager.addParam("pName", name);
            dbmanager.addParam("pType", type);
            dbmanager.addParam("pTimelimit", timelimit);
            dbmanager.executeCommand();
            dbmanager.endTransaction();
        }

        public static void DeleteTest(int testid)
        {
            dbmanager = dbManager.Instance();
            String query = "DELETE FROM tests WHERE testid=" + testid + ";";
            //delete all questions belonging to that test
            String query2 = "DELETE FROM questions WHERE testid=" + testid + ";";

            dbmanager.execute(query);
            dbmanager.execute(query2);
        }

        public static void SetTimeLimit(int testid, int timelimit)
        {
            dbmanager = dbManager.Instance();

            String updateTestTimelimit = "update tests set timelimit='" + timelimit + "' where testid=" + testid.ToString() + ";";
            dbmanager.execute(updateTestTimelimit);
        }

        public static List<String> GetTestTypes()
        {
            dbmanager = dbManager.Instance();

            List<String> results = new List<String>();

            String query = "SELECT DISTINCT type FROM tests;";

            dbmanager.executeQuery(query);

            while (dbmanager.Reader.Read())
            {
                string testType;
                testType = dbmanager.Reader.GetString(0);

                results.Add(testType);
            }
            return results;
        }

        public static int GetNumberOfQuestions(int testid)
        {
            dbmanager = dbManager.Instance();

            String query = "SELECT COUNT(*) FROM questions WHERE testid=" + testid + ";";

            dbmanager.executeQuery(query);

            return Int32.Parse(dbmanager.Reader[0].ToString());
        }
        public static String GetLastTestResultId()
        {
            dbmanager = dbManager.Instance();

            String lastRecordId = "";

            // FIXME: this is SQL Server specific query
            dbmanager.executeQuery(dbmanager.getLastRecord("test_result_id", "test_results"));

            while (dbmanager.Reader.Read())
            {
                lastRecordId = dbmanager.Reader.GetInt32(0).ToString();
            }
            return lastRecordId;
        }

        public static string GetLastTestId()
        {
            dbmanager = dbManager.Instance();

            string lastRecordId = "";

            // FIXME: this is SQL Server specific query
            dbmanager.executeQuery(dbmanager.getLastRecord("testid", "tests"));

            while (dbmanager.Reader.Read())
            {
                lastRecordId = dbmanager.Reader.GetInt32(0).ToString();
            }
            return lastRecordId;
        }
    }
}
