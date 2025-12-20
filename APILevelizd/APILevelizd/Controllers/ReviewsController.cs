using APILevelizd.Models;
using APILevelizd.Repositories;
using APILevelizd.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APILevelizd.Controllers;

[Route("reviews")]
[ApiController]
public class ReviewsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Review>> GetReviews()
    {
        var reviews = _unitOfWork.ReviewRepository.GetAll().ToList();

        return Ok(reviews);
    }

    [HttpGet("{id}", Name = "ObterReview")]
    public ActionResult<Review> Get(int id)
    {
        var review = _unitOfWork.ReviewRepository.Get(r => r.ReviewId == id);

        if (review is null)
            return NotFound($"A review de id = {id} não foi encontrada");

        return Ok(review);
    }


    [HttpPost]
    public ActionResult<Review> Post(Review review)
    {
        if (review is null)
            return BadRequest("Dados inválidos");

        var reviewCriada = _unitOfWork.ReviewRepository.Create(review);

        return new CreatedAtRouteResult("ObterReview",
            new { id = reviewCriada.ReviewId }, reviewCriada);
    }

    [HttpPut]
    public ActionResult<Review> Put(int id, Review review)
    {
        if (review.ReviewId != id)
            return BadRequest("Dados inválidos. O id foi modificado.");

        _unitOfWork.ReviewRepository.Update(review);
        // considerar se não precisa de uma nova validação aqui
        _unitOfWork.Commit();
        return Ok(review);
    }

    [HttpDelete]
    public ActionResult<Review> Delete (int id)
    {
        var review = _unitOfWork.ReviewRepository.Get(r => r.ReviewId == id);

        if (review is null)
            return NotFound($"Não foi encontrado um review com o id = {id}");

        var reviewExcluida = review;

        _unitOfWork.ReviewRepository.Delete(review);
        _unitOfWork.Commit();

        return Ok(reviewExcluida);
    }
}

