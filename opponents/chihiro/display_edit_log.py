import json
import sys


def display_case_conditions(case_obj):
    for k, v in case_obj.items():
        if k == "counters":
            for counter in v:
                print(
                    "        Counter: "
                    + ", ".join("{}={}".format(k, v) for k, v in o.items())
                )
        elif k == "tests":
            for test in v:
                print(
                    "        Test: {} {} {}".format(
                        test["expr"], test["cmp"], test["value"]
                    )
                )
        else:
            print("        {}: {}".format(k, v))


def display_state(state):
    ret = '{} | "{}"'.format(state["image"], state["text"])

    if "marker" in state:
        ret += " | Marker: {}={}".format(
            state["marker"]["name"], state["marker"]["value"]
        )

        if state["marker"]["perTarget"]:
            ret += "(Per-Target)"

    return ret


def display_target_info(target):
    print("    ID: " + target["id"])
    print("    Stage: " + str(target["stage"]))
    print("    Current Case:")
    display_case_conditions(target["case"])
    print("    Current State: " + display_state(target["state"]))


def display_new_dialogue_info(entry):
    print("Intent: " + entry["intent"])
    print("New Dialogue: " + display_state(entry["state"]))
    print("Speaker Stage: " + str(entry["stage"]))
    print("Suggested Generic Tag: " + str(entry["suggestedTag"]))

    hasResponseTarget = False
    if "responseTarget" in entry and entry["responseTarget"] is not None:
        hasResponseTarget = True
        print("Response Target Info:")
        display_target_info(entry["responseTarget"])

    if "phaseTarget" in entry and entry["phaseTarget"] is not None:
        if (
            hasResponseTarget
            and entry["phaseTarget"]["id"] == entry["responseTarget"]["id"]
        ):
            return

        print("Phase Target Info:")
        display_target_info(entry["phaseTarget"])


def display_edited_dialogue_info(entry):
    print("Speaker Stage: " + str(entry["stage"]))
    print("Edited Case:")
    display_case_conditions(entry["case"])
    print("Old Dialogue: " + display_state(entry["oldState"]))
    print("New Dialogue: " + display_state(entry["state"]))


def main():
    with open(sys.argv[1], "r") as f:
        log = json.load(f)

    for idx, entry in enumerate(log):
        if entry["type"] == "new":
            print("\n== Entry {:02d}: New Dialogue ==".format(idx))
            display_new_dialogue_info(entry)
        elif entry["type"] == "edit":
            print("\n== Entry {:02d}: Edited Dialogue ==".format(idx))
            display_new_dialogue_info(entry)


if __name__ == "__main__":
    main()
