using System.ComponentModel.DataAnnotations;

namespace OAK.Model.ViewModels.CompanyModels
{
    public class UeCompany
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Company.Name.Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company.Email.Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company.Guid.Required")]
        public string Guid { get; set; }

    }
}
