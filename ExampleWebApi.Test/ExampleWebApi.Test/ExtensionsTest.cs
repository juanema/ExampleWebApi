using NUnit.Framework;
using ExampleWebApi.Core.Extensions;
using System;

namespace ExampleWebApi.Test
{
    public class ExtensionsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestObjectExtensions()
        {
            object anyObject = null;
            try
            {
                anyObject.ThrowExceptionIfNull();
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }
            catch(Exception)
            {
                Assert.Fail("Doesn´t work ThrowExceptionIfNull()");
            }
        }
    }
}