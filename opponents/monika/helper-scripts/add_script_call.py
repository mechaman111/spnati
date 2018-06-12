import re
import sys
import shutil
                    
def generate_script_tag(callback_name, params):
    param_string = ','.join("'{}'".format(p) for p in params)
    callback_code = "monika.{}({});".format(callback_name, param_string)
    return "<script>"+callback_code+"</script>"
    
def process(in_fname, out_fname, marker, cb_name, params):
    pattern = r"(\<state.*?marker=\"{}\".*?\>)(.+)(\<\/state\>)".format(re.escape(marker))
    tag = generate_script_tag(cb_name, params).replace('\\', r'\\')
    
    with open(in_fname) as infile:
        with open(out_fname, 'w') as outfile:
            for i, line in enumerate(infile):
                outstr, n_subs = re.subn(pattern, r"\1{}\2\3".format(tag), line)
                if n_subs > 0:
                    print("Found matching state at line {}".format(i))
                
                outfile.write(outstr.rstrip()+'\n')
    
if __name__ == '__main__':
    filename = sys.argv[1]
    marker = sys.argv[2]
    cb_name = sys.argv[3]
    params = sys.argv[4:]
    
    copied_file = filename+'.bak'
    
    shutil.copy(filename, copied_file)
    
    process(copied_file, filename, marker, cb_name, params)
    
