var eventCostumeHighlights = {};
var eventTagHighlights = {};
var eventCharacterHighlights = {};

/**
 * Parse a child object of a <date-range> element (a <from> or <to> element).
 * @param {JQuery} $xml 
 * @returns {Date}
 */
function parseDateRangeElem($xml) {
    var month = parseInt($xml.attr("month"), 10) - 1;
    var day = parseInt($xml.attr("day"), 10);
    var year = parseInt($xml.attr("year"), 10);

    if (isNaN(year)) {
        year = (new Date()).getUTCFullYear();
    }
    
    return new Date(Date.UTC(year, month, day));
}

function DateRange($xml) {
    /** @type {Date} */
    this.from = parseDateRangeElem($xml.children("from"));

    var toElem = $xml.children("to");
    var baseEndDate = this.from;

    if (toElem.length > 0) {
        baseEndDate = parseDateRangeElem(toElem);
    }

    // Add one day to the end date/time to make the range inclusive on both sides.
    /** @type {Date} */
    this.to = new Date(baseEndDate.getTime() + (24 * 60 * 60 * 1000));

    /** @type {boolean} */
    this.override = (($xml.attr("override") || "").trim().toLowerCase() === "true");
}

/**
 * Test whether a given date falls within this range.
 * @param {Date} queryDate 
 * @returns {boolean}
 */
DateRange.prototype.contains = function (queryDate) {
    var ts = queryDate.getTime();
    return (ts >= this.from.getTime()) && (ts < this.to.getTime());
}

/**
 * Convert this range to a human-readable string.
 * @returns {string}
 */
DateRange.prototype.toString = function () {
    var endDay = new Date(this.to.getTime() - 1);
    return "from " + this.from.toDateString() + " to " + endDay.toDateString();
}

/**
 * 
 * @param {string} id 
 * @param {string} name
 * @param {DateRange} dateRanges 
 * @param {Object<string, string>} costumeHighlights
 * @param {string?} background 
 * @param {Set<string>} candyImages 
 * @param {Object<string, string>} tagHighlights
 * @param {Set<string>} includeStatuses
 * @param {Object<string, string>} characterHighlights
 */
function GameEvent(id, name, dateRanges, costumeHighlights, background, candyImages, tagHighlights, includeStatuses, characterHighlights) {
    /** @type {string} */
    this.id = id;

    /** @type {string} */
    this.name = name;

    /** @type {DateRange[]} */
    this.dateRanges = dateRanges;

    /** @type {string?} */
    this.background = background;

    /** @type {Set<string>} */
    this.candyImages = candyImages;

    /** @type {Set<string>} */
    this.includeStatuses = includeStatuses;

    /** @type {Object<string, string>} */
    this.costumeHighlights = costumeHighlights;

    /** @type {Object<string, string>} */
    this.tagHighlights = tagHighlights;

    /** @type {Object<string, string>} */
    this.characterHighlights = characterHighlights;
}

/**
 * 
 * @param {Date} queryDate 
 * @returns {DateRange[]}
 */
 GameEvent.prototype.getActiveRanges = function (queryDate) {
    var queryYear = queryDate.getUTCFullYear();
    var currentYearOverrides = this.dateRanges.filter(function (range) {
        return range.override && queryYear >= range.from.getUTCFullYear() && queryYear <= range.to.getUTCFullYear();
    });

    if (currentYearOverrides.length > 0) {
        /*
         * At least one override exists for this year. Match _only_ the ranges marked as overrides.
         * This allows override ranges to be shorter than repeating ranges (since the repeating range would overlap and cause the event to activate past the override).
         */
        return currentYearOverrides.filter(function (range) { return range.contains(queryDate); })
    } else {
        /* Otherwise, just match against everything. */
        return this.dateRanges.filter(function (range) { return range.contains(queryDate); })
    }
}

/**
 * 
 * @param {Date} queryDate 
 * @returns {boolean}
 */
GameEvent.prototype.isActiveDate = function (queryDate) {
    return this.getActiveRanges(queryDate).length > 0;
}

/**
 * 
 * @param {Date} queryDate 
 * @returns {Date?}
 */
