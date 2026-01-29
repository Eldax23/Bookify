using Bookify.Application.Apartments.SearchApartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.API.Controllers.Apartments;
[ApiController]
[Route("api/apartments")]
public class ApartmentController : ControllerBase
{
    private readonly ISender _sender;

    public ApartmentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchApartments(DateOnly startDate ,
        DateOnly endDate ,
        CancellationToken cancellationToken)
    {   
        SearchApartmentsQuery query = new(startDate, endDate);
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result);   
    }
}