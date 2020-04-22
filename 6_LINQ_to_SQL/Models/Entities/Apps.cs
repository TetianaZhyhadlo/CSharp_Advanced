using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using IteaLinqToSql.Models.Interfaces;

namespace IteaLinqToSql.Models.Entities
{
    public class Apps : IIteaModel
    {
        [Key] public int Id { get; set; }
        public string Appname { get; set; }
        public string Admin { get; set; }
        public string Email { get; set; }

        public Apps()
        {

        }
    }
}

