"""Basic sanity tests for core game features.

These tests mostly check to see if game screens load at all, and if basic
UI components (buttons, input fields, etc.) are visible.
"""

import pytest
import selenium
from selenium import webdriver
from selenium.common import exceptions
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
import time


def wait_for_clickable(
    driver: webdriver.remote.webdriver.WebDriver, by: By, sel: str, timeout=10
) -> webdriver.remote.webelement.WebElement:
    """Wait for an element to become displayed and clickable."""
    return WebDriverWait(driver, timeout).until(EC.element_to_be_clickable((by, sel)))


def wait_for_visible(
    driver: webdriver.remote.webdriver.WebDriver, by: By, sel: str, timeout=10
) -> webdriver.remote.webelement.WebElement:
    """Wait for an element to become displayed."""
    return WebDriverWait(driver, timeout).until(
        EC.visibility_of_element_located((by, sel))
    )


def wait_for_hidden(
    driver: webdriver.remote.webdriver.WebDriver, by: By, sel: str, timeout=10
) -> webdriver.remote.webelement.WebElement:
    """Wait for an element to become hidden."""
    return WebDriverWait(driver, timeout).until(
        EC.invisibility_of_element_located((by, sel))
    )


def visible_element(
    driver: webdriver.remote.webdriver.WebDriver, by: By, sel: str
) -> webdriver.remote.webelement.WebElement:
    """Get an element and assert that it is visible."""
    elem = driver.find_element(by, sel)
    assert elem.is_displayed(), "Element {} not visible".format(sel)

    return elem


#
# Sanity tests for Warning / Title screen + attached modal dialogs
#


def test_warning_screen(driver: webdriver.remote.webdriver.WebDriver):
    start_button = wait_for_clickable(driver, By.ID, "warning-start-button")
    assert start_button.is_displayed(), "Warning screen enter button not displayed"


def test_title_screen(driver: webdriver.remote.webdriver.WebDriver):
    # Enter title screen
    wait_for_clickable(driver, By.ID, "warning-start-button").click()

    # Wait for warning screen to be displayed
    wait_for_clickable(driver, By.ID, "title-start-button")

    # Test for presence of name field
    visible_element(driver, By.ID, "player-name-field").send_keys("Selenium")

    # Look for male-specific options
    visible_element(driver, By.ID, "male-gender-button")
    visible_element(driver, By.ID, "small-junk-button")
    visible_element(driver, By.ID, "medium-junk-button")
    visible_element(driver, By.ID, "large-junk-button")

    for i in range(18):
        visible_element(driver, By.ID, "male-clothing-option-" + str(i))

    # Look for female-specific options
    visible_element(driver, By.ID, "female-gender-button").click()

    wait_for_clickable(driver, By.ID, "small-boobs-button")
    visible_element(driver, By.ID, "medium-boobs-button")
    visible_element(driver, By.ID, "large-boobs-button")

    for i in range(18):
        visible_element(driver, By.ID, "female-clothing-option-" + str(i))


def test_background_modal(driver: webdriver.remote.webdriver.WebDriver):
    # Go to background modal
    wait_for_clickable(
        driver, By.CSS_SELECTOR, "#title-screen .title-settings-button"
    ).click()

    # Wait for it to show up
    wait_for_visible(driver, By.ID, "game-settings-modal")

    # Look for "Close" button
    visible_element(
        driver, By.CSS_SELECTOR, '#game-settings-modal button[data-dismiss="modal"]'
    )

    # Look for at least one background option
    opt = visible_element(
        driver, By.CSS_SELECTOR, "#game-settings-modal .background-option"
    )

    opt.click()

    # Wait for element to become invisible
    WebDriverWait(driver, 5).until(EC.invisibility_of_element(opt))


def test_bug_report_modal(driver: webdriver.remote.webdriver.WebDriver):
    # Go to bug report modal
    wait_for_clickable(
        driver, By.CSS_SELECTOR, "#title-screen .title-bug-report-button"
    ).click()
    wait_for_visible(driver, By.ID, "bug-report-modal")

    # Test "Send" button
    send_btn = visible_element(driver, By.ID, "bug-report-modal-send-button")
    assert not send_btn.is_enabled(), "Bug report 'send' button incorrectly enabled"

    # Test "Close" button
    visible_element(driver, By.ID, "bug-report-modal-button").click()
    wait_for_hidden(driver, By.ID, "bug-report-modal")


