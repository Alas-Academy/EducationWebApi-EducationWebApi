

using EducationWebApi.Shared.Services.Impl;

namespace EducationWebApi.Shared.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
