using APILevelizd.Models;
using APILevelizd.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APILevelizd.Controllers;

[Route("[Controller]")]
[ApiController]
public class ReviewController : Controller
{
    private readonly IRepository<Review> _repository;

    public ReviewController(IRepository<Review> repository)
    {
        _repository = repository;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Review>> GetReviews()
    {
        var reviews = _repository.GetAll().ToList();

        return Ok(reviews);
    }

    [HttpGet("{name}", Name = "ObterReview")]
    public ActionResult<Review> Get(int id)
    {
        var review = _repository.Get(r => r.ReviewId == id);

        if (review is null)
            return NotFound($"A review de id = {id} não foi encontrada");

        return Ok(review);
    }


    [HttpPost]
    public ActionResult<Review> Post(Review review)
    {
        if (review is null)
            return BadRequest("Dados inválidos");

        var reviewCriada = _repository.Create(review);

        return new CreatedAtRouteResult("ObterReview",
            new { id = reviewCriada.ReviewId }, reviewCriada);
    }

    [HttpPut]
    public ActionResult<Review> Put(int id, Review review)
    {
        if (review.ReviewId != id)
            return BadRequest("Dados inválidos. O id foi modificado.");

        _repository.Update(review);
        // considerar se não precisa de uma nova validação aqui
        return Ok(review);
    }

    [HttpDelete]
    public ActionResult<Review> Delete (int id)
    {
        var review = _repository.Get(r => r.ReviewId == id);

        if (review is null)
            return NotFound($"Não foi encontrado um review com o id = {id}");

        var reviewExcluida = review;

        _repository.Delete(review);

        return Ok(reviewExcluida);
    }
}

