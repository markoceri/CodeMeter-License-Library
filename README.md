# 🇮🇹 Controllo della Licenza con CodeMeter
Questa libreria implementa il controllo della licenza software utilizzando le API fornite dall'SDK di *CodeMeter* e le relative dongle USB (CmDongle).

## Funzionalità principali
  - uso del pattern **Singleton Thread-Safe** per la classe `CodeMeterLicense`.
  - uso del pattern **Builder** per la creazione del controllore della licenza tramite l'uso della classe `CodeMeterLicenseBuilder`.
  - **Verifica periodica**: È possibile impostare un intervallo di tempo in cui verificare la licenza in modo ripetuto. Ad esempio, ogni 10 secondi.
  - **Callback personalizzate**: È possibile passare delle callback personalizzate alla verifica della licenza. Questo consente di eseguire azioni specifiche quando la licenza è valida o scaduta.

## Esempio d’uso
Controllo della presenza di una qualsiasi Dongle USB CodeMeter:
```c#
using LicenseLibrary;

CodeMeterLicense license = new CodeMeterLicenseBuilder()
  .Build()
  .Start();

// First license check
Console.WriteLine("Is license valid? {0}", license.isLicenseValid());
```

Controllo della presenza della Dongle USB con la licenza Demo `6000010` del prodotto con id `0` e la funzionalità `100` attiva. IL controllo della licenza viene ripetuto ogni 10 secondi ed è stata aggiunta una funzione lambda eseguita ad ogni controllo.
```C#
using LicenseLibrary;

// Use builder pattern to build CodeMeterLicense class
CodeMeterLicense license = new CodeMeterLicenseBuilder()
    .FirmCode(6000010)
    .ProductCode(0)
    .FeatureCode(100)
    .PeriodicValidationSec(10)
    .AddValidationCallback(
        (validLicense) => Console.WriteLine("User callback as lambda function - validation: {0}", validLicense)
    )
    .Build()
    .Start();

// First license check
Console.WriteLine("Is license valid? {0}", license.isLicenseValid());
```

## Contributi
Ogni contributo è sempre ben accettato! Sentiti libero di aprire una pull request o segnalare eventuali problemi.

# 🇬🇧 License Control with CodeMeter
This library implements software license control using the APIs provided by the *CodeMeter* SDK and the corresponding USB dongles (CmDongle).

## Main Features
  - **Singleton Thread-Safe**: The `CodeMeterLicense` class uses the thread-safe singleton pattern.
  - **Builder Pattern** The license controller can be created using the `CodeMeterLicenseBuilder` class.
  - **Periodic Verification**:You can set a time interval for repeated license verification. For example, every 10 seconds.
  - **Custom Callbacks**: Custom callbacks can be passed for license verification. This allows you to perform specific actions when the license is valid or expired.

## Usage Example
Checking for the presence of any CodeMeter USB dongle:
```c#
using LicenseLibrary;

CodeMeterLicense license = new CodeMeterLicenseBuilder()
  .Build()
  .Start();

// First license check
Console.WriteLine("Is license valid? {0}", license.isLicenseValid());
```

Checking for the presence of the USB dongle with Demo license `6000010` for product ID `0` and active feature `100`. The license check is repeated every 10 seconds, and a lambda function is executed on each check.
```C#
using LicenseLibrary;

// Use builder pattern to build CodeMeterLicense class
CodeMeterLicense license = new CodeMeterLicenseBuilder()
    .FirmCode(6000010)
    .ProductCode(0)
    .FeatureCode(100)
    .PeriodicValidationSec(10)
    .AddValidationCallback(
        (validLicense) => Console.WriteLine("User callback as lambda function - validation: {0}", validLicense)
    )
    .Build()
    .Start();

// First license check
Console.WriteLine("Is license valid? {0}", license.isLicenseValid());
```

## Contributions
Contributions are always welcome! Feel free to open a pull request or report any issues.
