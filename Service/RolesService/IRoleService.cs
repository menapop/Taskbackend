using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTOS.Dto;

namespace Service.RolesService
{
    public interface IRoleService
    {
        Task<List<OutPutRoleDto>> GetAll();
        Task<RoleEditViewDto> GetRole(int roleId);
        Task<bool> CreateRole(CreateRoleDto createRoleDto);
        bool UpdateRole(UpdateRoleDto updateRoleDto);
        Task<bool> DeleteRole(int id);
        
    }
}
