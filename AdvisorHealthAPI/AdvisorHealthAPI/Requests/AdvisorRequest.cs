using System.ComponentModel.DataAnnotations;

namespace AdvisorHealthAPI.Requests;

public record AdvisorRequest([Required] string Name, [Required] int SinNumber,  string? Address, int? Phone);
