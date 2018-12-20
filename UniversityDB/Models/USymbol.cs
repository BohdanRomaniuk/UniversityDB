using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Symbols")]
    public class USymbol : UObject
    {
        public string SymbolType { get; set; }

        public USymbol() :
            base()
        {
        }

        public USymbol(string _name, string _type, int _classId) :
            base(_name, _classId)
        {
            SymbolType = _type;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as USymbol).SymbolType = SymbolType;
        }
    }
}
