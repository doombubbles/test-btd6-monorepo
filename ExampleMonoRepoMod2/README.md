<a href="/ExampleMonoRepoMod2.dll">
    <img align="left" alt="Icon" height="90" src="Icon.png">
    <img align="right" alt="Download" height="75" src="https://raw.githubusercontent.com/gurrenm3/BTD-Mod-Helper/master/BloonsTD6%20Mod%20Helper/Resources/DownloadBtn.png">
</a>

<h1 align="center">ExampleMonoRepoMod2</h1>

An example mod in a mono repo where the dll is hosted at the root level of the mono repo.

This source code folder is not necessary for the mod to work but is included for demonstration purposes.


Doing it this way does mean that the ModHelperData values need to be updated in two places: ExampleMonoRepoMod2.json in the
folder above and whichever way you use here to include it in the mod itself. Since the other one has to be JSON already,
you can just use a JSON for both and setup a post-build task to copy/rename it like this project does.

[![Requires BTD6 Mod Helper](https://raw.githubusercontent.com/gurrenm3/BTD-Mod-Helper/master/banner.png)](https://github.com/gurrenm3/BTD-Mod-Helper#readme)
