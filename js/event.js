/** @type {HighlightedAttributeList} */
var eventCostumeSettings = null;

/** @type {HighlightedAttributeList} */
var eventTagSettings = null;

/** @type {string[]} */
var eventTagList = [];

/** @type {HighlightedAttributeList} */
var eventCharacterSettings = null;

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
 * Common class for costume, tag, and character settings from events.
 * @param {Set<string>} ids
 * @param {Object<string, string>} highlights
 * @param {Object<string, number>} sorting
 * @param {Object<string, number>} partitions
 * @param {Object<string, boolean?>} prefills
 */
function HighlightedAttributeList (ids, highlights, sorting, partitions, prefills) {
    /** @type {Set<string>} */
    this.ids = ids;

    /** @type {Object<string, string>} */
    this.highlights = highlights;

    /** @type {Object<string, number?>} */
    this.sorting = sorting;

    /** @type {Object<string, number?>} */
    this.partitions = partitions;

    /** @type {Object<string, boolean?>} */
    this.prefills = prefills;
}

HighlightedAttributeList.empty = function () {
    return new HighlightedAttributeList(new Set(), {}, {}, {}, {});
}

/**
 * Parse an attribute list from an XML element.
 * @param {JQuery} $xml 
 * @param {string} selector 
 */
HighlightedAttributeList.parse = function ($xml, selector) {
    var ids = new Set();
    var highlights = {};
    var sorting = {};
    var partitions = {};
    var prefills = {};

    $xml.find(selector).each(function (index, elem) {
        var $elem = $(elem);
        var id = $elem.text();
        var sortPriority = parseInt($elem.attr("sort"), 10);
        var partitionIndex = parseInt($elem.attr("partition"), 10);

        ids.add(id);
        highlights[id] = $elem.attr("highlight") || "";
        sorting[id] = !isNaN(sortPriority) ? sortPriority : undefined;
        partitions[id] = !isNaN(partitionIndex) ? partitionIndex : undefined;

        if ($elem.attr("prefill")) {
            var prefillVal = $elem.attr("prefill").trim().toLowerCase();
            if (prefillVal === "default") {
                prefills[id] = false;
            } else if (prefillVal === "force") {
                prefills[id] = true;
            }
        }
    });

    return new HighlightedAttributeList(ids, highlights, sorting, partitions, prefills);
}

/**
 * Merge multiple attribute lists together.
 * If multiple lists specify a given attribute, the value from the
 * first overlapping list wins.
 * @param {HighlightedAttributeList[]} lists
 * @returns {HighlightedAttributeList}
 */
HighlightedAttributeList.merge = function (lists) {
    function mergeKV (dest, src) {
        Object.keys(src).forEach(function (key) {
            if (src[key] !== undefined && dest[key] === undefined) dest[key] = src[key];
        });
    }

    return lists.reduce(function (acc, list) {
        list.ids.forEach(function (id) { acc.ids.add(id); });
        mergeKV(acc.highlights, list.highlights);
        mergeKV(acc.sorting, list.sorting);
        mergeKV(acc.partitions, list.partitions);
        mergeKV(acc.prefills, list.prefills);

        return acc;
    }, HighlightedAttributeList.empty());
}

/**
 * 
 * @param {string} id 
 * @param {string} name
 * @param {DateRange} dateRanges 
 * @param {HighlightedAttributeList} costumes
 * @param {string?} background 
 * @param {Set<string>} candyImages 
 * @param {HighlightedAttributeList} tags
 * @param {Set<string>} includeStatuses
 * @param {HighlightedAttributeList} characters
 */
function GameEvent(id, name, dateRanges, costumes, background, candyImages, tags, includeStatuses, characters) {
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

    /** @type {HighlightedAttributeList} */
    this.costumes = costumes;

    /** @type {HighlightedAttributeList} */
    this.tags = tags;

    /** @type {HighlightedAttributeList} */
    this.characters = characters;
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

    var id = $xml.attr("id");

    var name = $xml.children("name").text() || null;

    var dateRanges = $xml.find("dates>date").map(function (index, elem) {
        return new DateRange($(elem));
    }).get();

    var altCostumes = HighlightedAttributeList.parse($xml, "costumes");
    var background = $xml.children("background").text() || null;
    var candyImages = loadChildSet($xml, "candy>path");
    var fillTags = HighlightedAttributeList.parse($xml, "tag");
    var includeStatuses = loadChildSet($xml, "include-status");
    var characters = HighlightedAttributeList.parse($xml, "character");

    return new GameEvent(id, name, dateRanges, altCostumes, background, candyImages, fillTags, includeStatuses, characters);
}

/**
 * @returns {Promise<void>}
 */
function loadEventData () {
    eventCostumeSettings = HighlightedAttributeList.empty();
    eventTagSettings = HighlightedAttributeList.empty();
    eventCharacterSettings = HighlightedAttributeList.empty();

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
            var costumeSettings = [];
            var tagSettings = [];
            var characterSettings = []

            activeGameEvents.forEach(function (event) {
                event.candyImages.forEach(function (path) {
                    candySet.add(path);
                });

                if (event.background) {
                    console.log("[" + event.id + "]" + " Adding default background option: " + event.background + ".");
                    eventBackgrounds.add(event.background);
                }

                event.includeStatuses.forEach(function (status) {
                    console.log("[" + event.id + "]" + " Enabling opponent status: " + status + ".");
                    includedOpponentStatuses[status] = true;
                });

                costumeSettings.push(event.costumes);
                tagSettings.push(event.tags);
                characterSettings.push(event.characters);
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

            eventCostumeSettings = HighlightedAttributeList.merge(costumeSettings);
            eventTagSettings = HighlightedAttributeList.merge(tagSettings);
            eventCharacterSettings = HighlightedAttributeList.merge(characterSettings);

            DEFAULT_COSTUME_SETS = new Set();
            alternateCostumeSets = {};

            eventCostumeSettings.ids.forEach(function (costumeSet) {
                alternateCostumeSets[costumeSet] = true;

                if (costumeSet === "all") {
                    console.log("Activating all costumes during event");
                } else {
                    console.log("Activating costume set: " + costumeSet + " during event");
                    DEFAULT_COSTUME_SETS.add(costumeSet);
                }
            });

            eventTagList = [];
            eventTagSettings.ids.forEach(function (tag) { eventTagList.push(tag); });

            sortingMode = "event";
        }
    });
}
