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
        var reviews = _unitOfWork.ReviewRepository.GetAll().ToList();

        var reviewsDto = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);

        return Ok(reviewsDto);
    }

    [HttpGet("{id}", Name = "GetReview")]
    public ActionResult<ReviewDTO> Get(int id)
    {
        var review = _unitOfWork.ReviewRepository.Get(r => r.ReviewId == id);

        if (review is null)
            return NotFound($"A review de id = {id} não foi encontrada");

        var reviewDto = _mapper.Map<ReviewDTO>(review);

        return Ok(reviewDto);
    }


    [HttpPost]
    public ActionResult<ReviewDTO> Post(ReviewDTO reviewDto)
    {
        if (reviewDto is null)
            return BadRequest("Dados inválidos");

        var review = _mapper.Map<Review>(reviewDto);

        var newReview = _unitOfWork.ReviewRepository.Create(review);
        _unitOfWork.Commit();

        var newReviewDto = _mapper.Map<ReviewDTO>(newReview);

        return new CreatedAtRouteResult("GetReview",
            new { id = newReviewDto.ReviewId }, newReviewDto);
    }

    [HttpPut]
    public ActionResult<ReviewDTO> Put(int id, ReviewDTO reviewDto)
    {
        if (reviewDto.ReviewId != id)
            return BadRequest("Dados inválidos. O id foi modificado.");

        var review = _mapper.Map<Review>(reviewDto);

        var updatedReview = _unitOfWork.ReviewRepository.Update(review);
        _unitOfWork.Commit();

        var updatedReviewDto = _mapper.Map<ReviewDTO>(updatedReview);

        return Ok(updatedReviewDto);
    }

    [HttpDelete]
    public ActionResult<ReviewDTO> Delete (int id)
    {
        var review = _unitOfWork.ReviewRepository.Get(r => r.ReviewId == id);

        if (review is null)
            return NotFound($"Não foi encontrado um review com o id = {id}");

        var deletedReview = review;

        _unitOfWork.ReviewRepository.Delete(review);
        _unitOfWork.Commit();

        var deletedReviewDto = _mapper.Map<ReviewDTO>(deletedReview);

        return Ok(deletedReviewDto);
    }
}

