namespace OAK.Localizer
{
    public class SqlLocalizationOptions
    {
        /// <summary>
        /// Returns only the Key if the value is not found. If set to false, the search key in the database is returned.
        /// </summary>
        public bool ReturnOnlyKeyIfNotFound { get; set; }

        /// <summary>
        /// Creates a new item in the SQL database if the resource is not found
        /// </summary>
        public bool CreateNewRecordWhenLocalisedStringDoesNotExist { get; set; }

        /// <summary>
        /// You can set the required properties to set, get, display the different localization
        /// </summary>
        /// <param name="useTypeFullNames"></param>
        /// <param name="useOnlyPropertyNames"></param>
        /// <param name="returnOnlyKeyIfNotFound"></param>
        /// <param name="createNewRecordWhenLocalisedStringDoesNotExist"></param>
        public void UseSettings(bool returnOnlyKeyIfNotFound, bool createNewRecordWhenLocalisedStringDoesNotExist)
        {
            ReturnOnlyKeyIfNotFound = returnOnlyKeyIfNotFound;
            CreateNewRecordWhenLocalisedStringDoesNotExist = createNewRecordWhenLocalisedStringDoesNotExist;
        }
    }
}
