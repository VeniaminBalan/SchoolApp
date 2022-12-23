using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Features.Test.Views;

public class TestsRequest
{
    [Required]public string Subject { get; set; }

    [Required]public DateTime TestDate { get; set; }
}