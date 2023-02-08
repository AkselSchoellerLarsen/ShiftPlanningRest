using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;
using ShiftPlanningRest.Managers;

namespace ShiftPlanningRest.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ShiftController : Controller, IShiftController {
        private readonly IShiftManager _manager;
        private readonly IUserController _controller;

        public ShiftController(IShiftManager shiftManager, IUserController userController) {
            _manager = shiftManager;
            _controller = userController;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<IShift>> GetShifts([FromBody] User user) {
            if(_controller.VerifyUser(user).Value) { // true -> user in database
                GetShifts(user);
                return Ok();
            }
            return Unauthorized("Invalid credentials");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Post([FromBody] Shift shift, [FromBody] User user) {
            if (_controller.VerifyUser(user).Value && user.IsAdmin) {
                _manager.AddShift(shift);
                return Created("", shift);
            }
            return Unauthorized("Invalid credentials");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Put([FromBody] Shift shift, [FromBody] User user) {
            if (_controller.VerifyUser(user).Value && user.IsAdmin) {
                _manager.PutShift(shift);
                return Accepted();
            }
            return Unauthorized("Invalid credentials");
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Delete([FromRoute] int id, [FromBody] User user) {
            if (_controller.VerifyUser(user).Value && user.IsAdmin) {
                _manager.RemoveShift(id);
                return Accepted();
            }
            return Unauthorized("Invalid credentials");
        }
    }
}
