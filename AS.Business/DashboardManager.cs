using AS.Core;
using AS.Entities.Entity;

namespace AS.Business
{
    public class DashboardManager : IDashboardService
    {


        private readonly IRepository<Faculty> _repositoryFaculty;
        private readonly IRepository<User> _repositoryUser;    




        public DashboardManager(IRepository<Faculty> repositoryFaculty, IRepository<User> repositoryUser)
        {
            _repositoryFaculty = repositoryFaculty;
            _repositoryUser = repositoryUser;           
        }
          

    }
}
