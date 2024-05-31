using Kitchen_MVC.Models;

namespace Kitchen_MVC.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<Role> GetRoles();
    }
}
