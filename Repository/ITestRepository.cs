using mysqlAPI.Model;

namespace mysqlAPI.Repository;

public interface ITestRepository
{
    IEnumerable<Test> GetAll();
    Test Get(int id);
    int UpdateTeste1(int id, string Teste1);
    int UpdateTeste2(int id, string Teste2);
}