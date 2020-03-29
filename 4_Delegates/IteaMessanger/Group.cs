using System;
using System.Collections.Generic;
using System.Text;

namespace IteaDelegates.IteaMessanger
{
    public class Groups
    {
        public string GroupName { get; set; }
        public List<Account> Group { get; private set; }

        public Groups (string name)
        {
            GroupName = name;
            Group = new List<Account>();
        }
        

     
    }
}
