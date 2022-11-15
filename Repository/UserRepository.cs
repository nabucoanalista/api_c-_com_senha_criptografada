using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using mysqlAPI.Model;
using MySqlConnector;

namespace mysqlAPI.Repository;

public class UserRepository : IUserRepository
{

    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<User> GetAll()
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            return connection.Query<User>("SELECT * FROM users");
        };
    }
    public User Get(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            var user = connection.QuerySingleOrDefault<User>($"SELECT * FROM users WHERE id = {id}");
            return user;
        };
    }
    public int InsertUser(User newUser)
    {
        var sql = "INSERT INTO users VALUES (@id, @Nome, @Idade, @Email, @Senha, @CPF)";

        User user = new User()
        {
            Nome = newUser.Nome,
            Idade = newUser.Idade,
            Email = newUser.Email,
            Senha = newUser.Senha,
            CPF = newUser.CPF,

        };

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            return connection.Execute(sql, user);
        };
    }
    public int UpdateUser(int id, string name)
    {
        var sql = "UPDATE users SET Nome = @Name WHERE id = @id";

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            return connection.Execute(sql, new{ id, name});
        };
    }
    public int DeleteUser(int id)
    {
        var sql = "DELETE FROM users WHERE id = @id";

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            return connection.Execute(sql, new {id});
        };
    }
 
}