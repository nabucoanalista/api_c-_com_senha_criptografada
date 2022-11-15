using mysqlAPI.Model;

namespace mysqlAPI.Repository;

public interface IUserRepository
{
    IEnumerable<User> GetAll();

    User Get(int id);
    int InsertUser(User newUser);
    int DeleteUser(int id);
    int UpdateUser(int id, string name);    
}