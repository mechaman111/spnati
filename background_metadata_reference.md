# Background Metadata

## Status (`status`)

This metadata value is used to indicate whether this background should
be available in the online version or not.

If a background does not have a defined `status`, it is assumed to be
available online and offline, similarly to opponent statuses.

### Possible Values
| Value       | Description 
| ----------- | ------------
| (no value)  | The background should be available online and offline.
| `offline`   | The background should only be available offline.


## Location (`location`)

This metadata value is used to indicate whether the background is of an
indoors location or an outdoors location.

Generally, all backgrounds have a defined `location` value.

A background's `location` is also automatically added as a background tag.

### Possible Values
| Value      | Description 
| ---------- | ------------
| `indoors`  | The background depicts an indoors location.
| `outdoors` | The background depicts an ourdoors location.


## Location Type (`location_type`)

This metadata value is used for some backgrounds to further categorize
what kind of location is depicted.

Not every background has a defined `location_type`, such as the
'propless' backgrounds (Classic, Romantic, Purple Room, etc).

Backgrounds with a defined `location_type` are automatically given a
corresponding tag.

### Current Possible Values
| Value     | Description
| --------- | -----------
| `bar`     | This background depicts a bar of some form.
| `school`  | This background depicts part of a school.
| `home`    | This background depicts a home.
| `forest`  | This background depicts a forested area.

### Examples
 - **Bars:**
   - Inventory
   - Tiki Bar
 - **School:**
   - Classroom
   - Hall (offline)
 - **Homes:**
   - Bedroom
   - Mansion
 - **Forests:**
   - Haunted Forest
   - Night (offline)


## Time of Day (`time`)

This metadata value indicates whether the background appears to depict
a day or night setting.

If this is not explicitly defined by the background, the local system
time will be consulted to determine this value.

Backgrounds with an explicit `time` are automatically tagged with either
`day` or `night`.

### Current Possible Values
| Value     | Description
| --------- | -----------
| `day`     | This background depicts a daytime setting.
| `night`   | This background depicts a nighttime setting.


## Temperature (`temperature`)

This metadata value indicates whether the background's setting is significantly
hot or cold.

Backgrounds with a defined `temperature` are automatically given a corresponding tag.

### Current Possible Values
| Value     | Description
| --------- | -----------
| `hot`     | This background's setting depicts a hot or at least warm environment.
| `cold`    | This background's setting depicts a cold or chilly environment.

### Examples

 - **Hot:**
   - Tiki Bar
   - Beach
   - Hot Springs
 - **Cold:**
   - Haunted Forest
   - Seasonal (offline)


## Water (`water`)

This metadata value indicates whether there is a nearby body of water in the
background.

This is a boolean value; it will either be `true` or completely undefined.

Backgrounds with this set will also be automatically tagged with the `water` tag.

### Examples
 - Hot Spring
 - Poolside
 - Beach

 
## Bathing Location (`bathe`)

This metadata value indicates whether this is a location where people
are expected to get naked for bathing.

This is a boolean value; it will either be `true` or completely undefined.

Backgrounds with this set will also be automatically tagged with the `bathe` tag.

### Examples
 - Hot Spring
 - Poolside
 - Beach


## Public Location (`public`)

This metadata value indicates whether there the setting appears to be a
publically-accessible place, regardless of the presence of onlookers.

This is a boolean value; it will either be `true` or completely undefined.

Backgrounds with this set will also be automatically tagged with the `public` tag.

### Examples
 - Classroom
 - Roof
 - Poolside
 - Street


## Voyeurism (`voyeur`)

This metadata value indicates the presence of peeping onlookers.
Note that the onlookers aren't necessarily obvious or wanted, unlike the
`exhibition` metadata value.

This is a boolean value; it will either be `true` or completely undefined.

Backgrounds with this set will also be automatically tagged with the `voyeur` tag.

### Examples
 - Street
 - Haunted Forest
 - Locker Room (look carefully)


## Exhibitionism (`exhibition`)

This metadata value indicates the presence of _obvious_ onlookers.

This is a boolean value; it will either be `true` or completely undefined.

Backgrounds with this set will also be automatically tagged with the `exhibition` tag.

### Examples
 - Street
 - Club (offline)
 - Green Screen (offline)


## Gender-Segregated Area (`gendered`)

This metadata value indicates the location is normally separated by gender.

This is a boolean value; it will either be `true` or completely undefined.

Backgrounds with this set will also be automatically tagged with the `gendered` tag.

### Example
 - Hot Spring _(Note: this is debatable-- feedback is welcome)_
 - Locker Room
 - Showers (offline)


## Dark Setting (`dark`)

This metadata value indicates the location is noticeably dark.

This is a boolean value; it will either be `true` or completely undefined.

Backgrounds with this set will also be automatically tagged with the `dark` tag.

### Example
 - Haunted Forest
 - Purple Room
 - Night (offline)