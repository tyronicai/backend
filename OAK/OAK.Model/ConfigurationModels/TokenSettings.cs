namespace OAK.Model.ConfigurationModels
{
    public class TokenSettings
    {
        public string ClaimSecret { get; set; }

        /// <summary>
        /// days
        /// </summary>
        public int TokenExpires { get; set; }
    }
}
