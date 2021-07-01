using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using DTOS.Dto;
using Repo.Repository.RoleRepository;
using Repo.UnitOfWork;

namespace Service.RolesService
{
    public class RoleService : IRoleService
    {
        
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRoleRepository _roleRepository;
        public RoleService(IMapper mapper, IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }
  

        public async Task<bool> CreateRole(CreateRoleDto createRoleDto)
        {
            var role = _mapper.Map<Role>(createRoleDto);
           // role.RolePermissions = createRoleDto.permissions.Select(r => new RolePermission { ActionId = r }).ToList();
            await _roleRepository.InsertAsync(role);
            _unitOfWork.Commit();
            return true;
        }


        public async Task<RoleEditViewDto> GetRole(int roleId)
        {
            var role = await _roleRepository.GetAsync(r => r.Id == roleId, "RolePermissions");
            var data = _mapper.Map<RoleEditViewDto>(role);
            return data;
        }


        public bool UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var role = _mapper.Map<Role>(updateRoleDto);
           // role.RolePermissions = updateRoleDto.permissions.Select(r => new RolePermission { ActionId = r }).ToList();
            _roleRepository.Update(role);
            _unitOfWork.Commit();
            return true;
        }

        public async Task<List<OutPutRoleDto>>GetAll()
        {
            var data = _mapper.Map<List<OutPutRoleDto>>(await _roleRepository.GetAllAsync(r => r.IsDeleted != true));
            return data;
        }

        public  async Task<bool> DeleteRole(int id)
        {
          
                var role = await _roleRepository.GetAsync(r => r.Id == id);
                _roleRepository.Delete(role);
                _unitOfWork.Commit();
                return true;
  
           
        }
    }
}
