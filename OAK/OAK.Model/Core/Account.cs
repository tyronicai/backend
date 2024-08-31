using OAK.Model.BusinessModels.CommentModels;

namespace OAK.Model.Core
{
    using OAK.Model.BaseModels;
    using OAK.Model.BusinessModels;
    using OAK.Model.BusinessModels.DemandModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Account : ModelBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Account.Username.Required")]
        [MaxLength(150, ErrorMessage = "Account.Username.MaxLength")]
        public string Username { get; set; }
        public string Password { get; set; }

        [Required(ErrorMessage = "Account.FirstName.Required")]
        [MaxLength(100, ErrorMessage = "Account.FirstName.MaxLength")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Account.LastName.Required")]
        [MaxLength(100, ErrorMessage = "Account.LastName.MaxLength")]
        public string LastName { get; set; }

        public int LoginAttempts { get; set; }

        [Required(ErrorMessage = "Account.PhoneNumber.Required")]
        [MaxLength(20, ErrorMessage = "Account.PhoneNumber.MaxLength")]
        public string PhoneNumber { get; set; }


        public DateTime? LastLoginDate { get; set; }
        public DateTime? EmailActivationDate { get; set; }
        public DateTime LastPasswordChangeDate { get; set; }

        [Required(ErrorMessage = "Account.Email.Required")]
        [MaxLength(255, ErrorMessage = "Account.Email.MaxLength")]
        [EmailAddress(ErrorMessage = "Account.Email.EmailAddress")]
        public string Email { get; set; }

        public bool IsEmailActivated { get; set; }

        public bool TwoFactorAuthenticationEnabled { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompanyOwner { get; set; }
        public int ActivationCode { get; set; }
        public string FcmToken { get; set; }
        public string TempVerificationString { get; set; }
        public DateTime? VerificationValidityTime { get; set; }
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        public virtual ICollection<Demand> Demands { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public virtual DemandChat DemandChat { get; set; }
    }
}
