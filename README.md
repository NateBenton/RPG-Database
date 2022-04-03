# RPG-Database
This project is a custom RPG editor, with a simple runtime UI that shows the data populating. Heroes, weapons, armors, spells and more can be configured in this designer focused toolset.

The system is based on ScriptableObjects. At runtime, all of the SO data is used to populate the GameManager class.

Highlights:

* A strong understanding of custom Unity Editors; third party packages such as Odin were specifically ignored for this project (although I do have a firm understanding of Odin as well!)
* One database window allows designers to create, and modify: Heroes, Classes, Skills, Level Curves, Weapons, Armors and Starting Party Members.
* ScriptableObjects are created and stored in the proper folder automatically by the editor. File name generation assures friendliness in source control team environments.
* Graphics in this package are provided courtesy of FinalBossBlues.
