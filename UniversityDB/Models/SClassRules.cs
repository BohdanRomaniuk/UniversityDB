using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("ClassesRules")]
    public class SClassRules
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SClassRulesId { get; set; }

        [Required]
        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public SClass Class { get; set; }

        [Required]
        public int ClassIdInside { get; set; }
        [ForeignKey("ClassIdInside")]
        public SClass ClassInside { get; set; }
    }
}
