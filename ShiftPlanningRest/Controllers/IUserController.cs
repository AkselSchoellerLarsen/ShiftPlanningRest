using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;

namespace ShiftPlanningRest.Controllers {
    public interface IUserController {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<IUser>> GetUsers([FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult RegisterUser([FromBody] User user);

        /*
        [Route("Verify")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> VerifyUser([FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin);
        */
        [Route("Verify")]
        [HttpGet]
        public bool VerifyUser([FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult MakeUserAdmin([FromQuery] string userEmail, [FromHeader] string email, [FromHeader] string password, [FromHeader] bool isAdmin);
    }
}
