### Citation batch upload
<span style="color:red">Requires Authentication</span>  
Provides the ability to batch upload citation resources.

### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /gagestatsservices/citations/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[{
    "title": "example title 1",
    "author": "example author 1",
    "citationURL": "https://example.com/"
},
{
    "title": "example title 2",
    "author": "example author 2",
    "citationURL": "https://example.com/"
},
{
    "title": "example title 3",
    "author": "example author 3",
    "citationURL": "https://example.com/"
}]
```

```
HTTP/1.1 200 OK
[{
	"id": 100,
	"title": "example title 1",
    "author": "example author 1",
    "citationURL": "https://example.com/"
},
{
    "id": 101,
	"title": "example title 2",
    "author": "example author 2",
    "citationURL": "https://example.com/"
},
{
    "id": 102,
	"title": "example title 3",
    "author": "example author 3",
    "citationURL": "https://example.com/"
}]
```