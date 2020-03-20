## Source Type batch upload
<span style="color:red">Requires Administrators Authentication</span>   
Provides the ability to batch upload Source type resources.

#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /wateruseservices/sourcetypes/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[{
    "name":"Source typeSample 1",
    "description":"Description of source type Sample 1",
    "code":"UniqueCode1"
},
{
    "name":"Source typeSample 2",
    "description":"Description of source type Sample 2",
    "code":"UniqueCode2"
},
{
    "name":"Source typeSample 3",
    "description":"Description of source type Sample 3",
    "code":"UniqueCode3"
}]
```

```
HTTP/1.1 200 OK
[{
	"id":51,
    "name":"Source type Sample 1",
    "description":"Description of Source type Sample 1",
    "code":"UniqueCode1"
},
{
	"id":52,
    "name":"Source type Sample 2",
    "description":"Description of Source type Sample 2",
    "code":"UniqueCode2"
},
{
	"id":53,
    "name":"Source type Sample 3",
    "description":"Description of Source type Sample 3",
    "code":"UniqueCode3"
}]
```