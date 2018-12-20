using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Documents")]
    public class UDocument : UObject
    {
        public long RegistoryNumber { get; set; }

        public UDocument():
            base()
        {
        }

        public UDocument(string _name, long _regNumber, int _classId) :
            base(_name, _classId)
        {
            RegistoryNumber = _regNumber;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UDocument).RegistoryNumber = RegistoryNumber;
        }
    }
}
