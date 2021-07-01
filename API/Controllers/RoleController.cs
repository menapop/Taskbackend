
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Filters;
using DTOS.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.RolesService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        // GET: api/<RoleController>
        [HttpGet]
      
        public async Task<ActionResult<IEnumerable<OutPutRoleDto>>> GetAllRoles()
        {

            return Ok(await _roleService.GetAll());
        }
        [HttpGet]
        public async Task<ActionResult<RoleEditViewDto>> UpdateRole(int id)
        {

            return Ok(await _roleService.GetRole(id));
        }
        
        // GET api/<RoleController>/5
        [HttpPost]
        public async Task<ActionResult<bool>> AddRole(CreateRoleDto createRoleDto)
        {

            return Ok(await _roleService.CreateRole(createRoleDto));
        }
        [HttpPost]
        public ActionResult<bool> UpdateRole(UpdateRoleDto updateRoleDto)
        {

            return Ok(_roleService.UpdateRole(updateRoleDto));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> DeleteRole(int id)
        {
           
                return Ok( await _roleService.DeleteRole(id));
            
        }


    }
}
