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

        [XmlIgnore]
        [JsonIgnore]
        public Company Company { get; set; }
        public Guid Id { get; set; } 
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
            if (obj == null)
                return false;
            Department obj1 = obj as Department;
            if (obj1 as Department == null)
                return false;
            return (obj1.Name == this.Name && obj1.Id == this.Id && obj1.Company==this.Company);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
