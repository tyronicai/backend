#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using MimeKit;
using OAK.Model.ConfigurationModels;
using OAK.Model.Core;
using OAK.ServiceContracts;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using IReportService = OAK.WebReport.Services.ServiceContracts.IReportService;

namespace OAK.WebReport.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportService _reportService;
        private readonly IEmailService _emailService;
        private static IRazorViewEngine _viewEngine;

        public ReportController(
            IReportService reportService,
            ILogger<ReportController> logger,
            IRazorViewEngine viewEngine,
            IEmailService emailService)
        {
            _reportService = reportService;
            _logger = logger;
            _viewEngine = viewEngine;
            _emailService = emailService;
        }

        public IActionResult DemandReport(int id, bool? isPdfRequest, string cultureName)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            string language = cultureName;
            if (language != "en" && language != "de")
            {
                return BadRequest("Unsupported CultureName");
            }

            var demandDetails = _reportService.GetDemandDetails(id);
            if (demandDetails == null)
            {
                return BadRequest();
            }

            var furnitures = _reportService.GetEstateDetailsAndFurnitures(demandDetails.FromEstateId).Items.ToList();

            foreach (var f in furnitures)
            {
                f.FurnitureName = _reportService.GetFurnitureName(f.LocalKey, language);
            }

            if (furnitures.Any() == false)
            {
                return BadRequest();
            }

            Account account = _reportService.GetAccount(demandDetails.AccountId);
            if (account == null)
            {
                return BadRequest();
            }

            var fromAddressData = _reportService.GetEstateAddress(demandDetails.FromAddressId);
            var toAddressData = _reportService.GetEstateAddress(demandDetails.ToAddressId);
            var fromAddress = $"{fromAddressData.Street} {fromAddressData.PlaceName} {fromAddressData.HouseNumber} {fromAddressData.PostCode}, {fromAddressData.Country.Name}";
            var toAddress = $"{toAddressData.Street} {toAddressData.PlaceName} {toAddressData.HouseNumber} {toAddressData.PostCode}, {toAddressData.Country.Name}";

            ViewData["Account"] = account;
            ViewData["Furnitures"] = furnitures;
            ViewData["Demand"] = demandDetails;
            ViewData["FromAddress"] = fromAddress;
            ViewData["ToAddress"] = toAddress;
            ViewData["Language"] = language;

            if (isPdfRequest == true)
            {
                return View(furnitures);
            }

            void Starter()
            {
                CreatePdf(id, account);
            }

            Thread tr = new Thread(Starter) { IsBackground = true };
            tr.Start();

            return View(furnitures);
        }

        private void SendMail(int demandId, Account account)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(account.Email, ""));
            message.Subject = "MeinUmzug24 - Demand Report";

            var body = new TextPart("plain")
            {
                Text = $@"Demand Report Test {account.FirstName} {account.LastName}
                        -- MeinUmzug24 Team"
            };

            var attachment = new MimePart("pdf/application")
            {
                Content = new MimeContent(
                    System.IO.File.OpenRead(DocumentSettings.PdfDocumentPath + $"{demandId}-DemandReport.pdf")),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName($"{demandId}-DemandReport.pdf")
            };

            var multipart = new Multipart("pdf/application") { body, attachment };

            message.Body = multipart;
            _emailService.Send(message);
        }

        private void CreatePdf(int demandId, Account account)
        {
            var filename = $"{demandId}-DemandReport";
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "wkhtmltopdf",
                    Arguments =
                        $"https://localhost:5001/report/DemandReport?id={demandId}&isPdfRequest=true {filename}.pdf",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            SendMail(demandId, account);
        }

        private string RenderViewToString(object model)
        {
            ViewData.Model = model;
            using StringWriter sw = new StringWriter();
            ViewEngineResult viewResult = _viewEngine.FindView(ControllerContext, "DemandReport", false);
            HtmlHelperOptions htmlHelperOptions = new HtmlHelperOptions();
            ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw,
                htmlHelperOptions);
            viewResult.View.RenderAsync(viewContext);
            return sw.GetStringBuilder().ToString();
        }
    }
}