using System.Linq.Expressions;
using APILevelizd.Context;
using APILevelizd.Models;
using APILevelizd.Repositories;
using APILevelizd.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace APILevelizd.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{

    private readonly IRepository<Game> _repository;

    public GameController(IRepository<Game> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IEnumerable<Game> GetAll()
    {
        return _repository.GetAll();
    }

    [HttpGet("{name}",Name = "ObterJogo")]
    public ActionResult<Game> Get(string name)
    {
        var game = _repository.Get(g => g.Name == name);

        if (game is null)
{            return NotFound("Jogo não encontrado...");}

        return game;
    }

    [HttpPost]
    public ActionResult<Game> Post (Game game)
    {
        var novoJogo = _repository.Create(game);

        return new CreatedAtRouteResult("ObterJogo",
            new { nome = game.Name });
    }

}
