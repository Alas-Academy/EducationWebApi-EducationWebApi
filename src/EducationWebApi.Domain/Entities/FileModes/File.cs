using EducationWebApi.Domain.Common;

namespace EducationWebApi.Core.Entities.FileModes;

public class File : BaseAuditableEntity
{
    public string FileName { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string Storage { get; set; } = null!;
}
