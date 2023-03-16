using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using BookProject.Data.Entities;
using BookProject.Application.Interfaces;
using BookProject.Application.Models;

namespace BookProject.Application.Validation.MagazineValidation
{
    public class MagazineUpdateValidator:AbstractValidator<MagazineModel>
    {
        private readonly IMagazineService _magazineService;
        public MagazineUpdateValidator(IMagazineService magazineService)
        {
            _magazineService = magazineService;


            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
