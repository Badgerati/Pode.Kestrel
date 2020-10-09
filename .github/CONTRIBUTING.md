# Contributing to Pode.Kestrel

:star2::tada: First of all, thank you for taking the time and contributing to Pode.Kestrel! :tada::star2:

The following is a set of guidelines for contributing to Pode.Kestrel on GitHub. These are mostly guidelines, not rules. Use your best judgment, and feel free to propose changes to this document in a pull request.

## Table of Contents

* [Code of Conduct](#code-of-conduct)
* [I just have a Question](#i-just-have-a-question)
* [About Pode](#about-pode)
* [How to Contribute](#how-to-contribute)
  * [Issues](#issues)
  * [Branch Names](#branch-names)
  * [Pull Requests](#pull-requests)
  * [Building](#building)

## Code of Conduct

This project and everyone participating in it is governed by the Pode.Kestrel's [Code of Conduct](../.github/CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code.

## I just have a Question

[![Gitter](https://badges.gitter.im/Badgerati/Pode.svg)](https://gitter.im/Badgerati/Pode?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

If you have a question, feel free to either ask it on [GitHub Issues](https://github.com/Badgerati/Pode.Kestrel/issues), or head over to Pode's [Gitter](https://gitter.im/Badgerati/Pode?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge) channel.

## About Pode

Pode is a PowerShell framework/web server. The aim is to make it purely PowerShell, with *no* external dependencies - other than what is available in .NET Core. This allows Pode to be very lightweight, and just work out-of-the-box when the module is installed on any platform.

The only current exception to the "all PowerShell" rule is the socket listener Pode uses. This listener is a part of Pode, but is written in .NET Core.

## How to Contribute

When contributing, please try and raise an issue first before working on the issue. This allows us, and other people, to comment and help. If you raise an issue that you're intending on doing yourself, please state this within the issue - to above somebody else picking the issue up.

### Issues

You can raise new issues, for bugs, enhancements, feature ideas; or you can select an issue currently not being worked on.

### Branch Names

Branches should be named after the issue you are working on, such as `Issue-123`. If you're working on an issue that hasn't been raised (such as a typo, tests, docs, etc), branch names should be descriptive.

When branching, please create your branches from `develop` - unless another branch is far more appropriate.

### Pull Requests

When you open a new Pull Request, please ensure:

* The Pull Request must be done against the `develop` branch.
* The title of the Pull Request contains the original issue number (or is descriptive if there isn't one).
* Details of the change are explained within the description of the Pull Request.
* Where possible, include examples of how to use (if it's a new feature especially).

Once opened GitHub will automatically run CI on Windows, Linux and MacOS, as well as Code Coverage.

### Building

Before using Pode.Kestrel, you will need to compile the Listener first. To do so you will need [`Invoke-Build`](https://github.com/nightroman/Invoke-Build). Once installed, run the following:

```powershell
Invoke-Build Build
```
