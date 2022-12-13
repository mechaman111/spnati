const TAU = [
    0x61707865,  // "expa"
    0x3320646E,  // "nd 3"
    0x79622D32,  // "2-by"
    0x6B206574,  // "te k"
];

function rotateLeft (x, n) {
    return ((x << n) | (x >>> (32 - n))) & 0xFFFFFFFF;
}

/**
 * 
 * @param {ArrayBuffer} key A 256-bit key value.
 * @param {ArrayBuffer} nonce A 96-bit nonce value.
 */
function ChaCha20 (key, nonce) {
    this.key = new Uint32Array(key);
    this.nonce = new Uint32Array(nonce);

    /**
     * @type {Uint32Array}
     */
    this.state = new Uint32Array(16);

    /**
     * @type {Uint32Array}
     */
    this.initState = new Uint32Array(16);

    for (let i = 0; i < 4; i++) {
        this.initState[i] = TAU[i];
    }

    for (let i = 0; i < 8; i++) {
        this.initState[i+4] = this.key[i];
    }

    for (let i = 0; i < 3; i++) {
        this.initState[i+13] = this.nonce[i];
    }
}

ChaCha20.prototype.initialize = function (counter) {
    this.initState[12] = counter;
    for (let i = 0; i < 16; i++) {
        this.state[i] = this.initState[i];
    }
}

/**
 * Perform a quarter-round on given indices of the state.
 * 
 * @param {number} ia 
 * @param {number} ib 
 * @param {number} ic 
 * @param {number} id 
 */
ChaCha20.prototype.quarterRound = function (ia, ib, ic, id) {
    var a = this.state[ia];
    var b = this.state[ib];
    var c = this.state[ic];
    var d = this.state[id];

    a = ((a + b) & 0xFFFFFFFF); d ^= a; d = rotateLeft(d, 16);
    c = ((c + d) & 0xFFFFFFFF); b ^= c; b = rotateLeft(b, 12);
    a = ((a + b) & 0xFFFFFFFF); d ^= a; d = rotateLeft(d, 8);
    c = ((c + d) & 0xFFFFFFFF); b ^= c; b = rotateLeft(b, 7);

    this.state[ia] = (a >>>= 0);
    this.state[ib] = (b >>>= 0);
    this.state[ic] = (c >>>= 0);
    this.state[id] = (d >>>= 0);

}

/**
 * 
 * @param {number} counter 
 */
ChaCha20.prototype.hash = function (counter) {
    this.initialize(counter);

    for (var i = 0; i < 10; i++) {
        /* Odd round */
        this.quarterRound(0, 4, 8, 12);
        this.quarterRound(1, 5, 9, 13);
        this.quarterRound(2, 6, 10, 14);
        this.quarterRound(3, 7, 11, 15);
    
        /* Even round */
        this.quarterRound(0, 5, 10, 15);
        this.quarterRound(1, 6, 11, 12);
        this.quarterRound(2, 7, 8, 13);
        this.quarterRound(3, 4, 9, 14);
    }

    for (var i = 0; i < 16; i++) {
        this.state[i] = (this.initState[i] + this.state[i]) & 0xFFFFFFFF;
    }
}


/**
 * 
 * @param {number} startCounter 
 * @param {number} nonce1 
 * @param {number} nonce2 
 */
function ChaCha20RNG (startCounter, nonce1, nonce2) {
    var key = new Uint32Array(8);
    var nonce = new Uint32Array(3);

    for (var i = 0; i < 8; i++) {
        key[i] = 0x6972614D; // 'Mari'
    }

    nonce[0] = nonce1;
    nonce[1] = nonce2;

    this.chacha20 = new ChaCha20(key.buffer, nonce.buffer);
    this.streamCounter = startCounter;
    this.initalized = false;
}

/**
 * Generate a random 32-bit integer.
 * @returns {number}
 */
ChaCha20RNG.prototype.next = function () {
    var i = (this.streamCounter % 16);
    if ((i == 0) || !this.initalized) {
        let blockCounter = Math.floor(this.streamCounter / 16);
        this.chacha20.hash(blockCounter);
        this.initalized = true;
    }

    this.streamCounter++;
    return this.chacha20.state[i];
}

/**
 * Generate a random integer between `a` (inclusive) and `b` (exclusive).
 * @param {number} a 
 * @param {number} b 
 * @returns {number}
 */
ChaCha20RNG.prototype.randomInt = function (a, b) {
    return a + (this.next() % (b - a));
}

/**
 * Shuffle an array in-place.
 * @param {Array} arr 
 */
ChaCha20RNG.prototype.shuffle = function (arr) {
    for (var i = 0; i < (arr.length-1); i++) {
        let swapIdx = this.randomInt(i, arr.length);
        let tmp = arr[swapIdx];
        arr[swapIdx] = arr[i];
        arr[i] = tmp;
    }
}


/* The grouping epoch is midnight at Jan. 2, 2022.
 * (January 1, 2022 was a Saturday, but the week index calculation needs the epoch to lie on a Sunday.)
 */
//const GROUPING_EPOCH = 1641081600000;
const GROUPING_EPOCH = 1672617600000; // Grouping epoch is midnight Jan 2 2023, FOR DEMONSTRATION PURPOSES ONLY (makes sure demo doesn't start in the middle of a cycle)
//const MS_PER_WEEK = 7 * 24 * 3600 * 1000;
const MS_PER_WEEK = 2 * 24 * 3600 * 1000; // A "week" is two days, FOR DEMONSTRATION PURPOSES ONLY

