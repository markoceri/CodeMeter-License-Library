namespace LicenseLibrary;

public class CodeMeterLicenseBuilder
{
    private CodeMeterLicense _license = CodeMeterLicense.Instance;

    public CodeMeterLicenseBuilder FirmCode(uint firmCode)
    {
        _license.setFirmCode(firmCode);

        return this;
    }

    public CodeMeterLicenseBuilder ProductCode(uint productCode)
    {
        _license.setProductCode(productCode);

        return this;
    }

    public CodeMeterLicenseBuilder FeatureCode(uint featureCode)
    {
        _license.setFeatureCode(featureCode);

        return this;
    }

    public CodeMeterLicenseBuilder ServerName(string serverName)
    {
        _license.setServername(serverName);

        return this;
    }

    public CodeMeterLicenseBuilder PeriodicValidationSec(uint validationSec)
    {
        _license.setPeriodicValidationSec(validationSec);

        return this;
    }

    public CodeMeterLicenseBuilder AddValidationCallback(Action<bool> action)
    {
        _license.addValidationCallback(action);

        return this;
    }

    public CodeMeterLicense Build()
    {
        return CodeMeterLicense.Instance;
    }
}