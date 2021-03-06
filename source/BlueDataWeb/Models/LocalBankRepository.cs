using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BlueDataWeb.Models.Entites;

namespace BlueDataWeb.Models
{

    public class LocalBankRepository
    {

        #region Variables

        private BlueDataWebEntities entities = new BlueDataWebEntities();

        private const string MissingRole = "Role does not exist";
        private const string MissingUser = "User does not exist";
        private const string TooManyUser = "User already exists";
        private const string TooManyRole = "Role already exists";
        private const string AssignedRole = "Cannot delete a role with assigned users";

        #endregion

        #region Properties

        public int NumberOfUsers
        {
            get
            {
                return this.entities.Memberships.Count();
            }
        }

        public int NumberOfRoles
        {
            get
            {
                return this.entities.Roles.Count();
            }
        }

        #endregion

        #region Constructors

        public LocalBankRepository()
        {
            this.entities = new BlueDataWebEntities();
        }

        #endregion

        #region Query Methods

        public IQueryable<BlueDataWeb.Models.Entites.Membership> GetAllUsers()
        {
            return from user in entities.Memberships
                   orderby user.Username
                   select user;
        }

        public BlueDataWeb.Models.Entites.Membership GetUser(int id)
        {
            return entities.Memberships.SingleOrDefault(user => user.UserId == id);
        }

        public BlueDataWeb.Models.Entites.Membership GetUser(string userName)
        {
            return entities.Memberships.SingleOrDefault(user => user.Username == userName);
        }

        public IQueryable<BlueDataWeb.Models.Entites.Membership> GetUsersForRole(string roleName)
        {
            return GetUsersForRole(GetRole(roleName));
        }

        public IQueryable<BlueDataWeb.Models.Entites.Membership> GetUsersForRole(int id)
        {

            return GetUsersForRole(GetRole(id));
        }

        public IQueryable<BlueDataWeb.Models.Entites.Membership> GetUsersForRole(Role role)
        {
            //if (!RoleExists(role))
            //    throw new ArgumentException(MissingRole);

            return from user in entities.UsersInRoles
                   where user.RoleId == role.RoleId
                   select user.Membership;
        }

        public IQueryable<Role> GetAllUserRoles()
        {
            return from role in entities.Roles
                   orderby role.RoleName
                   select role;
        }

        public Role GetRole(int id)
        {
            return entities.Roles.SingleOrDefault(role => role.RoleId == id);
        }

        public Role GetRole(string name)
        {
            return entities.Roles.SingleOrDefault(role => role.RoleName == name);
        }

        public List<Role> GetRoleForUser(string userName)
        {
            int userid = GetUser(userName).UserId;
            List<Role> userinrole = entities.UsersInRoles.Where(role => role.UserId == userid).Select(m=>m.Role).ToList();
            return userinrole;
        }

        //public Role GetRoleForUser(int id)
        //{
        //    UsersInRole userinrole = entities.UsersInRoles.SingleOrDefault(role => role.UserId ==id);
        //    return GetRoleForUser(userinrole.Role.RoleName);
        //}

        //public Role GetRoleForUser(Membership user)
        //{
        //    if (!UserExists(user))
        //        throw new ArgumentException(MissingUser);

        //    UsersInRole userinrole = entities.UsersInRoles.Where(m => m.UserId == user.UserId).FirstOrDefault();
        //    return userinrole.Role;
        //}

        #endregion

        #region Insert/Delete

        //private void AddUser(Membership user)
        //{
        //    if (UserExists(user))
        //        throw new ArgumentException(TooManyUser);

        //    entities.Memberships.AddObject(user);
        //}

        //public void CreateUser(string username, string name, string password, string email, string roleName)
        //{
        //    Role role = GetRole(roleName);

        //    if (string.IsNullOrEmpty(username.Trim()))
        //        throw new ArgumentException("The user name provided is invalid. Please check the value and try again.");
        //    if (string.IsNullOrEmpty(name.Trim()))
        //        throw new ArgumentException("The name provided is invalid. Please check the value and try again.");
        //    if (string.IsNullOrEmpty(password.Trim()))
        //        throw new ArgumentException("The password provided is invalid. Please enter a valid password value.");
        //    if (string.IsNullOrEmpty(email.Trim()))
        //        throw new ArgumentException("The e-mail address provided is invalid. Please check the value and try again.");
        //    if (!RoleExists(role))
        //        throw new ArgumentException("The role selected for this user does not exist! Contact an administrator!");
        //    if (this.entities.Memberships.Any(user => user.Username == username))
        //        throw new ArgumentException("Username already exists. Please enter a different user name.");

        //    Membership newUser = new Membership()
        //    {
        //        Username = username,
        //        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password.Trim(), "md5"),
        //        Email = email
        //    };

        //    try
        //    {
        //        AddUser(newUser);
        //    }
        //    catch (ArgumentException ae)
        //    {
        //        throw ae;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ArgumentException("The authentication provider returned an error. Please verify your entry and try again. " +
        //            "If the problem persists, please contact your system administrator.");
        //    }

        //    // Immediately persist the user data
        //    Save();
        //}

        //public void DeleteUser(Membership user)
        //{
        //    if (!UserExists(user))
        //        throw new ArgumentException(MissingUser);

        //    entities.Memberships.DeleteObject(user);
        //}

        //public void DeleteUser(string userName)
        //{
        //    DeleteUser(GetUser(userName));
        //}

        //public void AddRole(Role role)
        //{
        //    if (RoleExists(role))
        //        throw new ArgumentException(TooManyRole);

        //    entities.Roles.AddObject(role);
        //}

        //public void AddRole(string roleName)
        //{
        //    Role role = new Role()
        //    {
        //        RoleName = roleName
        //    };

        //    AddRole(role);
        //}

        //public void DeleteRole(Role role)
        //{
        //    if (!RoleExists(role))
        //        throw new ArgumentException(MissingRole);

        //    if (GetUsersForRole(role).Count() > 0)
        //        throw new ArgumentException(AssignedRole);

        //    entities.Roles.De.(role);
        //}

        //public void DeleteRole(string roleName)
        //{
        //    DeleteRole(GetRole(roleName));
        //}

        #endregion

        #region Persistence

        public void Save()
        {
            entities.SaveChanges();
        }

        #endregion

        #region Helper Methods

        public bool UserExists(BlueDataWeb.Models.Entites.Membership user)
        {
            if (user == null)
                return false;

            return (entities.Memberships.SingleOrDefault(u => u.UserId == user.UserId || u.Username == user.Username) != null);
        }

        public bool RoleExists(List<Role> role)
        {
            if (role.Count() <1)
                return false;

            return true;
        }

        #endregion

    }

}