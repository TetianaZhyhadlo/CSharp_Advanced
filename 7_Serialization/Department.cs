using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;

using Newtonsoft.Json;

namespace IteaSerialization
{

    [Serializable]

    public class Department:IModel
    {
        public string Name { get; set; }
        public List<Person> People { get; set; } = new List<Person>();
        public Company Company { get; set; }
        public Guid Id { get; set; }
        //[XmlIgnore]
        //[JsonIgnore]

         protected Department() { }
        

        public Department(string name)
        {
            Name = name;
            Id = new Guid();
        }
        public void SetCompanyForDepartment(Company company)
        {
            Company = company;
            Company.Departments.Add(this);
        }
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            else
                return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
