using APILevelizd.DTO;
using APILevelizd.Models;
using APILevelizd.Repositories;
using APILevelizd.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace APILevelizd.Controllers;

[Route("games")]
[ApiController]
public class GamesController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GamesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]
    public ActionResult<IEnumerable<GameDTO>> GetGames()
    {
        IEnumerable<Game> games = _unitOfWork.GameRepository.GetAll();

        IEnumerable<GameDTO> gamesDto = _mapper.Map<IEnumerable<GameDTO>>(games);

        return Ok(gamesDto);
    }

    [HttpGet("{id:int}", Name = "GetGame")]
    public ActionResult<GameDTO> Get(int id)          
    {
        Game game = _unitOfWork.GameRepository.Get(g => g.GameId == id);


        if (game is null)
{            return NotFound("Jogo não encontrado...");}

        GameDTO gameDto = _mapper.Map<GameDTO>(game);

        return Ok(gameDto);
    }

    [HttpPost]
    public ActionResult<GameDTO> Post (GameDTO gameDto)
    {
        Game game = _mapper.Map<Game>(gameDto);

        Game newGame = _unitOfWork.GameRepository.Create(game);
        _unitOfWork.Commit();

        GameDTO newGameDto = _mapper.Map<GameDTO>(game);

        return new CreatedAtRouteResult("GetGame",
            new { id = newGameDto.GameId }, newGameDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<GameDTO> Put (int id, GameDTO gameDto)
    {
        if (gameDto.GameId != id)
        {
            return BadRequest("Dados inválidos. O id foi modificado.");
        }

        Game game = _mapper.Map<Game>(gameDto);

        Game updatedGame = _unitOfWork.GameRepository.Update(game);
        _unitOfWork.Commit();

        GameDTO updatedGameDto = _mapper.Map<GameDTO>(game);

        return Ok(updatedGameDto);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<GameDTO> Delete (int id)
    {
        Game game = _unitOfWork.GameRepository.Get(g => g.GameId == id);

        if (game is null)
        {
            return NotFound($"Não foi encontrado um game com o id = {id}");
        }

        _unitOfWork.GameRepository.Delete(game);
        _unitOfWork.Commit();

        return NoContent();
    }

}
