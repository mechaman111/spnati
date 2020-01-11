var assert = chai.assert;

describe('checkMarker', function () {
    it('evaluates a marker predicate string', function () {
        var test_dummy = getOpponent('test_dummy', 2);
        test_dummy.markers['test-marker'] = 5;

        assert.isTrue(checkMarker('test-marker==5', test_dummy, null, false), 'test-marker == 5');
        assert.isTrue(checkMarker('test-marker = 5', test_dummy, null, false), 'test-marker = 5');
        assert.isFalse(checkMarker('test-marker==4', test_dummy, null, false), 'test-marker == 4');

        assert.isFalse(checkMarker('test-marker!=5', test_dummy, null, false), 'test-marker != 5');
        assert.isTrue(checkMarker('test-marker!=4', test_dummy, null, false), 'test-marker != 4');

        assert.isTrue(checkMarker('test-marker<6', test_dummy, null, false), 'test-marker < 6');
        assert.isFalse(checkMarker('test-marker<4', test_dummy, null, false), 'test-marker < 4');

        assert.isFalse(checkMarker('test-marker>6', test_dummy, null, false), 'test-marker > 6');
        assert.isTrue(checkMarker('test-marker>4', test_dummy, null, false), 'test-marker > 4');

        assert.isTrue(checkMarker('test-marker<=6', test_dummy, null, false), 'test-marker <= 6');
        assert.isTrue(checkMarker('test-marker<=5', test_dummy, null, false), 'test-marker <= 5');
        assert.isFalse(checkMarker('test-marker<=4', test_dummy, null, false), 'test-marker <= 4');

        assert.isFalse(checkMarker('test-marker>=6', test_dummy, null, false), 'test-marker >= 6');
        assert.isTrue(checkMarker('test-marker>=5', test_dummy, null, false), 'test-marker >= 5');
        assert.isTrue(checkMarker('test-marker>=4', test_dummy, null, false), 'test-marker >= 4');
    });

    it('is capable of evaluating predicates involving strings', function () {
        var test_dummy = getOpponent('test_dummy', 2);
        test_dummy.markers['test-marker'] = 'foo';

        assert.isTrue(checkMarker('test-marker==foo', test_dummy, null, false), "test-marker == 'foo'");
        assert.isFalse(checkMarker('test-marker!=foo', test_dummy, null, false), "test-marker != 'foo'");

        assert.isTrue(checkMarker('test-marker < qux', test_dummy, null, false), "test-marker < 'qux'");
        assert.isTrue(checkMarker('test-marker > bar', test_dummy, null, false), "test-marker > 'bar'");

        assert.isFalse(checkMarker('test-marker > qux', test_dummy, null, false), "test-marker > 'qux'");
        assert.isFalse(checkMarker('test-marker < bar', test_dummy, null, false), "test-marker < 'bar'");
    });

    it('defaults to evaluating truthiness if no operator is specified', function () {
        var test_dummy = getOpponent('test_dummy', 2);

        test_dummy.markers['test-marker-1'] = 5;
        test_dummy.markers['test-marker-2'] = 0;

        assert.isTrue(checkMarker('test-marker-1', test_dummy, null, false), 'test marker 1');
        assert.isFalse(checkMarker('test-marker-2', test_dummy, null, false), 'test marker 2');
    });

    it('treats the entire predicate as a marker name if it is an invalid predicate', function () {
        var test_dummy = getOpponent('test_dummy', 2);

        test_dummy.markers['test?marker?1'] = 5;
        test_dummy.markers['test?marker?2'] = 0;

        assert.isTrue(checkMarker('test?marker?1', test_dummy, null, false), 'test marker 1');
        assert.isFalse(checkMarker('test?marker?2', test_dummy, null, false), 'test marker 2');
    });

    it('can use variables as right-hand side values', function () {
        var test_dummy = getOpponent('test_dummy', 2);
        var hk416 = getOpponent('hk416', 3);

        test_dummy.markers['test-1'] = 'HK416';
        test_dummy.markers['test-2'] = 'Black';

        assert.isTrue(checkMarker('test-1==~name~', test_dummy, hk416, false), 'testing target name marker');
        assert.isTrue(checkMarker('test-2==~self.name~', test_dummy, hk416, false), 'testing self name marker');
    });

    it('can use per-target markers as left-hand side values', function () {
        var test_dummy = getOpponent('test_dummy', 2);
        var hk416 = getOpponent('hk416', 3);

        test_dummy.markers['test'] = 0;
        test_dummy.markers['__hk416_test'] = 5;

        assert.isTrue(checkMarker('test*==5', test_dummy, hk416, false), 'testing per-target marker');
    });

    it('can specifically check current state', function () {
        var test_dummy = getOpponent('test_dummy', 2);

        test_dummy.markers['test'] = 1;
        test_dummy.updateBehaviour(['checkMarker_currentOnly_test_1'], null);

        assert.isTrue(checkMarker('test==5', test_dummy, null, true), 'currentOnly=true');
        assert.isFalse(checkMarker('test==5', test_dummy, null, false), 'currentOnly=false');
    });

    it('can specifically check current state with per-target markers', function () {
        var test_dummy = getOpponent('test_dummy', 2);
        var hk416 = getOpponent('hk416', 3);

        test_dummy.markers['__hk416_test'] = 1;
        test_dummy.updateBehaviour(['checkMarker_currentOnly_test_2'], null);

        assert.isTrue(checkMarker('test*==5', test_dummy, hk416, true), 'currentOnly=true');
        assert.isFalse(checkMarker('test*==5', test_dummy, hk416, false), 'currentOnly=false');
    });

    it('can specifically check current state when multiple markers are being set', function () {
        var test_dummy = getOpponent('test_dummy', 2);
        var hk416 = getOpponent('hk416', 3);

        test_dummy.markers['test1'] = 1;
        test_dummy.markers['test2'] = 'foo';
        test_dummy.markers['__hk416_test3'] = 15;
        test_dummy.markers['__hk416_test4'] = 'm16';

        test_dummy.updateBehaviour(['checkMarker_currentOnly_test_3'], null);

        assert.isTrue(checkMarker('test1==5', test_dummy, null, true), 'currentOnly=true (1)');
        assert.isTrue(checkMarker('test2==not foo', test_dummy, null, true), 'currentOnly=true (2)');
        assert.isTrue(checkMarker('test3*==45', test_dummy, hk416, true), 'currentOnly=true (3*)');
        assert.isTrue(checkMarker('test4*==sleepo', test_dummy, hk416, true), 'currentOnly=true (4*)');

        assert.isTrue(checkMarker('test1==1', test_dummy, null, false), 'currentOnly=false (1)');
        assert.isTrue(checkMarker('test2==foo', test_dummy, null, false), 'currentOnly=false (2)');
        assert.isTrue(checkMarker('test3*==15', test_dummy, hk416, false), 'currentOnly=false (3*)');
        assert.isTrue(checkMarker('test4*==m16', test_dummy, hk416, false), 'currentOnly=false (4*)');
    });
});

