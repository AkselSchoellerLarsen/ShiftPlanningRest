using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;

namespace ShiftPlanningRest.Controllers {
    public interface IShiftController {
        [HttpGet]
        public List<IShift> GetShifts();

        [HttpPost]
        public void Post([FromBody] IShift shift);
    }
}
