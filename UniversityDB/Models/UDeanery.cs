using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Deaneries")]
    public class UDeanery: UDepartment
    {
        public string Phone { get; set; }

        public UDeanery() :
            base()
        {
        }

        public UDeanery(string _name, string _address, string _phone, int _class) :
            base(_name, _address, _class)
        {
            Phone = _phone;
        }

        //---------Hierarchy of Behavior
        //protected override void CreateActions()
        //{
        //    base.CreateActions();
        //}

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UDeanery).Phone = Phone;
        }
    }
}
