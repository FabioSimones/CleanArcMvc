using CleanArcMvc.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArcMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName = "Create Category with valid state.")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category (1, "Category Name");
            action.Should()
                .NotThrow<CleanArcMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category with negative id value.")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Category Name");
            action.Should()
                .Throw<CleanArcMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value.");
        }

        [Fact(DisplayName = "Create Category with short name.")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<CleanArcMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid. Name is too short, minimum 3 charecters!");
        }

        [Fact(DisplayName = "Create Category missing name value.")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<CleanArcMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid. Name is required!");
        }
    }
}
