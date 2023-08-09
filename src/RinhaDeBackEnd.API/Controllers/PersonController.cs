using Microsoft.AspNetCore.Mvc;
using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Controllers;

[ApiController]
public class PersonController : ControllerBase
{
    [HttpPost]
    [Route("/pessoas")]
    public void CreatePerson()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("/pessoas/{id:guid}")]
    public void GetPersonById(Guid id)
    {
        throw new NotImplementedException();
    }
}