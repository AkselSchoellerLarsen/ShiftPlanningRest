using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;

namespace ShiftPlanningRest.Controllers {
    public interface IUserController {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<IUser>> GetUsers([FromBody] User user);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult RegisterUser([FromBody] User user);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> VerifyUser([FromBody] User user);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult MakeUserAdmin([FromBody] User user, [FromBody] string email);
    }
}
