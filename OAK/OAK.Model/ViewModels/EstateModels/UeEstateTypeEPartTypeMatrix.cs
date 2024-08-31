namespace OAK.Model.ViewModels.EstateModels
{
    /// <summary>
    /// Müstakil Ev, Daire, oda, garaj, depo etcs
    /// </summary>
    public class UeEstateTypeEPartType
    {
        public int Id { get; set; }

        public int EstateTypeId { get; set; }
        public int EstatePartTypeId { get; set; }

    }
}
