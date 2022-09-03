const DAY_MS = 24 * 60 * 60 * 1000;

/* Order is important (events in earlier listed files take priority over ones defined later) */
const EVENT_FILES = ["events.xml", "birthdays.xml"];

/** @type {HighlightedAttributeList} */
var eventCostumeSettings = null;

/** @type {HighlightedAttributeList} */
var eventTagSettings = null;

/** @type {string[]} */
var eventTagList = [];

/** @type {HighlightedAttributeList} */
var eventCharacterSettings = null;

/** @type {GameEvent?} */
var curResortEvent = null;

/** @type {Set<string>} */
var MANUAL_EVENTS = new Set();

/** @type {Set<string>} */
var OVERRIDE_EVENTS = new Set();

/** @type {GameEvent[]} */
var activeGameEvents = [];

/** 
 * Set to true if any event has customized partitioning or sort order.
 * @type {boolean} */
var eventSortingActive = false;

var $announcementDropdown = $("#title-announcement-dropdown");
var $titleResortButton = $("#title-resort-button");
var $dropdownResortButton = $("#dropdown-resort-button");

/**
 * Parse a child object of a date range element (an <on>, <from>, <to>, or <weekOf> element).
 * @param {JQuery} $xml 
 * @returns {Date}
 */
function parseDateRangeElem($xml, useUTC) {
    var month = parseInt($xml.attr("month"), 10) - 1;
    var day = parseInt($xml.attr("day"), 10);
    var year = parseInt($xml.attr("year"), 10);

    if (isNaN(year)) {
        if (useUTC) {
            year = (new Date()).getUTCFullYear();
        } else {
            year = (new Date()).getFullYear();
        }
    }

    if (useUTC) {
        return new Date(Date.UTC(year, month, day));
    } else {
        return new Date(year, month, day);
    }
}

/**
 * 
 * @param {Date} startDate 
 * @param {Date} endDate 
 * @param {boolean} override 
 */
function DateRange(startDate, endDate, override) {
    /** @type {Date} */
    this.from = startDate

    /** @type {Date} */
    this.to = endDate;

    /** @type {boolean} */
    this.override = override;
}

/**
 * Parse a <date> element.
 * @param {JQuery} $xml 
 * @param {boolean} useUTC
 * @returns {DateRange}
 */
DateRange.parseRange = function ($xml, useUTC) {
    var from = parseDateRangeElem($xml.children("from"), useUTC);
    var toElem = $xml.children("to");
    var to = from;

    if (toElem.length > 0) {
        var nDays = parseInt(toElem.attr("days"), 10);
        if (nDays) {
            to = new Date(from.getTime() + (nDays * DAY_MS));
        } else {
            // Parse the element as a year/month/day set.
            // Add one day to the end date/time to make the range inclusive on both sides.
            to = new Date(parseDateRangeElem(toElem, useUTC).getTime() + DAY_MS);

            if (to.getTime() <= from.getTime() && !toElem.attr("year")) {
                // Wrap the date around to the next year.
                if (useUTC) {
                    to.setUTCFullYear(to.getUTCFullYear() + 1);
                } else {
                    to.setFullYear(to.getFullYear() + 1);
                }
            }
        }
    } else {
        // Otherwise assume the end date is one day after the start.
        to = new Date(from.getTime() + DAY_MS);
    }

    var override = (($xml.attr("override") || "").trim().toLowerCase() === "true");

    return new DateRange(from, to, override);
}

/**
 * Parse a <weekOf> element.
 * @param {JQuery} $xml 
 * @param {boolean} useUTC
 * @returns {DateRange}
 */
DateRange.parseWeekOf = function ($xml, useUTC) {
    var refDate = parseDateRangeElem($xml, useUTC);
    var startDay = parseInt($xml.attr("start-on"), 10) || 0;
    var nDays = parseInt($xml.attr("days"), 10) || 8;
    var override = (($xml.attr("override") || "").trim().toLowerCase() === "true");

    var adjustDays;

    if (useUTC) {
        adjustDays = refDate.getUTCDay() - startDay;
    } else {
        adjustDays = refDate.getDay() - startDay;
    }

    /* If the reference day happens to fall on or later in the week than the start day,
     * push the start date back by a week so that the start is always strictly before the reference date.
     */
    if (adjustDays <= 0) adjustDays += 7;

    var startTs = refDate.getTime() - (adjustDays * DAY_MS);
    var endTs = startTs + (nDays * DAY_MS);

    return new DateRange(new Date(startTs), new Date(endTs), override);
}

/**
 * Parse an <on> element.
 * @param {JQuery} $xml
 * @param {boolean} useUTC
 * @returns {DateRange}
 */
