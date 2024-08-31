namespace OAK.Model.Core
{
    using OAK.Model.BaseModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Role : LocalizationExModelBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Role.Name.Required")]
        [MaxLength(150, ErrorMessage = "Role.Name.MaxLength")]
        public string Name { get; set; }

        [MaxLength(255, ErrorMessage = "Role.Description.MaxLength")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Role.IsDefault.Required")]
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}