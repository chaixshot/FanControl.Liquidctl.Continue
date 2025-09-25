# FanControl.Liquidctl.Continue

This is a fork of [jmarucha](https://github.com/jmarucha/FanControl.Liquidctl)'s original.
1. It stops **liquidctl** from starting and stopping all the time by attempting to retry on return any error.
2. Updated the **liquidctl** library to support **ASUS ROG pump embedded fan**.
3. Fixed **Degree Symbol (°)** UTF-8 error for some PC with **Windows Language for non-Unicode programs** changed.

This is a simple plugin that uses [liquidctl](https://github.com/liquidctl/liquidctl) to provide sensor data and pump control to variety of AIOs. So far it is tested with NZXT Kraken X63, but in principle shall work with [supported devices](https://github.com/liquidctl/liquidctl#supported-devices)

## Installation

1. Download **FanControl.Liquidctl.zip** from [Release](https://github.com/chaixshot/FanControl.Liquidctl.Continue/releases/latest)
2. Open **Fan Control** **>** Setting **>** Plugin **>** Install plugin... **>** Select **FanControl.Liquidctl.zip** > Open

## Setting up the developer environment

The project, after being imported to Visual Studio needs to have it reference to `FanControl.Plugins.dll` and `Newtonsoft.Json.dll` from FanControl directory. You also need to create the executable of liquidctl, which can be automatized with script `build-liquidctl.sh`.

## Screenshots

<img alt="image" src="https://github.com/user-attachments/assets/66e0f15c-cf37-4921-b6bd-88b6fd233c21" />
<img alt="image" src="https://github.com/user-attachments/assets/ea6e07db-cdac-482f-8755-8906ea24a7b7" />

## License
MIT license, because it's superior.
```
Copyright (c) 2022 Jan K. Marucha

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```

liquidctl, which is used by this plugin is provided on [GPLv3](https://github.com/liquidctl/liquidctl/blob/main/LICENSE.txt).
