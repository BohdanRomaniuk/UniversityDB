namespace UniversityDB.Models
{
    public class UPlan : UObject
    {
        public string PlanType { get; set; }
        public string ApprovedBy { get; set; }

        public UPlan():
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
            (another as UPlan).PlanType = PlanType;
            (another as UPlan).ApprovedBy = ApprovedBy;
        }
    }
}
