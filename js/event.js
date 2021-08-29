
/**
 * Parse a child object of a <date-range> element (a <from> or <to> element).
 * @param {JQuery} $xml 
 * @returns {Date}
 */
function parseDateRangeElem($xml) {
    var month = parseInt($xml.attr("month"), 10) - 1;
    var day = parseInt($xml.attr("day"), 10);
    var year = parseInt($xml.attr("year"), 10);

    if (year == null) {
        year = (new Date()).getFullYear();
    }

    return new Date(year, month, day);
}

function DateRange($xml) {
    /** @type {Date} */
    this.from = parseDateRangeElem($xml.children("from"));

    /** @type {Date} */
    this.to = parseDateRangeElem($xml.children("to"));
}

/**
 * Test whether a given date falls within this range.
 * @param {Date} queryDate 
 * @returns {boolean}
 */
DateRange.prototype.contains = function (queryDate) {
    var ts = queryDate.getTime();
    return (ts >= this.from.getTime()) && (ts <= this.to.getTime());
}

/**
 * Convert this range to a human-readable string.
 * @returns {string}
 */
DateRange.prototype.toString = function () {
    return "from " + this.from.toDateString() + " to " + this.to.toDateString();
}

/**
 * 
 * @param {string} id 
 * @param {DateRange[]} dateRanges 
 * @param {Set<string>} altCostumes 
 * @param {string?} background 
 * @param {Set<string>} candyImages 
 * @param {Set<string>} fillTags
 */
function GameEvent(id, dateRanges, altCostumes, background, candyImages, fillTags) {
    /** @type {string} */
    this.id = id;

    /** @type {DateRange[]} */
    this.dateRanges = dateRanges;

    /** @type {Set<string>} */
    this.altCostumes = altCostumes;

    /** @type {string?} */
    this.background = background;

    /** @type {Set<string>} */
    this.candyImages = candyImages;

    /** @type {Set<string>} */
    this.fillTags = fillTags;
}

/** @returns {GameEvent} */
function parseEventElement ($xml) {
    var id = $xml.attr("id");

    var dateRanges = $xml.children("dates>date").map(function (index, $elem) {
        return new DateRange($elem);
    });

    var altCostumes = new Set($xml.children("costume-sets>set").map(function (index, $elem) {
        return $elem.text();
    }));

    var background = $xml.children("background").text() || null;

    var candyImages = new Set($xml.children("candy>path").map(function (index, $elem) {
        return $elem.text();
    }));

    var fillTags = new Set($xml.children("fill>tag").map(function (index, $elem) {
        return $elem.text();
    }));

    return new GameEvent(id, dateRanges, altCostumes, background, candyImages, fillTags);
}

/**
 * @returns {Promise<void>}
 */
function loadEventData () {
    return fetchXML("events.xml").then(function ($xml) {
        var events = $xml.children("events>event").map(function (index, $elem) {
            return parseEventElement($elem);
        });

        /** @type {Set<string>} */
        var activeIds = new Set();
        var curDate = new Date();

        for (let i = 0; i < events.length; i++) {
            let event = events[i];
            if (FORCE_EVENTS.has(event.id) && !activeIds.has(event.id)) {
                console.log("Force activating event: " + event.name);
                activeGameEvents.push(event);
                activeIds.add(event.id);
            } else if (FORCE_EVENTS.size == 0) {
                for (let j = 0; j < event.dateRanges.length; j++) {
                    if (event.dateRanges[j].contains(curDate) && !activeIds.has(event.id)) {
                        console.log("Activating event: " + event.name + " (" + event.dateRanges[j].toString() + ")")
                        activeGameEvents.push(event);
                        activeIds.add(event.id);
                        break;
                    };
                }
            }
        }
        
        if (activeGameEvents.length > 0) {
            var candySet = new Set();
            var eventBackgrounds = new Set();
            
            DEFAULT_COSTUME_SETS = new Set();
            fillTagSet = new Set();

            activeGameEvents.forEach(function (event) {
                event.altCostumes.forEach(function (v) {
                    alternateCostumeSets[v.setId] = true;

                    if (v.setId == "all") {
                        console.log("[" + event.id + "]" + " Activating all costume sets.");
                    } else {
                        console.log("[" + event.id + "]" + " Activating costume set " + v.setId + ".");
                        DEFAULT_COSTUME_SETS.add(v.setId);
                    }
                });

                event.candyImages.forEach(function (path) {
                    candySet.add(path);
                });

                if (event.background) {
                    console.log("[" + event.id + "]" + " Adding default background option: " + event.background + ".");
                    eventBackgrounds.add(event.background);
                }

                event.fillTags.forEach(function (tag) {
                    if (!fillTagSet.has(tag)) {
                        console.log("[" + event.id + "]" + " Adding tag for event filling: " + tag + ".");
                        fillTagSet.add(tag);
                    }
                });
            });

            eventFillTags = [];
            fillTagSet.forEach(function (tag) {
                eventFillTags.push(tag);
            });

            CANDY_LIST = [];

            console.log("Event Candy Images:")
            candySet.forEach(function (path) {
                console.log("    * " + path);
                CANDY_LIST.push(path);
            });

            if (CANDY_LIST.length > 0) {
                selectTitleCandy();
            }

            if (eventBackgrounds.size > 0) {
                var choices = [];
                eventBackgrounds.forEach(function (v) { choices.push(v); });
                defaultBackgroundID = choices[getRandomNumber(0, choices.length)];
            }

            DEFAULT_FILL = "event";
            sortingMode = "event";
        }
    });
}
