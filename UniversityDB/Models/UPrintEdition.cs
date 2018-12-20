using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("PrintedEditions")]
    public class UPrintEdition : UObject
    {
        public int Count { get; set; }

        public UPrintEdition():
            base()
        {
        }

        public UPrintEdition(string _name, int _count, int _classId) :
            base(_name, _classId)
        {
            Count = _count;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UPrintEdition).Count = Count;
        }
    }
}
