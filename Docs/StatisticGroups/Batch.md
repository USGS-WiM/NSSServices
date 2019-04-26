## Status batch upload
<span style="color:red">Requires Administrators Authentication</span>  
Provides the ability to batch upload Status resources.
#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /wateruseservices/status/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[{
    "name":"StatusSample 1",
    "description":"Description of Status Sample 1",
    "code":"UniqueCode1"
},
{
    "name":"StatusSample 2",
    "description":"Description of Status Sample 2",
    "code":"UniqueCode2"
},
{
    "name":"StatusSample 3",
    "description":"Description of Status Sample 3",
    "code":"UniqueCode3"
}]
```

```
HTTP/1.1 200 OK
[{
	"id":51,
    "name":"Status Sample 1",
    "description":"Description of Status Sample 1",
    "code":"UniqueCode1"
},
{
	"id":52,
    "name":"Status Sample 2",
    "description":"Description of Status Sample 2",
    "code":"UniqueCode2"
},
{
	"id":53,
    "name":"Status Sample 3",
    "description":"Description of Status Sample 3",
    "code":"UniqueCode3"
}]
```