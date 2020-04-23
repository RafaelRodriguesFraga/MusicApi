using FluentValidation;
using MusicApi.Api.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Api.Validators
{
    public class SaveArtistResourceValidator : AbstractValidator<SaveArtistResource>
    {
        public SaveArtistResourceValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
