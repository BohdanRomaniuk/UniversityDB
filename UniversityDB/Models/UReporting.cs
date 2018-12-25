using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Reports")]
    public class UReporting : UObject
    {
        public string ReportingType { get; set; }
        public DateTime Date { get; set; }
        public double Mark { get; set; }

        public UReporting():
            base()
        {
            Date = Convert.ToDateTime("2018-12-21");
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UReporting).Date = Date;
            (another as UReporting).Mark = Mark;
            (another as UReporting).ReportingType = ReportingType;
        }
    }
}
