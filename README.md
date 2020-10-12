# Expert Debug

## Expert Watch

### How to

Just add the variable name as if you were in the native Watch window.

![preview](https://github.com/txiribimakula/expert-debug/blob/master/.github/eddemo.gif)

#### Configuration

Users have the possibility to create and customize how their own types are interpreted with *Blueprints*.

Premise is to indicate which members have to be accessed and what information is retrieved from each member.

```yaml
{
  # Array with the typenames that are supported by this blueprint
  "keys": ["Txiribimakula.ExpertWatch.Geometries.Point"],
  # Start of the blueprint object
  "root":
  {
    # This blueprint represents a point
    "key": "point",
    # Array of members that are accessed from the current member (Txiribimakula.ExpertWatch.Geometries.Point)
    "members": [
      {
        # Access the member X of the the current member 
        "name": "X",
        # This member represents the x coordinate of the point
        "key": "x"
      },
      {
        # Access the member Y of the current member
        "name": "Y",
        # This member represents the y coordinate of the point
        "key": "y"
      }
    ]
  }
}
```

| Key    | Value             | Description                                                 |
| ------ | ----------------- | ----------------------------------------------------------- |
| *keys* | array of strings  | lists the type names that are supported by this *blueprint* |
| *root* | object            | contains all the *blueprint*'s logic                        |
| *key*  | reserved keywords | contains all the *blueprint*'s logic                        |
| *...*  |  |  |
> Check this [configuration file](https://github.com/txiribimakula/expert-debug/blob/master/Txiribimakula.ExpertDebug/expert-debug-test.json) to learn by example.

*Blueprints* are imported via *Tools - Options*.

![preview](https://github.com/txiribimakula/expert-debug/blob/master/.github/edcfg.gif)

### Features
#### Background loading
* Elements are loaded asynchronously and progress can be observed on the fly.
* Turn off loading so they aren't reloaded (i.e. when debug is resumed).
* Loading can be interrumpted mid-operation.

#### Show / Hide
* Each variable can be shown / hidden.

#### Feedback
* When possible, progressbar is shown.
* Contextual information is shown for each variable.

#### Zoom / Panning
* Roll the mouse wheel to zooom in/out.
* Hold and drag the mouse wheel to pan across.

#### Variable support management
* Use the configuration window to edit/import/export blueprints.

### Upcoming
- [ ] Automatic color for each variable.
- [ ] Measure distances.
- [ ] Manual color picker for each variable.
- [ ] Dark mode support.
- [ ] Blueprint creation wizard.
- [ ] Toggable sense drawing.
- [ ] Extended blueprint cusomization.
