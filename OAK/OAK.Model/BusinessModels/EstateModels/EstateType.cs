namespace OAK.Model.BusinessModels.EstateModels
{
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    using System.ComponentModel.DataAnnotations;


    /// <summary>
    /// Estate Types:
    ///     - Wohnung:
    ///         - Property:
    ///             - InsideFlats: RadioButton :1. Single 2. Dublex 3. Triplex

    ///             - 
    ///             
    /// </summary>
    public class EstateType : LocalizationModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "EstateType.Name.Required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int? PropertyJsonId { get; set; }

        public virtual PropertyJson PropertyJson { get; set; }
    }
}
