### Variable batch upload
<span style="color:red">Requires Administrator Authentication</span>  
Provides the ability to batch upload variable resources.

### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /nssservices/variables/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[{
    "name":"Example Variable Type 1",
    "code":"EXVARTYPE1",
    "description": "First variable type example",
    "metricUnitTypeID": "1",
    "englishUnitTypeID": "1",
    "statisticGroupTypeID": "1"
},
{
    "name":"Example Variable Type 2",
    "code":"EXVARTYPE2",
    "description": "Second variable type example",
    "metricUnitTypeID": "1",
    "englishUnitTypeID": "1",
    "statisticGroupTypeID": "1"
}]
```

```
HTTP/1.1 200 OK
[{
	"id": 25,
	"name":"Example Variable Type 1",
    "code":"EXVARTYPE1",
    "description": "First variable type example",
    "unitTypeID": "1",
    "metricUnitTypeID": "1",
    "englishUnitTypeID": "1",
    "statisticGroupTypeID": "1"
},
{
	"id": 26,
	"name":"Example Variable Type 2",
    "code":"EXVARTYPE2",
    "description": "Second variable type example",
    "unitTypeID": "1",
    "metricUnitTypeID": "1",
    "englishUnitTypeID": "1",
    "statisticGroupTypeID": "1"
}]
```