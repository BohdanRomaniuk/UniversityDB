using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("EducationalSubjects")]
    public class UEducationalSubject : UObject
    {
        public int LecturesCount { get; set; }
        public int PractiveCount { get; set; }
        public string Reporting { get; set; }

        public UEducationalSubject():
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
            (another as UEducationalSubject).LecturesCount = LecturesCount;
            (another as UEducationalSubject).PractiveCount = PractiveCount;
            (another as UEducationalSubject).Reporting = Reporting;
        }
    }
}
