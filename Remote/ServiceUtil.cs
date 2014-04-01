using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Remote
{
    public struct ServiceUtil
    {
        public enum ServiceMethods
        {
            AddTest,
            ImportTest,
            DeleteTest,
            GetTests,
            GetTest,
            SetTestTimelimit,
            GetTestTypes,

            GetQuestions,
            GetQuestionsAll,
            AddQuestion,
            UpdateQuestion,
            DeleteQuestion,

            AddTestResult,
            GetTestResult,
            GetTestResults,

            AddQuestionResult
        }

        public enum ServiceCallType
        {
            Post,
            Get
        }
    }
}

