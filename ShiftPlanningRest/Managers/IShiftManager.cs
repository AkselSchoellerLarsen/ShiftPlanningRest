using ShiftPlanningLibrary;

namespace ShiftPlanningRest.Managers {
    public interface IShiftManager {
        public List<IShift> GetShifts(IUser user);
        public bool AddShift(IShift shift);
        public bool PutShift(IShift shift);
        public bool RemoveShift(int id);
        public bool RemoveShift(IShift shift);
    }
}
