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
