# Skybrud.Integrations.BorgerDk

C# (.NET) library for communicating with the Borger.dk web service.

## Installation

Install via [NuGet](https://www.nuget.org/packages/Skybrud.Integrations.BorgerDk):

```
Install-Package Skybrud.Integrations.BorgerDk
```

## Development

The package contains a SOAP client for the communication with the Borger.dk web service. The client can be updated by running the following command:

```
svcutil.exe https://www.borger.dk/_vti_bin/borger/ArticleExport.svc?wsdl
```

Calling `svcutil.exe` with the above URL will generate the necessary classes for making object oriented calls to the web service.
