using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;
using ShiftPlanningRest.Managers;

namespace ShiftPlanningRest.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ShiftController : Controller, IShiftController {
        private readonly IShiftManager _manager;

        public ShiftController(IShiftManager shiftManager) {
            _manager = shiftManager;
        }

        [HttpGet]
        public List<IShift> GetShifts() {
            return _manager.GetShifts();
        }

        [HttpPost]
        public void Post([FromBody] Shift shift) {
            _manager.AddShift(shift);
        }

        [HttpPut]
        public void Put([FromBody] Shift shift) {
            _manager.PutShift(shift);
        }

        [HttpDelete]
        public void Delete([FromBody] Shift shift) {
            _manager.RemoveShift(shift);
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete([FromRoute] int id) {
            _manager.RemoveShift(id);
        }
    }
}
