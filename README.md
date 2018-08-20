# IDirectPayService
.NET Standard Service and Wrapper for the [Directpay Online](https://www.directpay.online) Payment Gateway. 

### Quick Start

Install the [NuGet package](https://www.nuget.org/packages/IDirectPayService/)
```powershell
Install-Package IDirectPayService
```

Next, you will need to provide IDirectPayService with your URL and Company Token in code, build the request model and create the transaction on Directpay Online.

### Examples

####Console Application:

In Program.cs, call:

```CSharp
const string url = "https://secure1.sandbox.directpay.online/API/v6/";
const string companyToken = "token here";

var service = new DirectPayService(url, companyToken);

var data = new CreateTokenModel{
    ...
};

var response = service.CreateToken(data);

//TODO: Process the response received from Directpay Online

```
Please see example tests for more info/options

####.net core MVC Web Application:

In Startup.cs, call:
```CSharp
services.AddDirectPay(options =>
    {
        options.CompanyToken = "token here";
        options.Url = "https://secure1.sandbox.directpay.online/API/v6/";
    });
```

In your Controller's Constructor, use dependency injection to access the DirectPay Service
```CSharp
private IDirectPayService _directPayService;

public HomeController(IDirectPayService directPayService)
{
    _directPayService = directPayService;
}
```

In your controller method, call:
```CSharp
var data = new CreateTokenModel{
    ...
};

var response = _directPayService.CreateToken(data);

//TODO: Process the response received from Directpay Online
```

Application Flows:

Credit Card:
Option A. 
Immediate Credit card Payment
CreateToken -> ChargeTokenCreditCard -> VerifyToken -> Transaction Complete

Option B. 
Credit card Auth and Then Payment at a later stage(Authorizations are done after 18:00 on DPO's systems)
CreateToken -> ChargeTokenCreditCard -> ChargeTokenAuth -> VerifyToken -> Transaction Complete

Mobile Operator:
CreateToken -> ChargeTokenMobile -> (Instructions to be displayed on the web page and User Performs Mobile Payment) -> VerifyToken -> Transaction Complete

