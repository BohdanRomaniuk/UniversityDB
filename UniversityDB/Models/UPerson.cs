using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Persons")]
    public class UPerson : UObject
    {
        public DateTime Birthday { get; set; }
        public string Address { get; set; }

        public UPerson():
            base()
        {
        }

        public UPerson(string _name, DateTime _birthday, string _address, int _class) :
            base(_name, _class)
        {
            Birthday = _birthday;
            Address = _address;
        }
    }
}
