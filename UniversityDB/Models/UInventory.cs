using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Inventories")]
    public class UInventory : UObject
    {
        public int InventoryNumber { get; set; }

        public UInventory():
            base()
        {
        }

        public UInventory(string _name, int _invNum, int _classId):
            base(_name, _classId)
        {
            InventoryNumber = _invNum;
        }

        //---------Hierarchy of Behavior
        //protected override void CreateActions()
        //{
        //    base.CreateActions();
        //}

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UInventory).InventoryNumber = InventoryNumber;
        }
    }
}
