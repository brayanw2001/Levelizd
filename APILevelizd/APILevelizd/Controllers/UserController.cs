using APILevelizd.Models;
using APILevelizd.Repositories;
using APILevelizd.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APILevelizd.Controllers
{
    [Route("[controller]")]             
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<User> GetUsers()
        {
            var users = _repository.GetAll();

            return Ok(users);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterUser")]
        public ActionResult<User> Get(int id)
        {
            var user = _repository.Get(u => u.UserId == id);

            if (user is null)
                return NotFound($"Não foi encontrado um usuários com id = {id}.");

            return Ok(user);
        }

        [HttpGet("userreviews")]
        public ActionResult<User> GetUserReviews(string user)
        {
            var reviews = _repository.UserReviews(user);

            if (reviews is null)
                return NotFound($"Não foi encontrado um usuário com nome {user}");

            return Ok(reviews);
        }

        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            if (user is null)
                return BadRequest("Dados inválidos.");

            var novoUsuario = _repository.Create(user);

            return new CreatedAtRouteResult("ObterUser",
                new { id = user.UserId}, novoUsuario);
        }

        [HttpPut("{id:int}")]
        public ActionResult<User> Put (int id, User user)
        {
            if (user.UserId != id)
                return BadRequest("Dados inválidos. O id foi modificado.");

            _repository.Update(user);

            return Ok(user);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<User> Delete (int id)
        {
            var user = _repository.Get(u => u.UserId == id);

            if (user is null)
                return NotFound($"Não foi encontrado um game com o id = {id}");

            var userExcluido = user;

            _repository.Delete(user);

            return (userExcluido);
        }
    }
}
