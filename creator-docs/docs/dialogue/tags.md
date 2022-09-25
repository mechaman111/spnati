# Tagging List

Attributes that characters can target

---

This is a comprehensive list of all tags you can give your characters, or which your character can target in dialogue. [Having a certain amount of filtered dialogue is necessary for being added to test](/docs/policy/testing.html).

If your character is from a new franchise, please add a new franchise tag in the Character Editor for them. Otherwise, do not add new tags yourself. However, if you have an idea for a new tag and know of multiple characters in-game it could apply to, you are welcome to suggest it.

Tags can be set to only the stages for when they are applicable. This includes tags for specific items of clothing, changes in attitude, or changes in appearance. Tags for personality, physical features, or overall outfit should be set for all stages.

# Physical Features

## Hair Colors

Many of these should be self-explanatory.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`black_hair`| |
|`blonde`    | |
|`brunette`  | |Brown hair, NOT black 
|`ginger`    | |Generally used for red hair
|`white_hair`| |For white, gray, or silver hair

### Exotic Hair Colors

`exotic_hair` is a catchall term for multi-colored or otherwise exotic hair:

|Tag Name |Supertag| Description|
|---------|--------|------------|
|`blue_hair`|`exotic_hair`| For blue/cyan/aqua hair |
|`green_hair`|`exotic_hair`| |
|`pink_hair`|`exotic_hair`| |
|`purple_hair`|`exotic_hair`| |

## Hair Length

|Tag Name|Supertag|Description|
|--------|--------|-----------|
|`bald`| | For characters with no hair on their head.
|`short_hair`| | For hair that does not pass the jawline.
|`medium_hair`| | For hair that goes below the jaw and may reach the shoulders.
|`long_hair` | |  For hair that goes beyond the shoulders.
|`very_long_hair`|`long_hair`| For hair that reaches the thighs or beyond. A character with this tag also has the `long_hair` supertype.

## Hair Style

|Tag Name|Supertag|Description
|--------|--------|-----------
|`ahoge`| | The character has a hair antenna or messy locks above their hair, like Nagisa. Does NOT imply the messy hair tag.
|`drill_hair`| | The character sports anime-style drill hair, for example Nugi-chan.
|`messy_hair`| | The character's hair is notably untidy.
|`ponytail` | | The character has their hair up in ponytail.
|`twintails`| | The character's hair is fashioned into two tails. These can be located anywhere on the head.

## Eye Colors

|Tag Name|Supertag|Description
|--------|--------|-----------
|`dark_eyes`| | For black, brown, or dark grey eyes.
|`blue_eyes`| |
|`pale_eyes`| | For white to medium-light grey eyes.
|`green_eyes`| |
|`violet_eyes`| | Also covers pink or purple eyes.
|`red_eyes`| |
|`amber_eyes`| | Also covers orange or yellow eyes.
|`heterochromia`| | Characters eyes are two different colors. Include both other colors as well. 

## Skin Tones

|Tag Name|Supertag|Description
|--------|--------|-----------
|`dark-skinned`| | For anything north of general brownness.
|`olive-skinned`| | For characters with skin in the light browns or orange hues. 
|`fair-skinned`| | For characters in the ruddy white to lighter asiatic skin tones.
|`pale-skinned`| | For characters that see minimal sunlight or are extremely pale.
|`tan_lines`| | For characters with visible tan lines. This filter should only be used on characters after a chest or crotch reveal, anything sooner warrants a targeted line.
|`unusual_skin`| | For skin colors beyond the Crayola box, including green, grey, and so forth.

## Physical Builds

