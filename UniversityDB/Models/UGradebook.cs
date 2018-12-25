using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Gradebooks")]
    public class UGradebook : UDocument
    {
        public double AvgMark { get; set; }
        public int GradeYear { get; set; }

        public UGradebook():
            base()
        {
        }

        public UGradebook(string _name, long _regNumber, double _mark, int _gradeYear, int _classId) :
            base(_name, _regNumber, _classId)
        {
            AvgMark = _mark;
            GradeYear = _gradeYear;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UGradebook).AvgMark = AvgMark;
            (another as UGradebook).GradeYear = GradeYear;
        }
    }
}
