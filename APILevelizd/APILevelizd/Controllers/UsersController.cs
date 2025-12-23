using System.Collections.Generic;
using APILevelizd.DTO;
using APILevelizd.Models;
using APILevelizd.Repositories;
using APILevelizd.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APILevelizd.Controllers
{
    [Route("users")]             
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            IEnumerable<User> users = _unitOfWork.UserRepository.GetAll();

            IEnumerable<UserDTO> usersDto = _mapper.Map<IEnumerable<UserDTO>>(users);

            return Ok(usersDto);
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        public ActionResult<UserDTO> Get(int id)
        {
            User user = _unitOfWork.UserRepository.Get(u => u.UserId == id);

            if (user is null)
                return NotFound($"Não foi encontrado um usuário com id = {id}.");

            UserDTO userDto = _mapper.Map<UserDTO>(user);

            return Ok(userDto);
        }

        [HttpGet("/{userId:int}/reviews")]
        public ActionResult<IEnumerable<ReviewDTO>> GetUserReviews(int userId)
        {
            IEnumerable<Review> userReviews = _unitOfWork.UserRepository.UserReviews(userId);

            if (userReviews is null)
                return NotFound($"Não foram encontradas reviews para o usuário de id: {userId}");

            IEnumerable<ReviewDTO> userReviewsDto = _mapper.Map<IEnumerable<ReviewDTO>>(userReviews);


            return Ok(userReviewsDto);
        }

        [HttpPost]
        public ActionResult<UserDTO> Post(UserDTO userDto)
        {
            if (userDto is null)
                return BadRequest("Dados inválidos.");

            User user = _mapper.Map<User>(userDto);

            User newUser = _unitOfWork.UserRepository.Create(user);
            _unitOfWork.Commit();

            UserDTO newUserDto = _mapper.Map<UserDTO>(newUser);

            return new CreatedAtRouteResult("GetUser",
                new { id = newUserDto.UserId}, newUserDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<UserDTO> Put (int id, UserDTO userDto)
        {
            if (userDto.UserId != id)
                return BadRequest("Dados inválidos. O id foi modificado.");

            User user = _mapper.Map<User>(userDto);

            User updatedUser = _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Commit();

            UserDTO updatedUserDto = _mapper.Map<UserDTO>(updatedUser);

            return Ok(updatedUserDto);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<UserDTO> Delete (int id)
        {
            User user = _unitOfWork.UserRepository.Get(u => u.UserId == id);

            if (user is null)
                return NotFound($"Não foi encontrado um game com o id = {id}");

            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.Commit();

            return NoContent();
        }
    }
}
