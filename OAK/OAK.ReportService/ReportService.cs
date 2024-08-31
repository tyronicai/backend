using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using OAK.Data;
using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.BusinessModels.TransportationModels;
using OAK.ServiceContracts;
/*
using PdfSharp.Drawing;
using PdfSharp.Pdf;
*/
namespace OAK.ReportServices
{
    public class ReportService:IReportService
    {

        public ILocalizationService LocalizationService { get; }
        private readonly IMapper _mapper;

        public ReportService(IMapper mapper)
        {
            _mapper = mapper;
        }

        async public Task<bool> SendDemandReportToCustomer(Demand demand, Transportation transportation)
        {
            bool retVal = true;
            /*
            PdfDocument document = new PdfDocument();
 
// Create an empty page
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            
                gfx.DrawString("Hello, World!", font, XBrushes.Black,
                    new XRect(0, 0, page.Width, page.Height),
                    XStringFormat.Center);

            string filename = "HelloWorld.pdf";
            document.Save(filename);
            */            
            return retVal;
        }

    }
}