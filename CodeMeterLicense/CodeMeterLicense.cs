using CodeMeter;

namespace LicenseLibrary;

public sealed class CodeMeterLicense
{
    private static Lazy<CodeMeterLicense> instance = null;
    private bool licenseValid = false;
    private Api? cmApi = new Api();
    private CmAccess2 cmAcc = new CmAccess2();
    private uint periodicValidationSec = 0;
    private List<Action<bool>> onValidationCallbacks = new List<Action<bool>>();

    private CodeMeterLicense()
    {

    }

    public CodeMeterLicense Start()
    {
        cmAcc.Ctrl |= CmAccess.Option.NoUserLimit;

        // First license validation
        licenseValid = validate();

        if (periodicValidationSec > 0)
            startCallbackManager();

        return GetInstance();
    }

    public bool isLicenseValid() => licenseValid;
    public static CodeMeterLicense Instance
    {
        get
        {
            return GetInstance();
        }
    }

    public static CodeMeterLicense GetInstance()
    {
        if (instance == null)
        {
            instance = new Lazy<CodeMeterLicense>(() => new CodeMeterLicense());
        }

        return instance.Value;
    }

    public void setFirmCode(uint firmCode)
    {
        cmAcc.FirmCode = firmCode;
    }

    public void setProductCode(uint productCode)
    {
        cmAcc.ProductCode = productCode;
    }

    public void setFeatureCode(uint featureCode)
    {
        cmAcc.FeatureCode = featureCode;
    }

    public void setServername(string serverName)
    {
        cmAcc.Servername = serverName;
    }

    public void setPeriodicValidationSec(uint validationSec)
    {
        periodicValidationSec = validationSec;
    }

    public void addValidationCallback(Action<bool> action)
    {
        onValidationCallbacks.Add(action);
    }

    public void addValidationCallback(List<Action<bool>> actions)
    {
        actions.ForEach(action => addValidationCallback(action));
    }

    private async Task startCallbackManager()
    {
        PeriodicTimer periodicTimer = new PeriodicTimer(
            TimeSpan.FromSeconds(periodicValidationSec)
        );

        await Task.Run(async () =>
        {
            while (await periodicTimer.WaitForNextTickAsync())
            {
                licenseValid = validate();

                onValidationCallbacks.ForEach(action => action(licenseValid));
            }
        });
    }

    public bool validate()
    {
        if (cmApi != null)
        {
            HCMSysEntry hcmse = cmApi.CmAccess2(CmAccessOption.LocalLan, cmAcc);

            if(hcmse != null)
            {
                cmApi.CmRelease(hcmse);

                return true;
            }

            cmApi.CmRelease(hcmse);
        }

        return false;
    }
}
