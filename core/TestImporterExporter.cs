using System.Xml;
using System.Collections.Generic;
using Ats.Model;
using Ats.DAL;

namespace core
{
    public class TestImporterExporter
    {
        public static void exportTest(Test t)
        {
            Test _test = TestDAL.GetTest(t.Id);
            List<Question> questions = new List<Question>();
            questions = QuestionDAL.GetQuestions(t.Id);
            

            XmlTextWriter textWriter = new XmlTextWriter("test_" + _test.Id + ".xml", null);
            textWriter.WriteStartDocument();

            textWriter.WriteStartElement("Test");

            textWriter.WriteElementString("ID", _test.Id.ToString());   // <-- These are new
            textWriter.WriteElementString("Name", _test.Name);
            textWriter.WriteElementString("Type", _test.TestType);
            textWriter.WriteElementString("TimeLimit", _test.TimeLimit.ToString());
            textWriter.WriteElementString("NumberOfQuestions", questions.Count.ToString());

            textWriter.WriteStartElement("Questions");
            foreach (Question q in questions)
            {
                textWriter.WriteStartElement("Question");

                textWriter.WriteElementString("Ask", q.Quest);
                textWriter.WriteElementString("Get", q.Answer);

                textWriter.WriteEndElement();
            }
            textWriter.WriteEndElement();
            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();
            textWriter.Close();
        }

        public static void importTest(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement name = doc.CreateElement(filename);

        }
    }
}
