#!/usr/bin/env python3

from bottle import get, post, request, response, redirect, route, run, static_file
import os
import os.path as osp

host='localhost'
port=5000

@route('/')
def redir_index():
    return redirect('/index.html')

ext_mimetypes = {
    '.png': 'image/png',
    '.jpg': 'image/jpeg',
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

run(host=host, port=port, reloader=True)
