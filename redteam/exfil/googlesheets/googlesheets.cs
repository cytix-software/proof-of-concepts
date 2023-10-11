using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

class Program
{
    static void Main()
    {
        string spreadsheetId = "YOUR_SPREADSHEET_ID";
        string range = "Sheet1!A1"; // Change this to the desired cell

        // Load the service account credentials from the JSON key file
        string credentialsPath = "ServiceAccountKey.json";
        GoogleCredential credential;

        using (var stream = new System.IO.FileStream(credentialsPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(SheetsService.Scope.Spreadsheets);
        }

        // Create the Sheets API service
        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Google Sheets C# Example",
        });

        // Write data to a cell
        var values = new List<IList<object>> { new List<object> { "Hello, Google Sheets!" } };
        var body = new ValueRange { Values = values };
        var request = service.Spreadsheets.Values.Update(body, spreadsheetId, range);
        request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
        var response = request.Execute();
        Console.WriteLine("Data written to the cell.");

        // Read data from a cell
        var readRequest = service.Spreadsheets.Values.Get(spreadsheetId, range);
        var readResponse = readRequest.Execute();
        var readValue = readResponse.Values[0][0];
        Console.WriteLine($"Data read from the cell: {readValue}");
    }
}
using System;
using System.Collections.Generic;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using Google.Apis.Util.Store;

class Program
{
    static void Main()
    {
        string spreadsheetId = "YOUR_SPREADSHEET_ID";
        string range = "Sheet1!A1"; // Change this to the desired cell
        string apiKey = "YOUR_API_KEY"; // Replace with your API key

        // Create the Sheets API service without authentication
        var service = new SheetsService(new BaseClientService.Initializer()
        {
            ApplicationName = "Google Sheets C# Example",
            ApiKey = apiKey
        });

        // Read data from a cell
        var readRequest = service.Spreadsheets.Values.Get(spreadsheetId, range);
        var readResponse = readRequest.Execute();
        if (readResponse != null && readResponse.Values != null && readResponse.Values.Count > 0)
        {
            var readValue = readResponse.Values[0][0];
            Console.WriteLine($"Data read from the cell: {readValue}");
        }
        else
        {
            Console.WriteLine("No data found in the cell.");
        }
    }
}