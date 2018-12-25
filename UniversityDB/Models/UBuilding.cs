using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Buildings")]
    public class UBuilding : UObject
    {
        public string Address { get; set; }

        public UBuilding():
            base()
        {
        }

        public UBuilding(string _name, string _address, int _classId):
            base(_name, _classId)
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
            (another as UBuilding).Address = Address;
        }
    }
}
