
/*
 * Index files use a simple binary format, consisting of any number of file blocks
 * containing the following data:
 * - file path length
 * - file data length
 * - file path (relative to origin)
 * - file data
 * 
 * Both length values are 4 bytes long.
 * The file path and file data strings are both padded so that their length is
 * a multiple of 4 bytes, where the value of the padding bytes indicates the number of
 * padding bytes present. This is so that the length of the padding can be easily determined
 * by reading the last byte of the overall padded string.
 * 
 * For example, a 1-byte long string would use 3 bytes of padding, each with the value 0x03.
 * Strings with lengths that are already a multiple of 4 require 4 extra bytes of padding
 * (with each byte being 0x04).
 * 
 * Each file contained in the index is stored using these file blocks, and all of the file
 * blocks are simply concatenated to form the overall index file.
 */

/**
 * Read a padded UTF-8 string from a DataView (and its associated ArrayBuffer).
 * 
 * @param {DataView} dataView 
 * @param {number} pos
 * @param {number} len
 * @returns {string}
 */
function readPaddedString (dataView, pos, len) {
    var padByte = dataView.getUint8(pos + len - 1);
    var actualLen = len - padByte;
    var byteArray = new Uint8Array(dataView.buffer, pos, actualLen);
    var decoder = new TextDecoder();
    return decoder.decode(byteArray);
}

/**
 * Normalize a file path for lookup in a data index.
 *
 * @param {string} path
 * @returns {string}
 */
function normalizeIndexedPath (path) {
    var norm = path.replace(/\/\/|\\/gm, "/").toLowerCase();
    if (norm.startsWith("/")) {
        return norm.substring(1);
    } else {
        return norm;
    }
}

/**
 * Represents a potentially-indexed collection of XML data files.
 * 
 * Parsed versions of files belonging to the collection can be retrieved
 * using the `getFile()` method.
 * 
 * If a valid path to an index file is supplied, that file can be loaded via
 * `loadIndex()`, and the contents of that index file will be cached internally.
 * Otherwise, requested files will be fetched from the host server.
 * 
 * Fetched files (whether from an index file or from the host server) will be parsed
 * once, and the resulting jQuery objects will be cached internally.
 * The "raw" versions of files fetched from the index will also be deleted from
 * the internal cache once parsed, to save memory.
 * 
 * @param {string} indexFilePath The full path to the index file for this collection.
 * This should be a placeholder beginning with "__" that is automatically filled in during CI/CD.
 * (For example, "__METADATA_XML_INDEX".)
 */
 function DataFileCollection (indexFilePath) {
    this.indexFilePath = indexFilePath.startsWith("__") ? null : indexFilePath;
    this.rawIndex = {};
    this.parsedIndex = {};
    this.fetchedIndex = false;
}

/**
 * Load the contents of an index file from a raw byte buffer.
 * @param {ArrayBuffer} buf 
 */
DataFileCollection.prototype.parseIndex = function (buf) {
    const dataView = new DataView(buf);
    var curPos = 0;

    while (curPos < dataView.byteLength) {
        let pathLen = dataView.getUint32(curPos, false);
        let dataLen = dataView.getUint32(curPos + 4, false);
        let path = normalizeIndexedPath(readPaddedString(dataView, curPos + 8, pathLen));
        let data = readPaddedString(dataView, curPos + 8 + pathLen, dataLen);

        this.rawIndex[path] = data;
        curPos += 8 + pathLen + dataLen;
    }
}

/**
 * Fetch and load the index file associated with this collection, if any.
 * @returns {Promise<void>}
 */
DataFileCollection.prototype.loadIndex = function () {
    if (!this.indexFilePath || this.fetchedIndex) return immediatePromise();

    console.log("Loading index: " + this.indexFilePath);
    return fetch(this.indexFilePath).then(function (resp) {
        if (resp.ok) {
            return resp.arrayBuffer();
        } else {
            throw new Error("Could not fetch " + resp.url + ": error " + resp.status + " " + resp.statusText);
        }
    }).then(this.parseIndex.bind(this)).catch(function (err) {
        console.log(err);
    });
}

DataFileCollection.prototype.removeCachedPath = function (path) {
    delete this.rawIndex[path];
    delete this.parsedIndex[path];
}

/**
 * Fetch and parse a (potentially cached) file from this collection.
 * 
 * @param {string} path
 * @returns {string}
 */
DataFileCollection.prototype.getFile = function (path) {
    var norm = normalizeIndexedPath(path);

    if (this.parsedIndex[norm]) {
        // console.log("Found " + path + " in parsed cache");
        return Promise.resolve(this.parsedIndex[norm]);
    } else if (this.rawIndex[norm]) {
        // console.log("Found " + path + " in raw cache");
        this.parsedIndex[norm] = $(this.rawIndex[norm]);
        delete this.rawIndex[norm];
        return Promise.resolve(this.parsedIndex[norm]);
    } else {
        /* NOTE: use un-normalized path for requests */
        // console.log("Fetched " + path + " from origin");
        return fetchXML(path).then(function ($xml) {
            this.parsedIndex[path] = $xml;
            return $xml;
        }.bind(this));
    }
}

var metadataIndex = new DataFileCollection("__METADATA_XML_INDEX");
