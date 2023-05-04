using MilestoneWebApplication.Models;

namespace RegisterAndLoginApp.Services
{
    public class SecurityService
    {

        SecurityDAO securityDAO = new SecurityDAO();
       
        public bool isValid(User user)
        {
            return securityDAO.FindUserByNameAndPassword(user);
        }

        public bool registerSuccess(User user)
        {
            return securityDAO.AddNewUser(user);
        }

    }
}
