namespace Fansoft.PocArc.Api.Domain.Entities;

public class Customer : EntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
}