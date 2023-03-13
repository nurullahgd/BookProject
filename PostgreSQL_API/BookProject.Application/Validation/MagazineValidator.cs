using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using BookProject.Data.Entities;
using BookProject.Application.Interfaces;
using BookProject.Application.Models;

namespace BookProject.Application.Validation
{
    public class MagazineValidator:AbstractValidator<MagazineModel>
    {
        private readonly IMagazineService _magazineService;
        public MagazineValidator(IMagazineService magazineService)
        {
            _magazineService = magazineService;
            RuleFor(m => m.Id).NotEmpty().WithMessage("ID is required")
                .GreaterThan(0).WithMessage("Magazine Id must be greater than 0")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var magazine = _magazineService.GetByIdAsync(id);
                    return magazine == null;
                }).WithMessage("A magazine with the same id already exists.");
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
