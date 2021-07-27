![WiM](wimlogo.png)

# NSS Services

StreamStats supporting National StreamFlow Statistics REST web services.

### Prerequisites

[Visual Studio 2019](https://www.visualstudio.com/)

[.NET Core](https://www.microsoft.com/net/core#windowscmd)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Installing

https://help.github.com/articles/cloning-a-repository/

Open the solution file (.sln) using preferred IDE. Visual Studio Community is recommended.

In order to conduct database functions, you'll need to configure the User Secrets using the database credentials. To do so:
1. Open the .sln in Visual Studio Community
2. In the Solution Explorer, find the "NSSServices" item, right click on it and select "Manage User Secrets".
3. In the secrets.json, copy the following code and fill in the corresponding information for the StreamStats RDS Test instance (found in the WIM Keepass or ask a WIM team member for the information):
```
{
  "dbuser": "",
  "dbpassword": "",
  "dbHost": ""
}
```

## Building and testing

To build a `dist` folder before deployment to a server:
1. Open the .sln file in Visual Studio Community
2. In the Solution Explorer, find the "NSSServices" item, right click on it and select "Publish".
3. In the Publish screen, keep the default information and hit the Publish button.

## Deployment on IIS

see [link](https://docs.microsoft.com/en-us/aspnet/core/publishing/iis?tabs=aspnetcore2x)  for detailed instructions for deploying to windows server.

* Download and install [windows server hosting bundle](https://www.microsoft.com/net/download/core#/runtime) on the server.
* Create new application pool specifying the .netCLR version property to "No Managed Code".

To update an application already hosted on the server:
* Copy the new dist folder created from the "Building and testing" section, place it at D:\applications\NSSServices, or wherever the old dist folder is on the server (you may need to copy to your desktop on the server first)
* In the new `dist` folder on the server:
    * Edit the appsettings.json to include the database credentials and update the JWTBearer Settings secret key (you can look at past dist folders on the server for the key we usually use)
    * In the web.config, update the "ASPNETCORE_ENVIRONMENT" environment variable value to "Staging"
* In IIS, point the existing application to the new dist folder (usually you right click on the application in IIS, select Manage Application > Advanced Settings and point to the new path).

## Deployment on Linux

see [link](https://docs.microsoft.com/en-us/aspnet/core/publishing/apache-proxy) for detailed instructions for deploying to linux server

## Built With

* [Dotnetcore 2.0](https://github.com/dotnet/core) - ASP.Net core Framework

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on the process for submitting pull requests to us. Please read [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) for details on adhering by the [USGS Code of Scientific Conduct](https://www2.usgs.gov/fsp/fsp_code_of_scientific_conduct.asp).

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](../../tags). 

Advance the version when adding features, fixing bugs or making minor enhancement. Follow semver principles. To add tag in git, type git tag v{major}.{minor}.{patch}. Example: git tag v2.0.5

To push tags to remote origin: `git push origin --tags`

**Make sure to update the version in the appsettings.json for the version header.**

*Note that your alias for the remote origin may differ.

## Authors

* **[Jeremy Newson](https://www.usgs.gov/staff-profiles/jeremy-k-newson)**  - *Developer* - [USGS Web Informatics & Mapping](https://wim.usgs.gov/)
* **[Katrin Jacobsen](https://www.usgs.gov/staff-profiles/jeremy-k-newson)**  - *Developer* - [USGS Web Informatics & Mapping](https://wim.usgs.gov/)

See also the list of [contributors](../../graphs/contributors) who participated in this project.

## License

This project is licensed under the Creative Commons CC0 1.0 Universal License - see the [LICENSE.md](LICENSE.md) file for details

## Suggested Citation

In the spirit of open source, please cite any re-use of the source code stored in this repository. Below is the suggested citation:

`This project contains code produced by the Web Informatics and Mapping (WIM) team at the United States Geological Survey (USGS). As a work of the United States Government, this project is in the public domain within the United States. https://wim.usgs.gov`

## Acknowledgments

* [GeoJson.Net](https://github.com/GeoJSON-Net/GeoJSON.Net)

## About WIM

* This project authored by the [USGS WIM team](https://wim.usgs.gov)
* WIM is a team of developers and technologists who build and manage tools, software, web services, and databases to support USGS science and other federal government cooperators.
* WiM is a part of the [Upper Midwest Water Science Center](https://www.usgs.gov/centers/wisconsin-water-science-center).
