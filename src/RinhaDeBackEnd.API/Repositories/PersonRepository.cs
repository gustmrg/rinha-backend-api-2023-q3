using System.Data;
using Dapper;
using Npgsql;
using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;

    public PersonRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("DockerConnection"));
    }
    
    public IEnumerable<Person> GetAllPersons()
    {
        string sqlCommand = "SELECT id, nickname, name, date_of_birth FROM persons";

        var persons = _dbConnection.Query<Person>(sqlCommand);  
        
        _dbConnection.Dispose();
        
        return persons;
    }
    
    public Person GetPersonById(Guid id)
    {
        string sqlCommand = "SELECT id, nickname, name, date_of_birth FROM persons WHERE id = @Id";

        var person = _dbConnection.QuerySingleOrDefault<Person>(sqlCommand, new { Id = id });
        
        _dbConnection.Dispose();
        
        return person;
    }
    
    public Person CreatePerson(Person person)
    {
        person.Id = Guid.NewGuid();
 
        string sqlCommand = "INSERT INTO persons VALUES (:id, :nickname,:name, :date_of_birth)";
        
        _dbConnection.Execute(sqlCommand, new
        {
            id = person.Id,
            nickname = person.Nickname,
            name = person.Name,
            date_of_birth = person.DateOfBirth.ToDateTime(TimeOnly.MinValue)
        });
        
        _dbConnection.Dispose();

        return person;
    }

    public void UpdatePerson()
    {
        throw new NotImplementedException(); 
    }

    public void DeletePerson()
    {
        throw new NotImplementedException();
    }

    public int PersonCount()
    {
        string sqlCommand = "SELECT COUNT(*) FROM persons";

        var count = _dbConnection.ExecuteScalar<int>(sqlCommand);

        return count;
    }
}