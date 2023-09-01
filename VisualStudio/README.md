# Custom List
Your custom list must be a json file, placed in `\{GamePath}\Mods\FasterHarvesting\`. One file per item, so I recommend using the `ObjectName` to name the json file (eg: `ObjectName.json`). At this time, I don't believe there is any way to dynamically create mod settings entries for these extra items.
<br/>
Example

```json
{
	"ObjectName": "INTERACTIVE_LimbA_Prefab",
	"ObjectBreakDownTime": 1.5d
}
```

`ObjectName` = The prefab name without the extension.<br/>
`ObjectBreakDownTime` = The amount of time to break down this item. The game uses a `float` to represent amount of time in hours. So `1.0f` would be 1 hour. The mod uses a `double` for other reasons.<br/>