describe('expandDialogue', function () {
    it('expands variables in a line of dialogue', function () {
        var test_dummy = getOpponent('test_dummy', 1);
        var chihiro = getOpponent('chihiro', 2);

        assert.equal(
            expandDialogue('Your name is ~name~', test_dummy, chihiro),
            'Your name is Chihiro',
            'testing basic variable expansion'
        );
    });

    it('supports multiple variables in one dialogue line', function () {
        var test_dummy = getOpponent('test_dummy', 1);
        var chihiro = getOpponent('chihiro', 2);

        assert.equal(
            expandDialogue('~name~ ~chihiro.name~ ~target.name~', test_dummy, chihiro),
            'Chihiro Chihiro Chihiro',
            'testing multiple variable expansion'
        );
    });

    it('supports custom variable bindings', function () {
        var test_dummy = getOpponent('test_dummy', 1);
        var chihiro = getOpponent('chihiro', 2);

        assert.equal(
            expandDialogue('~name~ ~foo.name~ ~target.name~', test_dummy, chihiro, {
                'foo': chihiro
            }),
            'Chihiro Chihiro Chihiro',
            'testing multiple variable expansion'
        );
    });

    it('capitalizes variables with an uppercase first letter', function () {
        var test_dummy = getOpponent('test_dummy', 1);
        var chihiro = getOpponent('chihiro', 2);
        chihiro.label = 'chihiro';

        assert.equal(
            expandDialogue('~Name~', test_dummy, chihiro),
            'Chihiro',
            'testing initCap expansion'
        );

        assert.equal(
            expandDialogue('~name~', test_dummy, chihiro),
            'chihiro',
            'testing non-initCap expansion'
        );
    });
});

