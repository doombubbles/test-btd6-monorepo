Examples for setting up a BTD6 mod monorepo to work with the mod browser

ExampleMonoRepoMod1 shows a mod hosted inside a folder in the repo. Note the *folder's* name as the entry in ModHelperMods.json.

ExampleMonoRepoMod2 shows a mod hosted at the root level of the repo. Note the *ModHelperData file's* name as the entry in ModHelperMods.json.

The btd6.targets file is included here, as this could feasibly be the owners BTD6 Mod Sources folder.

These 2 mods are fully valid for appearing in the browser, but this repo just doesn't include the `btd6-mods` topic so as not to clutter the browser.