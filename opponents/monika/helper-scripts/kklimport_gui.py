import csv
from pathlib import Path
import functools as ft
import tkinter as tk
import tkinter.ttk as ttk
from tkinter import filedialog
from tkinter import messagebox
import traceback

import kkl_import as kkl

class KKLImportError(Exception):
    pass

class ImportInterface(tk.Frame):
    INPUT_FILETYPES = [
        ("Pose List", "*.csv"),
        ("Pose List", "*.txt"),
        ("Text File", "*.txt"),
        ("CSV Spreadsheet", "*.csv"),
        ("All Files", "*")
    ]
    
    def __init__(self, master=None):
        super().__init__(master)
        
        self.input_file = tk.StringVar()
        self.output_dir = tk.StringVar()
        
        self.filter_stage = tk.StringVar()
        self.filter_pose = tk.StringVar()
        
        self.import_progress = tk.IntVar()
        
        self.poses = []
        
        self.pack(fill=tk.X, expand=True)
        self.create_widgets()
        self.layout_widgets()
    
    def select_input_path(self):        
        file_str = filedialog.askopenfilename(filetypes=self.INPUT_FILETYPES).strip()
        if len(file_str) <= 0:
            return
            
        p = Path(file_str).resolve()
        if p.is_file():
            self.input_file.set(str(p))
            
        # read in poses:
        try:
            if p.suffix == '.csv':
                self.poses = []
                with p.open('r', encoding='utf-8', newline='') as f:
                    reader = csv.DictReader(f)
                    
                    for row in reader:
                        if 'stage' not in row:
                            raise KKLImportError("Input CSV file must have a 'stage' column!")
                        
                        if 'pose' not in row:
                            raise KKLImportError("Input CSV file must have a 'pose' column!")
                            
                        if 'code' not in row:
                            raise KKLImportError("Input CSV file must have a 'code' column!")
                            
                        if len(row['stage']) <= 0 or len(row['pose']) <= 0 or len(row['code']) <= 0:
                            continue
                            
                        stage = int(row['stage'])
                        
                        pose = row['pose'].strip().lower()
                        opts = row
                        
                        self.poses.append((stage, pose, opts))
            elif p.suffix == '.txt':
                self.poses = []
                with p.open('r', encoding='utf-8') as f:
                    for line in f:
                        try:
                            dest_filename, code = line.split('=', 1)
                            stage, pose = dest_filename.split('-', 1)
                        except ValueError:
                            pass
                            
                        
                        stage = int(stage)
                        self.poses.append((stage, pose, {'code': code}))
        except Exception as e:
            traceback.print_exc()
            messagebox.showerror(type(e).__name__, e.args[0])
        
            
    def set_stage_entry_options(self):
        stages = sorted(set(map(lambda p: str(p[0]), self.poses)))
        stages.insert(0, '')
        self.stage_entry['values'] = stages
        
        
    def set_pose_entry_options(self):
        poses = sorted(set(map(lambda p: p[1], self.poses)))
        poses.insert(0, '')
        self.pose_entry['values'] = poses
        
            
    def select_output_path(self):
        dir_str = filedialog.askdirectory()
        if dir_str:
            dir_str = dir_str.strip()
        
        if len(dir_str) <= 0:
            return
            
        p = Path(dir_str).resolve()
        if p.is_dir():
            self.output_dir.set(str(p))
            
    
    def create_widgets(self):
        self.input_file.set('')
        self.output_dir.set('')
        self.filter_stage.set('')
        self.filter_pose.set('')
        self.import_progress.set(0)
        
        file_select_style = ttk.Style()
        file_select_style.configure('KKLImportFileSelect.TButton', width=3)
        
        self.input_label = ttk.Label(self, text="Input file (.csv or .txt):")
        self.input_entry = ttk.Entry(self, textvariable=self.input_file)
        self.input_file_btn = ttk.Button(self,
            text="...", style="KKLImportFileSelect.TButton",
            command=self.select_input_path
        )
        
        self.output_label = ttk.Label(self, text="Output directory:")
        self.output_entry = ttk.Entry(self, textvariable=self.output_dir)
        self.output_dir_btn = ttk.Button(self,
            text="...", style="KKLImportFileSelect.TButton",
            command=self.select_output_path
        )
        
        self.stage_label = ttk.Label(self, text="Import specific stage (empty to import all): ")
        self.stage_entry = ttk.Combobox(self, textvariable=self.filter_stage, postcommand=self.set_stage_entry_options)
        
        self.pose_label = ttk.Label(self, text="Import specific pose (empty to import all): ")
        self.pose_entry = ttk.Combobox(self, textvariable=self.filter_pose, postcommand=self.set_pose_entry_options)
        
        self.imp_progressbar = ttk.Progressbar(self, maximum=100, orient='horizontal', mode='determinate', variable=self.import_progress)
        
        self.button_frame = tk.Frame(self)
        self.import_btn = ttk.Button(self.button_frame, text="Import to Image(s)", command=self.do_import)
        self.reset_kkl_btn = ttk.Button(self.button_frame, text="Setup Kisekae Scene", command=kkl.setup_kkl_scene)
        self.send_to_kkl_btn = ttk.Button(self.button_frame, text="Send to Kisekae", command=self.do_send_to_kisekae)
        
    def layout_widgets(self):
        self.columnconfigure(0, weight=0)
        self.columnconfigure(1, weight=1)
        self.columnconfigure(2, weight=0)
        
        self.input_label.grid(row=0, column=0, sticky=tk.E)
        self.output_label.grid(row=1, column=0, sticky=tk.E)
        self.stage_label.grid(row=2, column=0, sticky=tk.E)
        self.pose_label.grid(row=3, column=0, sticky=tk.E)
        
        self.input_entry.grid(row=0, column=1, sticky=tk.E+tk.W)
        self.output_entry.grid(row=1, column=1, sticky=tk.E+tk.W)
        self.stage_entry.grid(row=2, column=1, sticky=tk.E+tk.W)
        self.pose_entry.grid(row=3, column=1, sticky=tk.E+tk.W)
        
        self.input_file_btn.grid(row=0, column=2, sticky=tk.W)
        self.output_dir_btn.grid(row=1, column=2, sticky=tk.W)
        
        self.button_frame.grid(row=4, column=0, columnspan=3, sticky=tk.N)
        self.send_to_kkl_btn.pack(side=tk.LEFT)
        self.import_btn.pack(side=tk.LEFT)
        self.reset_kkl_btn.pack(side=tk.LEFT)
        
        self.imp_progressbar.grid(row=5, column=0, columnspan=3, sticky=tk.E+tk.W)
    
        
    def do_send_to_kisekae(self):
        f_stage = self.filter_stage.get().strip()
        f_pose = self.filter_pose.get().strip()
            
        if len(f_pose) <= 0:
            messagebox.showerror('Error', 'Please specify a pose name to send.')
            return
        
        
        if len(f_stage) <= 0:
            messagebox.showerror('Error', 'Please specify a stage to send.')
            return
            
        f_stage = int(f_stage)
            
        for stage, pose, opts in self.poses:
            if stage != f_stage or pose != f_pose:
                continue
            
            kkl.import_and_unlink(opts['code'])
            return
    
    def do_import(self):
        outdir = self.output_dir.get().strip()
        f_stage = self.filter_stage.get().strip()
        f_pose = self.filter_pose.get().strip()
        
        if len(self.poses) <= 0 or len(outdir) <= 0:
            return
            
        if len(f_stage) <= 0:
            f_stage = None
        else:
            f_stage = int(f_stage)
        
        if len(f_pose) <= 0:
            f_pose = None
        
        outdir = Path(outdir).resolve()
        
        to_import = []
        for stage, pose, opts in self.poses:
            if f_stage is not None and stage != f_stage:
                continue
            
            if f_pose is not None and pose != f_pose:
                continue
            
            dest_filename = outdir.joinpath('{:d}-{:s}.png'.format(stage, pose))
            
            to_import.append((dest_filename, opts))
            
        if len(to_import) <= 0:
            return
            
        self.import_progress.set(0)
        
        kkl.setup_kkl_scene()
        for i, t in enumerate(to_import):
            dest, opts = t
            
            kkl.process_single(dest=dest, do_setup=False, **opts)
            progress_pct = (i / len(to_import)) * 100
            self.import_progress.set(int(progress_pct))
            
            self.update()
        
        messagebox.showinfo('Convert', 'Conversion complete.')
        self.import_progress.set(100)

        
root = tk.Tk()
root.minsize(700, 100)
app = ImportInterface(root)
app.mainloop()
