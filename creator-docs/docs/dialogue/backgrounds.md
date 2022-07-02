# Background Metadata

A guide for what to target when targeting backgrounds

---

## How to use Background Metadata

Background metadata can be used in dialogue in one of two ways:
 
 - By directly testing the background metadata value  
 - By using background tags generated from the background metadata  

With the first method, you use a Variable Test to directly check the background metadata value:  
```
~background.location~ == indoors
~background.category~ == bar
~background.privacy~ == private
~background.water~ == true
```

With the second method, you use a Variable Test to check if a the background has an associated tag:  
```
~background.tag.indoors~ == true
~background.tag.bar~ == true
~background.tag.private~ == true
~background.tag.water~ == true
```

These two methods produce _identical_ results.
Which one you use is up to preference.

(If the metadata you're testing is a simple boolean value, though, directly
testing the metadata value involves a bit less typing!)

### `if` functions

Each tag can be tested in dialogue directly, yielding different text depending on whether it's set or not:  
```
It's hot in ~background.if.indoors(here|this place)~!
```

## The ID (`id`)

`~background.id~` is currently the same as just `~background~`.  Some
time in the future, `~background~` may instead become the same as
`~background.term~` (see below).

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

## Term (`term`)

This metadata value is intended as a better term for the background
than its ID for use in dialogue, in some cases. If not set,
`~background.term~` will yield the same value as `~background~`.

## Preposition (`preposition`)

The proper preposition to describe being at the background
location. Defaults to "in". Examples: _in_ the bedroom, _on_ the roof,
_at_ the pool.

## Adverb (`adverb`)

A (prepositional) adverb matching the general location. Can be "up"
(for rooftops), "in" or "out".  Currently automatically determined
from the surface (roof or other) and location (indoors or outdoors).

## Location (`location`)

This metadata value is used to indicate whether the background is of an
indoors location or an outdoors location.

Generally, all backgrounds have a defined `location` value.

A background's `location` is also automatically added as a background tag.

### Possible Values
| Value      | Description 
| ---------- | ------------
| `indoors`  | The background depicts an indoors location.
| `outdoors` | The background depicts an outdoors location.


## Location Category (`category`)

This metadata value is used for some backgrounds to further categorize
what kind of location is depicted.

Not every background has a defined `category`, such as the
'propless' backgrounds (Classic, Romantic, Purple Room, etc).

Backgrounds with a defined `category` are automatically given a
corresponding tag.

### Current Possible Values
| Value     | Description
| --------- | -----------
| `bar`     | This background depicts a bar of some form.
| `school`  | This background depicts part of a school.
| `home`    | This background depicts a home.
| `forest`  | This background depicts a forested area.

### Examples

**Bars:**

   - Inventory  
   - Tiki Bar  

**School:**

   - Classroom  
   - Hall (offline)  

**Homes:**

   - Bedroom  
   - Mansion  

**Forests:**

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

**Hot:**

   - Tiki Bar
   - Beach
   - Hot Springs

**Cold:**

   - Haunted Forest


## Location Privacy (`privacy`)

This metadata value indicates how private the background's setting is.

Backgrounds with a defined `privacy` level are automatically given a corresponding tag.

### Current Possible Values
| Value     | Description
| --------- | -----------
| `private` | This background's setting depicts a venue assumed to be set aside specifcally for strip poker or that is otherwise not open to the general public.
| `public`  | This background's setting depicts any location that could be open to any passerby or general patron of the venue, as well as outdoor areas.

### Examples

**Private:**

   - Inventory
   - Beach _(note: this could go either way depending on how you headcanon it)_
   - Tiki Bar
   - Bedroom

**Public:**

   - Classroom
   - Poolside
   - Street
   - Haunted Forest 


## Water (`water`)

This metadata value indicates whether there is a nearby body of water in the
background.

This is a boolean value; it will either be `true` or completely undefined.

Backgrounds with this set will also be automatically tagged with the `water` tag.

### Examples

 - Hot Spring
 - Poolside
 - Beach

 
## Bathing Location (`bathing`)

This metadata value indicates whether this is a location where people
are expected to get naked for bathing.

This is a boolean value; it will either be `true` or completely undefined.

Backgrounds with this set will also be automatically tagged with the `bathing` tag.

### Examples

 - Hot Spring
 - Poolside
 - Beach

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


## Dark Setting (`dark`)

This metadata value indicates the location is noticeably dark.

This is a boolean value; it will either be `true` or completely undefined.

Backgrounds with this set will also be automatically tagged with the `dark` tag.

### Examples

 - Haunted Forest
 - Purple Room
 - Night (offline)