﻿using FluentValidation;
using RentalCompany.Application.Models.Input;

namespace RentalCompany.Application.Validators
{
    public class CreateOrderInputValidator : AbstractValidator<CreateOrderInput>
    {
        public CreateOrderInputValidator()
        {
            RuleFor(x => x.StartDate)
                            .NotNull()
                            .NotEmpty()
                            .Must(BeValidDate)
                            .WithMessage("Start Date must be today or after.");

            RuleFor(x => x.ExpectedReturnDate)
                .NotEmpty()
                .NotNull()
                .Must(BeValidDate)
                .WithMessage("Expected Return Date must be today or after.");


            RuleFor(x => x.IdDeliveryMan)
                .NotEmpty()
                .NotNull()
                .WithMessage("IdDeliveryMan can't be empty or null.");
            
            RuleFor(x => x.IdMotorcycle)
                .NotEmpty()
                .NotNull()
                .WithMessage("IdMotorcycle can't be empty or null.");
        }
        private bool BeValidDate(DateTime date)
        {
            return date >= (DateTime.Today);
        }
    }
}