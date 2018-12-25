using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Magazines")]
    public class UMagazine : UPrintEdition
    {
        public int MagazineNumber { get; set; }
        public string MagazineType { get; set; }

        public UMagazine():
            base()
        {
        }

        public UMagazine(string _name, int _count, int _number, string _type, int _classId) :
            base(_name, _count, _classId)
        {
            MagazineNumber = _number;
            MagazineType = _type;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UMagazine).MagazineNumber = MagazineNumber;
            (another as UMagazine).MagazineType = MagazineType;
        }
    }
}
