using BookStoreApi.Models.Repositories;
using FluentValidation;

namespace BookStoreApi.Models.Validators
{
    public class GenreModelValidator : AbstractValidator<GenreModel>
    {
        public GenreModelValidator()
        {
            RuleFor(command => command.GenreName).NotNull().NotEmpty();
        }
    }
}