def test_save_load_modal(driver: webdriver.remote.webdriver.WebDriver):
    # Go to save/load modal
    wait_for_clickable(
        driver, By.CSS_SELECTOR, "#title-screen .title-load-button"
    ).click()
    wait_for_visible(driver, By.ID, "io-modal")

    # Test Import button
    import_btn = visible_element(driver, By.ID, "import-progress")
    assert (
        not import_btn.is_enabled()
    ), "Save/Load modal 'import' button incorrectly enabled"

    # Test Close button
    visible_element(
        driver, By.CSS_SELECTOR, '#io-modal button[data-dismiss="modal"]'
    ).click()
    wait_for_hidden(driver, By.ID, "io-modal")


def test_help_modal(driver: webdriver.remote.webdriver.WebDriver):
    # Go to FAQ modal
    wait_for_clickable(
        driver, By.CSS_SELECTOR, "#title-screen .title-help-button"
    ).click()
    wait_for_visible(driver, By.ID, "help-modal")

    # Test Close button
    visible_element(
        driver, By.CSS_SELECTOR, '#help-modal button[data-dismiss="modal"]'
    ).click()
    wait_for_hidden(driver, By.ID, "help-modal")


def test_version_modal(driver: webdriver.remote.webdriver.WebDriver):
    # Try to open the version modal
    # For some reason, this is finicky.
    for _ in range(5):
        try:
            wait_for_clickable(driver, By.ID, "title-version-button").click()

            wait_for_visible(driver, By.ID, "version-modal")
            break
        except selenium.common.exceptions.TimeoutException:
            pass
    else:
        assert False, "Could not enter version modal"

    # Test Close button
    visible_element(
        driver, By.CSS_SELECTOR, '#version-modal button[data-dismiss="modal"]'
    ).click()
    wait_for_hidden(driver, By.ID, "version-modal")


def test_tags_modal(driver: webdriver.remote.webdriver.WebDriver):
    wait_for_clickable(driver, By.ID, "warning-start-button").click()

    # Go to tags modal from title screen
    wait_for_clickable(
        driver, By.CSS_SELECTOR, "#title-screen .title-tags-button"
    ).click()

    # Look for choice inputs
    wait_for_visible(driver, By.ID, "player-tag-choice-hair_color")
    wait_for_visible(driver, By.ID, "player-tag-choice-eye_color")
    wait_for_visible(driver, By.ID, "player-tag-choice-skin_color")
    wait_for_visible(driver, By.ID, "player-tag-choice-hair_length")
    wait_for_visible(driver, By.ID, "player-tag-choice-physical_build")
    wait_for_visible(driver, By.ID, "player-tag-choice-height")
    wait_for_visible(driver, By.ID, "player-tag-choice-pubic_hair_style")
    wait_for_visible(driver, By.ID, "player-tag-choice-circumcision")
    wait_for_visible(driver, By.ID, "player-tag-choice-sexual_orientation")

    # Look for Close button
    visible_element(driver, By.CSS_SELECTOR, "#player-tags-modal .clearSelections")

    # Test Confirm button
    visible_element(driver, By.ID, "player-tags-confirm").click()
    wait_for_hidden(driver, By.ID, "player-tags-modal")


#
# Sanity tests for Gallery
#


def test_gallery_collectibles(driver: webdriver.remote.webdriver.WebDriver):
    wait_for_clickable(driver, By.ID, "warning-start-button").click()

    # Go to gallery from title screen
    wait_for_clickable(
        driver, By.CSS_SELECTOR, "#title-screen .title-gallery-button"
    ).click()

    # Look for some clickable item
    wait_for_visible(driver, By.ID, "collectible-gallery-screen")
    elem = wait_for_clickable(
        driver, By.CSS_SELECTOR, "#gallery-screen .collectibles-list-item"
    )

    elem.click()
    wait_for_visible(driver, By.ID, "collectibles-text-pane")
    wait_for_visible(driver, By.ID, "collectibles-image-pane")


def test_gallery_epilogues(driver: webdriver.remote.webdriver.WebDriver):
    wait_for_clickable(driver, By.ID, "warning-start-button").click()

    # Go to gallery from title screen
    wait_for_clickable(
        driver, By.CSS_SELECTOR, "#title-screen .title-gallery-button"
    ).click()

    # Go to Epilogues view
    wait_for_clickable(
        driver, By.CSS_SELECTOR, "#collectible-gallery-screen .gallery-switch-button"
    ).click()

    # Look for some clickable item
    wait_for_visible(driver, By.ID, "epilogue-gallery-screen")
    elem = wait_for_clickable(
        driver, By.CSS_SELECTOR, "#gallery-screen .gallery-ending"
    )

    elem.click()
    wait_for_visible(driver, By.ID, "gallery-selected-ending-block")


