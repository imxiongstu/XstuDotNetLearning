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
        private readonly IRepository<UserInfo> _userInfoRepository;
        public CurdController(IRepository<UserInfo> userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        [HttpPost("create")]
        public async Task<UserInfo> CreateUserInfoAsync(UserInfo dto)
        {
            return await _userInfoRepository.CreateAsync(dto);
        }

    }
}