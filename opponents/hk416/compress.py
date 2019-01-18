from pathlib import Path
import subprocess as sp

if __name__ == "__main__":
    p = Path('./').resolve()
    zopfli_bin = Path('/home/sebastian/Programming/zopfli/zopflipng')

    for ch in p.iterdir():
        if not ch.is_file() or ch.suffix != '.png':
            continue
        ch = ch.resolve()

        print("Processing: "+str(ch))
        sp.run([str(zopfli_bin), '-y', str(ch), str(ch)])
