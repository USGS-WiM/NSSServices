## Region batch upload
<span style="color:red">Requires Authentication</span>  
Provides the ability to batch upload region source resources.

Response as shown in the following sample.
#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /wateruseservices/sources/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[
	{
	"name": "test source",
	"facilityName": "WELL test # 3,
	"facilityCode": "abc345",
	"stationID": "",
	"catagoryTypeID": 1,
	"sourceTypeID": 1,
	"useTypeID": 1,
	"location": {
		"x": -112.2345,
		"y": 42.4566,
		"srid": 4269
		}
	},
  {
	"name": "test source 2",
	"facilityName": "WELL test #4 ",
	"facilityCode": "abc123",
	"stationID": "",
	"catagoryTypeID": 1,
	"sourceTypeID": 1,
	"useTypeID": 1,
	"location": {
		"x": -112.2789,
		"y": 42.123,
		"srid": 4269
		}
	}
]
```

```
HTTP/1.1 200 OK
[
	{
	"id":1,
	"name": "test source",
	"facilityName": "WELL test # 3,
	"facilityCode": "abc345",
	"stationID": "",
	"catagoryTypeID": 1,
	"sourceTypeID": 1,
	"useTypeID": 1,
	"location": {
		"x": -112.2345,
		"y": 42.4566,
		"srid": 4269
		}
	},
  {
	"id":1,
	"name": "test source 2",
	"facilityName": "WELL test #4 ",
	"facilityCode": "abc123",
	"stationID": "",
	"catagoryTypeID": 1,
	"sourceTypeID": 1,
	"useTypeID": 1,
	"location": {
		"x": -112.2789,
		"y": 42.123,
		"srid": 4269
		}
	}
]
```