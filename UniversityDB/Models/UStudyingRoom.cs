using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("StudyingRooms")]
    public class UStudyingRoom : URoom
    {
        public string Projector { get; set; }

        public UStudyingRoom(): base()
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
            (another as UStudyingRoom).Projector = Projector;
        }
    }
}
