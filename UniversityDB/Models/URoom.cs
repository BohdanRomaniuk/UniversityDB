namespace UniversityDB.Models
{
    public class URoom : UObject
    {
        public int RoomNumber { get; set; }

        public URoom():
            base()
        {
        }

        public URoom(string _name, int _roomNum, int _classId) :
            base(_name, _classId)
        {
            RoomNumber = _roomNum;
        }

        //---------Hierarchy of Behavior
        //protected override void CreateActions()
        //{
        //    base.CreateActions();
        //}

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as URoom).RoomNumber = RoomNumber;
        }
    }
}
