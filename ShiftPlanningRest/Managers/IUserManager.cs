using Microsoft.AspNetCore.Mvc;
using ShiftPlanningLibrary;

namespace ShiftPlanningRest.Managers {
    public interface IUserManager {
        public List<IUser> GetUsers();
        public bool RegisterUser(IUser user);
        public bool VerifyUser(IUser user);
        public bool MakeUserAdmin(string email);
    }
}
