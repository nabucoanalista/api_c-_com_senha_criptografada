using Dapper;
using mysqlAPI.Model;
using MySqlConnector;

namespace mysqlAPI.Repository;

public class TestRepository : ITestRepository
{

    private readonly string _connectionString;

    public TestRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Test> GetAll()
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            return connection.Query<Test>("SELECT * FROM users");
        };
    }
    public Test Get(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            var user = connection.QuerySingleOrDefault<Test>($"SELECT * FROM users WHERE id = {id}");
            return user;
        };
    }
    
    public int UpdateTeste1(int id, string Teste1)
    {
        var sql = "UPDATE users SET Teste1 = @Teste1 WHERE id = @id";

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            return connection.Execute(sql, new{ id, Teste1});
        };
    }
    public int UpdateTeste2(int id, string Teste2)
    {
        var sql = "UPDATE users SET Teste2 = @Teste2 WHERE id = @id";

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            return connection.Execute(sql, new{ id, Teste2});
        };
    }
}