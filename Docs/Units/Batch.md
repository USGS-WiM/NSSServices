### Unit Batch Upload
<span style="color:red">Requires Administrator Authentication</span>   
Provides the ability to batch upload unit resources.


### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /gagestatsservices/units/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[{
    "name":"unitSample 1",
    "abbreviation":"US1",
    "unitSystemTypeID": 1
},
{
    "name":"unitSample 2",
    "abbreviation":"US2",
    "unitSystemTypeID": 1
}]
```

```
HTTP/1.1 200 OK
[{
	"id":1,
    "name":"unitSample 1",
    "abbreviation":"US1",
    "unitSystemTypeID": 1
},
{
	"id":2,
    "name":"unitSample 2",
    "abbreviation":"US2",
    "unitSystemTypeID": 1
}]
```