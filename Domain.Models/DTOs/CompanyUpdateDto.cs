namespace Domain.Models.DTOs;

public record CompanyUpdateDto : CompanyForManipulationDto
{
    public int Id { get; set; }
}
