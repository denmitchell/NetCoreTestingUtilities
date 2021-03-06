﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace EDennis.NetCoreTestingUtilities.Tests.Excel {

    public class ExcelTests {

        private readonly ITestOutputHelper _output;
        public ExcelTests(ITestOutputHelper output) {
            _output = output;
        }

        internal class TestJsonA : TestJsonAttribute {
            public TestJsonA(string methodName, string testScenario, string testCase)
                : base("Some Project", "Some Class",
                      methodName, testScenario, testCase, DatabaseProvider.Excel, "Excel\\TestJson.xlsx") {
            }
        }

        private readonly Dictionary<string,int> _input 
            = new Dictionary<string, int> { 
                { "A", 1 }, 
                { "B", 2 } 
            };

        private readonly Dictionary<string, Person> _expected
            = new Dictionary<string, Person> { 
                { "A", new Person { FirstName = "Moe", LastName = "Stooge" } }, 
                { "B", new Person { FirstName = "Larry", LastName = "Stooge" } } 
            };


        [Theory]
        [TestJsonA("Some Method", "", "A")]
        [TestJsonA("Some Method", "", "B")]
        public void Excel(string t, JsonTestCase jsonTestCase) {
            _output.WriteLine($"Test case: {t}");

            var input = jsonTestCase.GetObject<int>("Input");
            var expected = jsonTestCase.GetObject<Person>("Expected");

            Assert.Equal(_input[jsonTestCase.TestCase], input);
            Assert.Equal(_expected[jsonTestCase.TestCase].FirstName, expected.FirstName);

        }

    }
}
