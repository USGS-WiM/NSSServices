# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [Unreleased](https://github.com/USGS-WiM/NSSServices/tree/dev)

### Added


### Changed


### Removed


### Fixed
- #258 Fix math associated with QPPQ


## [v2.2.0](https://github.com/USGS-WiM/NSSServices/releases/tag/v2.2.0) - 2022-03-24

### Added
- Added englishUnitTypeID, metricUnitTypeID and statisticGroupID to regressionTypes
- Add docker support
- Added Si output

### Changed
- Update readme
- Filtered GetRegressions endpoint by regression region
- Filtered GetCitations endpoint by regression region
- Filtered GetStatisticGroups endpoint by regression region
- Check if all BCs are available for each equation in a regression region, if required BCs are not available for an equation it returns a value of -99999 for the equation. When an equation can not be calculated a disclaimer message is added.
- If an equation cannot be calculated and that equation is needed for FDCTM then an error is added to the messages and FDCTM is not calculated or returned.
- Update version numbers

### Fixed
- Fix for get citation endpoint
- Throw error when QPPQ index gage has no flow

## [v2.1.2](https://github.com/USGS-WiM/NSSServices/releases/tag/v2.1.2) - 2021-05-06

### Changed
- Extended timeout in code to 5 minutes, eliminating issues with large basins timing out before regression regions were returned

### Fixed
- Issue where edits to regression regions failed when no limitations were attached

## [v2.1.1](https://github.com/USGS-WiM/NSSServices/releases/tag/v2.1.1) - 2021-04-06

### Changed
- Limitation variables now are deleted when the limitation is edited if the variables are not included in the edit
  
### Fixed
- An issue with status filters at the regions/{region}/scenarios endpoint
- Issues with updating variable types that have the same default English and Metric units when the full units are attached
- Issues with some equations validating when skipping the equation test

## [v2.1.0](https://github.com/USGS-WiM/NSSServices/releases/tag/v2.1.0) - 2021-03-26

### Added
- Added post/put/delete endpoints for methods

### Changed
- Multiple updates made to service documentation, including additional information for variables and changing "regions" to "study areas"
- Batch scenario endpoint updates, enhanced to allow users to skip the equation check
- Edited variable functionality to allow default units and statistic groups
- Enhancements made to the Flow Duration Curve Transfer Method (or QPPQ), now uses published flow durations for computation
- renamed UnitsSystems to UnitSystems

### Fixed
- Authentication issues

## [v2.0.0](https://github.com/USGS-WiM/NSSServices/releases/tag/v2.0.0) - 2020-08-10

### Changed
- Initial release of update to dotnetcore (currently v3.1.3)
  
## [v1.0.4](https://github.com/USGS-WiM/NSSServices/releases/tag/v1.0.4) - 2017-11-13
## [v1.0.3](https://github.com/USGS-WiM/NSSServices/releases/tag/v1.0.3) - 2017-02-23
## [v1.0.2](https://github.com/USGS-WiM/NSSServices/releases/tag/v1.0.2) - 2017-01-09
## [v1.0.1](https://github.com/USGS-WiM/NSSServices/releases/tag/v1.0.1) - 2017-01-05