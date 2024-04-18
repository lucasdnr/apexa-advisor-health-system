namespace AdvisorHealthAPI.Response;

public record AdvisorResponse(Guid Id, string Name, int SinNumber, string Address, int? Phone, string HealthStatus);
