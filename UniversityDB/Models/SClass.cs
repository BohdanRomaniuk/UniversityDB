using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace UniversityDB.Models
{
    [Table("Classes")]
    public class SClass : INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FormName { get; set; }

        [ForeignKey("ClassId")]
        public ObservableCollection<SClassRules> AllowedChildrens { get; set; }

        public SClass()
        {
            AllowedChildrens = new ObservableCollection<SClassRules>();
        }

        public SClass(string name, string formName)
        {
            Name = name;
            FormName = formName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
