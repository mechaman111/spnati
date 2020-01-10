// Type annotations for the objects used within SPNATI.
// I'm writing this so that VSCode stops yelling at me about things
// not existing in player.js.

/* Global state from spniCore.js */

declare var players: Player[5];

declare var SENTRY_INITIALIZED: boolean;
declare var FORCE_ALT_COSTUME: string;
declare var ALT_COSTUMES_ENABLED: boolean;

declare var includedOpponentStatuses: Object<string, boolean>;
declare var alternateCostumeSets: Object<string, boolean>;

/* Function from spniCore.js */

declare function fetchCompressedURL(url: string): JQuery<HTMLElement>;

/* Functions from spniBehaviour.js */

declare function canonicalizeTag(tag: string): string;
declare function expandTagsList(tags: string[]): string[];

declare function fixupDialogue(dialogue: string): string;

declare function inInterval(value: number, interval: Interval): boolean;

declare function updateAllBehaviours(
    target: Player,
    target_tags: Array<Array<string>> | Array<string> | string | null,
    other_tags: Array<Array<string>> | Array<string> | string | null
): void;

declare function updateSelectionVisuals(): void;

/* Structures from spniBehaviour.js */

interface Interval {
    min: number | null,
    max: number | null
}

declare class Case {
    /* Most of the actual Case fields here are omitted, since they should
     * be encapsulated and treated as private.
     *
     * The fields we _do_ have here are included because of the existing
     * code we have that breaks encapsulation and accesses the fields
     * directly.
     */
    tag: string;

    target: string;
    alsoPlaying: string;
    filter: string;

    counters: Condition[];
    states: State[];

    constructor($xml: JQuery<HTMLElement>);

    getStages(): number[];
    getPossibleImages(stage: number): string[];
};

declare class Condition {
    /* Ditto above regarding omitted fields. */
    id: string;
    tag: string;

    constructor($xml: JQuery<HTMLElement>);
}

declare class State {
    rawDialogue: string;

    constructor(xml_or_state: JQuery<HTMLElement> | State);
}

/* Structure from spniGallery.js */

declare class Collectible {
    constructor(xmlElem: JQuery<HTMLElement>, player: Player);
}

/* Structure from spniClothing.js */

declare class Clothing {
    name: string;
    generic: string;
    type: string;
    position: string;
    image: string;
    plural: boolean;
    id: number;

    constructor(
        name: string,
        generic: string,
        type: string,
        position: string,
        image: string,
        plural: boolean,
        id: number
    );
}

/* Structures from spniDisplay.js */

declare class PoseDefinition {
    id: string;

    constructor($xml: JQuery<HTMLElement>, player: Player);
    getUsedImages(): string[];
}

declare class OpponentSelectionCard {
    constructor(opponent: Player);
}

/* Global constants */

/* clothing types */
declare const IMPORTANT_ARTICLE: string;
declare const MAJOR_ARTICLE: string;
declare const MINOR_ARTICLE: string;
declare const EXTRA_ARTICLE: string;

/* clothing positions */
declare const UPPER_ARTICLE: string;
declare const LOWER_ARTICLE: string;
declare const FULL_ARTICLE: string;
declare const OTHER_ARTICLE: string;

declare const STATUS_LOST_SOME: string;
declare const STATUS_MOSTLY_CLOTHED: string;
declare const STATUS_DECENT: string;
declare const STATUS_EXPOSED: string;
declare const STATUS_EXPOSED_TOP: string;
declare const STATUS_EXPOSED_BOTTOM: string;
declare const STATUS_EXPOSED_TOP_ONLY: string;
declare const STATUS_EXPOSED_BOTTOM_ONLY: string;
declare const STATUS_NAKED: string;
declare const STATUS_LOST_ALL: string;
declare const STATUS_ALIVE: string;
declare const STATUS_MASTURBATING: string;
declare const STATUS_FINISHED: string;

/* dialogue tags */
declare const SELECTED: string;
declare const OPPONENT_SELECTED: string;
declare const GAME_START: string;

declare const DEALING_CARDS: string;

declare const SWAP_CARDS: string;
declare const ANY_HAND: string;
declare const BAD_HAND: string;
declare const OKAY_HAND: string;
declare const GOOD_HAND: string;

