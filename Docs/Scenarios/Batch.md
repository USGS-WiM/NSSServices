## Scenario batch upload
<span style="color:red">Requires Administrator Authentication</span>   
Provides the ability to batch upload scenario resources.

#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /nssservices/regions/TEST/scenarios/batch HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: 576

[
    {
        "statisticGroupCode": "AFS",
        "statisticGroupID": 6,
        "statisticGroupName": "Annual Flow Statistics",
        "regressionRegions": [
            {
                "ID": 1,
                "name":"Test Regression Region",
                "code":"GCTEST",
                "parameters":[
                    {
                        "code":"DRNAREA",
                        "limits":{
                            "max":50,
                            "min":0.1
                        },
                        "value":1,
                        "name":"Drainage Area",
                        "description":"Area that drains to a point on a stream",
                        "unitType":{
                            "id":35,
                            "name":"square miles",
                            "abbreviation":"mi^2",
                            "unitSystemTypeID":2
                        }
                    }
                ],
                "regressions":[
                    {
                        "ID":2,
                        "errors":[],
                        "unit":{
                            "id":35,
                            "name":"square miles",
                            "abbreviation":"mi^2",
                            "unitSystemTypeID":2
                        },
                        "equation":"DRNAREA*2",
                        "equivalentYears":null,
                        "predictionInterval":null,
                        "expected":{
                            "value":2,
                            "parameters":{"DRNAREA":1},
                            "intervalBounds":null
                        },
                        "code":"PK2",
                        "name":"2 Year Peak Flood",
                        "description":"Maximum instantaneous flow that occurs on average once in 2 years"
                    }
                ]
            }
        ],
        "statisticGroupName":"Annual Flow Statistics",
        "statisticGroupCode":"AFS"
    }
]
```

```
HTTP/1.1 200 OK
[
    {
        "statisticGroupCode": "AFS",
        "statisticGroupID": 6,
        "statisticGroupName": "Annual Flow Statistics",
        "regressionRegions": [
            {
                "ID": 1,
                "name":"Test Regression Region",
                "code":"GCTEST",
                "parameters":[
                    {
                        "code":"DRNAREA",
                        "limits":{
                            "max":50,
                            "min":0.1
                        },
                        "value":1,
                        "name":"Drainage Area",
                        "description":"Area that drains to a point on a stream",
                        "unitType":{
                            "id":35,
                            "name":"square miles",
                            "abbreviation":"mi^2",
                            "unitSystemTypeID":2
                        }
                    }
                ],
                "regressions":[
                    {
                        "ID":2,
                        "errors":[],
                        "unit":{
                            "id":35,
                            "name":"square miles",
                            "abbreviation":"mi^2",
                            "unitSystemTypeID":2
                        },
                        "equation":"DRNAREA*2",
                        "equivalentYears":null,
                        "predictionInterval":null,
                        "code":"PK2",
                        "name":"2 Year Peak Flood",
                        "description":"Maximum instantaneous flow that occurs on average once in 2 years"
                    }
                ]
            }
        ],
        "statisticGroupName":"Annual Flow Statistics",
        "statisticGroupCode":"AFS"
    }
]
```