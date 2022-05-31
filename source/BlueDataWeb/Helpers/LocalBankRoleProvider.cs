using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Models;

namespace BlueDataWeb.Helpers
{

    public class LocalBankRoleProvider : RoleProvider
    {

        #region Variables

        private LocalBankRepository repository;

        #endregion

        #region Constructors

        public LocalBankRoleProvider()
        {
            this.repository = new LocalBankRepository();
        }

        #endregion

        #region Implemented RoleProvider Methods

        public override bool IsUserInRole(string userName, string roleName)
        {
            BlueDataWeb.Models.Entites.Membership user = repository.GetUser(userName);
            Role role = repository.GetRole(roleName);

            if (!repository.UserExists(user))
                return false;
            //if (!repository.RoleExists(role))
            //    return false;

            return true;
            //Var
            //return user.UsersInRoles.Name == role.Name;
        }

        public override string[] GetRolesForUser(string userName)
        {
            if (userName != "administrator")
            {
                List<Role> role = this.repository.GetRoleForUser(userName);
                var lstRole = role.Select(m => m.RoleName);
                if (!this.repository.RoleExists(role))
                    return new string[] { string.Empty };

                return (String[])lstRole.ToArray();
            }
            else
            {
                String[] items = new string[] { "Admin" };
                return items;
            }
        }

        #endregion

        #region Not Implemented RoleProvider Methods

        #region Properties

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}