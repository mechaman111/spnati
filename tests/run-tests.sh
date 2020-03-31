#!/usr/bin/env bash

# Options:
SERVER_PORT=9000
SERVER_BIND=127.0.0.1
SERVER_URL="http://$SERVER_BIND:$SERVER_PORT/"

# Start up one server used by all tests:
pipenv run python3 -m http.server $SERVER_PORT --bind $SERVER_BIND --directory ../ > /dev/null 2>&1 &
SERVER_PID=$!
OVERALL_STATUS=0

# Make sure we clean up the server when we're done
trap "RV=\$?; kill $SERVER_PID; exit \$RV" EXIT

# Run tests.
pipenv run pytest -s --testing-server $SERVER_URL --headless --driver Firefox tests/
TEST_STATUS=$?
if [[ $TEST_STATUS -ne 0 ]]; then
    echo "Firefox tests failed" >&2
    OVERALL_STATUS=$TEST_STATUS
fi

pipenv run pytest -s --testing-server $SERVER_URL --headless --driver Chrome tests/
TEST_STATUS=$?
if [[ $TEST_STATUS -ne 0 ]]; then
    echo "Chrome tests failed" >&2
    OVERALL_STATUS=$TEST_STATUS
fi

exit $OVERALL_STATUS
