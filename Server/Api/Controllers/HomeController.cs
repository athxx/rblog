using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet("home/api")]
    public IEnumerable<HomeData> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new HomeData
            {
                Date = DateTime.Now.AddDays(index),
                Summary = "ssssssss"
            })
            .ToArray();
    }

    [HttpGet("/env")]
    public string GetEnv()
    {
        ;

        return "";
    }
}

public class HomeData
{
    public DateTime Date { get; set; }

    public string? Summary { get; set; }
}

public class Product
{
    public Guid pid { get; set; }
    public string pname { get; set; }
}
