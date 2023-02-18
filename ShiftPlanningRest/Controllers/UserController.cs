using Microsoft.AspNetCore.Http.HttpResults;
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
        public ActionResult<List<IUser>> GetUsers([FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin) {
            User user = new User(email, password, isAdmin);
            if (_manager.VerifyUser(user)) {
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

        /*
        [Route("Verify")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> VerifyUser([FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin) {
            User user = new User(email, password, isAdmin);
            return Ok(_manager.VerifyUser(user));
        }
        */
        [Route("Verify")]
        [HttpGet]
        public bool VerifyUser([FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin) {
            User user = new User(email, password, isAdmin);
            return _manager.VerifyUser(user);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult MakeUserAdmin([FromBody] string userEmail, [FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin) {
            User user = new User(email, password, isAdmin);
            if (!_manager.VerifyUser(user)) {
                return Unauthorized("Invalid credentials");
            }
            if(!user.IsAdmin) {
                return Unauthorized("You must be an admin to make others into admins");
            }
            _manager.MakeUserAdmin(userEmail);
            return Accepted();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser([FromBody] string userEmail, [FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin) {
            User user = new User(email, password, isAdmin);
            if (!_manager.VerifyUser(user)) {
                return Unauthorized("Invalid credentials");
            }
            if (!user.IsAdmin) {
                return Unauthorized("You must be an admin to make delete users");
            }
            if (_manager.RemoveUser(userEmail)) {
                return Accepted();
            }
            return NotFound("Failed to remove user with email=" + userEmail);
        }
    }
}
