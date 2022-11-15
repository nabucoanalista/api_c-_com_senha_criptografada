using Microsoft.AspNetCore.Mvc;
using mysqlAPI.Model;
using mysqlAPI.Repository;
using System.Security.Cryptography;
using System.Text;

namespace mysqlAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController : ControllerBase
{

    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("GetAllUsers")]
    [Produces(typeof(User))]
    public IActionResult GetAll()
    {
        var users = _userRepository.GetAll();

        if (users.Count() == 0)
        return NoContent();

        return Ok(users);
    }
    [HttpGet("Get")]
    [Produces(typeof(User))]
    public IActionResult Get(int id)
    {
        var user = _userRepository.Get(id);

        if (user == null)
            return NoContent();

        return Ok(user);
    }
    [HttpPost("Post")]
    public IActionResult Post(User newUser)
    {
        var SenhaMD5 = RetornarMD5(newUser.Senha);
        newUser.Senha = SenhaMD5;
        // return Ok(newUser.Senha);
        var response = _userRepository.InsertUser(newUser);
        return Ok(response);
    }
    [HttpPut("Put")]
    public IActionResult Update(int id, string name)
    {
        var response = _userRepository.UpdateUser(id, name);

        return Ok(response);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        var response = _userRepository.DeleteUser(id);

        return Ok(response);
    }    
    // criamos uma função privada
    // que utiliza a classe MD5
    // por isso usamos "using System.Security.Cryptography;"
    // e ela também recebe uma string input, que é justamente o texto que iremos criptografar
    private string RetornarHash(MD5 md5Hash, string input)
    {
        // criamos um array de bytes utilizando o computeHash da classe md5, e recebendo os bytes do texto do input
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        // vamos contruir uma string
        StringBuilder sBuilder = new StringBuilder();
        // vamoe percorrer o array de bytes transformado em hexadecimal
        for(uint i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("X2"));
        }
        // retornamos a string em hexa
        return sBuilder.ToString();
    }
    // vamos criar a função que será utilizada para retornar a senha md5 de forma simples
    private string RetornarMD5(string Senha)
    {
        using (MD5 md5Hash = MD5.Create())
        {
            return RetornarHash(md5Hash, Senha);
        }
    }
    // criamos agora a função de verificar as senhas
    private bool VerificarHash(string input, string hash)
    {
        // usaremos o string comparer para fazer com o ordinal ignore case para os caracteres maiusculos serem comparados sem problema com o minusculo
        StringComparer comparar = StringComparer.OrdinalIgnoreCase;
        // fazemos um if ja função compare, se retornar 0, é pq são iguais
        if(comparar.Compare(input, hash) == 0)
        {
            return true;
        }
        return false;
    }
    // e aqui fazemos a função de verificar as senhas como md5, essa que será a função usada na aplicação.
    private bool CompararMd5(string SenhaDB, string SenhaForm)
    {
        string senha = RetornarMD5(SenhaForm);
        if(VerificarHash(SenhaDB, senha))
        {
            return true;
        }
        return false;
    }
}