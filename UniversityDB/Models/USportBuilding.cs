using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("SportBuildings")]
    public class USportBuilding : UBuilding
    {
        public string FootballFieldsCount { get; set; }
        public string SwimingPool { get; set; }

        public USportBuilding():
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
            (another as USportBuilding).FootballFieldsCount = FootballFieldsCount;
            (another as USportBuilding).SwimingPool = SwimingPool;
        }
    }
}