/**
 * Partition characters into groups based on franchise magnetism rules.
 * nearby characters from the same franchise will stick together even after
 * the group is shuffled as a whole.
 * 
 * @param {ChaCha20RNG} rng
 * @param {Opponent[]} opponents 
 * @returns {Opponent[][]}
 */
function computeMagnetismGroups(rng, opponents) {
    var groups = [];
    opponents = opponents.slice();

    while (opponents.length > 0) {
        let curOpp = opponents.shift();
        let group = [curOpp];
        if (curOpp.magnetismTag) {
            let j = 0;
            for (let i = 0; i < 10; i++) {
                if (j >= opponents.length) break;
                if (opponents[j].magnetismTag == curOpp.magnetismTag) {
                    group.push(opponents.splice(j, 1)[0]);
                } else {
                    j++;
                }
            }
        }

        if (rng && (group.length > 1)) rng.shuffle(group);
        groups.push(group);
    }

    return groups;
}


/**
 * Orders a group of opponents into a randomly generated cycling set of rows.
 * 
 * Conceptually, each week is part of a multi-week cycle; each cycle corresponds to a
 * different randomly-generated permutation of the group's opponents, grouped into
 * rows of five.
 * 
 * Each week within a cycle has a different row rotate to the front of the group,
 * with the foremost row from the previous week cycling to the back of the group.
 * 
 * Once all rows have had a week at the front, a new cycle begins with a new
 * set of randomly-selected rows.
 * 
 * Franchise magnetism rules are applied throughout the row generation process;
 * nearby characters from the same franchise will stick together even after
 * the group is shuffled as a whole.
 * 
 * @param {Opponent[]} opponents 
 * @param {number} groupNumber
 * @param {number} [weekIdx]
 * @returns {Opponent[]} 
 */
function applySelectGroupOrdering(opponents, groupNumber, weekIdx) {
    if (weekIdx === undefined) {
        weekIdx = Math.floor((Date.now() - GROUPING_EPOCH) / MS_PER_WEEK);
    }

    const nRows = Math.ceil(opponents.length / 5);
    const cycleNumber = Math.floor(weekIdx / nRows);
    const cycleIndex = weekIdx % nRows;
    var rng = new ChaCha20RNG(0, groupNumber, cycleNumber);

    if (groupNumber == 1) {
        opponents = opponents.slice();

        /* Perform an initial shuffle to diffuse characters across rows. */
        rng.shuffle(opponents);

        /* Combine opponents into rows with at most one character per franchise. */
        let groups = [];
        while (opponents.length > 0) {
            let curOpp = opponents.shift();
            let group = [curOpp];

            let i = 0;
            while (group.length < 5 && i < opponents.length) {
                if (opponents[i].magnetismTag && group.some((opp) => opp.magnetismTag == opponents[i].magnetismTag)) {
                    i++;
                } else {
                    group.push(opponents.splice(i, 1)[0]);
                }
            }

            rng.shuffle(group);
            groups.push(group);
        }

        /* Shuffle rows and recombine into a flat opponents array. */
        rng.shuffle(groups);
        opponents = Array.prototype.concat.apply([], groups);

        /* Shift rows. */
        let tmp = opponents.splice(cycleIndex * 5);
        opponents = tmp.concat(opponents);
        
        /* Ensure that there are no two franchisemates within the top 10. */
        tmp = [];
        let i = 0;
        let foundMale = false;
        while (tmp.length < 10 && i < opponents.length) {
            let curOpp = opponents[i];
            if (curOpp.magnetismTag && tmp.some((opp) => opp.magnetismTag == curOpp.magnetismTag)) {
                i++;
                continue;
            }

            foundMale = foundMale || (curOpp.metaGender === "male");
            tmp.push(opponents.splice(i, 1)[0]);
        }

        /* Apply magnetism to remaining opponents and recombine again. */
        opponents = Array.prototype.concat.apply(tmp, computeMagnetismGroups(null, opponents));

        /* If there isn't already a male in the top 10, pull one up. */
        if (!foundMale) {
            for (let i = 10; i < opponents.length; i++) {
                if (opponents[i].metaGender === "male") {
                    /* Move opponents[i] to opponents[9]. */
                    let opp = opponents.splice(i, 1)[0];
                    opponents.splice(9, 0, opp);
                    break;
                }
            }
        }

        return opponents;
    } else {
        let groups = computeMagnetismGroups(rng, opponents);
        rng.shuffle(groups);
    
        /* Combine groups into a flat list of opponents. */
        opponents = Array.prototype.concat.apply([], groups);

        /* Shift rows. */
        let tmp = opponents.splice(cycleIndex * 5);
        return tmp.concat(opponents);
    }
}

/**
 * Compute each loaded opponent's selectGroupIndex value.
 * 
 * This is mostly in its own function as an aid for debugging;
 * calling this from a dev tools console with different weekIdx values
 * allows you to manually move through group cycles.
 * 
 * @param {number} [weekIdx] 
 */
function computeSelectGroupIndices(weekIdx) {
    var selectGroups = {};
    loadedOpponents.slice().sort((a, b) => a.listingIndex - b.listingIndex).forEach(function (opp) {
        if (opp.status) return;

        var selectGroup = parseInt(opp.selectGroup, 10) || 99
        if (!selectGroups[selectGroup]) {
            selectGroups[selectGroup] = [];
        }

        selectGroups[selectGroup].push(opp);
    });

    Object.entries(selectGroups).forEach(function (entry) {
        applySelectGroupOrdering(entry[1], entry[0], weekIdx).forEach(function (opp, idx) {
            opp.selectGroupIndex = idx;
        });
    });
}
