using DBase.Abstact;
using DBase.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBase.Concrete
{
    public class UserRepository
    {
        IDbContext context;
        public UserRepository(IDbContext context)
        {
            this.context = context;
        }
        public bool Login(string email, string password)
        {
            User user = FindUser(email, password);
            if (user != null)
            {
                return true;
            }
            else
                return false;
        }
        public User FindUser(string email, string password)
        {
            var user = FindUserByEmail(email);
            if(user!=null)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                if (user.Password == crypto.Compute(password, user.PasswordSalt))
                    return user;
                else
                    return null;
            }
            return user;
        }
        public User FindUserByEmail(string email)
        {
            //User user =  context.Users.Where(u => u.Email == email).FirstOrDefault();
            User user = context.Set<User>().AsQueryable().Where(u => u.Email == email).FirstOrDefault();
            return user;
        }
        public User CreateUser(string email,string password)
        {
            User user = null;
            if (this.FindUserByEmail(email)==null)
            {
                user = new User() { Email = email, Password = password };
                var crypto = new SimpleCrypto.PBKDF2();
                user.Password = crypto.Compute(user.Password);
                user.PasswordSalt = crypto.Salt;
                context.Set<User>().Add(user);
                this.SaveChange();
            }           
            return user;
        }
        public User CreateUser(string email, string password, string lastName, string name, string secondName)
        {

            User user = this.CreateUser(email,password);
            if(user!=null)
            {
                UserProfile userProf = new UserProfile()
                {
                    Id = user.Id,
                    //User = user,
                    Name = name,
                    LastName = lastName,
                    SecondName = secondName
                };
                context.Set<UserProfile>().Add(userProf);
                this.SaveChange();
            }
            
            return user;
        }
        public User GetUserById(int id)
        {
            return context.Set<User>().AsQueryable().Where(u => u.Id == id).SingleOrDefault();
        }
        public bool ChangePassword(int id, string oldPassword, string newPassword)
        {
            User user = this.GetUserById(id);
            if (user != null)
            {
                if(user.Password==oldPassword)
                {
                    user.Password = newPassword;
                    this.SaveChange();
                    return true;
                }                           
            }
            return false;
        }
        public void RemoveUserById(int id)
        {
            User user = this.GetUserById(id);
            if(user!=null)
            {
                context.Set<User>().Remove(user);
                this.SaveChange();
            }
        }
        public void EditeUser(int id, string email)
        {
            User user = this.GetUserById(id);
            if (user != null)
            {
                if (this.FindUserByEmail(email) == null)
                {
                    user.Email = email;
                    this.SaveChange();
                }
            }
        }

        public IQueryable<User> GetAllUsers()
        {
            IQueryable<User> listUsers = from data in context.Set<User>().AsQueryable() select data;
            return listUsers;
        }
        public Role AddRole(string name)
        {
            Role role = new Role()
            { Name = name };
            this.context.Set<Role>().Add(role);
            this.SaveChange();
            return role;
        }
        public void AddRoleUser(User user, int RoleId)
        {
            Role role = this.context.Set<Role>().SingleOrDefault(r => r.Id == RoleId);
            if (role != null)
            {
                role.Users.Add(user);
                this.SaveChange();
            }
        }
        public IList<Role> GetAllRoles()
        {
            return this.context.Set<Role>().ToList();
        }
        public string[] GetRolesForUser(string email)
        {
            var user=this.FindUserByEmail(email);
            if(user!=null)
            {
                return user.Roles.Select(r=>r.Name).ToArray();
            }
            return null;
        }
        public Role GetRoleById(int id)
        {
            return this.GetAllRoles().SingleOrDefault(r => r.Id == id);
        }
        public void ClearUserRoles(int userId)
        {
            var user = GetUserById(userId);
            if(user!=null)
            {
                foreach(Role item in user.Roles)
                {
                    item.Users.Remove(user);
                    //user.Roles.Remove(item);
                }
                this.SaveChange();
            }
        }
        public void AddUserRoles(int userId, IEnumerable<int> listRoleId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                foreach (int item in listRoleId)
                {
                    Role role = this.GetRoleById(item);
                    if (role != null)
                    {
                        role.Users.Add(user);
                        this.SaveChange();
                    }

                }
                
            }
        }
        public void SaveChange()
        {
            context.SaveChanges();
        }
    }
}
