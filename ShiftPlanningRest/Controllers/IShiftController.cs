using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;

namespace ShiftPlanningRest.Controllers {
    public interface IShiftController {
        [HttpGet]
        public List<IShift> GetShifts();

        [HttpPost]
        public void Post([FromBody] Shift shift);

        [HttpPut]
        public void Put([FromBody] Shift shift);

        [HttpDelete]
        public void Delete([FromBody] Shift shift);

        [Route("{id}")]
        [HttpDelete]
        public void Delete([FromRoute] int id);
    }
}
