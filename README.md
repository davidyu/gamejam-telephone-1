## Installing Unity

1. Download and install [Unity Hub](https://unity.com/download)
2. Create an account so you can sign into Unity Hub
3. In Unity Hub, under Installs, click on Install Editor
4. Select and install 2021.3.0f1 under Long Term Support (LTS)

## Downloading and opening this project

1. Using your git client of choice (commandline or [Github Desktop](https://desktop.github.com/)), clone this repository
2. In Unity Hub, under Projects, click on Open or Add project from disk
3. In the dialog, navigate to the *parent folder* where this repository was cloned
4. Click on the repository folder (game-jam-telephone-1) and choose 'Add project'
5. Unity should now automatically open the project
6. To run the game, click on the play button in the center top bar of the Unity editor

## Tips
- For code, it is advisable to focus on the top-level files in the Assets/Scripts folder, you may not have enough time to modify and/or debug the code in the Daedalus folder
- When in doubt, reach out to the folks who worked on iterations before you!

Please make sure to update this README if you feel it would help the next person, and make sure to acknowledge any authors of assets you used in the credits section below.

## Commit permissions

There are a few ways you can commit changes to the repository. You can either fork this repository on github by clicking on the fork button near the top right.
That will give you personal version of the repository where you automatically have write access. However, if you do so you will be responsible for creating pull requests so you can merge your iteration back to the main branch after you are done.
Alternatively, you can reach out to David Yu for collaborator/commit permissions to the original repository. This is the recommended approach.

## After your turn (building)

1. Commit your changes to the repository
2. Click on File -> Build Settings -> Build
3. Create and select a folder outside of the repository folder (do not commit the binary output of the build to git)
4. Package the binary output of the build in a zip file (call it something distinctive - this will be the name of your iteration of the game; as an example, the first iteration of this game is called "David" but you can call it anything you want)
5. On the github page, click the '[some number of] tags' button in the secondary toolbar where the branch options are located
6. Draft a new release and attach the zip file you have created (make sure to enter a tag name in the tag dropdown to create a new tag)

## Credits

- [Daedalus Dungeon Generator](https://troglobytes.itch.io/daedalus)
- [Hand Bomb by SkywolfGameStudios](https://skywolfgamestudios.itch.io/black-hand-bomb)
- [Free 2D Impact FX by Inguz Media](https://assetstore.unity.com/packages/vfx/particles/fire-explosions/free-2d-impact-fx-201222)
