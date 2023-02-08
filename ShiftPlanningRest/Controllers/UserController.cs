using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;
using ShiftPlanningRest.Managers;

namespace ShiftPlanningRest.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller, IUserController {
        private readonly IUserManager _manager;

        public UserController(IUserManager userManager) {
            _manager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<IUser>> GetUsers([FromBody] User user) {
            if(VerifyUser(user).Value) {
                return Ok(_manager.GetUsers());
            }
            return Unauthorized("Invalid credentials");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult RegisterUser([FromBody] User user) {
            _manager.RegisterUser(user);
            return Created("", user);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> VerifyUser([FromBody] User user) {
            return Ok(_manager.VerifyUser(user));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult MakeUserAdmin([FromBody] User user, [FromBody] string email) {
            if(!_manager.VerifyUser(user)) {
                return Unauthorized("Invalid credentials");
            }
            if(!user.IsAdmin) {
                return Unauthorized("You must be an admin to make others into admins");
            }
            _manager.MakeUserAdmin(email);
            return Accepted();
        }
    }
}
