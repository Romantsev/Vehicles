using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using BLL.Interfaces;
using BLL.Services;
using Core.Helpers;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vehicles.ViewModels;

namespace Vehicles.Controllers;

public class HomeController : Controller
{
    private readonly IOperationService _operationService;
    private readonly IReportService _reportService;
    private readonly IMailService _mailService;
    public HomeController(IOperationService operationService, IReportService reportService, IMailService mailService)
    {
        _operationService = operationService;
        _reportService = reportService;
        _mailService = mailService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var operations = _operationService.GetByPredicate();
        var indexViewModel = new IndexViewModel
        {
            Operations = operations
        };

        return View(indexViewModel);
    }

    [HttpGet]
    public IActionResult Delete(int operationId)
    {
        _operationService.DeleteOperation(operationId);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult DeleteAll()
    {
        _operationService.ClearAllOperations();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int operationId)
    {
        var operation = _operationService.GetOperationById(operationId);

        return View(operation);
    }

    [HttpPost]
    public IActionResult Edit(Operation operation, int year)
    {
        operation.MakeYear = new DateTime(year, 1, 1);
        _operationService.UpdateOperation(operation);

        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult Add()
    {
        var operation = new Operation();

        return View(operation);
    }

    [HttpPost]
    public IActionResult Add(Operation operation, int year)
    {
        operation.MakeYear = new DateTime(year, 1, 1);
        operation.OperationId = _operationService.GetByPredicate().ToList().Max(op => op.OperationId) + 1;

        _operationService.AddOperation(operation);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult SearchOperation(IndexViewModel indexViewModel)
    {
        var operations = _operationService.GetByPredicate();

        if (!string.IsNullOrEmpty(indexViewModel.Model))
        {
            operations = operations.Where(op => op.Model.ToLower().Contains(indexViewModel.Model.ToLower()))
                .ToList();
        }

        if (indexViewModel.FromYear != null)
        {
            operations = operations.Where(op => op.MakeYear.Year >= indexViewModel.FromYear).ToList();
        }

        if (indexViewModel.ToYear != null)
        {
            operations = operations.Where(op => op.MakeYear.Year <= indexViewModel.ToYear).ToList();
        }

        indexViewModel.Operations = operations;

        return View("Index", indexViewModel);
    }

    [HttpPost]
    public IActionResult GenerateReport(string operations)
    {
        var opList = JsonConvert.DeserializeObject<List<Operation>>(operations);

        if (opList.Count == 0)
        {
            return Ok("Not enough elements");
        }
        var html = GenerateHtmlReport(opList);
        if (html == null)
        {
            return NotFound();
        }

        var report = _reportService.GeneratePdfReport(html);
        if (report == null)
        {
            return NotFound();
        }

        return File(report, "application/pdf");
    }

    private string GenerateHtmlReport(List<Operation> operations)
    {
        var html = new StringBuilder();
        html.AppendLine("<style>");
        html.AppendLine("body, html, main, .container {");
        html.AppendLine("margin: 5px;");
        html.AppendLine("padding: 0;");
        html.AppendLine("width:  100%;");
        html.AppendLine("}");

        html.AppendLine(".operation {");
        html.AppendLine("border: 1px solid #dddddd;");
        html.AppendLine("padding: 8px;");
        html.AppendLine("margin-bottom: 10px;");
        html.AppendLine("}");

        html.AppendLine("</style>");

        foreach (var operation in operations)
        {
            html.AppendLine("<div class=\"operation\">");
            html.AppendLine($"<p><strong>Person:</strong> {operation.Person}</p>");
            html.AppendLine($"<p><strong>KOATUU:</strong> {operation.RegAddrKoatuu}</p>");
            html.AppendLine($"<p><strong>Operation Code:</strong> {operation.OperationCode}</p>");
            html.AppendLine($"<p><strong>Operation Name:</strong> {operation.OperationName}</p>");
            html.AppendLine($"<p><strong>Date Registered:</strong> {operation.DateReg.ToString("dd.MM.yyyy")}</p>");
            html.AppendLine($"<p><strong>Department Code:</strong> {operation.DepCode}</p>");
            html.AppendLine($"<p><strong>Department Name:</strong> {operation.DepartmentName}</p>");
            html.AppendLine($"<p><strong>Brand:</strong> {operation.Brand}</p>");
            html.AppendLine($"<p><strong>Model:</strong> {operation.Model}</p>");
            html.AppendLine($"<p><strong>VIN:</strong> {operation.Vin}</p>");
            html.AppendLine($"<p><strong>Make Year:</strong> {operation.MakeYear.ToString("yyyy")}</p>");
            html.AppendLine($"<p><strong>Color:</strong> {operation.Color}</p>");
            html.AppendLine($"<p><strong>Kind:</strong> {operation.Kind}</p>");
            html.AppendLine($"<p><strong>Body:</strong> {operation.Body}</p>");
            html.AppendLine($"<p><strong>Purpose:</strong> {operation.Purpose}</p>");
            html.AppendLine($"<p><strong>Fuel:</strong> {operation.Fuel}</p>");
            html.AppendLine($"<p><strong>Capacity:</strong> {operation.Capacity}</p>");
            html.AppendLine($"<p><strong>Own Weight:</strong> {operation.OwnWeight}</p>");
            html.AppendLine($"<p><strong>Total Weight:</strong> {operation.TotalWeight}</p>");
            html.AppendLine($"<p><strong>Num_reg_new:</strong> {operation.NumberRegNew}</p>");
            html.AppendLine("</div>");
        }

        return html.ToString();
    }

    [HttpGet]
    public IActionResult EnterEmail(int operationId)
    {
        var sendMailViewModel = new SendMailViewModel
        {
            OperationId = operationId
        };
        return View(sendMailViewModel);
    }

    [HttpPost]
    public IActionResult SendEmail(SendMailViewModel sendMailViewModel)
    {
        var operation = _operationService.GetByPredicate().First(op => op.OperationId == sendMailViewModel.OperationId);
        _mailService.SendEmail(operation, sendMailViewModel.Email);

        return RedirectToAction("Index", "Home");
    }

}
