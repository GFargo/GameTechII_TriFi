Team Name: Team Miraculous Dudes
Game Title: D'Orchestra

Authors:
Producer: Timothy Scribner
Lead Designer: Griffen Fargo
Designers: Philip Holland, Steven Cannon
Artist: Joshua Stowe
Programmer: Chris Brough

Please use Unity3D Project folder structure:

./Extensions/ // (for extensions, e.g., iTween), name must be 'extensions', otherwise iTweenpath will break
./Resources/  // (for prefabs, sounds, etc.)
./Scenes/     // (for scenes)
./Scripts/    // (for scripts [C# ONLY!])

Important! Upgrade to Unity 3.5 Public Beta: http://unity3d.com/unity/preview/download
Git Help: http://progit.org/, http://progit.org/ebook/progit.pdf

Controls:
---------

Mouse Hover: Highlights Tiles
Mouse Click: Activates Tiles
Key V: Build Tower (note: a tile must be active)
Key X: Destroy Tower (note: a tile must be active)

** these are just temporary controls. **

Tile Map:
---------
Adding new Tiles and Editing Tile IDs:
1. Add new tiles to Unity project in: "Project > Resources > Prefabs"
2. After adding new tiles to prefabs, you must add the tile and an id to 'id.txt' in: "Project > Resources > Tiles"

e.g.,

ID Name
--------
0, Default
1, Base
2, Void
...

Tile Map Creation: 
1. Create a '.txt' file in: "Project > Resources > Tiles"
2. Make a grid of numbers (i.e., the IDs associated with tiles).

e.g., 

0, 0, 0 
0, 1, 0
0, 1, 0
0, 0, 0 
 
Tile Map Loader Setup:

1. Add 'TileMap' script to an object that persists through the entirety of a level (e.g., the 'Main Camera')
2. Attach the 'id' and 'level-?' text asset to the 'TileMap' script in the Inspector.
3. That's all!

edited by cb on 2/9/12 at 10:00 am
edited by cb on 2/8/12 at 7:38 pm
edited by cb on 2/1/12 at 9:01 pm
edited by cb on 2/1/12 at 4:17 pm
