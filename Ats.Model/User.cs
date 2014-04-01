using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ats.Model
{
    [DataContract]
    public class User
    {
        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public string Pass { get; set; }

        [DataMember]
        public int Id { get; set; }

        public override string ToString()
        {
            return Id.ToString() + " " + Name;
        }
    }
}
