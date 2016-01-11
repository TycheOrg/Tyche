#Tyche
[![Join the chat at https://gitter.im/TycheOrg/Tyche](https://badges.gitter.im/TycheOrg/Tyche.svg)](https://gitter.im/TycheOrg/Tyche?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)  [![NuGet](https://img.shields.io/nuget/dt/Tyche.svg)](https://www.nuget.org/packages/Tyche/)  [![NuGet](https://img.shields.io/nuget/v/Tyche.svg)](https://www.nuget.org/packages/Tyche/)

[Tyche](https://en.wikipedia.org/wiki/Tyche) was the Greek god of fortune. (In Rome she is know as Fortuna).

Tyche can be used to generate random names for releases or anything else your :heart: may desire.

Names are generated as pairs along the following theme: `{morpheme} {item from a given (or randomized) category}`.
Each name that is generated is stored so as to ensure names are unique.
##TOC
- [Contributions](#contributions)
- [General](#general)
- [API](#api)
 - [Generate](#generate)
   - [Category Specified](#category-specified)
   - [No Fail on invalid category](#no-fail-on-invalid-category)
 - [GetAvailableCategories](#getavailablecategories)
 - [SavePreviousNames](#savepreviousnames)
 - [ResetPreviousNames](#resetpreviousnames)
- [Source](#source)
- [See Also](#see-also)
- [License](#license)

##Contributions
Contributions are quite welcome. Please just read the [Guidelines](CONTRIBUTING.md) first.

##General
If no [source](#source) is provided then the default data source will be used.

##API
###Generate
This basic sample will use the default data set to create a name, using a random category.
```c#
var g = new Generator();

var name = g.Generate();
```
```c#
var g = new Generator();

var name = g.Generate();
```
####Category Specified
The generator will use the category provided to pick the second half of the name from.
```c#
var g = new Generator();

var name = g.Generate("Mammals");
```
####No Fail on invalid category
If the category passed in is invalid, instead of failing a random category will be picked to use instead.
```c#
var g = new Generator();

var name = g.Generate("Non-existent Category", false);
```
###GetAvailableCategories
Gets the list of categories availble from the source.
```c#
var g = new Generator();

var allCategories = g.GetAvailableCategories();
```
###SavePreviousNames
Persists the current list of previously generated name.
The persistence model is defined by the source used when creating the generator.
```c#
var g = new Generator();

g.SavePreviousNames();
```
###ResetPreviousNames
Clears the list of previously generated names that is currently held by the source.
**Do not rely on the source implementing a save within this method. Use it as shown in the sample.**
```c#
var g = new Generator();

g.ResetPreviousNames();
g.SavePreviousNames();
```
##Source
Please see the [docs](docs) to learn about the `Source` api, and how to write your own.
##Future
Please see the [Roadmap](Roadmap.md) to see what the future may hold for Tyche.
##See Also
See [TycheOrg home repo](https://github.com/TycheOrg/Home) for a general roadmap of the ecosystem.
##License
This project is released under the the MIT License. <br/> Please refer to the [LICENSE file](LICENSE) for full details.
