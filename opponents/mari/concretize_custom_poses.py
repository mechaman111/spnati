from argparse import ArgumentParser, FileType

if __name__ == "__main__":
    parser = ArgumentParser()
    parser.add_argument("infile", type=FileType("r", encoding="utf-8"))
    parser.add_argument("outfile", type=FileType("w", encoding="utf-8"))
    parser.add_argument("start", type=int)
    parser.add_argument("end", type=int)
    args = parser.parse_args()

    with args.infile as inf:
        template: str = inf.read()

    with args.outfile as outf:
        for i in range(args.start, args.end):
            outf.write(template.replace("#", str(i)) + "\n")
