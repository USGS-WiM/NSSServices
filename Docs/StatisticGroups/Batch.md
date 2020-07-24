### Statistic Group batch upload
<span style="color:red">Requires Administrator Authentication</span>  
Provides the ability to batch upload Statistic Group resources.

### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /gagestatsservices/statisticgroups/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[{
    "name":"Example Statistics 1",
    "code":"UniqueCode1"
},
{
    "name":"Example Statistics 2",
    "code":"UniqueCode2"
},
{
    "name":"Example Statistics 3",
    "code":"UniqueCode3"
}]
```

```
HTTP/1.1 200 OK
[{
	"id":51,
    "name":"Example Statistics 1",
    "code":"UniqueCode1"
},
{
	"id":52,
    "name":"Example Statistics 2",
    "code":"UniqueCode2"
},
{
	"id":53,
    "name":"Example Statistics 3",
    "code":"UniqueCode3"
}]
```