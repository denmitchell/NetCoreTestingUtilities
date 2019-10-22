# NetCoreTestingUtilities Wiki
The NetCoreTestingUtilities library provides supplementary tools for .NET Core 3 unit and integration testing.  The library is divided into three main functional areas:
1. **[Extensions](https://github.com/denmitchell/NetCoreTestingUtilities/wiki/Object-Extensions)** provides methods for generating JSON from objects; performing deep, but flexible comparisons of objects; for providing side-by-side comparisons of actual and expected values during testing; and for extracting objects and status codes from ActionResult and IActionResult.
2. **[TestJsonAttribute](https://github.com/denmitchell/NetCoreTestingUtilities/wiki/TestJsonAttribute)** -- which enables test input parameters and expected results to be retrieved from database records (in an efficient way) and populated as Xunit test cases.  The TestJsonAttribute allows developers to follow a standardardized, but flexible organization for test records.
3. **[Jxml](https://github.com/denmitchell/NetCoreTestingUtilities/wiki/Jxml-Class-and-JXML)**-- which provides methods for translating between JSON and JXML -- a flavor of XML that includes special processing instructions, as well as generic elements and attributes, for ensuring the full recovery of a JSON document from XML.  In addition to the classes for translating between XML and JSON, there are classes for sorting and filtering json records.
