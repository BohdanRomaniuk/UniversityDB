namespace UniversityDB.Models
{
    public class UTransportInventory : UInventory
    {
        public string Type { get; set; }
        public int Year { get; set; }

        public UTransportInventory():
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
            (another as UTransportInventory).Type = Type;
            (another as UTransportInventory).Year = Year;
        }
    }
}
