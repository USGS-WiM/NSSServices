### Regression type batch upload
<span style="color:red">Requires Administrator Authentication</span>  
Provides the ability to batch upload regression type resources.

### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /nssservices/regressiontypes/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[{
    "name": "example regression type 1",
    "code": "EXREGTYPE1",
    "description": "example of a regression type 1",
	"metricUnitTypeID": "1",
    "englishUnitTypeID": "1",
    "statisticGroupTypeID": "1"
},
{
    "name": "example regression type 2",
    "code": "EXREGTYPE2",
    "description": "example of a regression type 2",
	"metricUnitTypeID": "1",
    "englishUnitTypeID": "1",
    "statisticGroupTypeID": "1"
}]
```

```
HTTP/1.1 200 OK
[{
    "id": 1,
    "name": "example regression type 1",
    "code": "EXREGTYPE1",
    "description": "example of a regression type 1",
	"unitTypeID": "1",
    "metricUnitTypeID": "1",
    "englishUnitTypeID": "1",
    "statisticGroupTypeID": "1"
},
{
    "id": 2,
    "name": "example regression type 2",
    "code": "EXREGTYPE2",
    "description": "example of a regression type 2",
	"unitTypeID": "1",
    "metricUnitTypeID": "1",
    "englishUnitTypeID": "1",
    "statisticGroupTypeID": "1"
}]
```