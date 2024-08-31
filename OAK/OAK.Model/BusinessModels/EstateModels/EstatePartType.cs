namespace OAK.Model.BusinessModels.EstateModels
{
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    using System.ComponentModel.DataAnnotations;


    /// <summary>
    /// Müstakil Ev, Daire, oda, garaj, depo etcs
    /// </summary>
    public class EstatePartType : LocalizationModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "EstatePartType.Name.Required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsOuterPart { get; set; }

        public int? PropertyJsonId { get; set; }

        public virtual PropertyJson PropertyJson { get; set; }
    }
}
