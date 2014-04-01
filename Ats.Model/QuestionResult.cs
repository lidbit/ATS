using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ats.Model
{
    public class QuestionResult
    {
        public QuestionResult()
        {
        }

        # region properties
        # region Question
        public String Question { get; set; }
        # endregion
        # region TestId
        public int TestId { get; set; }
        # endregion
        # region QuestionId
        public int QuestionId { get; set; }
        # endregion
        # region UserAnswer
        public String UserAnswer { get; set; }
        # endregion
        # region Answer
        public String Answer { get; set; }
        # endregion
        # region Correct
        public String Correct { get; set; }
        # endregion
        # region ResponseTime
        public int ResponseTime { get; set; }
        # endregion
        # region Result
        public String Result { get; set; }
        # endregion
        # endregion
    }
}
