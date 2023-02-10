using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;
using ShiftPlanningRest.Managers;

namespace ShiftPlanningRest.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ShiftController : Controller, IShiftController {
        private readonly IShiftManager _shiftManager;
        private readonly IUserManager _userManager;

        public ShiftController(IShiftManager shiftManager, IUserManager userManager) {
            _shiftManager = shiftManager;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<IShift>> GetShifts([FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin) {
            User user = new User(email, password, isAdmin);
            if(_userManager.VerifyUser(user)) {
                return Ok(_shiftManager.GetShifts(user));
            }
            return Unauthorized("Invalid credentials");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Post([FromBody] Shift shift, [FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin) {
            User user = new User(email, password, isAdmin);
            if (_userManager.VerifyUser(user) && user.IsAdmin) {
                _shiftManager.AddShift(shift);
                return Created("", shift);
            }
            return Unauthorized("Invalid credentials");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Put([FromBody] Shift shift, [FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin) {
            User user = new User(email, password, isAdmin);
            if (_userManager.VerifyUser(user) && user.IsAdmin) {
                return Accepted(_shiftManager.PutShift(shift));
            }
            return Unauthorized("Invalid credentials");
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Delete([FromRoute] int id, [FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin) {
            User user = new User(email, password, isAdmin);
            if (_userManager.VerifyUser(user) && user.IsAdmin) {
                return Accepted(_shiftManager.RemoveShift(id));
            }
            return Unauthorized("Invalid credentials");
        }
    }
}