DateRange.parseSingleDay = function ($xml, useUTC) {
    var from = parseDateRangeElem($xml, useUTC);
    var to = new Date(from.getTime() + DAY_MS);
    var override = (($xml.attr("override") || "").trim().toLowerCase() === "true");
    return new DateRange(from, to, override);
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
 * @param {Object<string, boolean?>} allowTestingGuests
 */
function HighlightedAttributeList (ids, highlights, sorting, partitions, prefills, allowTestingGuests) {
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

    /** @type {Object<string, boolean?>} */
    this.allowTestingGuests = allowTestingGuests;
}

HighlightedAttributeList.empty = function () {
    return new HighlightedAttributeList(new Set(), {}, {}, {}, {}, {});
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
    var allowTestingGuests = {};

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

        if ($elem.attr("testing-guest")) {
            var val = $elem.attr("testing-guest").trim().toLowerCase();
            if (val === "true") {
                allowTestingGuests[id] = true;
            } else if (val === "false") {
                allowTestingGuests[id] = false;
            }
        }
    });

    return new HighlightedAttributeList(ids, highlights, sorting, partitions, prefills, allowTestingGuests);
}

/**
 * Merge multiple attribute lists together.
 * If multiple lists specify a given attribute, the value from the first overlapping list wins.
 * (Except for overlapping `sort` values, which are isntead added together.)
 * @param {HighlightedAttributeList[]} lists
 * @returns {HighlightedAttributeList}
 */
HighlightedAttributeList.merge = function (lists) {
    function mergeKV (dest, src, mergeFn) {
        Object.keys(src).forEach(function (key) {
            if (src[key] !== undefined) {
                if (dest[key] === undefined) {
                    dest[key] = src[key];
                } else if (mergeFn) {
                    dest[key] = mergeFn(dest[key], src[key]);
                }
            }
        });
    }

    return lists.reduce(function (acc, list) {
        list.ids.forEach(function (id) { acc.ids.add(id); });
        mergeKV(acc.highlights, list.highlights);
        mergeKV(acc.sorting, list.sorting, (a, b) => a + b);
        mergeKV(acc.partitions, list.partitions);
        mergeKV(acc.prefills, list.prefills);
        mergeKV(acc.allowTestingGuests, list.allowTestingGuests);

        return acc;
    }, HighlightedAttributeList.empty());
}

/**
 * 
 * @param {JQuery} header 
 * @param {JQuery} body 
 * @param {number} threshold
 */
function EventAnnouncement (header, body, threshold) {
    /** @type {GameEvent?} */
    this.event = null;

    /** @type {JQuery} */
    this.header = header;

    /** @type {JQuery} */
    this.body = body;

    /** @type {number} */
    this.threshold = threshold;
}

/**
 * 
 * @param {JQuery} $xml 
 * @returns {EventAnnouncement}
 */
EventAnnouncement.parse = function ($xml) {
    var header = $xml.children("header").contents();
    var body = $xml.children("announce-body").contents();
    var threshold = parseInt($xml.children("min-characters").text(), 10) || 0;

    return new EventAnnouncement(header, body, threshold);
}

/**
 * Get whether this announcement has been previously shown.
 * @param {GameEvent} event
 * @returns {boolean}
 */
EventAnnouncement.prototype.previouslyShown = function () {
    return save.hasShownEventAnnouncement(this.event);
}

/**
 * Check whether the character threshold for this announcement has been met.
 * @returns {boolean}
 */
EventAnnouncement.prototype.checkCharacterThreshold = function () {
    return save.getPlayedCharacterSet().length >= this.threshold;
}


/**
 * Show this announcement.
 * @param {GameEvent} event
 */
EventAnnouncement.prototype.show = function () {
    if (!this.checkCharacterThreshold()) return;

    save.setEventAnnouncementFlag(this.event, true);
    $("#event-announcement-header").contents().detach();
    $("#event-announcement-body").contents().detach();

    this.header.appendTo("#event-announcement-header");
    this.body.appendTo("#event-announcement-body");

    $eventAnnouncementModal.modal('show');
}

/**
 * 
 * @param {string} season 
 * @param {string} link 
 * @param {number} threshold
 */
function ResortEventInfo (season, link, threshold) {
    /** @type {string} */
    this.season = season;

    /** @type {string} */
    this.link = link;

    /** @type {GameEvent?} */
    this.event = null;

    /** @type {number} */
    this.threshold = threshold;

    /** @type {number?} */
    this.year = null;
}

/**
 * 
 * @param {JQuery} $xml 
 * @returns {ResortEventInfo}
 */
