using System;
using System.Runtime.Serialization;

namespace Ats.Model
{
    [Serializable]
    [DataContract]
    public class Question
    {
        [DataMember]
        public string Quest { get; set; }

        [DataMember]
        public string Answer { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int TestId { get; set; }

        [DataMember]
        public int Correct { get; set; }

        [DataMember]
        public string Useranswer { get; set; }

        [DataMember]
        public int TimetoAnswer { get; set; }

        [DataMember]
        public string Type { get; set; }

        public Question()
        {
        }

        public Question(int id, string question, string answer)
        {
            Correct = 0;
            Useranswer = String.Empty;
            this.Id = id;
            Quest = question;
            this.Answer = answer;
        }

        public Question(string question, string answer)
        {
            Quest = question;
            this.Answer = answer;
        }

        public override string ToString()
        {
            return Quest;
        }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }
    }
}
