using EducationWebApi.Application.Common.Models;
using EducationWebApi.Application.Features;
using Microsoft.AspNetCore.Mvc;

namespace EducationWebApi.API.Controllers;

public class EduPartnersController : ApiControllerBase
{

    [HttpPost("[action]")]
    public async Task<ActionResult<Result>> AddPartner([FromForm] CreatePartnerCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePartner(Guid id)
    {
        await Mediator.Send(new DeletePartnerCommand(id));
        return NoContent();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<GetAllPartnersQueryDto>>> GetAllPartners([FromQuery] GetAllPartnersQuery query)
    {
        return await Mediator.Send(query);
    }
}