|Tag Name|Supertag|Description
|--------|--------|-----------
|`skinny`  | | Notably skinnier than an average build
|`athletic`| |  Characters that are traditionally athletic; they’re thin but have defined builds. (EG not skinnyfat).
|`muscular`|`athletic` |  Characters that are built, with overt musculature. Muscular characters usually have defined abs, large arms, or formidable legs. Characters with the muscular tag should also have the athletic supertype tag.
|`chubby`| | Characters that have pronounced body fat. This can be anything from a little extra, a beer belly, to larger sizes.
|`curvy`| | Curvier build, with wide, well-defined hips/thighs. Female only.
|`big_butt`| | Notably large derrière
|`short`| | Characters that are pronouncedly small. (EG, Futaba)
|`tall`| | Characters that are overtly larger than most other characters. (EG, Aella).
|`androgynous`| | Characters that resemble their opposite sex. This includes tomboys who dress in masculine clothing, or men who dress in effeminate gear. (e.g. this tag, when used in conjunction with male_X or female_X should output lines like “Huh, you like to dress like the opposite sex, ~name~.”) This is supposed to be _extremely_ broad, with the intuitiveness and rancor of the lines based on the gender targeted and character speaking.

## Pubic Hair Status

|Tag Name|Supertag|Description
|--------|--------|-----------
|`pubic_hair`| |  Character has any amount of pubic hair. See trimmed and hairy for more granularity. 
|`hairy`|`pubic_hair` |  Characters with high amounts or dense pubic hair. Note that the pubic_hair tag is for *any* amount of hair, while this is for notably hairy characters. 
|`trimmed`|`pubic_hair`| Characters with manicured pubic hair, either by regular grooming, or styling (such as into a heart or a landing strip). 
|`shaved`| | Characters with no pubic hair. This is inclusive of naturally hairless species/automata, and any method that removes pubic hair including waxes, etc. 

## Attribute Sizes

**NOTE:** These should correspond to a character’s size category, and is mostly used for multiple incidences of the same size using `count-filter:X`, or for characters ogling another in their underwear. Please note that for the latter situation, a targeted line will usually yield best results.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`small_breasts`| |
|`medium_breasts`| |
|`large_breasts`| |
|`huge_breasts`|`large_breasts`| For truly monumental mammaries. Characters with this tag should also have the `large_breasts` tag.
|`small_penis`| |
|`medium_penis`| |
|`large_penis`| |
|`huge_penis`|`large_penis`| For truly prodigious members. Characters with this tag should also have the `large_penis` tag.
|`circumcised`| | The character has no foreskin. Only target during/after penis reveals.
|`uncircumcised`| | The character has a foreskin. Only target during/after penis reveals.

## Species

**NOTE:** Only tag with these if the character is visibly non-human, or, failing that, freely mentions that they aren't human.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`alien`| | The character is visibly of extraterrestrial origin, or freely mentions it.
|`angel`| | Character is an angel
|`catfolk`| | Catgirls/catboys
|`demon` | | Character is a demon, whether Judeo-Christian, an Eldritch horror, or a succubus  
|`elf`|`supernatural`| Character is an elf, or visually resembles one (Hylians, etc.)
|`fairy`|`spirit`| The character is a fairy or sprite-like, with wings and various magical powers.
|`non-human`| | The character is visibly inhuman. Can be combined with other tags.
|`monster`| | The character belongs to a classic monster species, i.e. vampires, succubi, lamias, harpies, etc.
|`succubus`|`monster`| The character is visibly a succubus (Morrigan), or freely mentions being one (Kurumu).
|`undead`|`monster`| The character is obviously a walking corpse or reanimated person. This tag often combines with the supernatural tag. 
|`vampire`|`monster`| The character exhibits classical vampire traits, such as long fangs, a thirst for blood, uncanny mesmeric ability, and so forth. 
|`robot`| | The character is very visibly a robot, or is a synthetic human. Some robot tagged characters will be offended by being called robots. 
|`supernatural`| | The character is of an otherworldly/paranormal/religious origin. Note, this does *not* include characters with magic like a wizard, just creatures that are traditionally spiritual creatures rather than physical monsters. See the monster tag. 
|`deity`|`supernatural`| The character is some sort of divine being, like a god or goddess. A character can be a spirit and a deity, while the deity is a more specific usecase.
|`spirit`|`supernatural`| The character is some sort of apparition, like a ghost, angel, or so forth. 

