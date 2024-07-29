using CleanArcMvc.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArcMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create Product with valid state.")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image");
            action.Should()
                .NotThrow<CleanArcMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product with negative id value.")]
        public void CreateProduct_WithNegativeId_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "product image");
            action.Should()
                .Throw<CleanArcMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid id value!");
        }

        [Fact(DisplayName = "Create Product with short name value.")]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "product image");
            action.Should()
                .Throw<CleanArcMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid, name is too short, minimum 3 characters!");
        }

        [Fact(DisplayName = "Create Product missing name value.")]
        public void CreateProduct_MissingNameValue_DomainExceptionMissingName()
        {
            Action action = () => new Product(1, "", "Product Description", 9.99m, 99, "product image");
            action.Should()
                .Throw<CleanArcMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid, name is required!");
        }

        [Fact(DisplayName = "Create Product missing description value.")]
        public void CreateProduct_MissingDescriptionValue_DomainExceptionMissingDescription()
        {
            Action action = () => new Product(1, "Product Name", "", 9.99m, 99, "product image");
            action.Should()
                .Throw<CleanArcMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid, description is required!");
        }

        [Fact(DisplayName = "Create Product with short description value.")]
        public void CreateProduct_ShortDescriptionValue_DomainExceptionShortDescription()
        {
            Action action = () => new Product(1, "Product Name", "pro", 9.99m, 99, "product image");
            action.Should()
                .Throw<CleanArcMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid, description is too short, minimum 5 characters!");
        }

        [Fact(DisplayName = "Create Product with invalid price.")]
        public void CreateProduct_InvalidPriceValue_DomainExceptionPriceValueInvalid()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -1, 99, "product image");
            action.Should()
                .Throw<CleanArcMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid price value!");
        }

        [Theory]
        [InlineData(-5)]        
        public void CreateProduct_InvalidStockValue_DomainExceptionStockValueInvalid(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, value, "product image");
            action.Should()
                .Throw<CleanArcMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid stock value!");
        }

        [Fact(DisplayName = "Create Product with empty image name.")]
        public void CreateProduct_WithEmptyNameImage_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
            action.Should()
                .NotThrow<CleanArcMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product with null image name.")]
        public void CreateProduct_WithNullNameImage_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<CleanArcMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product long image name.")]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99, "product image tooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo looooooooooooooooooooooooooooooooooooonnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnggggggggggggggggggggggggggggggggggggggg naaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaameeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
            action.Should()
                .Throw<CleanArcMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid, image is too long, maximum 250 characters!");
        }
    }
}
