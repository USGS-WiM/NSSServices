## Region batch upload
<span style="color:red">Requires Administrator Authentication</span>    
Provides the ability to batch upload region resources.

#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /nssservices/regions/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 276

[
	{
        "name":"testRegion",
        "code":"TEST",
        "description":"test region"
	},
	{
        "name":"testRegion2",
        "code":"TEST2",
        "description":"test region 2"
	}
]
```

```
HTTP/1.1 200 OK
[
	{
        "id": 1,
        "name":"testRegion",
        "code":"TEST",
        "description":"test region"
	},
	{
        "id": 2,
        "name":"testRegion2",
        "code":"TEST2",
        "description":"test region 2"
	}
]
```