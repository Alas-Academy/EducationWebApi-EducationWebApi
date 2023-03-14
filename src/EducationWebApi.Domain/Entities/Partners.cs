using EducationWebApi.Core.Entities.FileModes;
using EducationWebApi.Domain.Common;

namespace EducationWebApi.Core.Entities;
public class Partners : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public Guid PartnersImageId { get; set; }
    public virtual PartnersImageFile PartnersImage { get; set; } = null!;
}
