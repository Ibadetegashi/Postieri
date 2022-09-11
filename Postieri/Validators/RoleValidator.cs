﻿using FluentValidation;
using Postieri.Models;

namespace Postieri.Validators
{
    public class RoleValidator:AbstractValidator<Roles>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().Length(3,25);
            RuleFor(x => x.Description).NotEmpty().NotNull();

        }
    }
}
