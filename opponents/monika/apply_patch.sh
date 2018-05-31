#!/bin/sh
unix2dos behaviour.xml.patch behaviour.xml
patch behaviour.xml behaviour.xml.patch
unix2dos behaviour.xml
