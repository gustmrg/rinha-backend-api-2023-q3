using Microsoft.AspNetCore.Mvc;

namespace RinhaDeBackEnd.API.Controllers;

[ApiController]
public class PersonController : ControllerBase
{
    [HttpPost]
    [Route("/pessoas")]
    public void CreatePerson()
    {

    }

    [HttpGet]
    [Route("/pessoas/{id:guid}")]
    public void GetPersonById()
    {
        
    }
}