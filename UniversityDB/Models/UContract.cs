using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Contracts")]
    public class UContract : UDocument
    {
        public string ContractType { get; set; }

        public UContract() :
            base()
        {
        }

        public UContract(string _name, long _regNumber, string _type, int _classId) :
            base(_name, _regNumber, _classId)
        {
            ContractType = _type;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UContract).ContractType = ContractType;
        }
    }
}
