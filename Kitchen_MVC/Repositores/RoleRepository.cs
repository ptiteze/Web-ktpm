using Kitchen_MVC.Data;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.Singleton;

namespace Kitchen_MVC.Repositores
{
    public class RoleRepository : IRoleRepository
    {
        //private readonly DataContext SingletonDataBridge.GetInstance();
        public RoleRepository(/*DataContext context*/)
        {
            //this.SingletonDataBridge.GetInstance() = context;
        }
        public ICollection<Role> GetRoles()
        {
            return SingletonDataBridge.GetInstance().Roles.ToList();
        }
    }
}
