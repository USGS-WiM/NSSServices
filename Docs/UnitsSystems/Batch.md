### Unit System Batch Upload
<span style="color:red">Requires Administrator Authentication</span>   
Provides the ability to batch upload unit system resources.


### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /gagestatsservices/unitssystems/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[{
    "unitSystem":"unit system 1"
},
{
    "unitSystem":"unit system 2"
}]
```

```
HTTP/1.1 200 OK
[{
	"id":1,
    "unitSystem":"unit system 1"
},
{
	"id":2,
    "unitSystem":"unit system 2"
}]
```