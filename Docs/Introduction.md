## Introduction
The U.S. Geological Survey [National Streamflow Statistics (NSS)](https://test.streamstats.usgs.gov/nss/) program is used for a variety of water-resources and emergency planning, management and regulatory purposes, and for design of structures such as bridges and culverts. NSS provides U.S. Geological Survey developed and published regression equations, such as mean annual and monthly mean flows, flow-duration percentiles, and peak and low flows for or every State, the Commonwealth of Puerto Rico, and several metropolitan areas in the United States. 

The NSS Services API provides an easy way to explore and consumed resources by custom client applications. As documented by this page, which can also serve as a simple URL builder, the NSS Services API is built following [RESTful](http://en.wikipedia.org/wiki/Representational_state_transfer) principles to ensure scalability and predictable URLs. JSON is returned by all API responses, including errors and example results and summaries for each resource. This API is intended to provide some guidance in working with the services, however some methods may only provide visual examples due to authentication requirements or limitations of the API documentation application. 

## Getting Started
The URL and sample of each resource can be obtained by accessing one of the following resources located on the sidebar (or selecting the menu button located on the bottom of the screen, if viewing on a mobile device).

<img alt="rsz_capture.jpg" 
src="data:image/jpeg;base64,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" />

The sidebar displays the available resources with accompanying HTTP method and associated resources. Selecting a resource will open the resources documentation page which includes a resource and service description, url and other accompanying information listed below:     

Every resource is exposed as a URL and follows the outlined pattern described below:
* The description of the resource
* The Service URL.
* URL Parameters (if any).
  * Parameter Name,
  * Value Type,
  * A description of what the parameter represents,
  * Whether the parameter is required or optional,
  * And an example input parameter.
* A REST Query URL test tool that builds an example url, based on the given input parameter values.
* And an example response from the REST Query.

## Web Service Request Example
Web service request can be performed using most HTTP client libraries. The following examples will be illustrated using jQuery's `$.ajax()` function to represent a typical request performed by a client application.
```
$.ajax({
		url: uri,
		type: methodtype,
		contentType: "application/json",
		accepts: "application/json",
		cache: false,
		dataType: 'json',
		data: JSON.stringify(data),
		success: function(resultData) { 
            var results = resultData;
        },
		error: function(jqXHR) {
			console.log("ajax error " + jqXHR.status);
		}
	});

 ```
 Where:  
* `uri` is the string containing the URL to which the request is sent,
* `data` is a plain object or string that is sent to the server with the request, 
* `methodtype` is the HTTP request-response method, such as GET, PUT, POST, or DELETE,
* and there are 2 callback functions: `success` and `error`, which will be invoked once the asynchronous function associated when the request ends with either the result or an error.
