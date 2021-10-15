# Skybrud.Integrations.BorgerDk

C#-bibliotek til kommunikation med Borger.dk's webservices. Dette bibliotek er CMS-uafhængigt.

#### Opdatering af SOAP-klient ###

Skal SOAP-klienten opdateres, kan følgende kommando afvikles fra konsollen:

```
svcutil.exe https://www.borger.dk/_vti_bin/borger/ArticleExport.svc?wsdl
```

Kommandoen vil generere de nødvendige klasser for, at kunne lave objekt-orienterede kald til webservicen.
