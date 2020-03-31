import pytest
import threading
import http.server
import socketserver
from urllib.parse import urlparse
from pathlib import Path
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
import time


@pytest.fixture(scope="session")
def server_url(pytestconfig) -> str:
    serv = pytestconfig.getoption("testing_server", None)
    try:
        serv = urlparse(serv, scheme="http")
        if serv.hostname is not None:
            scheme = serv.scheme
            hostname = serv.hostname
            if serv.port is not None:
                port = serv.port
            else:
                port = 80

            return "{}://{}:{}/index.html".format(scheme, hostname, port)
    except (TypeError, ValueError):
        pass

    serve_path = pytestconfig.getoption("serve_dir", Path("../")).expanduser().resolve()
    serve_dir = str(serve_path)
    print("Serving from " + serve_dir)

    handler = http.server.SimpleHTTPRequestHandler
    handler.directory = serve_dir

    port = 9001
    ev = threading.Event()

    class Handler(http.server.SimpleHTTPRequestHandler):
        def __init__(self, *args, **kwargs):
            super().__init__(*args, directory=serve_dir, **kwargs)

        def log_message(self, format, *args):
            pass

    def server():
        for p in range(9001, 10000):
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
    WebDriverWait(selenium, 30).until(
        EC.visibility_of_element_located((By.ID, "title-screen"))
    )
    selenium.execute_script("return window.initializeTestingMode();")

    return selenium


def pytest_addoption(parser):
    group = parser.getgroup("selenium", "selenium")
    group._addoption(
        "--headless",
        action="store_true",
        help="enable headless mode for supported browsers.",
    )

    server_group = parser.getgroup("server")
    server_group.addoption(
        "--testing-server",
        help="URL to server hosting testing files (internal server used if not provided)",
    )

    server_group.addoption(
        "--serve-dir",
        default=Path("../"),
        type=Path,
        help="directory to serve files from for internal testing server",
    )


@pytest.fixture
def chrome_options(chrome_options, pytestconfig):
    if pytestconfig.getoption("headless"):
        chrome_options.add_argument("--headless")

    # Try to prevent "unable to receive message from renderer" issue:
    chrome_options.add_argument("--disable-gpu")
    chrome_options.add_argument("--no-sandbox")

    return chrome_options


@pytest.fixture
def driver_args():
    return ["--log-level=ALL"]


@pytest.fixture
def firefox_options(firefox_options, pytestconfig):
    if pytestconfig.getoption("headless"):
        firefox_options.add_argument("-headless")

    firefox_options.log.level = "trace"

    return firefox_options
