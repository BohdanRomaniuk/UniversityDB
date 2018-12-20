using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Processes")]
    public class UProcess : UObject
    {
        public string ProcessType { get; set; }

        public UProcess():
            base()
        {
        }

        public UProcess(string _name, string _type, int _classId) :
            base(_name, _classId)
        {
            ProcessType = _type;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UProcess).ProcessType = ProcessType;
        }
    }
}
