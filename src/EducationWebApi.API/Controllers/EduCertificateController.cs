using EducationWebApi.Application.Common.Models;
using EducationWebApi.Application.Features;
using Microsoft.AspNetCore.Mvc;

namespace EducationWebApi.API.Controllers;

public class EduCertificateController :  ApiControllerBase
{

    [HttpPost("[action]")]
    public async Task<ActionResult<Result>> AddCertificate([FromForm] CreateEduCertificateCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<GetAllEduCertificateDto>>> GetAllCertificate([FromQuery] GetAllEduCertificateQuery query)
    {
        return await Mediator.Send(query);
    }


}
