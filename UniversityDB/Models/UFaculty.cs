using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Faculties")]
    public class UFaculty: UDepartment
    {
        public string SiteUrl { get; set; }

        public UFaculty():
            base()
        {
        }

        public UFaculty(string _name, string _address, string _siteUrl, int _class) :
            base(_name, _address, _class)
        {
            SiteUrl = _siteUrl;
        }

        //---------Hierarchy of Behavior
        //protected override void CreateActions()
        //{
        //    base.CreateActions();
        //}

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UFaculty).SiteUrl = SiteUrl;
        }
    }
}
