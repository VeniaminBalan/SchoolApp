using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Features.Test.Views;

public class TestsRequest
{
    [Required]public string Description { get; set; }

    public decimal Grade { get; set; }
}