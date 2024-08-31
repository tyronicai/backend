using OAK.Model.Core;
using OAK.Model.ResultModels;
using OAK.Model.ViewModels.CoreModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class CountryListResMdl
    {
        public CountryListResMdl()
        {
            UeCountryList = new List<UeCountry>();
            ResultBaseMdl = new ResultBaseModel();
            UeSupportedPostCodeDataList = new List<PostCodeData>();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeCountry> UeCountryList { get; set; }
        public List<PostCodeData> UeSupportedPostCodeDataList { get; set; }
    }
}