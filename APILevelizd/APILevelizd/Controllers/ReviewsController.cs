using APILevelizd.DTO;
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
    public ActionResult<IEnumerable<ReviewDTO>> GetReviews()
    {
        IEnumerable<Review> reviews = _unitOfWork.ReviewRepository.GetAll().ToList();

        IEnumerable<ReviewDTO> reviewsDto = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);

        return Ok(reviewsDto);
    }

    [HttpGet("{id}", Name = "GetReview")]
    public ActionResult<ReviewDTO> Get(int id)
    {
        Review review = _unitOfWork.ReviewRepository.Get(r => r.ReviewId == id);

        if (review is null)
            return NotFound($"A review de id = {id} não foi encontrada");

        ReviewDTO reviewDto = _mapper.Map<ReviewDTO>(review);

        return Ok(reviewDto);
    }


    [HttpPost]
    public ActionResult<ReviewDTO> Post(ReviewDTO reviewDto)
    {
        if (reviewDto is null)
            return BadRequest("Dados inválidos");

        Review review = _mapper.Map<Review>(reviewDto);

        Review newReview = _unitOfWork.ReviewRepository.Create(review);
        _unitOfWork.Commit();

        ReviewDTO newReviewDto = _mapper.Map<ReviewDTO>(newReview);

        return new CreatedAtRouteResult("GetReview",
            new { id = newReviewDto.ReviewId }, newReviewDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ReviewDTO> Put(int id, ReviewDTO reviewDto)
    {
        if (reviewDto.ReviewId != id)
            return BadRequest("Dados inválidos. O id foi modificado.");

        Review review = _mapper.Map<Review>(reviewDto);

        Review updatedReview = _unitOfWork.ReviewRepository.Update(review);
        _unitOfWork.Commit();

        ReviewDTO updatedReviewDto = _mapper.Map<ReviewDTO>(updatedReview);

        return Ok(updatedReviewDto);
    }

    [HttpDelete]
    public ActionResult<ReviewDTO> Delete (int id)
    {
        Review review = _unitOfWork.ReviewRepository.Get(r => r.ReviewId == id);

        if (review is null)
            return NotFound($"Não foi encontrado um review com o id = {id}");

        _unitOfWork.ReviewRepository.Delete(review);
        _unitOfWork.Commit();

        return NoContent();
    }
}

