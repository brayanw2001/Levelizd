using System.Linq.Expressions;
using APILevelizd.Context;
using APILevelizd.Models;
using APILevelizd.Repositories;
using APILevelizd.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace APILevelizd.Controllers;

[Route("games")]
[ApiController]
public class GamesController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;

    public GamesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Game>> GetGames()
    {
        var games = _unitOfWork.GameRepository.GetAll();
        return Ok(games);
    }

    [HttpGet("{id:int}", Name = "ObterJogo")]
    public ActionResult<Game> Get(int id)          // trocar para id?
    {
        var game = _unitOfWork.GameRepository.Get(g => g.GameId == id);

        if (game is null)
{            return NotFound("Jogo não encontrado...");}

        return Ok(game);
    }

    [HttpPost]
    public ActionResult<Game> Post (Game game)
    {
        var novoJogo = _unitOfWork.GameRepository.Create(game);

        return new CreatedAtRouteResult("ObterJogo",
            new { name = game.Name }, novoJogo);
    }

    [HttpPut]
    public ActionResult<Game> Put (int id, Game game)
    {
        if (game.GameId != id)
        {
            return BadRequest("Dados inválidos. O id foi modificado.");
        }

        _unitOfWork.GameRepository.Update(game);
        _unitOfWork.Commit();
        // considerar se não precisa de uma nova validação aqui
        return Ok(game);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<Game> Delete (int id)
    {
        var game = _unitOfWork.GameRepository.Get(g => g.GameId == id);

        if (game is null)
        {
            return NotFound($"Não foi encontrado um game com o id = {id}");
        }

        var gameExcluido = game;
        _unitOfWork.GameRepository.Delete(game);
        _unitOfWork.Commit();

        return Ok(gameExcluido);

    }

}
