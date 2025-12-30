using System.Collections.Generic;
using APILevelizd.DTO.Request;
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
        public ActionResult<IEnumerable<CreateUserDTO>> GetUsers()
        {
            IEnumerable<User> users = _unitOfWork.UserRepository.GetAll();

            IEnumerable<CreateUserDTO> usersDto = _mapper.Map<IEnumerable<CreateUserDTO>>(users);

            return Ok(usersDto);
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        public ActionResult<CreateUserDTO> Get(int id)
        {
            User user = _unitOfWork.UserRepository.Get(u => u.UserId == id);

            if (user is null)
                return NotFound($"Não foi encontrado um usuário com id = {id}.");

            CreateUserDTO userDto = _mapper.Map<CreateUserDTO>(user);

            return Ok(userDto);
        }

        [HttpGet("/{userId:int}/reviews")]
        public ActionResult<IEnumerable<CreateReviewDTO>> GetUserReviews(int userId)
        {
            IEnumerable<Review> userReviews = _unitOfWork.UserRepository.UserReviews(userId);

            if (userReviews is null)
                return NotFound($"Não foram encontradas reviews para o usuário de id: {userId}");

            IEnumerable<CreateReviewDTO> userReviewsDto = _mapper.Map<IEnumerable<CreateReviewDTO>>(userReviews);


            return Ok(userReviewsDto);
        }

        [HttpPost]
        public ActionResult<CreateUserDTO> Post(CreateUserDTO userDto)
        {
            if (userDto is null)
                return BadRequest("Dados inválidos.");

            User user = _mapper.Map<User>(userDto);

            User newUser = _unitOfWork.UserRepository.Create(user);
            _unitOfWork.Commit();

            CreateUserDTO newUserDto = _mapper.Map<CreateUserDTO>(newUser);

            return new CreatedAtRouteResult("GetUser",
                new { id = newUserDto.UserId}, newUserDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CreateUserDTO> Put (int id, CreateUserDTO userDto)
        {
            if (userDto.UserId != id)
                return BadRequest("Dados inválidos. O id foi modificado.");

            User user = _mapper.Map<User>(userDto);

            User updatedUser = _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Commit();

            CreateUserDTO updatedUserDto = _mapper.Map<CreateUserDTO>(updatedUser);

            return Ok(updatedUserDto);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<CreateUserDTO> Delete (int id)
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