#
# Sanity tests for selection screens
#


def go_to_selection_screen(driver: webdriver.remote.webdriver.WebDriver):
    wait_for_clickable(driver, By.ID, "warning-start-button").click()
    visible_element(driver, By.ID, "player-name-field").send_keys("Selenium")
    wait_for_clickable(driver, By.ID, "title-start-button").click()

    try:
        wait_for_clickable(driver, By.ID, "usage-reporting-deny", timeout=7).click()
        wait_for_hidden(driver, By.ID, "usage-reporting-modal")
    except exceptions.TimeoutException:
        pass


def test_main_select(driver: webdriver.remote.webdriver.WebDriver):
    go_to_selection_screen(driver)

    # Check for main opponent selection buttons
    for i in range(1, 5):
        wait_for_clickable(driver, By.ID, "select-slot-button-" + str(i))

    # Test UI hide button
    visible_element(
        driver, By.CSS_SELECTOR, "#main-select-screen .hide-table-button"
    ).click()
    wait_for_hidden(driver, By.ID, "select-slot-button-1")
    visible_element(
        driver, By.CSS_SELECTOR, "#main-select-screen .hide-table-button"
    ).click()

    for i in range(1, 5):
        visible_element(driver, By.ID, "select-slot-button-" + str(i))

    # Look for the rest of the UI elements on the screen:
    visible_element(driver, By.ID, "select-main-back-button")
    visible_element(driver, By.ID, "select-group-testing-button")
    visible_element(driver, By.ID, "select-group-button")
    visible_element(driver, By.ID, "select-help-button")

    visible_element(driver, By.ID, "select-random-female-button")
    visible_element(driver, By.ID, "select-random-male-button")
    visible_element(driver, By.ID, "select-random-group-button")
    visible_element(driver, By.ID, "select-random-button")

    visible_element(
        driver, By.CSS_SELECTOR, "#main-select-screen .table-bug-report-button"
    )

    remove_all = visible_element(driver, By.ID, "select-remove-all-button")
    assert (
        not remove_all.is_enabled()
    ), "Select screen 'Remove All' button incorrectly enabled"

    start_button = visible_element(driver, By.ID, "main-select-button")
    assert (
        not start_button.is_enabled()
    ), "Select screen 'Start' button incorrectly enabled"


def test_group_select(driver: webdriver.remote.webdriver.WebDriver):
    go_to_selection_screen(driver)
    wait_for_clickable(driver, By.ID, "select-group-testing-button").click()
    wait_for_visible(driver, By.ID, "group-select-screen")

    # Main "select group" button:
    visible_element(driver, By.ID, "group-button")

    # Look for side buttons:
    visible_element(driver, By.ID, "group-switch-testing-button")
    visible_element(driver, By.ID, "group-search-button")
    visible_element(driver, By.ID, "group-basic-info-button")
    visible_element(driver, By.ID, "group-credits-button")
    visible_element(driver, By.ID, "group-more-info-button")

    # Page controls:
    visible_element(driver, By.CSS_SELECTOR, "#group-select-screen .first-page-button")
    visible_element(driver, By.CSS_SELECTOR, "#group-select-screen .left-page-button")
    visible_element(driver, By.CSS_SELECTOR, "#group-select-screen .go-page-button")
    visible_element(driver, By.CSS_SELECTOR, "#group-select-screen .right-page-button")
    visible_element(driver, By.CSS_SELECTOR, "#group-select-screen .last-page-button")
    visible_element(driver, By.ID, "group-page-indicator")

    # Misc controls:
    visible_element(
        driver, By.CSS_SELECTOR, "#group-select-screen .table-bug-report-button"
    )

    visible_element(driver, By.ID, "group-hide-button")


