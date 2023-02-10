using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;

namespace ShiftPlanningRest.Controllers {
    public interface IShiftController {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<IShift>> GetShifts([FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Post([FromBody] Shift shift, [FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Put([FromBody] Shift shift, [FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin);

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Delete([FromRoute] int id, [FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin);
    }
}