describe('Dialogue Variables', function () {
    describe('~player~', function () {
        it("expands to the human player's name", function () {
            var test_dummy = getOpponent('test_dummy', 1);
            var chihiro = getOpponent('chihiro', 2);

            humanPlayer = new Player('human');
            players[HUMAN_PLAYER] = humanPlayer;
            players[HUMAN_PLAYER].slot = HUMAN_PLAYER;

            humanPlayer.label = 'Player';

            assert.equal(
                expandDialogue('~player~', test_dummy, chihiro),
                'Player',
                'testing expansion'
            );
        });
    });

    describe('~name~', function () {
        it("expands to the phase target's name", function () {
            var test_dummy = getOpponent('test_dummy', 1);
            var chihiro = getOpponent('chihiro', 2);

            assert.equal(
                expandDialogue('~name~', test_dummy, chihiro),
                'Chihiro',
                'testing expansion'
            );
        });
    });

    describe('~clothing~', function () {
        it("expands to the generic name of the clothing the phase target is removing, by default", function () {
            var test_dummy = getOpponent('test_dummy', 1);
            var chihiro = getOpponent('chihiro', 2);

            test_dummy.removedClothing = test_dummy.clothing[4];

            assert.equal(
                expandDialogue('~clothing~', chihiro, test_dummy),
                'socks',
                'testing expansion'
            );
        });

        describe('~clothing.type~', function () {
            it("expands to the importance / type of the clothing the phase target is removing", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                var chihiro = getOpponent('chihiro', 2);

                test_dummy.removedClothing = test_dummy.clothing[4];

                assert.equal(
                    expandDialogue('~clothing.type~', chihiro, test_dummy),
                    'minor',
                    'testing expansion'
                );
            });
        });

        describe('~clothing.position~', function () {
            it("expands to the position of the clothing the phase target is removing", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                var chihiro = getOpponent('chihiro', 2);

                test_dummy.removedClothing = test_dummy.clothing[4];

                assert.equal(
                    expandDialogue('~clothing.position~', chihiro, test_dummy),
                    'lower',
                    'testing expansion'
                );
            });
        });

        describe('~clothing.plural~', function () {
            it("expands to 'plural' if the clothing in question uses the plural form, and 'single' otherwise", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                var chihiro = getOpponent('chihiro', 2);

                test_dummy.removedClothing = test_dummy.clothing[4];

                assert.equal(
                    expandDialogue('~clothing.plural~', chihiro, test_dummy),
                    'plural',
                    'testing expansion (plural case)'
                );

                test_dummy.removedClothing = test_dummy.clothing[3];

                assert.equal(
                    expandDialogue('~clothing.plural~', chihiro, test_dummy),
                    'single',
                    'testing expansion (singular case)'
                );
            });
        });

        describe('~clothing.ifPlural~', function () {
            it("expands to the first given option if the removed clothing is plural, and to the second option otherwise", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                var chihiro = getOpponent('chihiro', 2);

                test_dummy.removedClothing = test_dummy.clothing[4];

                assert.equal(
                    expandDialogue('~clothing.ifPlural(a|b)~', chihiro, test_dummy),
                    'a',
                    'testing expansion (plural case)'
                );

                test_dummy.removedClothing = test_dummy.clothing[3];

                assert.equal(
                    expandDialogue('~clothing.ifPlural(a|b)~', chihiro, test_dummy),
                    'b',
                    'testing expansion (singular case)'
                );
            });
        });

        describe('~cards~', function () {
            beforeEach(function () {
                setupPoker();
            });

            it("expands to a number of cards to trade in during a hand phase by default", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                var chihiro = getOpponent('chihiro', 2);

                for (var i = 0; i <= 5; i++) {
                    // generate tradeIns arrays ranging from all-false to all-true
                    test_dummy.hand.tradeIns = [0, 1, 2, 3, 4].map(function (x) {
                        return x < i;
                    });

                    assert.equal(
                        expandDialogue('~cards~', test_dummy, chihiro),
                        i.toString(),
                        i + ' trade-in(s)'
                    );
                }
            });

            describe('~cards.ifPlural~', function () {
                it('expands to the second option if exactly one card is traded in, and to the first option otherwise', function () {
                    var test_dummy = getOpponent('test_dummy', 1);
                    var chihiro = getOpponent('chihiro', 2);

                    for (var i = 0; i <= 5; i++) {
                        test_dummy.hand.tradeIns = [0, 1, 2, 3, 4].map(function (x) {
                            return x < i;
                        });

                        assert.equal(
                            expandDialogue('~cards.ifPlural(many|one)~', test_dummy, chihiro),
                            (i === 1) ? 'one' : 'many',
                            i + ' trade-in(s)'
                        );
                    }
                });
            });

            describe('~cards.text~', function () {
                it('expands to the word form of the number of cards being traded in', function () {
                    var test_dummy = getOpponent('test_dummy', 1);
                    var chihiro = getOpponent('chihiro', 2);

                    test_dummy.hand.tradeIns = [false, false, false, false, false];
                    assert.equal(expandDialogue('~cards.text~', test_dummy, chihiro), 'zero', '0 trade-ins');

                    test_dummy.hand.tradeIns = [true, false, false, false, false];
                    assert.equal(expandDialogue('~cards.text~', test_dummy, chihiro), 'one', '1 trade-in');

                    test_dummy.hand.tradeIns = [true, true, false, false, false];
                    assert.equal(expandDialogue('~cards.text~', test_dummy, chihiro), 'two', '2 trade-ins');

                    test_dummy.hand.tradeIns = [true, true, true, false, false];
                    assert.equal(expandDialogue('~cards.text~', test_dummy, chihiro), 'three', '3 trade-ins');

                    test_dummy.hand.tradeIns = [true, true, true, true, false];
                    assert.equal(expandDialogue('~cards.text~', test_dummy, chihiro), 'four', '4 trade-ins');

                    test_dummy.hand.tradeIns = [true, true, true, true, true];
                    assert.equal(expandDialogue('~cards.text~', test_dummy, chihiro), 'five', '5 trade-ins');
                });
            });
        });

        describe('~collectible.*~', function () {
            beforeEach(function () {
                var test_dummy = getOpponent('test_dummy', 2);

                /* mock some collectible methods */
                test_dummy.collectibles.forEach(function (c) {
                    if (c.id === 'test1') {
                        c.isUnlocked = function () {
                            return true;
                        };
                        c.getCounter = function () {
                            return 5;
                        };
                    } else if (c.id === 'test2') {
                        c.isUnlocked = function () {
                            return false;
                        };
                        c.getCounter = function () {
                            return 0;
                        };
                    }
                });
            });

            it('expands to true/false depending on whether the given collectible is unlocked, by default', function () {
                var test_dummy = getOpponent('test_dummy', 2);

                assert.equal(
                    expandDialogue('~collectible.test1~', test_dummy, null),
                    'true',
                    'testing unlocked collectible'
                );

                assert.equal(
                    expandDialogue('~collectible.test2~', test_dummy, null),
                    'false',
                    'testing locked collectible'
                );
            });

            describe('~collectible.*.counter~', function () {
                it('expands to the numeric counter value associated with the given collectible', function () {
                    var test_dummy = getOpponent('test_dummy', 2);

                    assert.equal(
                        expandDialogue('~collectible.test1.counter~', test_dummy, null),
                        '5',
                        'testing unlocked collectible'
                    );

                    assert.equal(
                        expandDialogue('~collectible.test2.counter~', test_dummy, null),
                        '0',
                        'testing locked collectible'
                    );
                });
            });
        });

        describe('~marker~', function () {
            it('expands to the value of a marker', function () {
                var test_dummy = getOpponent('test_dummy', 2);
                test_dummy.markers['test1'] = 'foo';
                test_dummy.markers['test2'] = 5;

                assert.equal(
                    expandDialogue('~marker.test1~', test_dummy, null),
                    'foo',
                    'testing string marker expansion'
                );

                assert.equal(
                    expandDialogue('~marker.test2~', test_dummy, null),
                    '5',
                    'testing numeric marker expansion'
                );
            });
        })

        describe('~persistent~', function () {
            before(function () {
                /* Dummy this function out so we don't touch actual persistent data */
                save.getPersistentMarker = function (pl, targetedName) {
                    if (targetedName === 'test1') return 'foo';
                    if (targetedName === 'test2') return 5;
                };
            });

            after(function () {
                delete save.getPersistentMarker;
            });

            it('expands to the value of a persistent marker', function () {
                var test_dummy = getOpponent('test_dummy', 2);
                test_dummy.markers['test1'] = 'foo';
                test_dummy.markers['test2'] = 5;

                assert.equal(
                    expandDialogue('~persistent.test1~', test_dummy, null),
                    'foo',
                    'testing string persistent marker expansion'
                );

                assert.equal(
                    expandDialogue('~persistent.test2~', test_dummy, null),
                    '5',
                    'testing numeric persistent marker expansion'
                );
            });
        });

        describe('~background~', function () {
            beforeEach(function () {
                activeBackground = new Background('inventory', 'img/backgrounds/inventory.png', {
                    name: "The Inventory",
                    author: "Zeuses-Swan-Song",
                });
            });
            after(function () {
                activeBackground = defaultBackground;
            })

            it('expands to the name of the current background', function () {
                var test_dummy = getOpponent('test_dummy', 2);

                assert.equal(
                    expandDialogue('~background~', test_dummy, null),
                    'inventory',
                    'testing variable expansion'
                );
            });

            describe('~background.location~', function () {
                it('expands to either "indoors" or "outdoors" depending on the background\'s metadata', function () {
                    var test_dummy = getOpponent('test_dummy', 2);

                    activeBackground.metadata['location'] = 'indoors';
                    assert.equal(
                        expandDialogue('~background.location~', test_dummy, null),
                        'indoors',
                        'testing variable expansion for Inventory background'
                    );

                    activeBackground.metadata['location'] = 'outdoors';
                    assert.equal(
                        expandDialogue('~background.location~', test_dummy, null),
                        'outdoors',
                        'testing variable expansion for Beach background'
                    );
                });
            });
        });
    });
});

