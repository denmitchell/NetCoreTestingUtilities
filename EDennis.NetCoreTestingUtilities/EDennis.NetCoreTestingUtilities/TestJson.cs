﻿using Dapper;
using EDennis.NetCoreTestingUtilities.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Xunit.Abstractions;

namespace EDennis.NetCoreTestingUtilities{

    /// <summary>
    /// Flat TestJson record, which reflects the 
    /// table in the database.
    /// </summary>
    public class TestJson {
        public string ProjectName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string TestScenario { get; set; }
        public string TestCase { get; set; }
        public string TestFile { get; set; }
        public string Json { get; set; }

    }

    /// <summary>
    /// Provides TestJson records in a hierarchical result
    /// grouped by a particular TestCase
    /// </summary>
    public class JsonTestCase : IXunitSerializable {
        public string ProjectName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string TestScenario { get; set; }
        public string TestCase { get; set; }

        public List<JsonTestFile> JsonTestFiles { get; set; }

        public void Deserialize(IXunitSerializationInfo info) {
            var obj = JsonConvert.DeserializeObject<JsonTestCase>(info.GetValue<string>("obj"));
            ProjectName = obj.ProjectName;
            ClassName = obj.ClassName;
            MethodName = obj.MethodName;
            TestScenario = obj.TestScenario;
            TestCase = obj.TestCase;
            JsonTestFiles = obj.JsonTestFiles;
        }

        public void Serialize(IXunitSerializationInfo info) {
            var json = JsonConvert.SerializeObject(this);
            info.AddValue("obj", json);
        }

        public string GetJson(string testFile) {
            return JsonTestFiles.FirstOrDefault(f => f.TestFile == testFile).Json;
        }

        public T GetObject<T>(string testFile)
            where T : new() {
            var json = GetJson(testFile);
            return new T().FromJsonString(json);
        }

        public static List<JsonTestCase> GetTestCasesForProject(string connectionString, string testJsonSchema, string testJsonTable, string projectName) {
            var testCases = new List<JsonTestCase>();


            using (var cxn = new SqlConnection(connectionString)) {
                var testJson = cxn.Query<TestJson>(
                    "select ProjectName, ClassName, MethodName, TestScenario, TestCase, TestFile, Json from "
                    + $"{testJsonSchema}.{testJsonTable} where "
                    + "ProjectName = @projectName",
                    new { projectName });


                if (testJson.Count() == 0) {
                    throw new ArgumentException(
                        $"No TestJson record found for ProjectName: {projectName}.");
                }

                //construct a JsonTestCase as the return object
                var qry = testJson.GroupBy(
                        r => new { r.ProjectName, r.ClassName, r.MethodName, r.TestScenario, r.TestCase },
                        r => new JsonTestFile { TestFile = r.TestFile, Json = r.Json },
                        (key, g) =>
                            new JsonTestCase {
                                ProjectName = key.ProjectName,
                                ClassName = key.ClassName,
                                MethodName = key.MethodName,
                                TestScenario = key.TestScenario,
                                TestCase = key.TestCase,
                                JsonTestFiles = g.ToList()
                            });

                //return all objects
                foreach (var rec in qry)
                    testCases.Add(rec);

                return testCases;
            }

        }

        public static IEnumerable<object[]> GetDataForXUnit(List<JsonTestCase> TestCases,
            string className, string methodName, string testScenario, string testCase) {

            var qry = TestCases.Where(t => t.ClassName == className && t.MethodName == methodName
                            && t.TestScenario == testScenario && t.TestCase == testCase);

            //return all objects
            foreach (var rec in qry.AsEnumerable())
                yield return new object[] { $"{testScenario}({testCase})", rec };
        }


    }

    /// <summary>
    /// Provides a single test file and JSON
    /// for a test case
    /// </summary>
    public class JsonTestFile {
        public string TestFile { get; set; }
        public string Json { get; set; }

    }



}