## Regression region batch upload
<span style="color:red">Requires Administrator Authentication</span>    
Provides the ability to batch upload regression region resources.

#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /nssservices/regions/TEST/regressionregions/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 276

[
	{
        "name":"Test Regression Region",
        "code":"GCTEST",
        "description":"test regression region",
        "citationID": 1,
        "statusID": 1,
        "methodID": 1
	},
	{
        "name":"Test Regression Region 2",
        "code":"GCTEST2",
        "description":"test regression region 2",
        "citationID": 1,
        "statusID": 1,
        "methodID": 1
	}
]
```

```
HTTP/1.1 200 OK
[
	{
        "id": 1,
        "name":"Test Regression Region",
        "code":"GCTEST",
        "description":"test regression region",
        "citationID": 1,
        "statusID": 1,
        "methodID": 1
	},
	{
        "id": 2,
        "name":"Test Regression Region 2",
        "code":"GCTEST2",
        "description":"test regression region 2",
        "citationID": 1,
        "statusID": 1,
        "methodID": 1
	}
]
```