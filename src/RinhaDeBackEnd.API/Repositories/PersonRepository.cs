using Dapper;
using Npgsql;
using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Repositories;

public class PersonRepository
{
    private IConfiguration _configuration;

    public PersonRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Person> GetAllPersons()
    {
        var connectionString = _configuration.GetConnectionString("DockerConnection");

        using var connection = new NpgsqlConnection(connectionString);
        string sqlCommand =
            "SELECT id AS \"Id\", nickname AS \"Nickname\", name AS \"Name\", date_of_birth AS \"DateOfBirth\" FROM persons";
        
        var persons =
            connection.Query<Person>(sqlCommand);  
        
        return persons;
    }
    
    public void GetPersonById()
    {
        throw new NotImplementedException();
    }
    
    public void CreatePerson()
    {
        throw new NotImplementedException(); 
    }

    public void UpdatePerson()
    {
        throw new NotImplementedException(); 
    }

    public void DeletePerson()
    {
        throw new NotImplementedException();
    }
}