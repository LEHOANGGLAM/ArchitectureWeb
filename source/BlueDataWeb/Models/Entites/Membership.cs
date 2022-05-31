//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueDataWeb.Models.Entites
{
    using System;
    using System.Collections.Generic;
    
    public partial class Membership
    {
        public Membership()
        {
            this.UsersInRoles = new HashSet<UsersInRole>();
        }
    
        public int UserId { get; set; }
        public Nullable<System.Guid> ApplicationId { get; set; }
        public string Password { get; set; }
        public Nullable<int> PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> IsLockedOut { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> LastPasswordChangedDate { get; set; }
        public Nullable<System.DateTime> LastLockoutDate { get; set; }
        public Nullable<int> FailedPasswordAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAttemptWindowStart { get; set; }
        public Nullable<int> FailedPasswordAnswerAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAnswerAttemptWindowsStart { get; set; }
        public string Comment { get; set; }
        public string Username { get; set; }
    
        public virtual ICollection<UsersInRole> UsersInRoles { get; set; }
    }
}
