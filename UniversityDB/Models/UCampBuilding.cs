using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("CampBuilding")]
    public class UCampBuilding : UBuilding
    {
        public string Type { get; set; }

        public UCampBuilding():
            base()
        {
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UCampBuilding).Type = Type;
        }
    }
}