ResortEventInfo.parse = function ($xml) {
    var season = ($xml.children("season").text() || "auto").trim().toLowerCase();
    var link = $xml.children("poll").text();
    var threshold = parseInt($xml.children("min-characters").text(), 10) || 40;

    return new ResortEventInfo(season, link, threshold);
}

/**
 * 
 * @param {GameEvent} event 
 */
ResortEventInfo.prototype.setEvent = function (event) {
    var startDate = event.getStartDate() || new Date();

    this.event = event;
    var startMonth;
    
    if (event.useUTC) {
        this.year = startDate.getUTCFullYear();
        startMonth = startDate.getUTCMonth();
    } else {
        this.year = startDate.getFullYear();
        startMonth = startDate.getMonth();
    }
        
    if (!this.season || this.season === "auto") {
        switch (startMonth) {
        /* December - February */
        case 11:
        case 0:
        case 1:
            this.season = "winter";
            break;
        /* March - May */
        case 2:
        case 3:
        case 4:
            this.season = "spring";
            break;
        /* June - August */
        case 5:
        case 6:
        case 7:
            this.season = "summer";
            break;
        /* September - November */
        case 8:
        case 9:
        case 10:
            this.season = "autumn";
            break;
        }
    }
}

/**
 * Check whether the resort character threshold has been met.
 * @returns {boolean}
 */
ResortEventInfo.prototype.checkCharacterThreshold = function () {
    return save.getPlayedCharacterSet().length >= 40;
}

/**
 * Check whether the resort modal has previously been shown 
 * @returns {boolean}
 */
ResortEventInfo.prototype.previouslyShown = function () {
    return save.hasShownResortModal(this);
}

ResortEventInfo.prototype.setFlag = function (val) {
    save.setResortModalFlag(this, val);
}