GameEvent.prototype.getEndDate = function (queryDate) {
    var endDates = this.getActiveRanges(queryDate).map(function (range) { return range.to; });
    endDates.sort(function (a, b) { return a.getTime() - b.getTime(); });
    return endDates.pop();
}


/** @returns {GameEvent} */
function parseEventElement ($xml) {
    /**
     * Load a list of simple text elements from an XML JQuery object as a Set.
     * @param {JQuery} $xml 
     * @param {string} selector 
     * @returns {Set<string>}
     */
    function loadChildSet ($xml, selector) {
        return new Set($xml.find(selector).map(function (index, elem) {
            return $(elem).text();
        }).get());
    }

    /**
     * Load a list of text elements with highlight attributes from an XML JQuery object as an object.
     * @param {JQuery} $xml 
     * @param {string} selector 
     * @returns {Object<string, string>}
     */
    function loadHighlightedSet ($xml, selector) {
        var ret = {};
        $xml.find(selector).each(function (index, elem) {
            var key = $(elem).text();
            var highlight = $(elem).attr("highlight") || "";
            ret[key] = highlight
        });

        return ret
    }

    var id = $xml.attr("id");

    var name = $xml.children("name").text() || null;

    var dateRanges = $xml.find("dates>date").map(function (index, elem) {
        return new DateRange($(elem));
    }).get();

    var altCostumes = loadHighlightedSet($xml, "costume-set");
    var background = $xml.children("background").text() || null;
    var candyImages = loadChildSet($xml, "candy>path");
    var fillTags = loadHighlightedSet($xml, "tag");
    var includeStatuses = loadChildSet($xml, "include-status");
    var characterHighlights = loadHighlightedSet($xml, "characters>character");

    return new GameEvent(id, name, dateRanges, altCostumes, background, candyImages, fillTags, includeStatuses, characterHighlights);
}

/**
 * @returns {Promise<void>}
 */
function loadEventData () {
    return fetchXML("events.xml").then(function ($xml) {
        var events = $xml.children("event").map(function (index, elem) {
            return parseEventElement($(elem));
        }).get();

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
                if (event.isActiveDate(curDate) && !activeIds.has(event.id)) {
                    console.log("Activating event: " + event.name);
                    activeGameEvents.push(event);
                    activeIds.add(event.id);
                    break;
                }
            }
        }
        
        if (activeGameEvents.length > 0) {
            var candySet = new Set();
            var eventBackgrounds = new Set();
            
            DEFAULT_COSTUME_SETS = new Set();

            activeGameEvents.forEach(function (event) {
                Object.keys(event.costumeHighlights).forEach(function (setId) {
                    var highlightStatus = event.costumeHighlights[setId];

                    alternateCostumeSets[setId] = true;
                    if (setId == "all") {
                        console.log("[" + event.id + "]" + " Activating all costume sets.");
                    } else {
                        console.log("[" + event.id + "]" + " Activating costume set " + setId + " with highlight " + highlightStatus + ".");
                        
                        DEFAULT_COSTUME_SETS.add(setId);
                        if (!eventCostumeHighlights[setId] && highlightStatus)
                            eventCostumeHighlights[setId] = highlightStatus;
                    }
                });

                event.candyImages.forEach(function (path) {
                    candySet.add(path);
                });

                if (event.background) {
                    console.log("[" + event.id + "]" + " Adding default background option: " + event.background + ".");
                    eventBackgrounds.add(event.background);
                }
                
                Object.keys(event.tagHighlights).forEach(function (tag) {
                    if (!eventTagHighlights[tag] && event.tagHighlights[tag])
                        eventTagHighlights[tag] = event.tagHighlights[tag];
                });

                event.includeStatuses.forEach(function (status) {
                    console.log("[" + event.id + "]" + " Enabling opponent status: " + status + ".");
                    includedOpponentStatuses[status] = true;
                });

                Object.keys(event.characterHighlights).forEach(function (id) {
                    if (!eventCharacterHighlights[id] && event.characterHighlights[id])
                        eventCharacterHighlights[id] = event.characterHighlights[id];
                });
            });


            if (candySet.size > 0) {
                CANDY_LIST = [];
    
                console.log("Event Candy Images:")
                candySet.forEach(function (path) {
                    console.log("    * " + path);
                    CANDY_LIST.push(path);
                });
    
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
