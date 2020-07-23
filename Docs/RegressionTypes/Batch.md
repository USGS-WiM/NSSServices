## Regression type batch upload
<span style="color:red">Requires Authentication</span>  
Provides the ability to batch upload regression region resources.

Response as shown in the following sample.
#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /nssservices/regressiontypes/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[
	{
	    "name": "test reg type 1",
	    "code": "REGTYPE1",
	    "description": "Test regression type 1"
	},
    {
	    "name": "test reg type 2",
	    "code": "REGTYPE2",
	    "description": "Test regression type 2"
	}
]
```

```
HTTP/1.1 200 OK
[
	{
        "id": 1,
	    "name": "test reg type 1",
	    "code": "REGTYPE1",
	    "description": "Test regression type 1"
	},
    {
        "id": 2,
	    "name": "test reg type 2",
	    "code": "REGTYPE2",
	    "description": "Test regression type 2"
	}
]
```