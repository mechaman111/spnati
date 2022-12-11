var MAIN_CATEGORIES = ['online', 'testing', 'offline']

var SPECIAL_CASE_NAMES = {
    // formatting
    "9s": "9S",
    "ae86": "AE86",
    "alice_mgq": "Alice (MGQ)",
    "d.va": "D.Va",
    "frisk_and_friends": "Frisk",
    "hatsune_miku": "Miku",
    "hk416": "HK416",
    "kool-aid": "Kool-Aid Man",
    "larachel": "L'Arachel",
    "miki_miura": "Miki",
    "mom_alola": "Mom (Alola)",
    "ms.fortune": "Ms. Fortune",
    "nami_szs": "Nami (SZS)",
    "pa-15": "PA-15",
    "pauling": "Miss Pauling",
    "penny_polendina": "Penny (RWBY)",
    "pot_of_greed": "Pot of Greed",
    "ringo_ando": "Ringo",
    "rogue_x-men": "Rogue (X-Men)",
    "saki_zls": "Saki (ZLS)",
    "sandy_cheeks": "Sandy",
    "sannytess": "Sanny & Tess",
    "scm": "Suction Cup Man",
    "uravity": "Ochako", // nmasp, wtf?
    "wiifitfemale": "Wii Fit Trainer",
    "yshtola": "Y'shtola",
    "yuna_ffx": "Yuna (FFX)",
    "yuno_uno": "Yuno",
        
    // classic opponents
    "amy_rose_classic": "Amy Rose",
    "dawn_classic": "Dawn",
    "kyoko": "Kyoko Kirigiri",
    "penny_classic": "Penny",
    "ryuko_classic": "Ryuko",
    "shantae_classic": "Shantae",
    "sheena_classic": "Sheena",
    "velma_classic": "Velma",
    
    // newer opponents
    "asuna_yuuki": "Asuna",
    "blake_belladonna": "Blake",
    "cammy_white": "Cammy",
    "chara_dreemurr": "Chara",
    "chell_wheatley": "Chell",
    "hermione_granger": "Hermione",
    "mettaton_neo": "Mettaton",
    "misty_hgss": "Misty",
    "rarity_eg": "Rarity",
    "samus_aran": "Samus",
    "weiss_schnee": "Weiss",
    "yang_xiao_long": "Yang"
}

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

function format_name(name) {
    if (SPECIAL_CASE_NAMES[name] !== undefined) {
        return SPECIAL_CASE_NAMES[name];
    }
    
    var nameParts = name.split("_");
    var newName = "";
    
    for (var i = 0; i < nameParts.length; i++) {
        newName += " " + capitalize(nameParts[i]);
    }
    
    return newName.substring(1); // remove initial space
}

function create_bundle_entry (manifest) {
    var tr = document.createElement('tr');

    if (MAIN_CATEGORIES.indexOf(manifest.category) >= 0 || manifest.category === 'incomplete' || manifest.category === 'event' || manifest.category === 'duplicate') {
        var title = capitalize(manifest.category) + ' Opponents #' + manifest.index;

        var includes_opponents = manifest.folders.reduce(function (acc, val) {
            var opp = val.replace(/opponents[\\\/]/gi, '');

            if (acc.length > 0) acc += ', ';
            acc += format_name(opp);

            return acc;
        }, '');

        if (manifest.category === 'incomplete') {
            var desc = 'Includes the following extra opponents: '+includes_opponents;
        } else if (manifest.category === 'event') {
            var desc = 'Includes the following April Fool\'s Day opponents: '+includes_opponents;
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
