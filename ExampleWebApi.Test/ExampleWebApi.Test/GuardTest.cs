using NUnit.Framework;
using ExampleWebApi.Core;
using System;
using ExampleWebApi.Domain.Communications;
using ExampleWebApi.Core.Communications;
using System.Collections.Generic;

namespace ExampleWebApi.Test
{
    public class GuardTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAgainst()
        {
            try
            {
                object isNull = null;
                Guard.Against<ArgumentNullException>(isNull == null, $"Parameter is null");
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }
           
        }

        [Test]
        public void TestAgainst2()
        {
            try
            {                
                Guard.Against<ArgumentNullException>(!true, $"Condition is false");
                Assert.Pass();
            }
            catch (ArgumentNullException)
            {
                Assert.Fail("Doesn´t work Against");
            }
           
        }

        [Test]
        public void TestInheritsFrom()
        {
            try
            {
                Guard.InheritsFrom<ArgumentException>(typeof(ArgumentNullException), "ArgumentNullException inherits from Exception");               
            }
            catch (ArgumentNullException)
            {
                Assert.Fail("Doesn´t work InheritsFrom");
            }
            Assert.Pass();
        }

        [Test]
        public void TestImplements()
        {
            try
            {
                Guard.Implements<ApiBaseResponse>(typeof(UserResponse), "SaveUserResponse implements ApiBaseResponse");                
            }
            catch (InvalidOperationException)
            {
                Assert.Fail("Doesn´t work Implements");
            }
            Assert.Pass();
        }

        [Test]
        public void TestTypeOf()
        {
            try
            {
                var apiBaseResponse = new ApiBaseResponse(true, new List<string>());
                Guard.TypeOf<ApiBaseResponse>(apiBaseResponse, "Typeof apiBaseResponse is ApiBaseResponse");               
            }
            catch (InvalidOperationException)
            {
                Assert.Fail("Doesn´t work TypeOf");
            }
            Assert.Pass();
        }

        [Test]
        public void IsEqual()
        {
            try
            {
                string test = "Test";
                string test2 = "Test";

                Guard.IsEqual<ArgumentException>(test2, test, "test equals test2");           
            }
            catch (ArgumentException)
            {
                Assert.Fail("Doesn´t work IsEqual");
            }
            Assert.Pass();
        }

        [Test]
        public void IsEqual2()
        {
            try
            {
                string test = "Test";
                string test2 = "notTest";

                Guard.IsEqual<ArgumentException>(test2, test, "test equals test2");
                Assert.Fail("Doesn´t work IsEqual");

            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
           
        }
    }
}
