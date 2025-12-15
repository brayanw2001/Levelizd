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



}

