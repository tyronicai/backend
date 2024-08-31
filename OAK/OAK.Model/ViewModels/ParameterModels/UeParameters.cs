namespace OAK.Model.ViewModels.ParameterModels
{
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    using System.ComponentModel.DataAnnotations;
    public class UeParameters : LocalizationModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Parameters.Name.Required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Parameters.DriverHourRate.Required")]
        public decimal SprinterAvgVelocity { get; set; }

        [Required(ErrorMessage = "Parameters.DriverHourRate.Required")]
        public decimal SprinterAvgOilConsumption { get; set; }

        [Required(ErrorMessage = "Parameters.DriverHourRate.Required")]
        public decimal SprinterMaxVolume { get; set; }

        [Required(ErrorMessage = "Parameters.DriverHourRate.Required")]
        public decimal SprinterDailyKM { get; set; }

        [Required(ErrorMessage = "Parameters.DriverHourRate.Required")]
        public decimal LKWAvgVelocity { get; set; }

        [Required(ErrorMessage = "Parameters.DriverHourRate.Required")]
        public decimal LKWAvgOilConsumption { get; set; }

        [Required(ErrorMessage = "Parameters.DriverHourRate.Required")]
        public decimal LKWMaxVolume { get; set; }

        [Required(ErrorMessage = "Parameters.DriverHourRate.Required")]
        public decimal LKWDailyKM { get; set; }

        public int PropertyJsonId { get; set; }

        public virtual PropertyJson PropertyJson { get; set; }
    }
}
