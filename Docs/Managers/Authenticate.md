### Authenticate
Hits authentication service to receive an authentication token.

### Example
Web service request can be performed using most HTTP client libraries. The following illustrated a typical http request/response performed by a client application.

```
POST /nssservices/authenticate HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

{
    "username": "exampleUser",
    "password": "examplePassword"
}
```

```
HTTP/1.1 200 OK
{
    "id": 1,
    "username": "exampleUser",
    "role": "Manager",
    "token": "xxx"
}