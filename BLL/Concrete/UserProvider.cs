using System;
using System.Linq;
using BLL.Abstarct;
using BLL.ViewModels;
using DBase.Abstact;
using DBase.Concrete;
using System.Data.Entity;
using BLL.Helpers;
using System.Collections.Generic;
using DBase.Entity;
using AutoMapper;

namespace BLL.Concrete
{
    public class UserProvider : IUserProvider
    {
        private readonly UserRepository _userRepository;
        public UserProvider(IDbContext context)
        {
            _userRepository = new UserRepository(context);
        }

        public void AddRole(AddRoleViewModel role)
        {
            _userRepository.AddRole(role.Name);
        }

        public CreateUserStatus CreateUser(CreateUserViewModel userCreate)
        {
            CreateUserStatus status= CreateUserStatus.Success;
            if (_userRepository.FindUserByEmail(userCreate.Email) != null)
                status = CreateUserStatus.DuplicateEmail;
            else
            {
                var user = _userRepository.CreateUser(userCreate.Email,
                    userCreate.Password, userCreate.LastName, userCreate.Name, 
                    userCreate.SecondName);
                if (user != null)
                {
                    _userRepository.AddRoleUser(user, userCreate.RoleId);
                }
                else
                {
                    status = CreateUserStatus.UserErrorCreate;
                }
            }
            return status;
        }

        public UserInfoViewModel DeleteUser(UserInfoViewModel userDelete)
        {
            _userRepository.RemoveUserById(userDelete.ID);
            return userDelete;

        }

        public UserInfoViewModel DeleteUser(int id)
        {
            UserInfoViewModel model = null;
            User user = _userRepository.GetUserById(id);
            if (user != null)
            {
                model = new UserInfoViewModel()
                {
                    ID = user.Id,
                    Email = user.Email
                };
            }
            return model;
        }

        public UserEditViewModel EditUser(UserEditViewModel userEdite)
        {
            _userRepository.EditeUser(userEdite.ID, userEdite.Email);
            _userRepository.ClearUserRoles(userEdite.ID);
            if (userEdite.CurrentRoles != null)
                _userRepository.AddUserRoles(userEdite.ID, userEdite.CurrentRoles);
            _userRepository.ChangePassword(userEdite.ID, userEdite.OldPassword, userEdite.Password);
            return userEdite;
        }

        public UserEditViewModel EditUser(int id)
        {
            UserEditViewModel model = null;
            User user = _userRepository.GetUserById(id);
            if (user != null)
            {
                model = new UserEditViewModel()
                {
                    ID = user.Id,
                    Email = user.Email,
                    CurrentRoles=user.Roles.Select(r=>r.Id)
                };
                // Настройка AutoMapper
                Mapper.Initialize(cfg => cfg.CreateMap<Role, ListBoxItems>());
                //model.ListRoleItems = _userRepository.GetAllRoles().Select(r=> new ListBoxItems() { Id=r.Id, Name=r.Name });
                model.ListRoleItems = Mapper
                    .Map<IList<Role>, IEnumerable<ListBoxItems>>
                    (_userRepository.GetAllRoles());
            }
            
            return model;
        }

        public IList<ListBoxItems> GetListBoxRoles()
        {
            var listRoles = _userRepository.GetAllRoles()
                .Select(r => new ListBoxItems() { Id = r.Id, Name = r.Name })
                .ToList();
            return listRoles;
        }

        public IQueryable<UserViewModel> GetListUsers()
        {
            IQueryable<UserViewModel> listUsers = from data in _userRepository.GetAllUsers().Include(c => c.UserProfile)
                                                  .Include(r => r.Roles)
                                                  select new UserViewModel()
                                                  {
                                                      ID = data.Id,
                                                      Email = data.Email,
                                                      FullName = data.UserProfile != null ? data.UserProfile.LastName + " " + data.UserProfile.Name + " " + data.UserProfile.SecondName : "",
                                                      Roles = data.Roles.Select(r => new RoleViewModel() { Id = r.Id, Name = r.Name })
                                                  };
            return listUsers;
        }

        public string[] GetRolesForUser(string userLogin)
        {
            return _userRepository.GetRolesForUser(userLogin);
        }

        public bool Login(string login, string password)
        {
            return _userRepository.Login(login, password);
        }
    }
}
