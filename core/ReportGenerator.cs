using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ats.Model;
using Ats.DAL;

namespace core
{
    public class ReportGenerator
    {
        private StreamWriter streamWriterTests = null;
        private StreamWriter streamWriterQuestions = null;

        public void createReport()
        {
            string dir;
            String description;
            string testFileName = String.Empty;

            dir = Directory.GetCurrentDirectory();
            Directory.CreateDirectory(dir+"\\Reports");
            streamWriterTests = File.CreateText(dir + "\\Reports\\Reports.htm");

            streamWriterTests.BaseStream.Seek(0, SeekOrigin.End);

            DoWrite(streamWriterTests,"<html><head><title></title></head>");
            DoWrite(streamWriterTests,"<body><p><br />Test Results</p>");
            DoWrite(streamWriterTests,"<table style=\"width:70%;\">");
            DoWrite(streamWriterTests,"<tr><td width=\"20%\">Test</td><td>Date</td><td>Score</td></tr></table>");

            DoWrite(streamWriterTests,"<table style=\"width:70%;\">");

            # region Write question results



            List<TestResult> testResults = Remote.RemoteTestResultDAL.GetTestResults();
            foreach (TestResult tr in testResults)
            {
                description = tr.Description;
                testFileName = "Test_" + tr.TestResultId + ".htm";
                string dir2 = String.Empty;
                dir2 = Directory.GetCurrentDirectory();

                writeTableRow(dir2+"\\Reports\\TestResults\\"+testFileName,description, tr.DateTaken, tr.Score.ToString());
                
                
                Directory.CreateDirectory(dir2 + "\\Reports\\TestResults");

                streamWriterQuestions = File.CreateText(dir2 + "\\Reports\\TestResults\\" + testFileName);

                streamWriterQuestions.BaseStream.Seek(0, SeekOrigin.End);

                DoWrite(streamWriterQuestions,"<html><head><title></title></head>");

                DoWrite(streamWriterQuestions,"<script language=\"javascript\">");
                DoWrite(streamWriterQuestions, "function redirectHome() {");
                DoWrite(streamWriterQuestions, "var address = location.href;");
                DoWrite(streamWriterQuestions, "var home;");
                DoWrite(streamWriterQuestions, "home = address.slice( 0, address.indexOf(\"Reports\") );");
                DoWrite(streamWriterQuestions, "home = home + \"Reports/Reports.htm\"");
                DoWrite(streamWriterQuestions, "location.href=home;");
                DoWrite(streamWriterQuestions, "}");
                DoWrite(streamWriterQuestions, "</script>");

                DoWrite(streamWriterQuestions, "<body><p><br />Test Results</p>");
                DoWrite(streamWriterQuestions, "<table style=\"width:70%;\">");
                DoWrite(streamWriterQuestions, "<tr><td width=\"20%\">Question</td><td>User answer</td><td>Answer</td><td>Time to answer</td></tr></table>");

                DoWrite(streamWriterQuestions, "<table style=\"width:70%;\">");

                foreach (QuestionResult qr in tr.getQuestionResults())
                {
                    writeQuestionData(qr.Question, qr.UserAnswer, qr.Answer, qr.ResponseTime.ToString());
                }
                DoWrite(streamWriterQuestions, "</table>");
                DoWrite(streamWriterQuestions, "<button onClick=\"redirectHome();\"><b>Back</b></button>");
                DoWrite(streamWriterQuestions, "</body></html>");
            }
            # endregion

            DoWrite(streamWriterTests,"</table></body></html>");
            if (streamWriterTests != null)
                streamWriterTests.Close();
            if (streamWriterQuestions != null)
                streamWriterQuestions.Close();
        }

        private void DoWrite(StreamWriter strmWriter, String line)
        {
            strmWriter.WriteLine(line);
            strmWriter.Flush();
        }

        private void writeQuestionData(String question, String useranswer, String answer, String timetoAnswer)
        {
            DoWrite(streamWriterQuestions, "<tr><td width=\"20%\">" + question + "</td><td>" + useranswer + "</td><td>" + answer + "</td><td>" + timetoAnswer + "</td></tr>");
        }

        private void writeTableRow(string url, string testname, DateTime testdate, string testscore)
        {
            DoWrite(streamWriterTests, "<tr><td width=\"20%\"><a href=\""+url+"\">" + testname + "</a></td><td>" + testdate + "</td><td>" + testscore + "</td></tr>");
        }
    }
}