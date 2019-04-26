## Time series batch upload
<span style="color:red">Requires Authentication</span>  
Provides the ability to batch upload time series resources.

#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /wateruseservices/regions/1/timeseries/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[{
    "facilityCode":"UniqueFacilityCode1",
    "date":"2010-04-01T00:00:00"
    "value":0.0036,
	"unitTypeID":1
},
{
    "facilityCode":"UniqueFacilityCode1
    "date":"2010-05-01T00:00:00"
    "value":0.0156,
	"unitTypeID":1
},
{
    "facilityCode":"UniqueFacilityCode2",
    "date":"2010-04-01T00:00:00"
    "value":0.0025
	"unitTypeID":1
}]
```

```
HTTP/1.1 200 OK
[{
	"id": 25,
	"sourceID": 1,
	"date": "2010-04-01T00:00:00",
	"value": 0.0036,
	"unitTypeID": 1
},
{
	"id":52,
	"sourceID": 1,
	"date": "2010-05-01T00:00:00",
	"value": 0.0156
	"unitTypeID": 1
},
{
	"id":53,
    "sourceID": 2
	"date": "2010-04-01T00:00:00",
	"value": 0.0025
	"unitTypeID": 1
}]
```