## Inhuman Features

**NOTE:** Please only tag these features if they're visible from the start of the game. For example, Blake isn't tagged as "animal_ears" because she keeps them hidden for most of the game.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`animal_ears`| | The character has an animal's ears, such as cat ears.
|`tail`| | The character has a tail.
|`winged`| | The character has wings.
|`horned`| | The character has horns.
|`pointy_ears`| | The character has pointy ears.

## Body Quirks:

|Tag Name|Supertag|Description
|--------|--------|-----------
|`freckles`| | The character has freckles visible on their face or body.
|`pierced_nipples`| | The character has had their nipples pierced. Only target during/after chest reveals.
|`scarred`| | The character has one or more scars present on their face or body. Only tag if visible from the start of the game.
|`tattoo`| | The character has a visible tattoo or tribal markings, etc. Can be used for warpaint if it's non-obvious that the alteration is by makeup. 
|`tan_lines`| | Tan lines are visible

# Clothing and Accessories

## Weapons

|Tag Name|Supertag|Description
|--------|--------|-----------
|`weapon`| | The character has brought a weapon to the game. Tag in addition to one of the following, if applicable.
|`blade`|`weapon`| An edged weapon such as a sword or knife
|`gun`|`weapon`| A firearm or beam weapon, E.g. Revy, Aimee
|`wand`|`weapon`| A rod, staff, or other magical implement E.g. Hermione, Akko

## Other Clothing and Accessories:

