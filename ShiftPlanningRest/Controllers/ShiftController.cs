using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;
using ShiftPlanningRest.Managers;

namespace ShiftPlanningRest.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ShiftController : Controller, IShiftController {
        private static IShiftManager _manager = new ShiftManager();

        public List<IShift> GetShifts() {
            return _manager.GetShifts();
        }

        public void Post([FromBody] IShift shift) {
            _manager.AddShift(shift);
        }
    }
}
