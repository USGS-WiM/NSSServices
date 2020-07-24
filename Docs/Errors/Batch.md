### Variable batch upload
<span style="color:red">Requires Administrator Authentication</span>    
Provides the ability to batch upload error type resources.

### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /gagestatsservices/errors/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 276

[{
    "name":"Example error 1",
    "code":"EE1"
},
{
    "name":"Example error 2",
    "code":"EE2"
}]
```

```
HTTP/1.1 200 OK
[{
    "id":5,
    "name":"Example error 1",
    "code":"EE1"
},
{
    "id":6,
    "name":"Example error 2",
    "code":"EE2"
}]
```