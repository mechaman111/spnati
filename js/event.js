
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
    this.override = ($xml.attr("override").trim().toLowerCase() === "true");
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
 * @param {Set<string>} altCostumes 
 * @param {string?} background 
 * @param {Set<string>} candyImages 
 * @param {Set<string>} fillTags
 * @param {Set<string>} includeStatuses
 */
function GameEvent(id, name, dateRanges, altCostumes, background, candyImages, fillTags, includeStatuses) {
    /** @type {string} */
    this.id = id;

    /** @type {string} */
    this.name = name;

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

    /** @type {Set<string>} */
    this.includeStatuses = includeStatuses;
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
    console.log(endDates);
    return endDates.pop();
}

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

/** @returns {GameEvent} */
function parseEventElement ($xml) {
    var id = $xml.attr("id");

    var name = $xml.children("name").text() || null;

    var dateRanges = $xml.children("dates>date").map(function (index, $elem) {
        return new DateRange($elem);
    }).get();

    var altCostumes = loadChildSet($xml, "costume-sets>set");
    var background = $xml.children("background").text() || null;
    var candyImages = loadChildSet($xml, "candy>path");
    var fillTags = loadChildSet($xml, "fill>tag");
    var includeStatuses = loadChildSet($xml, "include>status");

    return new GameEvent(id, name, dateRanges, altCostumes, background, candyImages, fillTags, includeStatuses);
}

/**
 * @returns {Promise<void>}
 */
function loadEventData () {
    return fetchXML("events.xml").then(function ($xml) {
        var events = $xml.children("events>event").map(function (index, elem) {
            return parseEventElement($(elem));
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
                if (event.isActiveDate(curDate) && !activeIds.has(event.id)) {
                    console.log("Activating event: " + event.name + " (" + event.dateRanges[j].toString() + ")")
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
            fillTagSet = new Set();

            activeGameEvents.forEach(function (event) {
                event.altCostumes.forEach(function (setId) {
                    alternateCostumeSets[setId] = true;

                    if (setId == "all") {
                        console.log("[" + event.id + "]" + " Activating all costume sets.");
                    } else {
                        console.log("[" + event.id + "]" + " Activating costume set " + setId + ".");
                        DEFAULT_COSTUME_SETS.add(setId);
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

                event.includeStatuses.forEach(function (status) {
                    console.log("[" + event.id + "]" + " Enabling opponent status: " + status + ".");
                    includedOpponentStatuses[status] = true;
                });
            });

            eventFillTags = [];
            fillTagSet.forEach(function (tag) {
                eventFillTags.push(tag);
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
