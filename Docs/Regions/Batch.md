## Region batch upload
<span style="color:red">Requires Administrators Authentication</span>    
Provides the ability to batch upload region resources.

#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /wateruseservices/regions/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 276

[
	{"name":"testRegion",
	"shortName":"AB",
	"description":"testRegion",
	"fipsCode":"55"
	},
	{"name":"testRegion2",
	"shortName":"CD",
	"description":"testRegion2",
	"fipsCode":"56"
	}
]
```

```
HTTP/1.1 200 OK
[
	{
	"id":1,
	"name":"testRegion",
	"shortName":"AB",
	"description":"testRegion",
	"fipsCode":"55"
	},
	{
	"id":2,
	"name":"testRegion2",
	"shortName":"CD",
	"description":"testRegion2",
	"fipsCode":"56"
	}
]
```