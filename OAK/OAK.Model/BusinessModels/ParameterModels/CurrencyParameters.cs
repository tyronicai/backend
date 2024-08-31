namespace OAK.Model.BusinessModels.ParameterModels
{
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    using System.ComponentModel.DataAnnotations;
    public class CurrencyParameters : LocalizationModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CurrencyParameters.Name.Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CurrencyParameters.CurrencyName.Required")]
        public string CurrencyName { get; set; }

        [Required(ErrorMessage = "CurrencyParameters.CurrencyShortCode.Required")]
        public string CurrencyShortCode { get; set; }

        public string CurrencySymbol { get; set; }

        [Required(ErrorMessage = "CurrencyParameters.WorkerHourRate.Required")]
        public decimal WorkerHourRate { get; set; }

        [Required(ErrorMessage = "CurrencyParameters.DriverHourRate.Required")]
        public decimal DriverHourRate { get; set; }

        [Required(ErrorMessage = "CurrencyParameters.DailyRoomRate.Required")]
        public decimal DailyRoomRate { get; set; }

        [Required(ErrorMessage = "CurrencyParameters.PricePerKilometer.Required")]
        public decimal PricePerKilometer { get; set; }

        [Required(ErrorMessage = "CurrencyParameters.SprinterDailyRentPrice.Required")]
        public decimal SprinterDailyRentPrice { get; set; }

        [Required(ErrorMessage = "CurrencyParameters.LKWDailyRentPrice.Required")]
        public decimal LKWDailyRentPrice { get; set; }

        [Required(ErrorMessage = "CurrencyParameters.OilPrice.Required")]
        public decimal OilPrice { get; set; }

        public int PropertyJsonId { get; set; }

        public virtual PropertyJson PropertyJson { get; set; }
    }
}
