﻿using EDennis.NetCoreTestingUtilities.Tests.TestJsonTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace EDennis.NetCoreTestingUtilities.Tests {
    public class TestJsonTests : IClassFixture<TestJsonFixture> {

        private readonly ITestOutputHelper _output;
        public TestJsonTests(ITestOutputHelper output) {
            _output = output;
        }

        private Dictionary<string, string[]> mockActualA
            = new Dictionary<string, string[]>{
                { "123", new string[] {"A","B","C" } },
                { "2018-01-01", new string[] {"D","E","F" } },
                { "789", new string[] {"G","H","I" } },
                { "abc", new string[] {"J","K","L" } },
                { "uvw", new string[] {"U","V","W" } },
                { "xyz", new string[] {"X","Y","Z" } }
            };

        private Dictionary<string, string[]> mockActualB
            = new Dictionary<string, string[]>{
                { "123", new string[] {"A","B","C" } },
                { "2018-01-01", new string[] {"D","E","F" } },
                { "789", new string[] {"G","H","I" } },
                { "abc", new string[] {"J","K","L" } },
            };

        private JsonTestCase jcase = new JsonTestCase() {
            ProjectName = "MyProject",
            MethodName = "MyMethod",
            ClassName = "MyClass",
            TestScenario = "MyScenario",
            TestCase = "MyCase",
            JsonTestFiles = new List<JsonTestFile> {
                    new JsonTestFile() {
                        TestFile = "Integer",
                        Json = "123"
                    },
                    new JsonTestFile() {
                        TestFile = "String",
                        Json = "abc"
                    },
                    new JsonTestFile() {
                        TestFile = "DateTime",
                        Json = "2018-01-01"
                    },
                    new JsonTestFile() {
                        TestFile = "DateTimeOffset",
                        Json = "2018-04-05 10:15:00 -04:00"
                    },
                    new JsonTestFile() {
                        TestFile = "TimeSpan",
                        Json = "10:15:00"
                    },
                    new JsonTestFile() {
                        TestFile = "Expected",
                        Json = "{\"firstName\":\"Bob\",\"lastName\":\"Barker\"}"
                    },
                    new JsonTestFile() {
                        TestFile = "NullRec",
                        Json = null
                    },
                    new JsonTestFile() {
                        TestFile = "Guid",
                        Json = "0E984725-C51C-4BF4-9960-E1C80E27ABA0"
                    }
            }
        };

        internal class TestJsonA : TestJsonAttribute {
            public TestJsonA(string methodName, string testScenario, string testCase)
                : base("TestJson", "EDennis.NetCoreTestingUtilities.Tests", "ClassA",
                      methodName, testScenario, testCase) {
            }
        }

        internal class TestJsonB : TestJsonAttribute {
            public TestJsonB(string methodName, string testScenario, string testCase)
                : base("TestJson2", "EDennis.NetCoreTestingUtilities.Tests", "ClassB",
                      methodName, testScenario, testCase) {
            }
        }


        [Theory]
        [TestJsonA("MethodA", "TestScenarioA", "TestCaseA")]
        [TestJsonA("MethodA", "TestScenarioA", "TestCaseB")]
        [TestJsonA("MethodA", "TestScenarioB", "TestCaseA")]
        [TestJsonA("MethodA", "TestScenarioB", "TestCaseB")]
        [TestJsonA("MethodA", "", "TestCaseA")]
        [TestJsonA("MethodA", "", "TestCaseB")]
        public void TestJsonA_GetData(string t, JsonTestCase jsonTestCase) {
            _output.WriteLine($"Test case: {t}");

            var input = jsonTestCase.GetObject<string>("Input");
            var expected = jsonTestCase.GetObject<string[]>("Expected");
            var actual = mockActualA[input];

            Assert.Equal(expected, actual);
        }


        [Theory]
        [TestJsonB("MethodA", "TestScenarioA", "TestCaseA")]
        [TestJsonB("MethodA", "TestScenarioA", "TestCaseB")]
        [TestJsonB("MethodA", "TestScenarioB", "TestCaseA")]
        [TestJsonB("MethodA", "TestScenarioB", "TestCaseB")]
        public void TestJsonB_GetData(string t, JsonTestCase jsonTestCase) {
            _output.WriteLine($"Test case: {t}");

            var input = jsonTestCase.GetObject<string>("Input");
            var expected = jsonTestCase.GetObject<string[]>("Expected");
            var actual = mockActualB[input];

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void ToObjectPerson() {
            Person person = jcase.GetObject<Person>("Expected");
            Assert.Equal("Bob", person?.FirstName);
        }

        [Fact]
        public void ToObjectPersonNull() {
            Person person = jcase.GetObject<Person>("NullRec");
            Assert.Null(person);
        }

        [Fact]
        public void ToObjectInt() {
            int value = jcase.GetObject<int>("Integer");
            Assert.Equal(123, value);
        }

        [Fact]
        public void ToObjectDateTime() {
            DateTime value = jcase.GetObject<DateTime>("DateTime");
            Assert.Equal(DateTime.Parse("2018-01-01"), value);
        }

        [Fact]
        public void ToObjectDateTimeOffset() {
            DateTimeOffset value = jcase.GetObject<DateTimeOffset>("DateTimeOffset");
            Assert.Equal(DateTimeOffset.Parse("2018-04-05 10:15:00 -04:00"), value);
        }

        [Fact]
        public void ToObjectTimeSpan() {
            TimeSpan value = jcase.GetObject<TimeSpan>("TimeSpan");
            Assert.Equal(TimeSpan.Parse("10:15:00"), value);
        }

        [Fact]
        public void ToObjectGuid() {
            Guid value = jcase.GetObject<Guid>("Guid");
            Assert.Equal(Guid.Parse("0E984725-C51C-4BF4-9960-E1C80E27ABA0"), value);
        }

        [Fact]
        public void ToObjectString() {
            string value = jcase.GetObject<string>("String");
            Assert.Equal("abc", value);
        }

        [Fact]
        public void ToObjectBadCast() {
            Assert.Throws<System.ArgumentException>(() => jcase.GetObject<DateTime>("Integer"));
        }


    }
}
