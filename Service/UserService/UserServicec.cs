using AutoMapper;
using Data.Entities;
using DTOS;
using DTOS.Dto;
using DTOS.UserDtos;
using FoodSys.Service.Helper.Password;
using Repo.Repository.UserReposiory;
using Repo.UnitOfWork;
using Service.Helpers.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.UserService
{
    public class UserServicec : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;
        private readonly IPasswordHandler _passwordHandler;
        private readonly IUnitOfWork _unitOfWork;
        public UserServicec( ITokenHandler tokenHandler, IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHandler passwordHandler)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
            _passwordHandler = passwordHandler;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateUser(CreateUserDto createUserDto)
        {

            if (! await _userRepository.CheckIfEmailExist(createUserDto.Email))
            {
                var user = _mapper.Map<User>(createUserDto);

                user.Password = _passwordHandler.CreatePasswordHash(createUserDto.Password);
                user.UserRoles = createUserDto.Roles.Select(u => new UserRole { RoleId = u }).ToList();
                await _userRepository.InsertAsync(user);
                _unitOfWork.Commit();

                return true;
            }
            else
            {
                return true;
            }
            
          
        }
        public async  Task<OutputSignInUserDto> SignInAsync(InputSignInUserDto inputSignInUserDto)
        {
            var user = await _userRepository.GetAsync(em => em.Email.ToLower() == inputSignInUserDto.Email.ToLower(), "UserRoles");

            // return null if email and password isn't correct 
            if (user==null ||!_passwordHandler.Validate(inputSignInUserDto.Password, user.Password))
            {
                return null;
            }


            // get token for the user 
            OutputSignInUserDto outputSignInUserDto = _mapper.Map<OutputSignInUserDto>(_tokenHandler.GetToken(user));
            // update the refresh token for the  user 
            user.RefreshToken = outputSignInUserDto.RefreshToken;
            _userRepository.Update(user);
            _unitOfWork.Commit();
            outputSignInUserDto.Name = user.FirstName;
            return outputSignInUserDto;
        }

        public async Task<OutputRefreshTokenDto> RefreshToken(InputRefreshTokenDto inputRefreshTokenDto)
        {
            // Get User if exist 
            var user = await _userRepository.GetAsync(em => em.RefreshToken == inputRefreshTokenDto.RefreshToken, "UserRoles");
            // return null if email and password isn't correct 
            if (user == null)
            {
                return null;
            }

            // get token for the user 
            OutputRefreshTokenDto outputRefreshTokenDto = _tokenHandler.GetToken(user);
            // update the refresh token for the  user 
            user.RefreshToken = outputRefreshTokenDto.RefreshToken;
            _userRepository.Update(user);
            _unitOfWork.Commit();
            return outputRefreshTokenDto;
        }

        public async Task<List<OutputUserDto>> GetAllUsers()
        {
                var users = await _userRepository.GetAllAsync(u => u.IsDeleted != true, "UserRoles,UserRoles.Role");
            var data = _mapper.Map<List<OutputUserDto>>(users);
            return data;
        }

        public async Task<bool> DeleteUser(int Id)
        {
            var User = await _userRepository.GetAsync(r => r.Id == Id);
            _userRepository.Delete(User);
            _unitOfWork.Commit();
            return true;
        }

        public async Task<bool> UpdateUser(UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userRepository.GetAsync(u => u.Id == updateUserDto.Id, "UserRoles");
                var UserWithsameEmail = await _userRepository.GetAsync(u => u.Email == updateUserDto.Email);
                if (UserWithsameEmail != null && UserWithsameEmail.Id != updateUserDto.Id)
                {
                    return false;
                }
                user = _mapper.Map(updateUserDto, await _userRepository.GetAsync(u => u.Id == updateUserDto.Id, "UserRoles"));
                user.UserRoles = updateUserDto.Roles.Select(r => new UserRole { RoleId = r }).ToList();
                _userRepository.Update(user);
                _unitOfWork.Commit();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
         
        }
    }
}
