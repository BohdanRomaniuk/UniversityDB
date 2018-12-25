namespace UniversityDB.Models
{
    public class UTechniqueInventory : UInventory
    {
        public string Category { get; set; }
        public int Varanty { get; set; }

        public UTechniqueInventory():
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
            (another as UTechniqueInventory).Category = Category;
            (another as UTechniqueInventory).Varanty = Varanty;
        }
    }
}
