using System;
using System.Xml.Serialization;

using Newtonsoft.Json;
using System.Collections.Generic;

namespace IteaSerialization
{
    [Serializable]
    public class Company : IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();

        protected Company() { }

        public Company(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            Company obj1 = obj as Company;
            if (obj1 as Company == null)
                return false;
            return (obj1.Name == this.Name && obj1.Id == this.Id);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
    }
}
