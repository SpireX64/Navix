﻿using System;
using System.Diagnostics.CodeAnalysis;
using Spx.Navix.UnitTests.Stubs;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class AbstractNavigatorTest
    {
        [Fact]
        public void Navigator_TryForward_ThrowsNotImpl()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotImplementedException>(
                () => navigator.Forward(null!, null!));
        }

        [Fact]
        public void Navigator_TryBack_ThrowsNotSupported()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotSupportedException>(
                () => navigator.Back());
        }

        [Fact]
        public void Navigator_TryBackToScreen_ThrowsNotSupported()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotSupportedException>(
                () => navigator.BackToScreen(new ScreenStub1()));
        }

        [Fact]
        public void Navigator_TryBackToRoot_ThrowsNotSupported()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotSupportedException>(
                () => navigator.BackToRoot());
        }

        [Fact]
        public void Navigator_TryReplaceScreen_ThrowsNotSupported()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotSupportedException>(
                () => navigator.Replace(new ScreenStub1(), new ScreenResolverStub1()));
        }
        
        [Fact]
        public void NavigatorSpecification_CheckDefaultValues_AllAreFalse()
        {
            // -- Arrange:
            var spec = new NavigatorSpecification();
            
            // -- Assert:
            Assert.False(spec.ReplaceScreenSupported);
            Assert.False(spec.BackToRootSupported);
            Assert.False(spec.BackToScreenSupported);
        }

        [ExcludeFromCodeCoverage]
        private class AbstractNavigator : Navigator
        {
            [SuppressMessage("ReSharper", "UnusedMember.Local")]
            public AbstractNavigator(NavigatorSpecification specification)
            {
                Specification = specification;
            }

            public AbstractNavigator()
            {
                Specification = new NavigatorSpecification()
                {
                    BackToScreenSupported = true,
                    BackToRootSupported = true,
                    ReplaceScreenSupported = true,
                };
            }

            public override NavigatorSpecification Specification { get; }
        }
    }
}