using BLL.Helpers;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstarct
{
    public interface IUserProvider
    {
        bool Login(string login, string password);
        IQueryable<UserViewModel> GetListUsers();
        IList<ListBoxItems> GetListBoxRoles();
        CreateUserStatus CreateUser(CreateUserViewModel userCreate);
        UserEditViewModel EditUser(int id);
        UserEditViewModel EditUser(UserEditViewModel userEdite);
        UserInfoViewModel DeleteUser(int id);
        UserInfoViewModel DeleteUser(UserInfoViewModel userDelete);
      
        void AddRole(AddRoleViewModel role);
        string[] GetRolesForUser(string userLogin);
    }
}
