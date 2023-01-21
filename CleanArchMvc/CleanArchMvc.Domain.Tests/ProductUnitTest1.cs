using Xunit;
using FluentAssertions;
using CleanArchMvc.Domain.Entities;
using System;


namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create product with Valid State")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product("test", "test description", 10m, 10, "test image");
            action.Should()
                .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create product with Invalid Id")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalid()
        {
            Action action = () => new Product(-1, "test", "test description", 10m, 10, "test image");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }

        [Fact(DisplayName = "Create product with name too short")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product("te", "test description", 10m, 10, "test image");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Name is too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Create product with image too long")]
        public void CreateCategory_TooLongImageValue_DomainExceptionTooLongImage()
        {
            Action action = () => new Product("test", "test description", 10m, 10, @"test imageeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                                                                                    eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                                                                                    eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                                                                                    eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                                                                                    eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                                                                                    eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid image name, too long, maximum 250 characters");
        }

       

        [Fact(DisplayName = "Create product with missing name")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Product("", "test description", 10m, 10, "test image");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Name. Name is required");
        }

        [Fact(DisplayName = "Create product with null name")]
        public void CreateCategory_NullNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Product(null, "test description", 10m, 10, "test image");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Name. Name is required");
        }

        [Fact(DisplayName = "Create product with null image")]
        public void CreateCategory_NullImageValue_NoDomainException()
        {
            Action action = () => new Product("test", "test description", 10m, 10, null);
            action.Should()
                .NotThrow<Validation.DomainExceptionValidation>();

        }

        [Fact(DisplayName = "Create product with null image")]
        public void CreateCategory_NullImageValue_DomainExceptionNullImage()
        {
            Action action = () => new Product("test", "test description", 10m, 10, null);
            action.Should()
                .NotThrow<NullReferenceException>();
                
        }
    }
}