|Tag Name|Supertag|Description
|--------|--------|-----------
|`armor`| | The character wears armor.
|`bodysuit`| | The character wears a full bodysuit or jumpsuit.
|`cape`| | The character wears a cape or mantle. 
|`choker`| | The character wears a choker around his or her neck.
|`formal_attire`| | The character wears a suit, or a fine dress. If it's a school uniform, use that tag instead.
|`glasses`| | The character wears eye protection, including goggles. 
|`hat`| | The character wears a hat or some other form of headgear.
|`high_heels`| | Wearing high-heeled shoes or boots
|`leotard`| | The character wears a leotard or other apparel like a swimsuit or non-full bodysuit.
|`lingerie `| | Wearing notably fancy lingerie, likely with frills, lace, garters, etc.
|`makeup `| | Visibly wearing makeup. (Makeup like the *Zombieland Saga* girls' doesn't count.)
|`masked`| | The character wears a mask that partially or completely covers the face.
|`no_bra`| | The character is female and does not wear a bra or other breast support under her major items.
|`no_underwear`| | Character did not wear lower underwear (e.g. panties, boxers) to the game. 
|`pantyhose`| | The character wears pantyhose or other officewear. Compare with 'thigh_highs.'
|`scantily-clad`| | The character starts the game with revealing or minimal clothing.
|`school_uniform`| | The character wears a school uniform. Note that this tag is applicable until the character transforms, not necessarily undresses. (EG If it's an outfit for a single stage, then tag that stage specifically. Otherwise, it lasts the whole game.) 
|`spats`| | Wearing tight spandex/lycra bicycle shorts
|`sunglasses`| | The character is super cool. 
|`thigh_highs`| | The character wears thigh-high socks or stockings. 
|`thong`| | Please only target for stages where it'll be visible.


# Personality and Mental Traits

## Sexual Orientation

**NOTE:** These tags should be used mostly as a reference to how the character is written in SPNatI. If a character’s canon lacks sufficient information, it’s okay to omit a sexuality tag. Some characters will react to both sexes, but this does not necessarily make them bisexual.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`straight`| | The character is exclusively attracted to the opposite gender.
|`gay`| | The character is male and exclusively attracted to other males.
|`lesbian`| | The character is female and exclusively attracted to other females.
|`bisexual`| | The character is attracted to both genders.
|`bi-curious`| | The character is predominantly attracted to the opposite gender, but is curious about the same sex. Also applicable to otherwise-straight characters that open up to characters of the same sex in lategame.
|`reverse_bi-curious`| | The  character is predominantly attracted to the same sex, but is curious about the opposite sex. Also applicable to otherwise gay/lesbian characters that open up to opposite-sex characters late in the game.

## Occupation

**NOTE:** Avoid tagging if your character keeps this a secret.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`adventurer`| | Goes on adventures around their world, likely in a traditional RPG/Fantasy sense
|`artist`| | Is an artist of a traditional medium, such as painting or sculpting
|`bounty_hunter`| | 
|`celebrity`| | The character is famous enough that other characters could plausibly already know about them.
|`clown`| |
|`coder`| | Coder/Hacker/Programmer. Works with computer software, whether professionally, or as a hobby
|`criminal`| | The character regularly engages in criminal activity.
|`delinquent`| | The character is young and involved in petty crime
|`detective`| |
|`fighter`| | Regularly takes part in close-quarters combat.
|`gamer`| | The character plays video games as a career, e.g. D.va, Chiaki.
|`idol`|`musician `| Works as a Japanese-style idol
|`lawyer`| |
|`journalist`| | The character is a news reporter or perhaps might want to interview other characters.
|`magical_girl`| | Note: being a girl who uses magic doesn't qualify a character for this tag. They should belong to the "magical girl/mahou shoujo" genre.
|`mech_pilot`| | 
|`medic`| | The character is either a doctor, or a combat medic.
|`military`| | The character is from a military background, or belongs to a military organization.
|`model`| | The character works as a fashion model.
|`musician`| | The character regularly sings or plays an instrument, whether professionally or as a hobby.
|`ninja`| |
|`pirate`| | The character is a pirate, whether classic pirate, modern pirate, or space pirate.
|`police`| | The character belongs to a law enforcement organization.
|`prince`|`royalty`|
|`princess`|`royalty`|
|`royalty`| |
|`scientist`| |
|`superhero`| |
|`spy`| | The character is a spy, or some other form of covert operative.
|`student`| | The character attends a school, college or university.
|`witch`| |

## Relationship Status

**NOTE:** Ideally, your character shouldn't be written as "taken" unless they officially are in their source canon (so avoid shipping). A player will want your character to be interested in them, after all.

**NOTE:** Additionally, feel free to not use either of these tags if the character being single is not important, or if you want to leave it ambiguous.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`single`| | The character is not currently in a romantic/sexual relationship.
|`taken`| | The character is currently in a serious romantic/sexual relationship. However, it can still be an open relationship, E.G. Aimee.

## Personality Traits

**NOTE:** You are allowed to pick more than one if applicable. Try not to pick conflicting ones, however.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`aggressive`| | The character is outright hostile, and may regularly make threats of harm to the other players, e.g. Revy.
|`cheerful`| | The character is naturally happy and optimistic, and just tries to have fun with the game, e.g. Ruby.
|`confident`| | The character has little issue with their own nudity, may be proud of their body, and is unlikely to get embarrassed, e.g. Rinkah, Saki (ZLS).
|`ditzy`| | The character is absent-minded, an airhead, or otherwise unaware of normal social behavior, e.g. Harley, Twilight.
|`exhibitionist`| | The character actively wants to be seen naked and masturbating, and wants all eyes on them, e.g. Moon, Mettaton.
|`fancy`| | The character gives off an air of wealth and/or nobility, e.g. Weiss, Rosalina.
|`fashionable`| | The character always makes sure to dress well, and always dresses for the occasion, e.g. Ann, Weiss.
|`gloomy`| | The character is naturally mournful and pessimistic, e.g. Saki.
|`indifferent`| | The character seems relatively neutral or uncaring about the game going on, or it might take a lot to get a reaction out of them, e.g. Meia
|`innocent`| | The character is naive or unknowledgeable about sexual matters, e.g. Nagisa.
|`insane`| | The character is clearly not of sound mind, e.g. Harley, Moon.
|`kind`| | The character is friendly and supportive to other characters, e.g. Nagisa.
|`lonely`| | The character has few, if any friends, but wants them. Maybe they're playing SPNATI to make some? e.g. Saki.
|`mean`| | The character makes rude or harsh comments about other characters, e.g. Revy.
|`moe`| | The character has a conventionally cute/adorable appearance and personality, e.g. Nagisa
|`nerdy`| | The character has nerdy hobbies and/or interests, e.g. Chiaki, Futaba
|`perverted`| | The character has a fixation on sex, and is likely pretty kinky, e.g. Kyu, Moon.
|`quiet`| | The character doesn't talk much, and their sentences are usually brief, e.g. Kyoko.
|`sarcastic`| | Self-explanatory, e.g. Shego.
|`seductive`| | The character has an alluring, sexy, femme-fatale manner, and will flirt with other characters, e.g. Morrigan.
|`serious`| | The character has a no-nonsense personality, and isn't one to make jokes, e.g. Meia, Kyoko.
|`shut_in`| | The character is a NEET or homebody, and doesn't get out much, e.g. Futaba, Saki.
|`shy`| | The character is shy in social situations, and is likely reluctant to strip, e.g. Nagisa.
|`silent`| | The character is completely mute, e.g. Neo, Chell.
|`slutty`| | The character is extremely sexually open, feels no shame, and will regularly make obscene advances/suggestions to other characters, e.g. Moon, Revy.
|`smart`| | The character is highly knowledgeable or intelligent, e.g. Meia, Cynthia.
|`tomboy`| | The character is a girl that engages in traditionally male activities or acts in a typically masculine way. Popular on 90's characters. 
|`tsundere`| | The character starts out rude or hostile, but opens up over the course of the game, e.g. Angie.
|`witty`| | The character is always ready to crack a joke, e.g. Yang, Kyu.
|`yandere`| | The character is in murderously obsessive love with the player or another character.
|`boob_envy`| | The character is female and jealous of characters with larger breasts than them, and is insecure about their own size, e.g. Zizou
|`penis_envy`| | The character is male and jealous of characters with a larger penis than them, and is insecure about their own size.
|`enf`| | Embarrassed Nude Female. The character is female, and has a particularly adverse reaction to being naked, e.g. Nagisa, Weiss, Kyoko.
|`enm`| | Embarrassed Nude Male. The character is male, and has a particularly adverse reaction to being naked.
|`dominant`| | The character is sexually dominant.
|`submissive`| | The character is sexually submissive.

# Stripping and Forfeit Gimmicks

##Stripping 

|Tag Name|Supertag|Description
|--------|--------|-----------
|`assisted_strip`| | The character receives help while undressing from a third party. Common on Tandem characters. 
|`bottomless_first`| | The character removes their underwear before exposing their chest. 
|`cheater`| | The character uses underhanded means to gain an advantage in the game, e.g. Revy, 9S.
|`clothing_destruction`| | The character's clothing is destroyed while they undress, e.g. Aimee, Shimakaze. 
|`crossdresser`| | The character presents as the opposite sex. 
|`throws_game`| | Character intentionally loses the game

##Forfeit

|Tag Name|Supertag|Description
|--------|--------|-----------
|`bondage_forfeit`| | The character is restrained during forfeit, e.g. Croix, Vriska 
|`chair_forfeit`| | Character sits in a chair when masturbating
|`tandem`| | Another character helps the character masturbate, e.g. Elizabeth, Miko
|`tentacle_forfeit`| | The character is penetrated by noodly appendages, including tentacles, vines, cables, and other things of the type, e.g. Amalia
|`uses_toy`| | The character uses a sex toy during their forfeit. 

# Other

**NOTE:** These tags don't fit anywhere else easily.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`april_fools`| | Character is exclusive to April Fool's Day events
|`creepy`| | The character has a creepy or unnerving appearance or personality, e.g. Spooky, Raven.
|`drunk`| | The character drinks alcohol over the course of the game, e.g. Sei, Misato.
|`flies`| | The character visible flies, hovers or floats during the course of the game, e.g. Marceline, Spooky, Videl, Ochako.
|`goth`| | The character may dress in a gothic manner, or have a stereotypical goth personality.
|`has_companion`| | Character has one or more companions present during the game, that are be visible to other players, e.g. Miko, Adrien.
|`has_food`| | Character has food with them, that they eat during the game.
|`hero`| | The character is a hero in their source material, and fights for good. Note that protagonist does not necessarily mean hero.
|`magic`| | The character is from a magical background and/or is capable of using magic, e.g. Hermione, Kyu.
|`parent`| | Character is a parent, whether biological or adoptive.
|`psychic`| | The character is psychic or psionic; they utilize powerful wild talents with emotional components, or can do traditionally woo-woo things like seeing into the future, e.g. Jin, Raven
|`uses_camera`| | The character uses a camera during the game. 
|`villain`| | The character is a villain in their source material.
|`virtual`| | The character is a digital avatar, a hologram, or an AI.
|`wealthy`| | Character is notably rich.

## Nationality / Ethnicity

**NOTE:** Do not use `british` as a tag. use whatever applies out of `english` / `scottish` / `welsh`.

**NOTE:** For mixed-heritage characters, used the `mixed_heritage` tag as well as the tag for the country they're a citizen of. For example, Marinette would be tagged with `french` and `mixed_heritage`, as she is a French citizen, but has Chinese ancestry.)

|Tag Name|Supertag|Description
|--------|--------|-----------
|`african`| | 
|`american`| | The character is from the USA.
|`australian`| |
|`brazilian`| |
|`canadian`| |
|`chinese`| |
|`egyptian`| |
|`english`|`european`|
|`european`| | Of European origin or ethnicity, including Russia, but not Turkey
|`finnish`|`european`|
|`french`|`european`|
|`german`|`european`|
|`greek`|`european`|
|`hispanic`| |
|`italian`|`european`|
|`indian`| |
|`japanese`| |
|`korean`| |
|`mexican`|`hispanic`|
|`middle-eastern`| |
|`russian`|`european`|
|`scottish`|`european`|
|`swiss`|`european`|
|`mixed_heritage`| |

# Meta Tags

## Organizations / In-Universe Categories:

**NOTE:** This is to aid in dialogue targeting between characters belonging to a team or organization.

**NOTE:** This list is non-exhaustive. New characters added to the game with their own source material are included in subsequent versions of the character editor. 

|Tag Name|Supertag|Description
|--------|--------|-----------
|`mystery_inc`| | The character is part of Mystery Inc. in Scooby Doo.
|`phantom_thieves`| | The character is a member of the Phantom Thieves in Persona 5.
|`ua_academy`| | The character attends the UA Hero school in My Hero Academia.
|`overwatch`| | The character is part of the Overwatch organisation.
|`rwby`| | The character is part of Team RWBY.
|`teen_titans`| | The character belongs to the Teen Titans.
|`avengers`| | The character is part of the Avengers in Marvel.
|`dr1`| | The character was part of the class in the original Danganronpa game.
|`dr2`| | The character was part of the class in Danganronpa 2.
|`drv3`| | The character was part of the class in Danganronpa V3.
|`kanto`| | The character is from the Kanto region in Pokémon.
|`johto`| | The character is from the Johto region in Pokémon.
|`hoenn`| | The character is from the Hoenn region in Pokémon.
|`sinnoh`| | The character is from the Sinnoh region in Pokémon.
|`unova`| | the character is from the Unova region in Pokémon
|`kalos`| | The character is from the Kalos region in Pokémon.
|`alola`| | The character is from the Alola region in Pokémon.

## Genres

**NOTE:** The genre of your character's source material.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`fantasy`| | The character is from a fantasy setting.
|`future`| | The character is from the future.
|`post-apocalyptic`| | The character is from a conventional post-apocalyptic setting, such as Fallout, a zombie apocalypse, etc.
|`sci-fi`| | The character is from a sci-fi setting.
|`space`| | The source material takes place in space.

## Source Medium

**NOTE:** Select multiple if applicable. For example, Clannad, Nagisa's source material is known as both an anime and a visual novel.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`anime`| | The character is from a Japanese animation - RWBY and Avatar do not fall under this, as despite their art styles, they are of western origin.
|`book`| | The character is from written literature.
|`cartoon`| | The character is from non-Japanese animation.
|`comic`| | The character is from non-Japanese comic books.
|`hentai`| | The character is from a hentai animation or manga.
|`internet_meme`| | The character is an internet meme.
|`manga`| | The character is from a Japanese comic.
|`movie`| | The character is from a live-action of animated film.
|`tv_show`| | The character is from a live-action tv show.
|`video_game`| | The character is from a video game.
|`visual_novel`| | The character is from a visual novel game.
|`website`| | The character is a website mascot.

## Source Materials

**NOTE:** Feel free to add your character's source material as a tag if it's unrepresented here.

**NOTE:** Some companies that own multiple properties have their own tags, e.g. `valve`, `nintendo` and `capcom`.

**NOTE:** This list is non-exhaustive. New characters added to the game with their own source material are included in subsequent versions of the character editor. 

|Tag Name|Supertag|Description
|--------|--------|-----------
|`original_character`| | The character is your own original creation. If they are an original character but based on an existing franchise, tag them for that too.
|`.hack`| |
|`ace_attorney`| |
|`adventure_time`| |
|`alice_madness_returns`| |
|`batman`| |
|`big_hero_6`| |
|`bioshock`| |
|`black_lagoon`| |
|`bravely_default`| |
|`bravely_second`| |
|`buffy_the_vampire_slayer`| |
|`capcom`| |
|`clannad`| |
|`community`| |
|`crush_crush`| |
|`daria_franchise`| |
|`danganronpa`| |
|`darkstalkers`| |
|`dc_comics`| |
|`dead_by_daylight`| |
|`disney`| |
|`dragon_ball`| |
|`elena_of_avalor`| |
|`embla_academy`| |
|`emergence`| |
|`evangelion`| |
|`f_zero`| |
|`final_fantasy`| |
|`final_fantasy_7`| |
|`fire_emblem`| |
|`gundam`| |
|`harry_potter_franchise`| |
|`homestuck`| |
|`huniepop`| |
|`kid_icarus`| |
|`kim_possible`| |
|`left_4_dead`| |
|`legend_of_dark_witch`| |
|`legend_of_korra`| |
|`legend_of_zelda`| |
|`lotr`| | Lord of the Rings
|`marvel`| | Marvel Comics
|`mcu`| | Marvel Cinematic Universe
|`metroid`| |
|`miraculous`| |
|`monkey_island`| |
|`mortal_kombat`| |
|`my_hero_academia`| |
|`my_little_pony`| |
|`naruto`| |
|`nier_automata`| |
|`nintendo`| |
|`overwatch_franchise`| |
|`persona_5`| |
|`pokemon`| |
|`portal`| |
|`rosario_to_vampire`| |
|`rwby_franchise`| |
|`shantae_franchise`| |
|`snk`| |
|`smash_bros`| |
|`star_trek`| |
|`super_mario`| |
|`street_fighter`| |
|`sword_art_online`| |
|`team_fortress_2`| |
|`teen_titans_franchise`| |
|`tmnt`| | Teenage Mutant Ninja Turtles
|`toradora`| |
|`undertale`| |
|`vandread`| |
|`valve`| |
|`wii_fit`| |
|`yugioh`| |

# Human

Tags for the human player specifically. Able to be targeted, but do not put these on your character.

|Tag Name|Supertag|Description
|--------|--------|-----------
|`human`| |Character is the human player
|`human_male`| |Character is the human player as a male
|`human_female`| |Character is the human player as a female