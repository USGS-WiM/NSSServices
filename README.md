![WiM](wimlogo.png)

# Navigation Services

StreamStats supporting wateruse REST web services.

### Prerequisites

[Visual Studio 2017](https://www.visualstudio.com/)

[.NET Core](https://www.microsoft.com/net/core#windowscmd)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Installing

https://help.github.com/articles/cloning-a-repository/

Open the solution file (.sln) using perfered IDE.

## Building and testing

No testing files are currently available for this repository

## Deployment on IIS

see [link](https://docs.microsoft.com/en-us/aspnet/core/publishing/iis?tabs=aspnetcore2x)  for detailed instructions for deploying to windows server.

* Download and install [windows server hosting bundle](https://www.microsoft.com/net/download/core#/runtime) on the server.
* Create new application pool specifying the .netCLR version property to "No Managed Code".

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

*Note that your alias for the remote origin may differ.

## Authors

* **[Jeremy Newson](https://www.usgs.gov/staff-profiles/jeremy-k-newson)**  - *Lead Developer* - [USGS Web Informatics & Mapping](https://wim.usgs.gov/)

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