def test_individual_select(driver: webdriver.remote.webdriver.WebDriver):
    go_to_selection_screen(driver)
    wait_for_clickable(driver, By.ID, "select-slot-button-2").click()
    wait_for_visible(driver, By.ID, "individual-select-screen")

    # Look for top controls:
    visible_element(
        driver, By.CSS_SELECTOR, "#individual-select-screen .select-back-btn"
    )
    visible_element(driver, By.ID, "search-gender")
    visible_element(driver, By.ID, "sort-dropdown")
    visible_element(driver, By.ID, "search-creator")
    visible_element(driver, By.ID, "search-name")
    visible_element(driver, By.ID, "search-source")
    visible_element(driver, By.ID, "search-tag")

    # "Select Opponent" button:
    visible_element(driver, By.CSS_SELECTOR, "#individual-select-screen .select-button")

    # Opponent cards:
    opponents = driver.find_elements(
        By.CSS_SELECTOR, "#individual-select-screen .selection-card"
    )

    # Find and click on an opponent:
    for opp in opponents:
        if opp.is_displayed():
            opp.click()
            break
    else:
        assert False, "Could not find opponents on select screen"

    # Look for details:
    visible_element(
        driver, By.CSS_SELECTOR, "#individual-select-screen .opponent-details-panel"
    )

    visible_element(
        driver,
        By.CSS_SELECTOR,
        "#individual-select-screen .opponent-details-image-area",
    )


#
# Sanity tests for game and epilogue screens
#


def test_enter_game(driver: webdriver.remote.webdriver.WebDriver):
    go_to_selection_screen(driver)

    # Fill all slots with opponents:
    for i in range(1, 5):
        wait_for_clickable(driver, By.ID, "select-slot-button-" + str(i)).click()
        wait_for_visible(driver, By.ID, "individual-select-screen")

        opponents = driver.find_elements(
            By.CSS_SELECTOR, "#individual-select-screen .selection-card"
        )

        # Find and click on an opponent:
        for opp in opponents:
            if opp.is_displayed():
                opp.click()
                break
        else:
            assert False, "Could not find opponents on select screen"

        # Select this opponent:
        visible_element(
            driver, By.CSS_SELECTOR, "#individual-select-screen .select-button"
        ).click()
        wait_for_visible(driver, By.ID, "main-select-screen")

        # Use opponent dialogue bubble visibility as a proxy for determining
        # when an opponent loads:
        wait_for_visible(driver, By.ID, "select-bubble-" + str(i), timeout=30)

    # Start the game.
    wait_for_clickable(driver, By.ID, "main-select-button").click()
    wait_for_visible(driver, By.ID, "game-screen")

    # Look for game UI elements:

    # Main button and name area:
    main_btn = visible_element(driver, By.ID, "main-game-button")
    assert main_btn.is_enabled(), "Main button is not enabled"

    visible_element(driver, By.ID, "player-name-label-minimal")

    # Player clothing:
    for i in range(1, 9):
        visible_element(
            driver,
            By.CSS_SELECTOR,
            "#player-game-clothing-area-minimal .player-0-clothing-" + str(i),
        )

    # Hide table and bug report buttons:
    visible_element(driver, By.CSS_SELECTOR, "#game-screen .hide-table-button")
    visible_element(driver, By.CSS_SELECTOR, "#game-screen .table-bug-report-button")

    # Game menus:
    visible_element(driver, By.CSS_SELECTOR, "#game-screen .game-menu-dropup").click()
    visible_element(driver, By.ID, "game-fullscreen-button")
    visible_element(driver, By.ID, "game-settings-button")
    visible_element(driver, By.ID, "game-home-button")
    visible_element(driver, By.ID, "game-log-button")
    visible_element(driver, By.ID, "game-feedback-button")
    visible_element(driver, By.ID, "game-faq-button")


def test_enter_epilogue(driver: webdriver.remote.webdriver.WebDriver):
    wait_for_clickable(driver, By.ID, "warning-start-button").click()

    # Go to gallery from title screen
    wait_for_clickable(
        driver, By.CSS_SELECTOR, "#title-screen .title-gallery-button"
    ).click()

    # Go to Epilogues view:
    wait_for_clickable(
        driver, By.CSS_SELECTOR, "#collectible-gallery-screen .gallery-switch-button"
    ).click()

    # Select an epilogue:
    wait_for_visible(driver, By.ID, "epilogue-gallery-screen")
    elem = wait_for_clickable(
        driver, By.CSS_SELECTOR, "#gallery-screen .gallery-ending"
    )

    elem.click()
    visible_element(driver, By.ID, "gallery-start-ending-button").click()

    # Wait for epilogue to load:
    wait_for_visible(driver, By.ID, "epilogue-screen", timeout=30)

    # Look for control UI elements:
    next_btn = wait_for_clickable(driver, By.ID, "epilogue-next", timeout=30)
    assert next_btn.is_enabled(), "Next button is not enabled"

    prev_btn = visible_element(driver, By.ID, "epilogue-previous")
    assert not prev_btn.is_enabled(), "Previous button is incorrectly enabled"

    visible_element(driver, By.ID, "epilogue-fullscreen-button")
