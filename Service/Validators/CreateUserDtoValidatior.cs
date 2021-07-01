using System;
using System.Collections.Generic;
using System.Text;
using DTOS.UserDtos;
using FluentValidation;

namespace Service.Validators
{
   public class CreateUserDtoValidatior : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidatior()
        {
            RuleFor(u => u)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Can't found the object.");
                });

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email Is Reqired")
                .MaximumLength(100).WithMessage("Email must be no longer than 100 characters.")
                .NotNull().WithMessage("Is necessary to inform the Departmentname Name.");

        }
    }
}
