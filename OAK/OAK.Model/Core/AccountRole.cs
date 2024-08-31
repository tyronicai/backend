namespace OAK.Model.Core
{
    using OAK.Model.BaseModels;
    public class AccountRole : ModelBase
    {
        public int AccountId { get; set; }
        public int RoleId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
    }
}