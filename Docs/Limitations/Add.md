## Add Regression Region Limitations Resource
<span style="color:red">Requires Authentication</span>  
Adds limitations to the designated regression region.

Limitations can also be added/edited by attaching to regression regions in the following structure, with the property name "Limitations":
```
[
    {
        "criteria": "ExampleVar>1",
        "description": "Limitation on regression region x for x variable",
        "variables": [
            {
                "variableTypeID": {exampleVarID},
                "unitTypeID": {unitTypeID}
            }
        ]
    }
]
```