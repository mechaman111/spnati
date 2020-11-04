
var MAIN_CATEGORIES = ['online', 'testing', 'offline']

function cell_with_text(text, classes) {
    var cell = document.createElement('td');
    cell.innerText = text;

    if (classes) {
        $(cell).addClass(classes);
    }

    return cell;
}

function capitalize(str) {
    return str[0].toUpperCase() + str.substring(1);
}

function create_bundle_entry (manifest) {
    var tr = document.createElement('tr');

    if (MAIN_CATEGORIES.indexOf(manifest.category) >= 0 || manifest.category === 'incomplete' || manifest.category === 'event' || manifest.category === 'duplicate') {
        var title = capitalize(manifest.category) + ' Opponents #' + manifest.index;

        var includes_opponents = manifest.folders.reduce(function (acc, val) {
            var opp = val.replace(/opponents[\\\/]/gi, '');

            if (acc.length > 0) acc += ', ';
            acc += capitalize(opp);

            return acc;
        }, '');

        if (manifest.category === 'incomplete') {
            var desc = 'Includes the following extra opponents: '+includes_opponents;
        } else if (manifest.category === 'event') {
            var desc = 'Includes the following event-only opponents: '+includes_opponents;
        } else if (manifest.category === 'duplicate') {
            var desc = 'Includes outdated versions of the following opponents: '+includes_opponents;
        } else {
            var desc = includes_opponents;
        }
    } else {
        var title = capitalize(manifest.category) + ' #' + manifest.index;
        var desc = manifest.description;
    }
    
    var size = Math.round(manifest.size / 1048576); // in MiB

    var download = $('<th scope=\"row\"><a class=\"btn btn-primary btn-sm\" href=\"dl/'+manifest.name+'\" download>Download</a></th>');
    $(tr).append(download);
    $(tr).append(cell_with_text(title, 'cell-nowrap'));
    $(tr).append(cell_with_text(size + ' MiB', 'cell-nowrap'));
    $(tr).append(cell_with_text(desc))

    return tr;
}

function populate_bundles() {
    return fetch('dl/manifest.json').then(function (res) {
        return res.json();
    }).then(function (data) {
        for (var i = 0; i < data.length; i++) {
            if (MAIN_CATEGORIES.indexOf(data[i].category) >= 0) {
                $('.bundle-table').append(create_bundle_entry(data[i]));
            } else {
                $('.extras-table').append(create_bundle_entry(data[i]));
            }
        }
    });
}

$(populate_bundles);