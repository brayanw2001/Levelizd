using APILevelizd.DTO.Request;
using APILevelizd.Models;
using APILevelizd.Repositories;
using APILevelizd.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APILevelizd.Controllers;

[Route("reviews")]
[ApiController]
public class ReviewsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReviewsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CreateReviewDTO>> GetReviews()
    {
        IEnumerable<Review> reviews = _unitOfWork.ReviewRepository.GetAll().ToList();

        IEnumerable<CreateReviewDTO> reviewsDto = _mapper.Map<IEnumerable<CreateReviewDTO>>(reviews);

        return Ok(reviewsDto);
    }

    [HttpGet("{id}", Name = "GetReview")]
    public ActionResult<CreateReviewDTO> Get(int id)
    {
        Review review = _unitOfWork.ReviewRepository.Get(r => r.ReviewId == id);

        if (review is null)
            return NotFound($"A review de id = {id} não foi encontrada");

        CreateReviewDTO reviewDto = _mapper.Map<CreateReviewDTO>(review);

        return Ok(reviewDto);
    }


    [HttpPost]
    public ActionResult<CreateReviewDTO> Post(CreateReviewDTO reviewDto)
    {
        if (reviewDto is null)
            return BadRequest("Dados inválidos");

        Review review = _mapper.Map<Review>(reviewDto);

        Review newReview = _unitOfWork.ReviewRepository.Create(review);
        _unitOfWork.Commit();

        CreateReviewDTO newReviewDto = _mapper.Map<CreateReviewDTO>(newReview);

        return new CreatedAtRouteResult("GetReview",
            new { id = newReviewDto.ReviewId }, newReviewDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CreateReviewDTO> Put(int id, CreateReviewDTO reviewDto)
    {
        if (reviewDto.ReviewId != id)
            return BadRequest("Dados inválidos. O id foi modificado.");

        Review review = _mapper.Map<Review>(reviewDto);

        Review updatedReview = _unitOfWork.ReviewRepository.Update(review);
        _unitOfWork.Commit();

        CreateReviewDTO updatedReviewDto = _mapper.Map<CreateReviewDTO>(updatedReview);

        return Ok(updatedReviewDto);
    }

    [HttpDelete]
    public ActionResult<CreateReviewDTO> Delete (int id)
    {
        Review review = _unitOfWork.ReviewRepository.Get(r => r.ReviewId == id);

        if (review is null)
            return NotFound($"Não foi encontrado um review com o id = {id}");

        _unitOfWork.ReviewRepository.Delete(review);
        _unitOfWork.Commit();

        return NoContent();
    }
}

