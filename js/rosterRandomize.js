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
 * Generate a random number from 0 - 1.
 * @returns {number}
 */
ChaCha20RNG.prototype.random = function () {
    return this.next() / 0xFFFF_FFFF;
}

/**
 * Generate a number from the normal distribution with the given mean and standard deviation.
 * @param {number} mean
 * @param {number} std
 * @returns {number}
 */
ChaCha20RNG.prototype.randomNormal = function (mean, std) {
    /* Irwin-Hall distribution */
    var ret = 0;
    for (let i = 0; i < 12; i++) {
        ret += this.random();
    }
    ret -= 6;

    return (ret * std) + mean;
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


/* The roster epoch is midnight at Jan. 2, 2022.
 * (January 1, 2022 was a Saturday, but the week index calculation needs the epoch to lie on a Sunday.)
 */
const ROSTER_EPOCH = 1641081600000;
const MS_PER_WEEK = 7 * 24 * 3600 * 1000;

/**
 * Partition characters into groups based on franchise magnetism rules.
 * nearby characters from the same franchise will stick together even after
 * the group is shuffled as a whole.
 * 
 * @param {Opponent[]} opponents 
 * @param {ChaCha20RNG} [rng]
- * @returns {Opponent[][]}
 */
function computeMagnetismGroups(opponents, rng) {
    var groups = [];

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
 * 
 * @param {Opponent[]} opponents 
- * @returns {Opponent[]}
 */
function applyFeaturedSortRules(opponents) {
    var tmp = [];
    var i = 0;
    var foundMale = false;
    opponents = opponents.slice();

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
    var ret = Array.prototype.concat.apply(tmp, computeMagnetismGroups(opponents));

    /* If there isn't already a male in the top 10, pull one up. */
    if (!foundMale) {
        for (let i = 10; i < ret.length; i++) {
            if (ret[i].metaGender === "male") {
                /* Move opponents[i] to opponents[9]. */
                let opp = ret.splice(i, 1)[0];
                ret.splice(9, 0, opp);
                break;
            }
        }
    }

    return ret;
}

function randomizeRosterOrder(weekIdx, startStd, endStd) {
    if (weekIdx === undefined) {
        weekIdx = Math.floor((Date.now() - ROSTER_EPOCH) / MS_PER_WEEK);
    }
    
    startStd = (startStd !== undefined) ? startStd : 0.3;
    endStd = (endStd !== undefined) ? endStd : 0.15;

    var roster = loadedOpponents.slice().filter(function (opp) {
        return opp && !!opp.rosterScore;
    }).sort(function (a, b) {
        return b.rosterScore - a.rosterScore;
    });

    roster.forEach(function (opp, idx) {
        let curStd = ((idx / roster.length) * (endStd - startStd)) + startStd;
        let multiplier = Math.exp(Math.random() * curStd);
        opp.effectiveScore = opp.rosterScore * multiplier;
    });
}
