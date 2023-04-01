using EducationWebApi.Application.Common.Models;
using EducationWebApi.Application.Features;
using Microsoft.AspNetCore.Mvc;

namespace EducationWebApi.API.Controllers;

public class CourseCoontroller : ApiControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult<Result>> AddCourse([FromForm] CreateCourseCommand command)
    {
        return await Mediator.Send(command);
    }    
    
    [HttpPost("[action]")]
    public async Task<ActionResult<Result>> AddCertificate([FromBody] AddCertificateCommand command)
    {
        return await Mediator.Send(command);
    }    
    
    [HttpPost("[action]")]
    public async Task<ActionResult<Result>> AddDetail([FromBody] AddCourseDetailCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<PaginatedList<GetAllCourseDto>>> GetCourses([FromQuery] GetAllCourseQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<CourseGetByIdQueryDto>> GetCourseById([FromQuery] CourseGetByIdQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(Guid id)
    {
        await Mediator.Send(new DeleteCourseCommand(id));
        return NoContent();
    }


}
