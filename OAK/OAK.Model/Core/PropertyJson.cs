namespace OAK.Model.Core
{
    using OAK.Model.BusinessModels.AddressModels;
    using OAK.Model.BusinessModels.CommentModels;
    using OAK.Model.BusinessModels.DemandModels;
    using OAK.Model.BusinessModels.DocumentModels;
    using OAK.Model.BusinessModels.EstateModels;
    using OAK.Model.BusinessModels.TransportationModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PropertyJson
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string JsonObject { get; set; }

        public virtual ICollection<FurnitureType> FurnitureTypes { get; set; }
        public virtual ICollection<TransportationType> TransportationTypes { get; set; }
        public virtual ICollection<EstateType> EstateTypes { get; set; }
        public virtual ICollection<EstatePartType> EstatePartTypes { get; set; }
        public virtual ICollection<CommentType> CommentTypes { get; set; }
        public virtual ICollection<DemandType> DemandTypes { get; set; }
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
        public virtual ICollection<GenericAddressType> GenericAddressTypes { get; set; }
    }
}