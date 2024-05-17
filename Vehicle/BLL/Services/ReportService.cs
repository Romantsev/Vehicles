using BLL.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace BLL.Services;

public class ReportService: IReportService
{
    private readonly IConverter _converter;
    public ReportService(IConverter converter)
    {
        _converter = converter;
    }

    public byte[] GeneratePdfReport(string html)
    {
        var globalSettings = new GlobalSettings
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings { Top = 25, Bottom = 25 }
        };
        var objectSettings = new ObjectSettings
        {
            PagesCount = true,
            HtmlContent = html
        };
        var webSettings = new WebSettings
        {
            DefaultEncoding = "utf-8"
        };
        var headerSettings = new HeaderSettings
        {
            FontSize = 15,
            FontName = "Ariel",
            Right = "Page [page] of [toPage]",
            Line = true
        };
        var footerSettings = new FooterSettings
        {
            FontSize = 12,
            FontName = "Ariel",
            Center = "1",
            Line = true
        };
        objectSettings.HeaderSettings = headerSettings;
        objectSettings.FooterSettings = footerSettings;
        objectSettings.WebSettings = webSettings;
        var htmlToPdfDocument = new HtmlToPdfDocument
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings },
        };
        return _converter.Convert(htmlToPdfDocument);
    }

}
