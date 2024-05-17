using Core.Helpers;
using Core.Models;

namespace Vehicles.ViewModels;

public class IndexViewModel
{
    public List<Operation> Operations { get; set; }
    public string? Model { get; set; }
    public int? FromYear { get; set; }
    public int? ToYear { get; set; }
    public MailModel MailModel { get; set; }
}