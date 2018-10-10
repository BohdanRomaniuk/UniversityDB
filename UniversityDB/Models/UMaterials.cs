using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Metherials")]
    public class UMaterial: UObject
    {
        public string Type { get; set; }

        public UMaterial():
            base()
        {
        }

        public UMaterial(string _name, string _type, int _class):
            base(_name, _class)
        {
            Type = _type;
        }
    }
}
