using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("PracticeRooms")]
    public class UPracticeRoom : URoom
    {
        public string Type { get; set; }

        public UPracticeRoom():
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
            (another as UPracticeRoom).Type = Type;
        }
    }
}
