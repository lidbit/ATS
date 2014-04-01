using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ats.Model
{
    [DataContract]
    public class Test
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int TimeLimit { get; set; }
        [DataMember]
        public int SecondsElapsed { get; set; }
        [DataMember]
        public string TestType { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool Running { get; set; }
        [DataMember]
        public int CurrentQuestion { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int Correct { get; set; }
    }
}
