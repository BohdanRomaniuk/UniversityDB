using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("StudyingBuildings")]
    public class UStudyingBuilding : UBuilding
    {
        public int LectorsRoomsCount { get; set; }
        public int PracticeRoomsCount { get; set; }

        public UStudyingBuilding():
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
            (another as UStudyingBuilding).LectorsRoomsCount = LectorsRoomsCount;
            (another as UStudyingBuilding).PracticeRoomsCount = PracticeRoomsCount;
        }
    }
}
