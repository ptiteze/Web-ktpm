using Kitchen_MVC.Data;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;

namespace Kitchen_MVC.Repositores
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context)
        {   
            this._context = context;
        }
        public ICollection<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
