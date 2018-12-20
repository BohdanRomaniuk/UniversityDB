using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("ClinicBuildings")]
    public class UClinicBuilding : UBuilding
    {
        public int EmployeeCount { get; set; }

        public UClinicBuilding():
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
            (another as UClinicBuilding).EmployeeCount = EmployeeCount;
        }
    }
}
