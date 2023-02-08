using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;

namespace ShiftPlanningRest.Controllers {
    public interface IShiftController {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<IShift>> GetShifts([FromBody] User user);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Post([FromBody] Shift shift, [FromBody] User user);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Put([FromBody] Shift shift, [FromBody] User user);

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Delete([FromRoute] int id, [FromBody] User user);
    }
}
