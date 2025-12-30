using APILevelizd.DTO.Request;
using APILevelizd.DTO.Response;
using APILevelizd.Models;
using APILevelizd.Repositories;
using APILevelizd.Repositories.Interfaces;
using APILevelizd.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace APILevelizd.Controllers;

[Route("games")]
[ApiController]
public class GamesController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public GamesController(IUnitOfWork unitOfWork, IMapper mapper, [FromForm] IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileService = fileService;
    }


    [HttpGet]
    public ActionResult<IEnumerable<CreateGameDTO>> GetGames()
    {
        IEnumerable<Game> games = _unitOfWork.GameRepository.GetAll();

        IEnumerable<ResponseGameDTO> gamesDto = _mapper.Map<IEnumerable<ResponseGameDTO>>(games);

        return Ok(gamesDto);
    }

    [HttpGet("{id:int}", Name = "GetGame")]
    public ActionResult<CreateGameDTO> Get(int id)          
    {
        Game game = _unitOfWork.GameRepository.Get(g => g.GameId == id);


        if (game is null)
{            return NotFound("Jogo não encontrado...");}

        ResponseGameDTO gameDto = _mapper.Map<ResponseGameDTO>(game);

        return Ok(gameDto);
    }

    [HttpPost]
    public async Task<ActionResult<CreateGameDTO>> Post (CreateGameDTO gameDto)
    {
        if (gameDto.GameImage.Length > 3 * 1024 * 1024)
            return StatusCode(StatusCodes.Status413RequestEntityTooLarge, "A imagem do game não pode ser maior que 1MB.");

        string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
        string createdImageName  = await _fileService.SaveFileAsync(gameDto.GameImage, allowedFileExtentions);

        Game game = _mapper.Map<Game>(gameDto);
        game.GameImageUrl = createdImageName; // depois trocar ImageName por ImageURL

        Game newGame = _unitOfWork.GameRepository.Create(game);
        _unitOfWork.Commit();

        ResponseGameDTO newResponseGameDto = _mapper.Map<ResponseGameDTO>(game);

        return new CreatedAtRouteResult("GetGame",
            new { id = newResponseGameDto.GameId }, newResponseGameDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CreateGameDTO> Put (int id, CreateGameDTO gameDto)
    {
        if (gameDto.GameId != id)
        {
            return BadRequest("Dados inválidos. O id foi modificado.");
        }

        Game game = _mapper.Map<Game>(gameDto);

        Game updatedGame = _unitOfWork.GameRepository.Update(game);
        _unitOfWork.Commit();

        ResponseGameDTO updatedGameDto = _mapper.Map<ResponseGameDTO>(game);

        return Ok(updatedGameDto);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<CreateGameDTO> Delete (int id)
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
