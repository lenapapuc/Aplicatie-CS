# Topic: Web Authentication & Authorisation.

### Course: Cryptography & Security
### Author: Vasile Drumea

----

## Overview

&ensp;&ensp;&ensp; Authentication & authorization are 2 of the main security goals of IT systems and should not be used interchangibly. Simply put, during authentication the system verifies the identity of a user or service, and during authorization the system checks the access rights, optionally based on a given user role.

&ensp;&ensp;&ensp; There are multiple types of authentication based on the implementation mechanism or the data provided by the user. Some usual ones would be the following:
- Based on credentials (Username/Password);
- Multi-Factor Authentication (2FA, MFA);
- Based on digital certificates;
- Based on biometrics;
- Based on tokens.

&ensp;&ensp;&ensp; Regarding authorization, the most popular mechanisms are the following:
- Role Based Access Control (RBAC): Base on the role of a user;
- Attribute Based Access Control (ABAC): Based on a characteristic/attribute of a user.


## Objectives:
1. Take the work from previous laboratories and put it in a web service / serveral web services.
2. The services should have implemented basic authentication and MFA.
3. The web app needs to simulate user authorization.

## Authentification:

&ensp;&ensp;&ensp; Authentication is the process of verifying a user’s identity. Essentially, it means making sure that a user is who they say they are.

&ensp;&ensp;&ensp; In order to authenticate the user, I implemented 2 methods, the first one is basic login, based on what the user knows
(user_name/email and password). The second factor is a one time code that the user gets on their device.

&ensp;&ensp;&ensp; The login is straight-forward. After the registration which consists in providing a 
username, an e-mail address and a password, the user can log in with either the email or the username and the password.
Based on the choice, the application makes on call or the other to the database.

> For email:
>```
> cmd = "SELECT * FROM Registration WHERE email ='" + login.Validation + "' AND password_hash = '" +login.Password+ "'";
>```
> For username:
>```
> cmd = "SELECT * FROM Registration WHERE user_name ='" + login.Validation + "' AND password_hash = '" +login.Password+ "'";
>```

![Screenshot 2022-12-13 190540.png](..%2FImages%2FScreenshot%202022-12-13%20190540.png)

&ensp;&ensp;&ensp; The second step consists in using Google Authenticator to, first of all, generate
a QR Code that introduces the user into the authentication system, by creating a special code for them
using the configuration code and their own email. 

```
        TwoFactorAuthenticator twoFactorAuthenticator = new TwoFactorAuthenticator(); 
        var two = _configuration["two"];
        var accountSecretKey = $"{two}-{login.Validation}";
        SetupCode setupCode = twoFactorAuthenticator.GenerateSetupCode("Two Factor Demo App", login.Validation, 
            Encoding.ASCII.GetBytes(accountSecretKey));
```

&ensp;&ensp;&ensp; A resulting QR code introduces the users into the authentication app.
![qr.jpg](..%2FImages%2Fqr.jpg)

![accounts.jpg](..%2FImages%2Faccounts.jpg)

&ensp;&ensp;&ensp; The Login Method, upon success goes to the verification method:

````
 if (data.Rows.Count > 0)
        {
            return RedirectToAction("ValidateTwoFactorPin");
        }
````

&ensp;&ensp;&ensp; The "ValidateTwoFactorPin" method computes again the credentials of the user
and puts them against the ones already computed and registered. Then, it performs the verification
against the code from the application:

```
        ...
        var result = twoFactorAuthenticator
            .ValidateTwoFactorPIN(accountSecretKey, login.UserInput);
        return result;
```

&ensp;&ensp;&ensp; The result is a boolean that shows if the user was able to login or not:

![suces.png](..%2FImages%2Fsuces.png)

![successful login.png](..%2FImages%2Fsuccessful%20login.png)

