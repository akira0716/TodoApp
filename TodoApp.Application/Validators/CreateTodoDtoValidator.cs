using FluentValidation;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Validators
{
    public class CreateTodoDtoValidator : AbstractValidator<CreateTodoDto>
    {
        public CreateTodoDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("タイトルは必須です。")
                .MaximumLength(50).WithMessage("タイトルは50文字以内で入力してください。");
        }
    }
}
