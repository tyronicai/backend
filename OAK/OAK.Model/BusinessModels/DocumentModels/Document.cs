using OAK.Model.BaseModels;

namespace OAK.Model.BusinessModels.DocumentModels
{
    public class Document : ModelBase
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public string PropertyValues { get; set; }

    }
}
