## Available Manager Resources
<span style="color:red">Requires Administrators Authentication</span>  

Returns an array of available manager resources currently provided by the services

### Example
Web service request can be performed using most HTTP client libraries. The following illustrated a typical http request/response performed by a client application.

```
GET /wateruseservices/managers HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
Authentication: Basic ************

```
```
HTTP/1.1 200 OK
[{
	"id":1,
    "firstname":"testLogin",
    "lastname":"testLast",
    "username":"Unique&UserName",
	"email":"email@test.com",
	"roleID":2
}]
```
