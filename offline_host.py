#!/usr/bin/env python

# Note: this script can be used to host a small, local web server using bottle
# that can be used for testing.
# It'll use gevent if you have that installed; otherwise it'll default to
# bottle's built-in development server.

try:
    from gevent import monkey; monkey.patch_all()
    gevent_available = True
except ImportError:
    gevent_available = False

from bottle import get, post, request, response, redirect, route, run, static_file
import os
import os.path as osp

host='localhost'
port=5000

@route('/')
def redir_index():
    return redirect('/index.html')

# Bottle should do this automatically, but it doesn't for some reason
ext_mimetypes = {
    '.png': 'image/png',
    '.jpg': 'image/jpeg',
    '.svg': 'image/svg+xml',
    '.css': 'text/css',
    '.js': 'text/javascript',
    '.html': 'text/html',
    '.xml': 'application/xml'
}

@route('/<filename:path>')
def statics(filename):
    ext = osp.splitext(filename)[-1]

    try:
        mimetype = ext_mimetypes[ext]
    except KeyError:
        mimetype = 'text/plain'

    return static_file(filename, root=os.getcwd(), mimetype=mimetype)


if gevent_available:
    server = 'gevent'
else:
    server = None

run(host=host, port=port, server=server)
