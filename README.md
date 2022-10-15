<div align="center">
<img alt="Logo" height="50" src="https://raw.githubusercontent.com/berrnd/Traything/master/logo.svg?sanitize=true" />
<h3>Traything</h3>
<h4>A simple but practical (Windows) tray helper tool<br>Created by <a href="https://github.com/berrnd">@berrnd</a></h4>
</div>

-----

## Features / Motivation

I wanted to have a simple tool which stays in the tray and let you define custom menu items to

- launch programs
- execute web requests (`GET` / `POST`)
- has a little web browser integrated (using [CefSharp](https://cefsharp.github.io))
- has a little media player integrated (using [LibVLCSharp](https://github.com/videolan/libvlcsharp))
- is a portable application, to have a kind of central configuration while also showing specific menu items only on certain machines

to have some things at hand right from the tray - I haven't found that, so this is Traything.

I personally use this to conveniently control my smart home and many more things.

## Questions / Help / Bug Reports / Feature Requests

Please use the [Issue Tracker](https://github.com/berrnd/Traything/issues/new/choose) for any requests.

## How to install

Traything is a portable application, just unpack the [latest release](https://github.com/berrnd/Traything/releases/latest) and launch `Traything.exe`.

## How to update

Just overwrite everything with the [latest release](https://github.com/berrnd/Traything/releases/latest) while keeping `Traything.xml` (in this file any configuration is stored).

## Contributing / Say Thanks

Any help is welcome, feel free to contribute anything which comes to your mind or see [https://berrnd.de/say-thanks](https://berrnd.de/say-thanks?project=Traything) if you just want to say thanks.

## Roadmap

There is none. The progress of a specific bug/enhancement is always tracked in the corresponding issue, at least by commit comment references.

## Screenshots

![tray-menu](https://github.com/berrnd/Traything/raw/master/.github/publication_assets/tray-menu.png "tray-menu")

![main-window](https://github.com/berrnd/Traything/raw/master/.github/publication_assets/main-window.png "main-window")

![edit-item](https://github.com/berrnd/Traything/raw/master/.github/publication_assets/edit-item.png "edit-item")

![actions](https://github.com/berrnd/Traything/raw/master/.github/publication_assets/actions.mp4 "actions")

## How to build

You will need Visual Studio 2022. All dependencies are included, available via NuGet or will be downloaded at compile time.

## License

The MIT License (MIT)
