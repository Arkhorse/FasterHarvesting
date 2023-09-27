# Custom List
Your custom list must be a json file, placed in `\{GamePath}\Mods\FasterHarvesting\`. One file per item, so I recommend using the `ObjectName` to name the json file (eg: `ObjectName.json`). At this time, I don't believe there is any way to dynamically create mod settings entries for these extra items.
<br/>
## Example

```json
{
	"ObjectName": "INTERACTIVE_LimbA_Prefab",
	"ObjectBreakDownTime": 1.5,
	"ObjectBreakDownTimeOriginal": 1.5
}
```

## `ObjectName` 
The prefab name without the extension. It should always end in `_Prefab`.<br/>
Note: If you get an item with a name ending in `_LOD#`, remove that bit and replace it with the above.
## `ObjectBreakDownTime` 
The amount of time to break down this item. The game uses a `float` to represent amount of time in hours. So `1.0f` would be 1 hour.<br/>
### Limits:
* Must be between `0.01f` and `12.00f`
## `ObjectBreakDownTimeOriginal`
This is the original breakdown time. This is used incase the mod needs to revert settings for any reason. Same limits as above, however you will be getting this from the game data, so it should not be an issue
