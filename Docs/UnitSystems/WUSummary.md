## Summary Resources
Returns a water use summary resources

#### Request Example
The REST URL section below displays the example url and the body/payload of the request used to simulate a response.

```
POST /wateruseservices/summary?year=2005&endyear=2012&includePermits=true&computeReturns=true&computeDomestic=true HTTP/1.1
Host: streamstats.usgs.gov
Accept: application/json
content-type: application/json;charset=UTF-8
content-length: ***

{"type":"Polygon","coordinates":[[[-81.05865996028837,40.02565079553609],....[-81.05865996028837,40.02565079553609],[-81.05865996028837,40.02565079553609]]]}
```

```
HTTP/1.1 200 OK
{
  "startYear": 2005,
  "endYear": 2012,
  "processDate": "2018-08-21T13:03:56.820271-06:00",
  "return": {
    "annual": {
      "GW": {
        "name": "Groundwater",
        "value": 0,
        "unit": {
          "id": 1,
          "name": "Million Gallons per Day",
          "abbreviation": "MGD"
        },
        "description": "Daily Annual Average Groundwater well"
      },
      "SW": {
        "name": "Surface water",
        "value": 1.6840822276641054,
        "unit": {
          "id": 1,
          "name": "Million Gallons per Day",
          "abbreviation": "MGD"
        },
        "description": "Daily Annual Average Surface water point"
      }
    },
    "monthly": {
      "1": {
        "month": {
          "GW": {
            "name": "Jan Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jan daily monthly average"
          },
          "SW": {
            "name": "Jan Surface water",
            "value": 1.6949126798127832,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jan daily monthly average"
          }
        }
      },
      "2": {
        "month": {
          "GW": {
            "name": "Feb Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Feb daily monthly average"
          },
          "SW": {
            "name": "Feb Surface water",
            "value": 1.7607635471690335,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Feb daily monthly average"
          }
        }
      },
      "3": {
        "month": {
          "GW": {
            "name": "Mar Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Mar daily monthly average"
          },
          "SW": {
            "name": "Mar Surface water",
            "value": 1.6252477605502833,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Mar daily monthly average"
          }
        }
      },
      "4": {
        "month": {
          "GW": {
            "name": "Apr Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Apr daily monthly average"
          },
          "SW": {
            "name": "Apr Surface water",
            "value": 1.6117883855627833,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Apr daily monthly average"
          }
        }
      },
      "5": {
        "month": {
          "GW": {
            "name": "May Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "May daily monthly average"
          },
          "SW": {
            "name": "May Surface water",
            "value": 1.7496806235752838,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "May daily monthly average"
          }
        }
      },
      "6": {
        "month": {
          "GW": {
            "name": "Jun Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jun daily monthly average"
          },
          "SW": {
            "name": "Jun Surface water",
            "value": 1.7571054688190335,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jun daily monthly average"
          }
        }
      },
      "7": {
        "month": {
          "GW": {
            "name": "Jul Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jul daily monthly average"
          },
          "SW": {
            "name": "Jul Surface water",
            "value": 1.8129169138002832,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jul daily monthly average"
          }
        }
      },
      "8": {
        "month": {
          "GW": {
            "name": "Aug Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Aug daily monthly average"
          },
          "SW": {
            "name": "Aug Surface water",
            "value": 1.6553610668502834,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Aug daily monthly average"
          }
        }
      },
      "9": {
        "month": {
          "GW": {
            "name": "Sep Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Sep daily monthly average"
          },
          "SW": {
            "name": "Sep Surface water",
            "value": 1.6109417189252835,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Sep daily monthly average"
          }
        }
      },
      "10": {
        "month": {
          "GW": {
            "name": "Oct Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Oct daily monthly average"
          },
          "SW": {
            "name": "Oct Surface water",
            "value": 1.6353491718002835,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Oct daily monthly average"
          }
        }
      },
      "11": {
        "month": {
          "GW": {
            "name": "Nov Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Nov daily monthly average"
          },
          "SW": {
            "name": "Nov Surface water",
            "value": 1.6229035940377834,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Nov daily monthly average"
          }
        }
      },
      "12": {
        "month": {
          "GW": {
            "name": "Dec Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Dec daily monthly average"
          },
          "SW": {
            "name": "Dec Surface water",
            "value": 1.6742862685627828,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Dec daily monthly average"
          }
        }
      }
    }
  },
  "withdrawal": {
    "annual": {
      "GW": {
        "name": "Groundwater",
        "value": 0,
        "unit": {
          "id": 1,
          "name": "Million Gallons per Day",
          "abbreviation": "MGD"
        },
        "description": "Daily Annual Average Groundwater well"
      },
      "SW": {
        "name": "Surface water",
        "value": 1.8406093406436188,
        "unit": {
          "id": 1,
          "name": "Million Gallons per Day",
          "abbreviation": "MGD"
        },
        "description": "Daily Annual Average Surface water point"
      }
    },
    "monthly": {
      "1": {
        "month": {
          "GW": {
            "name": "Jan Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jan daily monthly average"
          },
          "SW": {
            "name": "Jan Surface water",
            "value": 1.8573071375591566,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jan daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.77467741925,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Jan average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.993375,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Jan average Public Water Supply"
          },
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
          "GW": {
            "name": "Feb Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Feb daily monthly average"
          },
          "SW": {
            "name": "Feb Surface water",
            "value": 1.9217761160591569,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Feb daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.8483589901249999,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Feb average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.9841624076250001,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Feb average Public Water Supply"
          },
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
          "GW": {
            "name": "Mar Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Mar daily monthly average"
          },
          "SW": {
            "name": "Mar Surface water",
            "value": 1.7781377828091567,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Mar daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.7588709677499998,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Mar average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.93001209675,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Mar average Public Water Supply"
          },
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
          "GW": {
            "name": "Apr Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Apr daily monthly average"
          },
          "SW": {
            "name": "Apr Surface water",
            "value": 1.7618172183091567,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Apr daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.761625,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Apr average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.9109375000000001,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Apr average Public Water Supply"
          },
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
          "GW": {
            "name": "May Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "May daily monthly average"
          },
          "SW": {
            "name": "May Surface water",
            "value": 1.910198266809157,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "May daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.8400806452499999,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily May average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.9808629032499999,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily May average Public Water Supply"
          },
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
          "GW": {
            "name": "Jun Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jun daily monthly average"
          },
          "SW": {
            "name": "Jun Surface water",
            "value": 1.9156755515591568,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jun daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.858541666625,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Jun average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.967879166625,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Jun average Public Water Supply"
          },
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
          "GW": {
            "name": "Jul Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jul daily monthly average"
          },
          "SW": {
            "name": "Jul Surface water",
            "value": 1.9726821376841566,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Jul daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.907580645125,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Jul average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.97584677425,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Jul average Public Water Supply"
          },
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
          "GW": {
            "name": "Aug Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Aug daily monthly average"
          },
          "SW": {
            "name": "Aug Surface water",
            "value": 1.8127184278091568,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Aug daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.76366935475,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Aug average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.9597943547500001,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Aug average Public Water Supply"
          },
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
          "GW": {
            "name": "Sep Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Sep daily monthly average"
          },
          "SW": {
            "name": "Sep Surface water",
            "value": 1.7659755516841569,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Sep daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.7324166666249999,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Sep average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.9443041667500001,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Sep average Public Water Supply"
          },
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
          "GW": {
            "name": "Oct Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Oct daily monthly average"
          },
          "SW": {
            "name": "Oct Surface water",
            "value": 1.7899079440591568,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Oct daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.759516129,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Oct average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.94113709675,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Oct average Public Water Supply"
          },
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
          "GW": {
            "name": "Nov Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Nov daily monthly average"
          },
          "SW": {
            "name": "Nov Surface water",
            "value": 1.7753130518091567,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Nov daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.75925,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Nov average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.9268083335,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Nov average Public Water Supply"
          },
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
          "GW": {
            "name": "Dec Groundwater",
            "value": 0,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Dec daily monthly average"
          },
          "SW": {
            "name": "Dec Surface water",
            "value": 1.8282305246841561,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Dec daily monthly average"
          }
        },
        "code": {
          "MI": {
            "name": "Mining",
            "value": 0.801935483875,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Dec average Mining"
          },
          "WS": {
            "name": "Public Water Supply",
            "value": 0.9370403224999999,
            "unit": {
              "id": 1,
              "name": "Million Gallons per Day",
              "abbreviation": "MGD"
            },
            "description": "Daily Dec average Public Water Supply"
          },
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
    },
    "permitted": {
      "Well": {
        "name": "Permitted Surface water",
        "value": 0,
        "unit": {
          "id": 1,
          "name": "Million Gallons per Day",
          "abbreviation": "MGD"
        },
        "description": "Daily Annual Average PermittedSurface water point"
      },
      "Intake": {
        "name": "Permitted Surface water",
        "value": 0.3811475409836066,
        "unit": {
          "id": 1,
          "name": "Million Gallons per Day",
          "abbreviation": "MGD"
        },
        "description": "Daily Annual Average PermittedSurface water point"
      }
    }
  }
}
```

