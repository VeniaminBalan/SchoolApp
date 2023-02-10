using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Features.Test.Views;

public class TestsRequest
{
    [Required]public string Title { get; set; }

    [Required]public DateTime TestDate { get; set; }
}