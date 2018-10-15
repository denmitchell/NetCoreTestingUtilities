# NetCoreTestingUtilities Wiki
The NetCoreTestingUtilities package provides supplementary tools for .NET Core 2 unit testing.  The package is divided into four main classes:
1. **[ObjectExtensions](https://github.com/denmitchell/NetCoreTestingUtilities/wiki/Object-Extensions)** -- which provides methods for loading objects from JSON and SQL, generating JSON from objects, and performing deep comparisons of objects.
2. **[Json](https://github.com/denmitchell/NetCoreTestingUtilities/wiki/Json-Class)** -- which provides methods similar to ObjectExtensions, but which targets a Json.NET JToken, rather than objects of specific classes.
3. **[Jxml](https://github.com/denmitchell/NetCoreTestingUtilities/wiki/Jxml-Class-and-JXML)**-- which provides methods for translating between JSON and JXML -- a flavor of XML that includes special processing instructions, as well as generic elements and attributes, for ensuring the full recovery of a JSON document from XML.
4. **[JsonAssert](https://github.com/denmitchell/NetCoreTestingUtilities/wiki/JsonAssert-Class)** -- which provides a highly structured, file-driven testing framework, in which test cases are specified in JSON files.
5. **[JsonSorter](https://github.com/denmitchell/NetCoreTestingUtilities/wiki/JsonSorter-Class)** -- which sorts array elements and properties within a JSON string according to the alphabetic sort order of these elements and properties -- which allows two JSON strings to be compared while ignoring the order of array elements and properties.
6. **[HttpClientPairBase](https://github.com/denmitchell/NetCoreTestingUtilities/wiki/HttpClientPairBase-Class)** -- which facilitates testing of solutions that have both an External API and an Internal API.
7. IActionResultExtensions and ActionResultExtensions -- which provide methods that extract the returned object (GetObject) and status code (GetStatusCode) from an IActionResult and ActionResult, respectively.
