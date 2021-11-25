using EFCoreMultipleDbContext.EntityFrameworkCore;
using EFCoreMultipleDbContext.Models;
using EFCoreMultipleDbContext.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreMultipleDbContext.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurdController : ControllerBase
    {
        private readonly Repository<ApplicationInfo, Guid> _applicationInfoRepository;
        public CurdController(MainDbContext dbContext)
        {
            _applicationInfoRepository = new Repository<ApplicationInfo, Guid>(dbContext);
        }

        [HttpPost("create")]
        public async Task<ApplicationInfo> CreateAsync(ApplicationInfo dto)
        {
            return await _applicationInfoRepository.CreateAsync(dto);
        }

    }
}