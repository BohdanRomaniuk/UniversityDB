using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Departmets")]
    public class UDepartment : UObject
    {
        public string Address { get; set; }

        public UDepartment():
            base()
        {
        }

        public UDepartment(string _name, string _address, int _class) :
            base(_name, _class)
        {
            Address = _address;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UDepartment).Address = Address;
        }
    }
}
