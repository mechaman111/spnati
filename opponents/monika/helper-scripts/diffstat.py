import sys

if __name__ == '__main__':
    with open(sys.argv[1]) as f:
        lines = [line.strip() for line in f]
        
    added_lines = [l[1:] for l in lines if len(l) > 1 and l[0] == '+' and not l.startswith('+++')]
    deleted_lines = [l[1:] for l in lines if len(l) > 1 and l[0] == '-' and not l.startswith('---')]

    added_set = set(added_lines)
    deleted_set = set(deleted_lines)
    
    net_additions = added_set - deleted_set
    net_deletions = deleted_set - added_set
    
    print("Total added lines: {}".format(len(added_lines)))
    print("Total deleted lines: {}".format(len(deleted_lines)))
    print("Unique added lines: {}".format(len(added_set)))
    print("Unique deleted lines: {}".format(len(deleted_set)))
    print("Net added lines: {}".format(len(net_additions)))
    print("Net deleted lines: {}".format(len(net_deletions)))
    
    for line in net_deletions:
        print('- '+line)
    
    for line in net_additions:
        print('+ '+line)
