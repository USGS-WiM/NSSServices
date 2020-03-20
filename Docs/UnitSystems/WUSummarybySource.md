## Summary Resources by Source.
<span style="color:red">Requires Authentication</span>  
Returns a water use summary resources by source.

#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /wateruseservices/summary/bysource?year=2005&endyear=2012&includePermits=true&computeReturns=true&computeDomestic=true HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: ***
Authorization: Basic ****

{"type":"Polygon","coordinates":[[[-81.05865996028837,40.02565079553609],....[-81.05865996028837,40.02565079553609],[-81.05865996028837,40.02565079553609]]]}
```

```
HTTP/1.1 200 OK
{
  "FC393029": {
    "startYear": 2005,
    "endYear": 2012,
    "processDate": "2018-08-21T13:09:12.2150517-06:00",
    "withdrawal": {
      "annual": {
        "SW": {
          "value": 0.08925471830915653,
          "unit": {
            "id": 0,
            "name": "Million Gallons per Day",
            "abbreviation": "MGD"
          },
          "description": "Daily Annual Average "
        }
      },
      "monthly": {
        "1": {
          "month": {
            "SW": {
              "name": "Jan ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Jan daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily Jan average Domestic"
            }
          }
        },
        "2": {
          "month": {
            "SW": {
              "name": "Feb ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Feb daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily Feb average Domestic"
            }
          }
        },
        "3": {
          "month": {
            "SW": {
              "name": "Mar ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Mar daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily Mar average Domestic"
            }
          }
        },
        "4": {
          "month": {
            "SW": {
              "name": "Apr ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Apr daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily Apr average Domestic"
            }
          }
        },
        "5": {
          "month": {
            "SW": {
              "name": "May ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "May daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily May average Domestic"
            }
          }
        },
        "6": {
          "month": {
            "SW": {
              "name": "Jun ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Jun daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily Jun average Domestic"
            }
          }
        },
        "7": {
          "month": {
            "SW": {
              "name": "Jul ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Jul daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily Jul average Domestic"
            }
          }
        },
        "8": {
          "month": {
            "SW": {
              "name": "Aug ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Aug daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily Aug average Domestic"
            }
          }
        },
        "9": {
          "month": {
            "SW": {
              "name": "Sep ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Sep daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily Sep average Domestic"
            }
          }
        },
        "10": {
          "month": {
            "SW": {
              "name": "Oct ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Oct daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily Oct average Domestic"
            }
          }
        },
        "11": {
          "month": {
            "SW": {
              "name": "Nov ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Nov daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily Nov average Domestic"
            }
          }
        },
        "12": {
          "month": {
            "SW": {
              "name": "Dec ",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Dec daily monthly average"
            }
          },
          "code": {
            "DO": {
              "name": "Domestic",
              "value": 0.08925471830915654,
              "unit": {
                "id": 0,
                "name": "Million Gallons per Day",
                "abbreviation": "MGD"
              },
              "description": "Daily Dec average Domestic"
            }
          }
        }
      }
    }
  }
}
```
