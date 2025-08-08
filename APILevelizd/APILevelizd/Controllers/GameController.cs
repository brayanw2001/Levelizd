using APILevelizd.Context;
using Microsoft.AspNetCore.Mvc;

namespace APILevelizd.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly AppDbContext _context;

    public GameController(AppDbContext context)     // vai receber uma instancia do appdbcontext, fazendo assim uma injeção de dependencia
    {
        _context = context;
    }

}
