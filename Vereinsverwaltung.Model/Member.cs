using System;
using System.Collections.Generic;
using System.Text;

namespace Vereinsverwaltung.Model
{
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Birthday { get; set; }
    }
}
