#!/usr/bin/env python

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

try:
    import cherrypy
    run(server='cherrypy', host=host, port=port)
except ImportError:
    run(host=host, port=port)
