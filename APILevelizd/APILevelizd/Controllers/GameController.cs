using System.Linq.Expressions;
using APILevelizd.Context;
using APILevelizd.Models;
using APILevelizd.Repositories;
using APILevelizd.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace APILevelizd.Controllers;

[Route("[controller]")]
[ApiController]
public class GameController : ControllerBase
{

    private readonly IRepository<Game> _repository;

    public GameController(IRepository<Game> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Game>> GetAll()
    {
        var games = _repository.GetAll();
        return Ok(games);
    }

    [HttpGet("{name}", Name = "ObterJogo")]
    public ActionResult<Game> Get(string name)
    {
        var game = _repository.Get(g => g.Name == name);

        if (game is null)
{            return NotFound("Jogo não encontrado...");}

        return Ok(game);
    }

    [HttpPost]
    public ActionResult<Game> Post (Game game)
    {
        var novoJogo = _repository.Create(game);

        return new CreatedAtRouteResult("ObterJogo",
            new { name = game.Name }, novoJogo);
    }

    [HttpPut]
    public ActionResult<Game> Put (int id, Game game)
    {
        if (id != game.GameId)
        {
            return BadRequest("Dados inválidos. O id foi modificado.");
        }

        _repository.Update(game);
        // considerar se não precisa de uma nova validação aqui
        return Ok(game);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<Game> Delete (int id)
    {
        var game = _repository.Get(g => g.GameId == id);

        if (game is null)
        {
            return BadRequest("Não foi encontrado um game com o id = {id}");
        }

        var gameExcluido = game;
        _repository.Delete(game);

        return Ok(gameExcluido);

    }

}
