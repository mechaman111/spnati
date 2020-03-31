# Tests

## How to run tests

Use `pipenv` to run `pytest` with the appropriate arguments from the `tests/` directory:

```bash
(note: cwd == repository tests/ directory)
pipenv run pytest [ARGS] tests/
```

Helpful command line parameters:
| Parameter            | Values                       | Description |
| -------------------- | ---------------------------- | ----------- |
| `--driver`           | `Firefox`, `Chrome`, etc.    | Selenium driver to use for testing. See the `pytest-selenium` docs for all supported values.
| `--headless`         | None                         | If passed, tests will run headless (with no GUI window, in the background)
| `--tests-per-worker` | integer or `auto`, default 1 | Number of concurrent tests (threads) per worker
| `--workers`          | integer or `auto`, default 1 | Number of parallel processes for running tests

Tests are more IO-bound than CPU-bound, so tweaking `--tests-per-worker` might help more than adjusting `--workers`.

See also the [pytest-selenium quickstart guide](https://pytest-selenium.readthedocs.io/en/latest/user_guide.html#quick-start)
and [pytest-parallel](https://github.com/browsertron/pytest-parallel) for more info.

## How to write tests

Tests are written using the [`pytest`](https://docs.pytest.org/en/latest/) and
[Selenium](https://selenium-python.readthedocs.io/) frameworks.

See also the official [Selenium API docs](https://www.selenium.dev/selenium/docs/api/py/api.html).

### The `driver` fixture

Using the `driver` fixture (defined in `conftest.py`) in a test case will
automatically take care of:
 - Starting a static HTTP server for testing
 - Starting browser and driver instances
 - Navigating to the testing HTTP server
 - Initializing "testing mode" (debug mode + forced epilogue/collectible unlocks) within SPNATI

 