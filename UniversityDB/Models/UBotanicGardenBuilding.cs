using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("BotanicGardens")]
    public class UBotanicGardenBuilding : UBuilding
    {
        public int PlantsCount { get; set; }

        public UBotanicGardenBuilding():
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
            (another as UBotanicGardenBuilding).PlantsCount = PlantsCount;
        }
    }
}
