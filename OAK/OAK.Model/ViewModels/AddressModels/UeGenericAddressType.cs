namespace OAK.Model.ViewModels
{
    using OAK.Model.BaseModels;
    using System.ComponentModel.DataAnnotations;
    public class UeGenericAddressType : LocalizationModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "GenericAddressType.Name.Required")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public int? PropertyJsonId { get; set; }
    }
}
