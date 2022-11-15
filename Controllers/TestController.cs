using Microsoft.AspNetCore.Mvc;
using mysqlAPI.Model;
using mysqlAPI.Repository;

namespace mysqlAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TestController : ControllerBase
{

    private readonly ITestRepository _userRepository;

    public TestController(ITestRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("GetAllUsers")]
    [Produces(typeof(Test))]
    public IActionResult GetAll()
    {
        var users = _userRepository.GetAll();

        if (users.Count() == 0)
        return NoContent();

        return Ok(users);
    }
    [HttpGet("Get")]
    [Produces(typeof(Test))]
    public IActionResult Get(int id)
    {
        var user = _userRepository.Get(id);

        if (user == null)
            return NoContent();

        return Ok(user);
    }
    
    [HttpPut("Ibercson1")]
    public IActionResult UpdateTeste1(int id, string Teste1)
    {
        var response = _userRepository.UpdateTeste1(id, Teste1);

        return Ok(response);
    }
    [HttpPut("Ibercson2")]
    public IActionResult UpdateTeste2(int id, string Teste2)
    {
        var response = _userRepository.UpdateTeste2(id, Teste2);

        return Ok(response);
    }

}