var assert = chai.assert;

describe('fetchCompressedURL', function () {
    it('should retrieve a gz-compressed file from the host webserver', function (done) {
        fetchCompressedURL('opponents/test_dummy/retrieval-test.txt').then(function (data) {
            /* This test should be synced to the actual contents of retrieval-test.txt.gz . */
            assert.typeOf(data, 'string', 'checking type of result');
            assert.equal(data, 'compressed test string', 'checking result data');
            done();
        }).fail(function (err) {
            return done(err || true);
        });
    });

    it('should fall back to an uncompressed version if a compressed version is not found', function (done) {
        fetchCompressedURL('opponents/test_dummy/retrieval-test-2.txt').then(function (data) {
            assert.typeOf(data, 'string', 'checking type of result');
            assert.equal(data, 'other test data', 'checking result data');
            done();
        }).fail(function (err) {
            return done(err || true);
        });
    });

    it('should indicate an error if neither compressed nor uncompressed versions are found', function (done) {
        fetchCompressedURL('opponents/test_dummy/nonexistent.txt').then(function (data) {
            assert.fail('somehow found a nonexistent file');
        }).fail(function (err) {
            return done();
        });
    });
});


describe('Opponent (spniCore)', function () {
    describe('Constructor', function () {
        it('should populate basic opponent metadata from meta.xml', function () {
            var opp = new Opponent(
                'test_dummy', opponent_metadata['test_dummy'], undefined, 0
            );

            assert.isOk(opp.metaXml, 'checking opponent metaXml object');

            assert.equal(opp.id, 'test_dummy', 'checking opponent ID');
            assert.equal(opp.folder, 'opponents/test_dummy/', 'checking folder');
            assert.equal(opp.base_folder, 'opponents/test_dummy/', 'checking base_folder');
            assert.equal(opp.status, undefined, 'checking opponent status');

            /* Ensure the assertion values here are synced to data in
             * opponents/test_dummy/meta.xml.
             */

            // fun facts with regards to her names:
            // - her name can be written as A. Sert ('assert', geddit?)
            // - 'Alice' was chosen as (one of) the traditional name of stand-ins for people in computing
            // - 'Black' is her opponent ID from the spnatitest repo.
            assert.equal(opp.first, 'Alice', 'checking first name');
            assert.equal(opp.last, 'Sert', 'checking last name');
            assert.equal(opp.label, 'Black', 'checking label');

            assert.equal(opp.image, '0-calm.png', 'checking selection image');
            assert.equal(opp.gender, 'female', 'checking gender');
            assert.equal(opp.height, '???', 'checking height');
            assert.equal(opp.source, 'SPNATI Research Institute', 'checking source material');
            assert.equal(opp.artist, 'Artist', 'checking artist credit');
            assert.equal(opp.writer, 'Writer', 'checking writer credit');
            assert.equal(
                opp.description,
                fixupDialogue("This character is used to automatically test SPNATI. If you can see this character, please file a bug report."),
                "checking opponent description"
            );

            assert.equal(opp.layers, 5, 'checking layer count');
            assert.equal(opp.has_collectibles, true, 'checking has_collectibles')
            assert.equal(opp.ending, false, 'checking ending flag');
            assert.equal(opp.scale, 100.0, 'checking scale');
            assert.equal(opp.release, Number.POSITIVE_INFINITY, 'checking release number');
        });

        it("should initialize the opponent's tag list", function () {
            var opp = new Opponent(
                'test_dummy', opponent_metadata['test_dummy'], undefined, 0
            );

            assert.sameMembers(
                opp.baseTags,
                ['black_hair', 'medium_hair', 'dark_eyes', 'unusual_skin', 'virtual', 'robot', 'medium_breasts', 'kind'],
                'checking base tags list'
            );

            /* The active tags list should always include the opponent ID as a tag. */
            assert.sameMembers(
                opp.tags,
                ['test_dummy', 'black_hair', 'medium_hair', 'dark_eyes', 'unusual_skin', 'virtual', 'robot', 'medium_breasts', 'kind'],
                'checking active tags list'
            );
        });
    });

    describe('#loadBehaviour', function () {
        afterEach(cleanUpPlayers);

        it('should load extra metadata from behaviour.xml', function (done) {
            var opp = new Opponent(
                'test_dummy', opponent_metadata['test_dummy'], undefined, 0
            );

            opp.loadBehaviour(2, true, function () {
                try {
                    assert.isOk(opp.xml, 'checking opponent xml object');

                    assert.equal(opp.size, 'medium', 'checking opponent size');
                    assert.equal(opp.stamina, 25, 'checking opponent forfeit timer');
                    assert.isObject(opp.intelligence, 'checking opponent AI intelligence datatype');
                    assert.equal(opp.gender, 'female', 'checking opponent gender');

                    var intelligence = opp.intelligence.map(function () {
                        return $(this).text();
                    }).get();
                    assert.sameMembers(intelligence, ['average'], 'checking opponent AI intelligence');

                    return done();
                } catch (e) {
                    return done(e);
                }
            }, function () {
                return done(new Error('Error loading test_dummy behaviour.xml'));
            });
        });

        it('should load default costume info from behaviour.xml', function (done) {
            var opp = new Opponent(
                'test_dummy', opponent_metadata['test_dummy'], undefined, 0
            );

            opp.loadBehaviour(2, true, function () {
                try {
                    assert.isNull(opp.default_costume.id, 'checking opponent default costume ID');
                    assert.equal(opp.default_costume.folders, opp.folder, 'checking opponent default costume folder');

                    var labels = opp.default_costume.labels.map(function () {
                        return $(this).text();
                    }).get();
                    assert.sameMembers(labels, ['Black'], 'checking opponent default costume labels');

                    assert.isObject(opp.default_costume.poses, 'checking opponent pose map');

                    assert.sameDeepMembers(
                        opp.default_costume.tags,
                        [{
                                'tag': 'black_hair',
                                'from': undefined,
                                'to': undefined
                            },
                            {
                                'tag': 'medium_hair',
                                'from': undefined,
                                'to': undefined
                            },
                            {
                                'tag': 'dark_eyes',
                                'from': undefined,
                                'to': undefined
                            },
                            {
                                'tag': 'unusual_skin',
                                'from': undefined,
                                'to': undefined
                            },
                            {
                                'tag': 'virtual',
                                'from': undefined,
                                'to': undefined
                            },
                            {
                                'tag': 'robot',
                                'from': undefined,
                                'to': undefined
                            },
                            {
                                'tag': 'medium_breasts',
                                'from': undefined,
                                'to': undefined
                            },
                            {
                                'tag': 'kind',
                                'from': undefined,
                                'to': undefined
                            },
                            {
                                'tag': 'test_1',
                                'from': "4",
                                'to': undefined
                            },
                            {
                                'tag': 'test_2',
                                'from': "4",
                                'to': "5"
                            },
                        ],
                        'checking opponent default costume tags list'
                    );

                    return done();
                } catch (e) {
                    return done(e);
                }
            }, function () {
                return done(new Error('Error loading test_dummy behaviour.xml'));
            });
        });

        it('should populate the targetedLines count object', function (done) {
            var opp = new Opponent(
                'test_dummy', opponent_metadata['test_dummy'], undefined, 0
            );

            opp.loadBehaviour(2, true, function () {
                try {
                    assert.containsAllKeys(opp.targetedLines, ['target_linecount_test'], 'checking opponent targetedLines keys');
                    assert.equal(opp.targetedLines['target_linecount_test'].seen.size, 2, 'checking target line test count');

                    return done();
                } catch (e) {
                    return done(e);
                }
            }, function () {
                return done(new Error('Error loading test_dummy behaviour.xml'));
            });
        });

        it('should load collectibles if present', function (done) {
            var opp = new Opponent(
                'test_dummy', opponent_metadata['test_dummy'], undefined, 0
            );

            opp.loadBehaviour(2, true, function () {
                try {
                    assert.isArray(opp.collectibles, 'testing collectibles type');
                    assert.lengthOf(opp.collectibles, 2, 'test if two collectibles were loaded');

                    assert.instanceOf(opp.collectibles[0], Collectible, 'test collectible 1 type');
                    assert.instanceOf(opp.collectibles[1], Collectible, 'test collectible 2 type');

                    return done();
                } catch (e) {
                    return done(e);
                }
            }, function () {
                return done(new Error('Error loading test_dummy behaviour.xml'));
            });
        });
    });
});