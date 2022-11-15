using System.ComponentModel.DataAnnotations;

namespace mysqlAPI.Model;

public class Test
{
    [Required]
    public int id {get; set;}

    [Required]
    public string? Teste1 {get; set;}

    [Required]
    public string? Teste2 {get; set;}

}
