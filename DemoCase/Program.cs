using System.Threading;

using LicenseLibrary;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("CodeMeter license check demo program using SDK API");

        // Use builder pattern to build CodeMeterLicense class
        CodeMeterLicense license = new CodeMeterLicenseBuilder()
            .FirmCode(6000010) // Demo firm code
            .ProductCode(0) // Custom product code
            .FeatureCode(100) // Custom feature code
            .PeriodicValidationSec(10) // Periodic validation in seconds
            // Add a callback function as short lambda function
            .AddValidationCallback(
                (validLicense) => Console.WriteLine("User callback as lambda function - validation: {0}", validLicense)
            )
            // Add a callback function as full function
            .AddValidationCallback(myCustomFunction)
            .Build()
            .Start();

        // First license check
        Console.WriteLine("Is license valid? {0}", license.isLicenseValid());

        // Simulate application loop 
        while (true)
        {
            Thread.Sleep(1000);
        }
    }

    static void myCustomFunction(bool validLicense)
    {
        Console.WriteLine("User callback as full function - validation: {0}", validLicense);
    }
}
