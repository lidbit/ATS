using System;
using System.Collections.Generic;

namespace Ats.Model
{
    public class TestResult
    {
        private List<QuestionResult> questionResults;

        public TestResult()
        {
            questionResults = new List<QuestionResult>();
        }

        public TestResult(int _testresultid, int _testId, int _userId, int _score, int _totalQuestions, int _timeTaken, String description)
        {
            questionResults = new List<QuestionResult>();
            this.TestResultId = _testresultid;
            this.TotalQuestions = _totalQuestions;
            this.TestId = _testId;
            this.UserId = _userId;
            this.Score = _score;
            this.TimeTaken = _timeTaken;
            this.Description = description;
        }

        # region Properties
        # region TestResultId
        public int TestResultId { get; set; }
        # endregion
        # region TestId
        public int TestId { get; set; }
        # endregion
        # region UserId
        public int UserId { get; set; }
        # endregion
        # region Score
        public int Score { get; set; }
        # endregion
        # region TimeTaken
        public int TimeTaken { get; set; }
        # endregion
        # region Description
        public String Description { get; set; }
        # endregion
        # region TotalQuestions
        public int TotalQuestions { get; set; }
        # endregion
        # region DateTaken
        public DateTime DateTaken { get; set; }
        # endregion
        # endregion


        public void setTestResultId(int testresultid)
        {
            this.TestResultId = testresultid;
        }

        public void addQuestionResult(QuestionResult qr)
        {
            questionResults.Add(qr);
        }

        public List<QuestionResult> getQuestionResults()
        {
            return questionResults;
        }

        public override string ToString()
        {
            return "Test " + this.TestId.ToString() + " Score " + Score.ToString() + "/" + this.TotalQuestions.ToString() + " in " + this.TimeTaken.ToString() + " seconds";
        }
    }
}
