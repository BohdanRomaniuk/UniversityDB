using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Classes")]
    public class SClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FormName { get; set; }

        public SClass()
        {
        }

        public SClass(string name, string formName)
        {
            Name = name;
            FormName = formName;
        }
    }
}
