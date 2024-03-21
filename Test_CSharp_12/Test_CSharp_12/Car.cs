using System.ComponentModel.DataAnnotations;

namespace Test_CSharp_12;

public class Car 
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required DateOnly ConfiguredDate { get; set; }
}
