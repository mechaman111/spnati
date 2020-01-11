var assert = chai.assert;


describe('MarkerOperation (spniBehaviour)', function () {
    describe('Constructor', function () {
        it('should derive perTarget from the marker name if not explicitly given', function () {
            var a = new MarkerOperation('foo', '=', 5);
            assert.isFalse(a.perTarget, 'testing non-targeted marker');

            var b = new MarkerOperation('bar*', '+', 10);
            assert.isTrue(b.perTarget, 'testing targeted marker');

            var c = new MarkerOperation('foo*', '-', 5, false, false);
            assert.isFalse(c.perTarget, 'testing non-targeted marker w/ explicit perTarget value');

            var d = new MarkerOperation('bar', '*', 10, false, true);
            assert.isTrue(d.perTarget, 'testing targeted marker w/ explicit perTarget value');
        });

        it('should only allow arithmetic ops and \'=\' in the op field', function () {
            var a = new MarkerOperation('foo', 'z', 5);
            assert.strictEqual(a.op, '=', 'testing invalid op field');

            var b = new MarkerOperation('bar', '=', 3);
            assert.strictEqual(b.op, '=', "testing op field = \'=\'");

            MARKER_ARITHMETIC_OPS.forEach(function (i) {
                var x = new MarkerOperation('bar', i, 3);
                assert.strictEqual(x.op, i, "testing op field = \'" + i + "\'");
            });
        });

        it('should determine if an arithmetic op is being performed', function () {
            var a = new MarkerOperation('foo', 'z', 5);
            assert.isFalse(a.arithmetic, 'testing invalid op field');

            var b = new MarkerOperation('bar', '=', 3);
            assert.isFalse(b.arithmetic, "testing op field = \'=\'");

            MARKER_ARITHMETIC_OPS.forEach(function (i) {
                var x = new MarkerOperation('bar', i, 3);
                assert.isTrue(x.arithmetic, "testing op field = \'" + i + "\'");
            });
        });
    });

    describe('parseMarkerOperation', function () {
        it("should be able to parse the traditional 'set marker to 1' syntax", function () {
            var foo = parseMarkerOperation("foo");

            assert.strictEqual(foo.name, "foo", "foo's name is foo");
            assert.strictEqual(foo.op, "=", "foo should be directly assigned");
            assert.strictEqual(foo.value, 1, "foo's value should be 1");
            assert.isFalse(foo.perTarget, "foo should be nontargeted");

            var bar = parseMarkerOperation("bar*");

            assert.strictEqual(bar.name, "bar", "bar's name is bar");
            assert.strictEqual(bar.op, "=", "bar should be directly assigned");
            assert.strictEqual(bar.value, 1, "bar's value should be 1");
            assert.isTrue(bar.perTarget, "bar should be targeted");
        });

        it("should be able to parse increment/decrement-by-1 syntax", function () {
            var inc_nontarget = parseMarkerOperation("+foo");
            assert.strictEqual(inc_nontarget.name, "foo", "testing nontargeted increment name");
            assert.strictEqual(inc_nontarget.op, "+", "testing nontargeted increment op");
            assert.strictEqual(inc_nontarget.value, 1, "testing nontargeted increment value");
            assert.isFalse(inc_nontarget.perTarget, "testing nontargeted increment targeting flag");

            var inc_target = parseMarkerOperation("+bar*");
            assert.strictEqual(inc_target.name, "bar", "testing targeted increment name");
            assert.strictEqual(inc_target.op, "+", "testing targeted increment op");
            assert.strictEqual(inc_target.value, 1, "testing targeted increment value");
            assert.isTrue(inc_target.perTarget, "testing targeted increment targeting flag");

            var dec_nontarget = parseMarkerOperation("-baz");
            assert.strictEqual(dec_nontarget.name, "baz", "testing nontargeted decrement name");
            assert.strictEqual(dec_nontarget.op, "-", "testing nontargeted decrement op");
            assert.strictEqual(dec_nontarget.value, 1, "testing nontargeted decrement value");
            assert.isFalse(dec_nontarget.perTarget, "testing nontargeted decrement targeting flag");

            var dec_target = parseMarkerOperation("-qux*");
            assert.strictEqual(dec_target.name, "qux", "testing targeted decrement name");
            assert.strictEqual(dec_target.op, "-", "testing targeted decrement op");
            assert.strictEqual(dec_target.value, 1, "testing targeted decrement value");
            assert.isTrue(dec_target.perTarget, "testing targeted decrement targeting flag");
        });

        it("should be able to parse basic assignment operations", function () {
            var set_nontarget = parseMarkerOperation("foo = 10");
            assert.strictEqual(set_nontarget.name, "foo", "testing nontargeted assignment name");
            assert.strictEqual(set_nontarget.op, "=", "testing nontargeted assignment op");
            assert.strictEqual(set_nontarget.value, '10', "testing nontargeted assignment value");
            assert.isFalse(set_nontarget.perTarget, "testing nontargeted assignment targeting flag");

            var set_target = parseMarkerOperation("bar* = 10");
            assert.strictEqual(set_target.name, "bar", "testing targetedassignmentt name");
            assert.strictEqual(set_target.op, "=", "testing targetedassignmentt op");
            assert.strictEqual(set_target.value, '10', "testing targeted assignment value");
            assert.isTrue(set_target.perTarget, "testing targeted assignment targeting flag");

            MARKER_ARITHMETIC_OPS.forEach(function (op) {
                var nontarget = parseMarkerOperation("foo " + op + "= 10");
                assert.strictEqual(nontarget.name, "foo", "testing nontargeted \'" + op + "\' name");
                assert.strictEqual(nontarget.op, op, "testing nontargeted \'" + op + "\' op value");
                assert.strictEqual(nontarget.value, '10', "testing nontargeted \'" + op + "\' value");
                assert.isFalse(nontarget.perTarget, "testing nontargeted \'" + op + "\' targeting flag");

                var target = parseMarkerOperation("bar* " + op + "= 10");
                assert.strictEqual(target.name, "bar", "testing nontargeted \'" + op + "\' name");
                assert.strictEqual(target.op, op, "testing nontargeted \'" + op + "\' op value");
                assert.strictEqual(target.value, '10', "testing nontargeted \'" + op + "\' value");
                assert.isTrue(target.perTarget, "testing nontargeted \'" + op + "\' targeting flag");
            });
        });

        it("should assume that inputs not following operation syntax are whole marker names", function () {
            var bogusA = parseMarkerOperation("foo-bar");
            assert.strictEqual(bogusA.name, "foo-bar", "testing bogusA name");
            assert.strictEqual(bogusA.op, "=", "testing bogusA op");
            assert.strictEqual(bogusA.value, 1, "testing bogusA value");
            assert.isFalse(bogusA.perTarget, "testing bogusA targeting flag");

            var bogusB = parseMarkerOperation("+ foo");
            assert.strictEqual(bogusB.name, "+ foo", "testing bogusB name");
            assert.strictEqual(bogusB.op, "=", "testing bogusB op");
            assert.strictEqual(bogusB.value, 1, "testing bogusB value");
            assert.isFalse(bogusB.perTarget, "testing bogusB targeting flag");
        });
    });

    describe('#getCurrentValue', function () {
        it("should get the current value of the referenced marker without changing anything", function () {
            var test_dummy = getOpponent('test_dummy', 1);
            var chihiro = getOpponent('chihiro', 2);

            let markerOpA = new MarkerOperation('A', '=', 'bar');
            let markerOpB = new MarkerOperation('B*', '=', 'baz');

            test_dummy.markers['A'] = 10;
            test_dummy.markers['__chihiro_B'] = 20;

            assert.strictEqual(markerOpA.getCurrentValue(test_dummy, chihiro), 10, 'testing non-targeted');
            assert.strictEqual(markerOpB.getCurrentValue(test_dummy, chihiro), 20, 'testing targeted');

            assert.strictEqual(test_dummy.markers['A'], 10, "checking for side-effects");
            assert.strictEqual(test_dummy.markers['__chihiro_B'], 20, "checking for side-effects");
        });
    });

    describe('#evaluate', function () {
        describe("Assignment:", function () {
            it("should return numeric values directly", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                let marker = new MarkerOperation('A', '=', 1);

                assert.strictEqual(marker.evaluate(test_dummy), 1);
            });

            it("should cast non-numeric and non-string types to 0 or 1 depending on truthiness", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                let marker0 = new MarkerOperation('A', '=', undefined);
                let marker1 = new MarkerOperation('A', '=', ['a', 'b', 'c']);

                assert.strictEqual(marker0.evaluate(test_dummy), 0, "testing false-y value");
                assert.strictEqual(marker1.evaluate(test_dummy), 1, "testing truthy value");
            });

            it("should return non-variable, non-numeric string values as-is", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                let marker = new MarkerOperation('A', '=', 'i am a string');

                assert.strictEqual(marker.evaluate(test_dummy), "i am a string");
            });

            it("should expand variables", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                var chihiro = getOpponent('chihiro', 2);
                let marker = new MarkerOperation('A', '=', '~name~');

                assert.strictEqual(marker.evaluate(test_dummy, chihiro), "Chihiro");
            });

            it("should parse numeric string results", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                var chihiro = getOpponent('chihiro', 2);

                let markerA = new MarkerOperation('A', '=', '50');
                assert.strictEqual(markerA.evaluate(test_dummy), 50, "testing direct numeric string assignment");

                let markerB = new MarkerOperation('B', '=', '~chihiro.slot~');
                assert.strictEqual(markerB.evaluate(test_dummy, chihiro), 2, "testing numeric string assignment from variable");
            });
        });

        describe("Arithmetic:", function () {
            it('should attempt to convert both operands to numbers', function () {
                var test_dummy = getOpponent('test_dummy', 1);

                test_dummy.markers['A'] = '5';

                let markerA = new MarkerOperation('A', '+', '3');
                assert.strictEqual(markerA.evaluate(test_dummy), 8, "testing direct numeric string operands");
            });

            it("should attempt to expand variables in string operands", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                var chihiro = getOpponent('chihiro', 2);

                test_dummy.markers['A'] = 10;

                let marker = new MarkerOperation('A', '+', '~chihiro.slot~');
                assert.strictEqual(marker.evaluate(test_dummy, chihiro), 12, "testing numeric string operands from variable");
            });

            it("should round to the nearest integer when dividing", function () {
                var test_dummy = getOpponent('test_dummy', 1);

                test_dummy.markers['A'] = 1;
                test_dummy.markers['B'] = 2;

                // (1/3) => 0
                let markerA = new MarkerOperation('A', '/', 3);
                assert.strictEqual(markerA.evaluate(test_dummy), 0, "testing rounding down");
                assert(Number.isInteger(markerA.evaluate(test_dummy)), "testing rounding down");

                // (2/3) => 1
                let markerB = new MarkerOperation('B', '/', 3);
                assert.strictEqual(markerB.evaluate(test_dummy), 1, "testing rounding up");
                assert(Number.isInteger(markerB.evaluate(test_dummy)), "testing rounding up");
            });

            it("should return zero when attempting to divide by zero", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                test_dummy.markers['A'] = 10;

                let markerA = new MarkerOperation('A', '/', 0);
                assert.strictEqual(markerA.evaluate(test_dummy), 0, "testing divide by zero");

                let markerA2 = new MarkerOperation('A', '%', 0);
                assert.strictEqual(markerA2.evaluate(test_dummy), 0, "testing modulus of zero");
            });

            it("should assume missing operands (such as unset markers) are zero", function () {
                var test_dummy = getOpponent('test_dummy', 1);
                test_dummy.markers['A'] = 10;

                let markerA = new MarkerOperation('A', '+', undefined);
                assert.strictEqual(markerA.evaluate(test_dummy), 10, "testing access of undefined operand");

                let markerB = new MarkerOperation('unknown', '+', 25);
                assert.strictEqual(markerB.evaluate(test_dummy), 25, "testing access to unset marker");
            });
        });
    });

    describe('#apply', function() {
        it('should evaluate and apply the marker operation', function() {
            var test_dummy = getOpponent('test_dummy', 1);
            var hk416 = getOpponent('hk416', 2);

            test_dummy.markers['B'] = 2;
            test_dummy.markers['__hk416_C'] = 5;
            test_dummy.markers['__hk416_D'] = 6;

            let markerA = new MarkerOperation('A', '=', 'foo');
            let markerB = new MarkerOperation('B', '+', '3');
            let markerC = new MarkerOperation('C*', '=', 'bar');
            let markerD = new MarkerOperation('D*', '+', '14');

            markerA.apply(test_dummy, hk416);
            markerB.apply(test_dummy, hk416);
            markerC.apply(test_dummy, hk416);
            markerD.apply(test_dummy, hk416);

            assert.strictEqual(test_dummy.markers['A'], 'foo', "testing marker A");
            assert.strictEqual(test_dummy.markers['B'], 5, "testing marker B");
            assert.strictEqual(test_dummy.markers['__hk416_C'], 'bar', "testing marker C*");
            assert.strictEqual(test_dummy.markers['__hk416_D'], 20, "testing marker D*");
        })
    });

    describe('#serialize', function() {
        it('should dump the marker operation as a plain object', function() {
            let markerA = new MarkerOperation('A', '=', 'foo');
            let markerB = new MarkerOperation('B*', '+', 3);

            markerA = markerA.serialize();
            markerB = markerB.serialize();

            assert.deepEqual(markerA, {
                name: 'A',
                op: '=',
                value: 'foo',
                perTarget: false,
                persistent: false
            }, 'testing marker A');

            assert.deepEqual(markerB, {
                name: 'B',
                op: '+',
                value: 3,
                perTarget: true,
                persistent: false
            }, 'testing marker B');
        })
    });
});