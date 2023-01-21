using Xunit;
using FluentAssertions;
using CleanArchMvc.Domain.Entities;
using System;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName = "Create category with Invalid Id")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Name. Name is required");
        }

        [Fact(DisplayName = "Create category with Invalid Id")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalid()
        {
            Action action = () => new Category(-1, "Category Name");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }

        [Fact(DisplayName = "Create category with null name")]
        public void CreateCategory_NullNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Name. Name is required");
        }

        [Fact(DisplayName = "Create category with name too short")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Create category with Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name");
            action.Should()
                .NotThrow<Validation.DomainExceptionValidation>();
        }
    }
}