declare const PLAYER_MUST_STRIP: string;
declare const PLAYER_MUST_STRIP_WINNING: string;
declare const PLAYER_MUST_STRIP_NORMAL: string;
declare const PLAYER_MUST_STRIP_LOSING: string;
declare const PLAYER_STRIPPING: string;
declare const PLAYER_STRIPPED: string;

declare const PLAYER_MUST_MASTURBATE: string;
declare const PLAYER_MUST_MASTURBATE_FIRST: string;
declare const PLAYER_START_MASTURBATING: string;
declare const PLAYER_MASTURBATING: string;
declare const PLAYER_HEAVY_MASTURBATING: string;
declare const PLAYER_FINISHING_MASTURBATING: string;
declare const PLAYER_FINISHED_MASTURBATING: string;
declare const PLAYER_AFTER_MASTURBATING: string;

declare const OPPONENT_LOST: string;
declare const OPPONENT_STRIPPING: string;
declare const OPPONENT_STRIPPED: string;

declare const OPPONENT_CHEST_WILL_BE_VISIBLE: string;
declare const OPPONENT_CROTCH_WILL_BE_VISIBLE: string;
declare const OPPONENT_CHEST_IS_VISIBLE: string;
declare const OPPONENT_CROTCH_IS_VISIBLE: string;
declare const OPPONENT_START_MASTURBATING: string;
declare const OPPONENT_MASTURBATING: string;
declare const OPPONENT_HEAVY_MASTURBATING: string;
declare const OPPONENT_FINISHED_MASTURBATING: string;

declare const PLAYERS_TIED: string;

declare const MALE_HUMAN_MUST_STRIP: string;
declare const MALE_MUST_STRIP: string;

declare const MALE_REMOVING_ACCESSORY: string;
declare const MALE_REMOVING_MINOR: string;
declare const MALE_REMOVING_MAJOR: string;
declare const MALE_CHEST_WILL_BE_VISIBLE: string;
declare const MALE_CROTCH_WILL_BE_VISIBLE: string;

declare const MALE_REMOVED_ACCESSORY: string;
declare const MALE_REMOVED_MINOR: string;
declare const MALE_REMOVED_MAJOR: string;
declare const MALE_CHEST_IS_VISIBLE: string;
declare const MALE_SMALL_CROTCH_IS_VISIBLE: string;
declare const MALE_MEDIUM_CROTCH_IS_VISIBLE: string;
declare const MALE_LARGE_CROTCH_IS_VISIBLE: string;
declare const MALE_CROTCH_IS_VISIBLE: string;

declare const MALE_MUST_MASTURBATE: string;
declare const MALE_START_MASTURBATING: string;
declare const MALE_MASTURBATING: string;
declare const MALE_HEAVY_MASTURBATING: string;
declare const MALE_FINISHED_MASTURBATING: string;

declare const FEMALE_HUMAN_MUST_STRIP: string;
declare const FEMALE_MUST_STRIP: string;

declare const FEMALE_REMOVING_ACCESSORY: string;
declare const FEMALE_REMOVING_MINOR: string;
declare const FEMALE_REMOVING_MAJOR: string;
declare const FEMALE_CHEST_WILL_BE_VISIBLE: string;
declare const FEMALE_CROTCH_WILL_BE_VISIBLE: string;

declare const FEMALE_REMOVED_ACCESSORY: string;
declare const FEMALE_REMOVED_MINOR: string;
declare const FEMALE_REMOVED_MAJOR: string;
declare const FEMALE_SMALL_CHEST_IS_VISIBLE: string;
declare const FEMALE_MEDIUM_CHEST_IS_VISIBLE: string;
declare const FEMALE_LARGE_CHEST_IS_VISIBLE: string;
declare const FEMALE_CHEST_IS_VISIBLE: string;
declare const FEMALE_CROTCH_IS_VISIBLE: string;

declare const FEMALE_MUST_MASTURBATE: string;
declare const FEMALE_START_MASTURBATING: string;
declare const FEMALE_MASTURBATING: string;
declare const FEMALE_HEAVY_MASTURBATING: string;
declare const FEMALE_FINISHED_MASTURBATING: string;

declare const GAME_OVER_VICTORY: string;
declare const GAME_OVER_DEFEAT: string;

declare const GLOBAL_CASE: string;