describe('Opponent.updateBehaviour', function () {
    it('returns false if a stage cannot be found', function () {
        var test_dummy = getOpponent('test_dummy', 2);

        test_dummy.stage = 9; // impossible stage for this character   
        var ret = test_dummy.updateBehaviour([ANY_HAND], null);
        assert.equal(ret, false, 'checking return value');
    });

    it('returns false if no cases with the given tags can be found', function () {
        var test_dummy = getOpponent('test_dummy', 2);

        test_dummy.stage = 1;
        var ret = test_dummy.updateBehaviour(['foo', 'bar', 'baz'], null);
        assert.equal(ret, false, 'checking return value');
    });

    it('selects a dialogue state based on the given tags and case priorities', function () {
        var test_dummy = getOpponent('test_dummy', 2);

        /* In the XML, the `basic_test_a` case has a priority of 100,
         * while the `basic_test_b` case has a priority of 0.
         * 
         * In the absence of any other conditions, the `basic_test_a`
         * case should always be selected.
         */
        test_dummy.updateBehaviour(['basic_test_a', 'basic_test_b'], null);
        var state = test_dummy.chosenState;
        assert.equal(state.rawDialogue, 'basic_test_a', 'checking result dialogue state');
    });

    it('always considers `global`-tagged cases', function () {
        var test_dummy = getOpponent('test_dummy', 2);
        test_dummy.updateBehaviour(['basic_test_b'], null);

        var state = test_dummy.chosenState;
        assert.equal(state.rawDialogue, 'global_fallback', 'checking result dialogue state');
    });
});

