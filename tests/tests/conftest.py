import pytest
import threading
import http.server
import socketserver
from pathlib import Path
from selenium import webdriver
import time


@pytest.fixture(scope="session")
def server_url(pytestconfig) -> str:
    serve_dir = str(Path(pytestconfig.getini("serve_dir")).expanduser().resolve())
    print("Serving from " + serve_dir)

    handler = http.server.SimpleHTTPRequestHandler
    handler.directory = serve_dir

    port = 8000
    ev = threading.Event()

    class Handler(http.server.SimpleHTTPRequestHandler):
        def __init__(self, *args, **kwargs):
            super().__init__(*args, directory=serve_dir, **kwargs)

        def log_message(self, format, *args):
            pass

    def server():
        for p in range(8000, 10000):
            try:
                with http.server.ThreadingHTTPServer(
                    ("127.0.0.1", p), Handler
                ) as httpd:
                    port = p
                    ev.set()
                    httpd.serve_forever()
                break
            except OSError:
                continue
        else:
            raise Exception("Could not find open port for HTTP server")

    server_thread = threading.Thread(target=server, daemon=True)
    server_thread.start()

    ev.wait()

    return "http://localhost:{}/index.html".format(port)


@pytest.fixture(scope="session")
def base_url(server_url):
    return server_url


@pytest.fixture
def driver(
    server_url: str, selenium: webdriver.remote.webdriver.WebDriver
) -> webdriver.remote.webdriver.WebDriver:
    selenium.get(server_url)
    selenium.execute_script("return window.initializeTestingMode();")

    return selenium


def pytest_addoption(parser):
    parser.addini(
        "serve_dir", "directory from which to serve files under test", default="./"
    )

    group = parser.getgroup("selenium", "selenium")
    group._addoption(
        "--headless",
        action="store_true",
        help="enable headless mode for supported browsers.",
    )


@pytest.fixture
def chrome_options(chrome_options, pytestconfig):
    if pytestconfig.getoption("headless"):
        chrome_options.add_argument("--headless")
    return chrome_options


@pytest.fixture
def firefox_options(firefox_options, pytestconfig):
    if pytestconfig.getoption("headless"):
        firefox_options.add_argument("-headless")
    return firefox_options
