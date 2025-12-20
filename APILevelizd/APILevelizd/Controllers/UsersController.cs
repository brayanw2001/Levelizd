using APILevelizd.Models;
using APILevelizd.Repositories;
using APILevelizd.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APILevelizd.Controllers
{
    [Route("users")]             
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<User> GetUsers()
        {
            var users = _unitOfWork.UserRepository.GetAll();

            return Ok(users);
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        public ActionResult<User> Get(int id)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.UserId == id);

            if (user is null)
                return NotFound($"Não foi encontrado um usuários com id = {id}.");

            return Ok(user);
        }

        [HttpGet("/{userId:int}/reviews")]
        public ActionResult<User> GetUserReviews(int userId)
        {
            var reviews = _unitOfWork.UserRepository.UserReviews(userId);

            if (reviews is null)
                return NotFound($"Não foi encontrado um usuário com nome {userId}");

            return Ok(reviews);
        }

        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            if (user is null)
                return BadRequest("Dados inválidos.");

            var novoUsuario = _unitOfWork.UserRepository.Create(user);

            return new CreatedAtRouteResult("ObterUser",
                new { id = user.UserId}, novoUsuario);
        }

        [HttpPut("{id:int}")]
        public ActionResult<User> Put (int id, User user)
        {
            if (user.UserId != id)
                return BadRequest("Dados inválidos. O id foi modificado.");

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Commit();

            return Ok(user);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<User> Delete (int id)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.UserId == id);

            if (user is null)
                return NotFound($"Não foi encontrado um game com o id = {id}");

            var userExcluido = user;

            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.Commit();

            return (userExcluido);
        }
    }
}
