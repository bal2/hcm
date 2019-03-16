using hcm.Services;
using Microsoft.AspNetCore.Mvc;

namespace hcm.Controllers.Roles
{
    [Route("api/roles"), ApiController]
    public class RoleController : Controller
    {
        private RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        
    }
}