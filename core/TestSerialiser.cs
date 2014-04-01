// Copyright Andrew Douglas 2009
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System;
using Ats.Model;
using Ats.DAL;

namespace core
{
    class TestSerialiser
    {
        public void TestToXml(string id)
        {
            Test t = null;
            List<Question> questions = new List<Question>();
            questions = QuestionDAL.GetQuestions(Int32.Parse(id));

            t = TestDAL.GetTest(Int32.Parse(id));
            if (t != null)
            {
                XmlTextWriter xw = new XmlTextWriter(@"" + Directory.GetCurrentDirectory() + "Test_" + id.ToString(),System.Text.Encoding.UTF8);
                xw.WriteStartDocument();

                xw.WriteStartElement("Test");

                xw.WriteElementString("TestID", t.Id.ToString());
                xw.WriteElementString("Name", t.Name);
                xw.WriteElementString("Type", t.TestType);
                xw.WriteElementString("Timelimit", t.TimeLimit.ToString());

                xw.WriteStartElement("Questions");

                foreach (Question q in questions)
                {
                    xw.WriteElementString("QuestionID", q.Id.ToString());
                    xw.WriteElementString("Question", q.Quest);
                    xw.WriteElementString("Answer", q.Answer);
                }

                xw.WriteEndElement();    
                // end Test element
                xw.WriteEndElement();
            }
        }
    }
}
