using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("DiningRooms")]
    public class UDiningRoom : URoom
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public UDiningRoom(): 
            base()
        {
            From = Convert.ToDateTime("2018-12-20");
            To = Convert.ToDateTime("2018-12-20");
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UDiningRoom).From = From;
            (another as UDiningRoom).To = To;
        }
    }
}
