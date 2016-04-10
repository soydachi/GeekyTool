![GeekyTool](https://raw.github.com/dachibox/GeekyTool/dev/assets/GeekyToolHeader.png)

[**GeekyTool**](https://github.com/dachibox/GeekyTool) is a simple .NET toolkit inspired on [MVVMLight](https://mvvmlight.codeplex.com/), [Cimbalino Toollkit](https://github.com/Cimbalino/Cimbalino-Toolkit) and [Template10](https://github.com/Windows-XAML/Template10).

## Install GeekyTool using NuGet [![Build status](https://ci.appveyor.com/api/projects/status/3pksp70dv4e9euis?svg=true)](https://ci.appveyor.com/project/dachi/geekytool)

Package             | State | Description
--------------------|-------|--------------------------------------
[GeekyTool.Core][1] | ![Latest GeekyTool.Core stable version][4] | The portable class. Contains mvvm class, services, messenger, etc..
[GeekyTool][2]      | ![Latest GeekyTool stable version][5] | An concrete implementation of GeekyTool.Core on each platform.
[GeekyTool.UI][3]   | ![Latest GeekyTool.UI stable version][6] | Controls and UI based implementation for each platform.


## Documentation

See [wiki](https://github.com/dachibox/GeekyTool/wiki) _(under construction...)_




## Supported platforms

GeekyTool is a **Portable Class Library** implemented on each specific platforms:

- **.NET** >= 4.5
- **UWP**

You can find all the UWP code examples under `samples\UWP` ([@github](https://github.com/dachibox/GeekyTool/tree/dev/samples/UWP)) folder.

## Author

| [![Dachi](https://avatars1.githubusercontent.com/u/1771785?v=3&s=130)](https://github.com/dachibox) |
|---|---|
| [Dachi Gogotchuri](https://github.com/dachibox) |

## Contributors

| [![Garolard](https://avatars2.githubusercontent.com/u/1324904?v=3&s=100)](https://github.com/garolard) |
|---|---|
| [Gabriel Ferreiro](https://github.com/garolard) |

## License

[Apache License](https://github.com/dachibox/GeekyTool/master/LICENSE)


    Copyright 2015 Dachi Gogotchuri

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.

[1]: https://www.nuget.org/packages/GeekyTool.Core
[2]: https://www.nuget.org/packages/GeekyTool
[3]: https://www.nuget.org/packages/GeekyTool.UI
[4]: https://img.shields.io/nuget/v/GeekyTool.Core.svg?style=flat-square "Latest GeekyTool.Core stable version"
[5]: https://img.shields.io/nuget/v/GeekyTool.svg?style=flat-square "Latest GeekyTool stable version"
[6]: https://img.shields.io/nuget/v/GeekyTool.UI.svg?style=flat-square "Latest GeekyTool.UI stable version"