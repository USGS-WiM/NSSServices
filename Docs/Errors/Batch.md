## Error type batch upload
<span style="color:red">Requires Administrators Authentication</span>    
Provides the ability to batch upload error type resources.

#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /nssservices/errors/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 276

[
	{
        "name":"test error",
	    "shortName":"TE",
	    "description":"test error"
	},
	{
        "name":"test error 2",
	    "shortName":"TE2",
	    "description":"test error 2"
	}
]
```

```
HTTP/1.1 200 OK
[
	{
        "id": 1,
        "name":"test error",
	    "shortName":"TE",
	    "description":"test error"
	},
	{
        "id": 2,
        "name":"test error 2",
	    "shortName":"TE2",
	    "description":"test error 2"
	}
]
```