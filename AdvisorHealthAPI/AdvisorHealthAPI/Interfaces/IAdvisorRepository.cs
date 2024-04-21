using AdvisorHealthAPI.Caching;
using AdvisorHealthAPI.Models;
using AdvisorHealthAPI.Requests;

namespace AdvisorHealthAPI.Interfaces;

public interface IAdvisorRepository
{
    Task<IEnumerable<Advisor>> GetAdvisors();
    Task<Advisor?> GetAdvisor(Guid id, LRUCache<string, Advisor> cache);
    Task<Advisor> AddAdvisor(AdvisorRequest request, LRUCache<string, Advisor> cache);
    Task<Advisor?> UpdateAdvisor(Guid id, AdvisorRequest request, LRUCache<string, Advisor> cache);
    Task<bool> DeleteAdvisor(Guid id, LRUCache<string, Advisor> cache);
}
