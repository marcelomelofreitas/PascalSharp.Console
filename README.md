# PascalSharp (Pascal#)
Modern Pascal on .NET Platform for all. This repository contains only command line utilities, that is the compiler for Pascal#. Compiler core and runtime library are in different repositories.

## Building
Pascal# is being developed in Visual Studio Community 2017. Use IDE or MSBuild command line to build Pascal# solution. Do not forget to add `/restore` command line key to MSBuild and setup MyGet package feed.

Use any modern (5.4+) [Mono](http://www.mono-project.com/download/) MSBuild (not xbuild) to build Pascal# on non-Windows platforms.

## Roadmap
* Get rid of 2 different binaries for command line compiler
* Improve CLI arguments parser
* Cleanup code
* Implement a daemon to improve compilation performance