ResortEventInfo.prototype.show = function () {
    if (!this.checkCharacterThreshold()) return;

    $(".resort-label").text(this.season[0].toUpperCase() + this.season.substring(1) + " " + this.year.toString());
    $(".resort-link-button").attr("href", this.link);
    this.setFlag(true);

    $resortModal.modal('show');
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
 * @param {ResortEventInfo?} resort
 * @param {EventAnnouncement?} announcement
 * @param {boolean} useUTC
 */
function GameEvent(id, name, dateRanges, costumes, background, candyImages, tags, includeStatuses, characters, resort, announcement, useUTC) {
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

    /** @type {ResortEventInfo?} */
    this.resort = resort;
    if (this.resort) this.resort.setEvent(this);

    /** @type {EventAnnouncement?} */
    this.announcement = announcement;
    if (this.announcement) this.announcement.event = this;
    
    /** @type {boolean} */
    this.useUTC = useUTC;
}

/**
 * 
 * @param {Date} queryDate 
 * @returns {DateRange[]}
 */
 GameEvent.prototype.getActiveRanges = function (queryDate) {
    if (OVERRIDE_EVENTS.size > 0 || MANUAL_EVENTS.has(this.id)) return [];

    var queryYear;
    var currentYearOverrides;

    if (this.useUTC) {
        queryYear = queryDate.getUTCFullYear();
        currentYearOverrides = this.dateRanges.filter(function (range) {
            return range.override && queryYear >= range.from.getUTCFullYear() && queryYear <= range.to.getUTCFullYear();
        });
    } else {
        queryYear = queryDate.getFullYear();
        currentYearOverrides = this.dateRanges.filter(function (range) {
            return range.override && queryYear >= range.from.getFullYear() && queryYear <= range.to.getFullYear();
        });
    }

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
 * @returns {boolean}
 */
GameEvent.prototype.isManuallyActivated = function () {
    if (OVERRIDE_EVENTS.size > 0) return OVERRIDE_EVENTS.has(this.id);
    return MANUAL_EVENTS.has(this.id);
}

/**
 * 
 * @returns {boolean}
 */
GameEvent.prototype.isActive = function () {
    return this.isManuallyActivated() || this.getActiveRanges(new Date()).length > 0;
}

/**
 * Get the earliest start date of this event.
 * If this event is not active or was manually activated, returns null.
 * @returns {Date?}
 */
GameEvent.prototype.getStartDate = function () {
    if (this.isManuallyActivated()) return null;

    var activeRanges = this.getActiveRanges(new Date());
    activeRanges.sort(function (a, b) { return a.from.getTime() - b.from.getTime(); });

    if (activeRanges.length > 0) {
        return activeRanges[0].from;
    } else {
        return null;
    }
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
    var consistentTimezone = $xml.attr("consistent-timezone") || null;
    var useUTC = false;
    
    if (consistentTimezone == "true")
    {
        useUTC = true;
    }
    
    var name = $xml.children("name").text() || null;

    var dateRanges = $xml.find("dates>date").map(function (index, elem) {
        return DateRange.parseRange($(elem), useUTC);
    }).get();

    $xml.find("dates>weekOf").each(function (index, elem) {
        dateRanges.push(DateRange.parseWeekOf($(elem), useUTC));
    });

    $xml.find("on").each(function (index, elem) {
        dateRanges.push(DateRange.parseSingleDay($(elem), useUTC));
    });

    var altCostumes = HighlightedAttributeList.parse($xml, "costumes");
    var background = $xml.children("background").text() || null;
    var candyImages = loadChildSet($xml, "candy>path");
    var fillTags = HighlightedAttributeList.parse($xml, "tag");
    var includeStatuses = loadChildSet($xml, "include-status");
    var characters = HighlightedAttributeList.parse($xml, "character");

    var resortElem = $xml.children("resort");
    var resortInfo = null;
    if (resortElem.length > 0) {
        resortInfo = ResortEventInfo.parse(resortElem);
    }

    var announceElem = $xml.children("announcement");
    var announceInfo = null;
    if (announceElem.length > 0) {
        announceInfo = EventAnnouncement.parse(announceElem);
    }

    return new GameEvent(id, name, dateRanges, altCostumes, background, candyImages, fillTags, includeStatuses, characters, resortInfo, announceInfo, useUTC);
}


/**
 * @returns {Promise<void>}
 */
function loadEventData () {
    $resortModal.on("hidden.bs.modal", function () { showAnnouncements(); });
    $eventAnnouncementModal.on("hidden.bs.modal", function () { showAnnouncements(); });

    eventCostumeSettings = HighlightedAttributeList.empty();
    eventTagSettings = HighlightedAttributeList.empty();
    eventCharacterSettings = HighlightedAttributeList.empty();

    beginStartupStage("Events");
    console.log("Loading events...");

    Promise.all(EVENT_FILES.map((file) => fetchXML(file))).then(function ($xmls) {
        var events = $xmls.flatMap(($xml) => {
            return $xml.find("event").map(function (index, elem) {
                return parseEventElement($(elem));
            }).get();
        });

        /** @type {Set<string>} */
        var activeIds = new Set();

        events.forEach(function (event) {
            if (!activeIds.has(event.id) && event.isActive()) {
                console.log("Activating event: " + event.name);
                activeIds.add(event.id);
                activeGameEvents.push(event);
            }
        });
        
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

                if (event.resort && !curResortEvent) {
                    console.log("[" + event.id + "] Activating resort mode.");
                    curResortEvent = event;
                }
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
        }
    });
}

function showAnnouncements() {
    /* If a resort is active, show that first. */
    if (curResortEvent && !curResortEvent.resort.previouslyShown()) {
        curResortEvent.resort.show();
        return;
    }

    /* Otherwise show other event announcements. */
    if (activeGameEvents.some(function (event) {
        if (event.announcement && !event.announcement.previouslyShown()) {
            event.announcement.show();
            return true;
        }
        
        return false;
    })) return;
}

function updateAnnouncementDropdown () {
    var announceEvents = activeGameEvents.filter(function (event) {
        return event.announcement && event.announcement.checkCharacterThreshold();
    });
    var dropdownButtons = [];

    if (curResortEvent && curResortEvent.resort.checkCharacterThreshold()) {
        if (announceEvents.length > 0) {
            var liElem = document.createElement("li");
            var aElem = document.createElement("a");

            var headerText = (
                curResortEvent.resort.season[0].toUpperCase() +
                curResortEvent.resort.season.substring(1) + " " +
                curResortEvent.resort.year.toString() +
                " Re-Sort"
            );

            $(aElem)
                .addClass("smooth-button red bordered announcement-dropdown-item")
                .attr("a", "#")
                .text(headerText)
                .on("click", function (ev) { showResortModal(); })
                .appendTo(liElem);

            dropdownButtons.push(liElem);
            $(".title-resort-button").hide();
        } else {
            $(".title-resort-button").show();
        }
    } else {
        $(".title-resort-button").hide();
    }

    announceEvents.forEach(function (event) {
        var liElem = document.createElement("li");
        var aElem = document.createElement("a");

        $(aElem)
            .addClass("smooth-button red bordered announcement-dropdown-item")
            .attr("a", "#")
            .text(event.name)
            .on("click", function (ev) { event.announcement.show(); })
            .appendTo(liElem);

        dropdownButtons.push(liElem);
    });

    if (dropdownButtons.length > 0) {
        $(".announcement-dropdown-options").empty().append(dropdownButtons);
        $(".title-announcement-dropdown").show();
    } else {
        $(".title-announcement-dropdown").hide();
    }
}

function showResortModal () {
    if (curResortEvent) curResortEvent.resort.show();
}
