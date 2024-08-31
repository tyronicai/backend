namespace OAK.Model.ConfigurationModels
{
    public class DocumentSettings
    {
        public string DataDocumentPath { get; set; }
        public string AssetDocumentPath { get; set; }
        public string GeodataDocumentPath { get; set; }
        public static string PdfDocumentPath { get; set; }
        public static string HtmlFilesDocumentPath { get; set; }
    }
}