/* Most of these are pretty old; they predate the big move to <condition/> elements. */
describe('Conditions', function () {
    afterEach(cleanUpPlayers);

    describe('`stage` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', undefined, undefined]));

        it('passes if the character is at or within a given stage', function () {
            var test_dummy = players[2];
            var c1 = dummyCase({
                'stage': '0-1'
            });
            var c2 = dummyCase({
                'stage': '1-5'
            });

            assert.isTrue(c1.checkConditions(test_dummy), 'testing true case');
            assert.isFalse(c2.checkConditions(test_dummy), 'testing false case');
        });
    });

    describe('`target` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', 'chihiro', undefined]));

        it('passes if a given character is the focus target for a phase', function () {
            var test_dummy = players[2],
                chihiro = players[3];
            var c1 = dummyCase({
                'target': 'chihiro'
            });
            var c2 = dummyCase({
                'target': 'hk416'
            });

            assert.isTrue(c1.checkConditions(test_dummy, chihiro), 'testing true case');
            assert.isFalse(c2.checkConditions(test_dummy, chihiro), 'testing false case');
        });

        it('works from end-to-end', function () {
            var test_dummy = players[2],
                chihiro = players[3];
            test_dummy.updateBehaviour(['target_test_a', 'target_test_b'], chihiro);

            var state = test_dummy.chosenState;
            assert.equal(state.rawDialogue, 'target_test_a', 'checking result dialogue state');
        });
    });

    describe('`filter` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', 'chihiro', 'hk416']));

        it('detects if the focus target for a phase has a given tag', function () {
            var test_dummy = players[2],
                chihiro = players[3];
            var c1 = dummyCase({
                'filter': 'danganronpa'
            });

            assert.isTrue(c1.checkConditions(test_dummy, chihiro), 'testing true case');
        });

        it('will not be satisfied if the phase target does not have the given tag, even if other characters do', function () {
            var test_dummy = players[2],
                chihiro = players[3],
                hk416 = players[4];
            var c2 = dummyCase({
                'filter': 'girls_frontline'
            });

            assert.isFalse(c2.checkConditions(test_dummy, chihiro), 'testing false case');
        });

        it('works from end-to-end', function () {
            var test_dummy = players[2],
                chihiro = players[3],
                hk416 = players[4];

            test_dummy.updateBehaviour(['filter_test_1a', 'filter_test_1b'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'filter_test_1a', 'checking condition test 1');

            test_dummy.updateBehaviour(['filter_test_2a', 'filter_test_2b'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'filter_test_2a', 'checking condition test 2');
        });
    });

    describe('`alsoPlaying` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', 'chihiro', 'hk416']));

        it('detects if a given character is also at the table', function () {
            var test_dummy = players[2],
                chihiro = players[3],
                hk416 = players[4];
            var c = dummyCase({
                'alsoPlaying': 'hk416'
            });

            assert.isTrue(c.checkConditions(test_dummy, chihiro), 'testing true case');
        });

        it('will not be satisfied if the given character is the phase target', function () {
            var test_dummy = players[2],
                chihiro = players[3],
                hk416 = players[4];
            var c = dummyCase({
                'alsoPlaying': 'chihiro'
            });

            assert.isFalse(c.checkConditions(test_dummy, chihiro), 'testing false case');
        });

        it('works from end-to-end', function () {
            var test_dummy = players[2],
                chihiro = players[3],
                hk416 = players[4];

            test_dummy.updateBehaviour(['alsoplaying_test_1a', 'alsoplaying_test_1b'], null);
            assert.equal(test_dummy.chosenState.rawDialogue, 'alsoplaying_test_1a', 'checking condition test 1');

            test_dummy.updateBehaviour(['alsoplaying_test_2a', 'alsoplaying_test_2b'], hk416);
            assert.equal(test_dummy.chosenState.rawDialogue, 'alsoplaying_test_2a', 'checking condition test 2');
        })
    });

    describe('`targetStage` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', 'chihiro', undefined]));

        it("detects if the current phase target's current stage is within an interval", function () {
            var test_dummy = players[2],
                chihiro = players[3];
            var c1 = dummyCase({
                'targetStage': {
                    'min': 2,
                    'max': 3
                }
            });
            var c2 = dummyCase({
                'targetStage': {
                    'min': 0,
                    'max': 1
                }
            });
            var c3 = dummyCase({
                'targetStage': {
                    'min': 4,
                    'max': 5
                }
            });

            setOpponentStage(chihiro, 2);
            assert.isTrue(c1.checkConditions(test_dummy, chihiro), 'testing true case');
            assert.isFalse(c2.checkConditions(test_dummy, chihiro), 'testing false case 1');
            assert.isFalse(c3.checkConditions(test_dummy, chihiro), 'testing false case 2');
        });

        it('works from end-to-end', function () {
            var test_dummy = players[2],
                chihiro = players[3];

            setOpponentStage(chihiro, 1);
            test_dummy.updateBehaviour(['targetstage_test_1a', 'targetstage_test_1b', 'targetstage_test_1c'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'targetstage_test_1a', 'checking basic condition');

            setOpponentStage(chihiro, 2);
            test_dummy.updateBehaviour(['targetstage_test_2a', 'targetstage_test_2b', 'targetstage_test_2c'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'targetstage_test_2a', 'checking interval condition');
        })
    });

    describe('`targetLayers` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', 'hk416', undefined]));

        it("detects if the current phase target has a certain number of layers remaining within an interval", function () {
            var test_dummy = players[2],
                hk416 = players[3];
            var c1 = dummyCase({
                'targetLayers': {
                    'min': 2,
                    'max': 3
                }
            });
            var c2 = dummyCase({
                'targetLayers': {
                    'min': 0,
                    'max': 1
                }
            });
            var c3 = dummyCase({
                'targetLayers': {
                    'min': 4,
                    'max': 5
                }
            });

            setOpponentStage(hk416, 5);
            assert.isTrue(c1.checkConditions(test_dummy, hk416), 'testing true case');
            assert.isFalse(c2.checkConditions(test_dummy, hk416), 'testing false case 1');
            assert.isFalse(c3.checkConditions(test_dummy, hk416), 'testing false case 2');
        });

        it('works from end-to-end', function () {
            var test_dummy = players[2],
                hk416 = players[3];

            setOpponentStage(hk416, 3);
            test_dummy.updateBehaviour(['targetlayers_test_1a', 'targetlayers_test_1b', 'targetlayers_test_1c'], hk416);
            assert.equal(test_dummy.chosenState.rawDialogue, 'targetlayers_test_1a', 'checking non-interval condition');

            setOpponentStage(hk416, 4);
            test_dummy.updateBehaviour(['targetlayers_test_2a', 'targetlayers_test_2b', 'targetlayers_test_2c'], hk416);
            assert.equal(test_dummy.chosenState.rawDialogue, 'targetlayers_test_2a', 'checking interval condition');
        })
    });

    describe('`targetStartingLayers` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', 'chihiro', 'hk416']));

        it("detects if the current phase target began the game with a certain number of layers", function () {
            var test_dummy = players[2],
                chihiro = players[3],
                hk416 = players[4];
            var c = dummyCase({
                'targetStartingLayers': {
                    'min': 5,
                    'max': 6
                }
            });

            assert.isTrue(c.checkConditions(test_dummy, chihiro), 'testing true case');
            assert.isFalse(c.checkConditions(test_dummy, hk416), 'testing false case');
        });

        it('works from end-to-end', function () {
            var test_dummy = players[2],
                chihiro = players[3],
                hk416 = players[4];

            test_dummy.updateBehaviour(['targetstartinglayers_test_1a', 'targetstartinglayers_test_1b', 'targetstartinglayers_test_1c'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'targetstartinglayers_test_1a', 'checking non-interval condition');

            test_dummy.updateBehaviour(['targetstartinglayers_test_2a', 'targetstartinglayers_test_2b', 'targetstartinglayers_test_2c'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'targetstartinglayers_test_2a', 'checking interval condition');
        });
    });

    describe('`targetSaidMarker` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', 'chihiro', undefined]));

        it("detects if the current phase target's committed marker set matches a given predicate", function () {
            var test_dummy = players[2],
                chihiro = players[3];
            chihiro.markers['test1'] = 1;
            chihiro.markers['test2'] = 'foo';

            var c1 = dummyCase({
                'targetSaidMarker': 'test1==1'
            });
            var c2 = dummyCase({
                'targetSaidMarker': 'test2==foo'
            });
            var c3 = dummyCase({
                'targetSaidMarker': 'test1==foo'
            });

            assert.isTrue(c1.checkConditions(test_dummy, chihiro), 'testing numeric value predicate');
            assert.isTrue(c2.checkConditions(test_dummy, chihiro), 'testing string value predicate');
            assert.isFalse(c3.checkConditions(test_dummy, chihiro), 'testing false case');
        });

        it('works from end-to-end', function () {
            var test_dummy = players[2],
                chihiro = players[3];

            chihiro.markers['test'] = 42;
            test_dummy.updateBehaviour(['targetsaidmarker_test_1a'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'targetsaidmarker_test_1a', 'checking result dialogue state');

            test_dummy.resetState();

            chihiro.markers['test'] = 43;
            test_dummy.updateBehaviour(['targetsaidmarker_test_1a'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'global_fallback', 'checking result dialogue state (false case)');
        });
    });

    describe('`targetNotSaidMarker` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', 'chihiro', undefined]));

        it("detects if the given marker on the current phase target has a falsy committed value", function () {
            var test_dummy = players[2],
                chihiro = players[3];
            chihiro.markers['test1'] = 0;
            chihiro.markers['test2'] = 1;

            var c1 = dummyCase({
                'targetNotSaidMarker': 'test1'
            });
            var c2 = dummyCase({
                'targetNotSaidMarker': 'test2'
            });

            assert.isTrue(c1.checkConditions(test_dummy, chihiro), 'testing true case');
            assert.isFalse(c2.checkConditions(test_dummy, chihiro), 'testing false case');
        });

        it('works from end-to-end', function () {
            var test_dummy = players[2],
                chihiro = players[3];

            test_dummy.updateBehaviour(['targetnotsaidmarker_test_1a'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'targetnotsaidmarker_test_1a', 'checking result dialogue state');

            test_dummy.resetState();
            chihiro.markers['test'] = 1;

            test_dummy.updateBehaviour(['targetnotsaidmarker_test_1a'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'global_fallback', 'checking result dialogue state (false case)');
        });
    });

    describe('`targetTimeInStage` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', 'chihiro', undefined]));

        it("detects if the current phase target has spent a given amount of time in their current stage", function () {
            var test_dummy = players[2],
                chihiro = players[3];
            chihiro.timeInStage = 4;

            var c1 = dummyCase({
                'targetTimeInStage': {
                    'min': 3,
                    'max': 4
                }
            });
            var c2 = dummyCase({
                'targetTimeInStage': {
                    'min': 0,
                    'max': 2
                }
            });
            var c3 = dummyCase({
                'targetTimeInStage': {
                    'min': 5,
                    'max': null
                }
            });

            assert.isTrue(c1.checkConditions(test_dummy, chihiro), 'testing true case');
            assert.isFalse(c2.checkConditions(test_dummy, chihiro), 'testing false case 1');
            assert.isFalse(c3.checkConditions(test_dummy, chihiro), 'testing false case 2');
        });

        it("works end-to-end", function () {
            var test_dummy = players[2],
                chihiro = players[3];
            chihiro.timeInStage = 4;

            test_dummy.updateBehaviour(['targettimeinstage_test_1a', 'targettimeinstage_test_1b', 'targettimeinstage_test_1c'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'targettimeinstage_test_1a', 'checking non-interval condition');

            test_dummy.updateBehaviour(['targettimeinstage_test_2a', 'targettimeinstage_test_2b', 'targettimeinstage_test_2c'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'targettimeinstage_test_2a', 'checking interval condition');
        });
    });

    describe('`oppHand` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', 'chihiro', undefined]));

        it("detects if the current phase target has a hand of a given strength", function () {
            var test_dummy = players[2],
                chihiro = players[3];

            setupPoker();
            chihiro.hand.strength = THREE_OF_A_KIND;

            var c1 = dummyCase({
                'oppHand': 'Three of a Kind'
            });
            var c2 = dummyCase({
                'oppHand': 'High Card'
            });

            assert.isTrue(c1.checkConditions(test_dummy, chihiro), 'testing true case');
            assert.isFalse(c2.checkConditions(test_dummy, chihiro), 'testing false case');
        });

        it("works end-to-end", function () {
            var test_dummy = players[2],
                chihiro = players[3];

            setupPoker();
            chihiro.hand.strength = THREE_OF_A_KIND;

            test_dummy.updateBehaviour(['opphand_test_1a'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'opphand_test_1a', 'checking result dialogue state');

            test_dummy.resetState();
            chihiro.hand.strength = HIGH_CARD;

            test_dummy.updateBehaviour(['opphand_test_1a'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'global_fallback', 'checking result dialogue state (false case)');
        });
    });

    describe('`consecutiveLosses` Condition', function () {
        beforeEach(setupTableFixture.bind(this, [undefined, 'test_dummy', 'chihiro', undefined]));

        it("checks the current phase target's consecutive loss counter (if a phase target exists)", function () {
            var test_dummy = players[2],
                chihiro = players[3];

            chihiro.consecutiveLosses = 4;
            test_dummy.consecutiveLosses = 5;

            var c1 = dummyCase({
                'consecutiveLosses': {
                    'min': 3,
                    'max': 4
                }
            });
            var c2 = dummyCase({
                'consecutiveLosses': {
                    'min': 0,
                    'max': 2
                }
            });
            var c3 = dummyCase({
                'consecutiveLosses': {
                    'min': 5,
                    'max': 6
                }
            });

            assert.isTrue(c1.checkConditions(test_dummy, chihiro), 'testing true case');
            assert.isFalse(c2.checkConditions(test_dummy, chihiro), 'testing false case 1');
            assert.isFalse(c3.checkConditions(test_dummy, chihiro), 'testing false case 2');
        });

        it("checks the character's own consecutive loss counter (if a phase target does not exist)", function () {
            var test_dummy = players[2],
                chihiro = players[3];

            test_dummy.consecutiveLosses = 4;
            chihiro.consecutiveLosses = 5;

            var c1 = dummyCase({
                'consecutiveLosses': {
                    'min': 3,
                    'max': 4
                }
            });
            var c2 = dummyCase({
                'consecutiveLosses': {
                    'min': 0,
                    'max': 2
                }
            });
            var c3 = dummyCase({
                'consecutiveLosses': {
                    'min': 5,
                    'max': 6
                }
            });

            assert.isTrue(c1.checkConditions(test_dummy, null), 'testing true case');
            assert.isFalse(c2.checkConditions(test_dummy, null), 'testing false case 1');
            assert.isFalse(c3.checkConditions(test_dummy, null), 'testing false case 2');
        });

        it("works from end-to-end", function () {
            var test_dummy = players[2],
                chihiro = players[3];

            chihiro.consecutiveLosses = 4;
            test_dummy.consecutiveLosses = 5;

            test_dummy.updateBehaviour(['consecutivelosses_test_1a', 'consecutivelosses_test_1b', 'consecutivelosses_test_1c'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'consecutivelosses_test_1a', 'checking non-interval condition (phase target mode)');

            test_dummy.resetState();
            test_dummy.consecutiveLosses = 5;

            test_dummy.updateBehaviour(['consecutivelosses_test_2a', 'consecutivelosses_test_2b', 'consecutivelosses_test_2c'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'consecutivelosses_test_2a', 'checking interval condition (phase target mode)');

            test_dummy.resetState();
            chihiro.consecutiveLosses = 5;
            test_dummy.consecutiveLosses = 4;

            test_dummy.updateBehaviour(['consecutivelosses_test_1a', 'consecutivelosses_test_1b', 'consecutivelosses_test_1c'], null);
            assert.equal(test_dummy.chosenState.rawDialogue, 'consecutivelosses_test_1a', 'checking non-interval condition (self mode)');

            test_dummy.resetState();
            test_dummy.consecutiveLosses = 4;

            test_dummy.updateBehaviour(['consecutivelosses_test_2a', 'consecutivelosses_test_2b', 'consecutivelosses_test_2c'], chihiro);
            assert.equal(test_dummy.chosenState.rawDialogue, 'consecutivelosses_test_2a', 'checking interval condition (self mode)');
        });
    });
});