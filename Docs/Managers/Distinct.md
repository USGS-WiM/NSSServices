## Available Manager Resource
<span style="color:red">Requires Authentication</span>  
Returns the selected manager resource.

### Example
Web service request can be performed using most HTTP client libraries. The following illustrated a typical http request/response performed by a client application.

```
GET /wateruseservices/managers/1 HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
Authentication: Basic ************

```
```
HTTP/1.1 200 OK
{
	"id":1,
    "firstname":"testLogin",
    "lastname":"testLast",
    "username":"Unique&UserName",
	"email":"email@test.com",
	"roleID":2
}
```