import csv
from pathlib import Path
import functools as ft
import tkinter as tk
import tkinter.ttk as ttk
from tkinter import filedialog
from tkinter import messagebox

import csv2xml as c2x
from csv2xml.__main__ import convert
import csv2xml.behaviour_parser as bp
        

class C2XInterface(tk.Frame):
    def __init__(self, master=None):
        super().__init__(master)
        self.pack(fill=tk.X, expand=True)
        self.create_widgets()
        self.layout_widgets()
    
    def select_opponent_dir(self):
        dir_str = filedialog.askdirectory().strip()
        if len(dir_str) <= 0:
            return
            
        p = Path(dir_str).resolve()
        if p.is_dir():
            self.opponent_dir.set(str(p))
            
    def select_input_path(self):
        file_str = filedialog.askopenfilename(filetypes=(("CSV File", '*.csv'), ("XML File", "*.xml"), ("All Files", "*"))).strip()
        if len(file_str) <= 0:
            return
            
        p = Path(file_str).resolve()
        if p.is_file():
            self.input_file.set(str(p))
            
    def select_output_path(self):
        file_str = filedialog.asksaveasfilename(filetypes=(("XML File", "*.xml"), ("CSV File", '*.csv'), ("All Files", "*"))).strip()
        if len(file_str) <= 0:
            return
            
        p = Path(file_str).resolve()
        self.output_file.set(str(p))
    
    def create_widgets(self):
        self.opponent_dir = tk.StringVar()
        self.input_file = tk.StringVar()
        self.output_file = tk.StringVar()
        
        self.opponent_dir.set('')
        self.input_file.set('')
        self.output_file.set('')
        
        file_select_style = ttk.Style()
        file_select_style.configure('C2XFileSelect.TButton', width=3)
        
        self.opponent_label = ttk.Label(self, text="Path to SPNATI opponents/ directory: ")
        self.opponent_entry = ttk.Entry(self, textvariable=self.opponent_dir)
        self.opponent_file_btn = ttk.Button(self,
            text="...", style="C2XFileSelect.TButton",
            command=self.select_opponent_dir
        )
        
        self.input_label = ttk.Label(self, text="Input file (.csv or .xml):")
        self.input_entry = ttk.Entry(self, textvariable=self.input_file)
        self.input_file_btn = ttk.Button(self,
            text="...", style="C2XFileSelect.TButton",
            command=self.select_input_path
        )
        
        self.output_label = ttk.Label(self, text="Output file (.csv or .xml):")
        self.output_entry = ttk.Entry(self, textvariable=self.output_file)
        self.output_file_btn = ttk.Button(self,
            text="...", style="C2XFileSelect.TButton",
            command=self.select_output_path
        )
        
        self.convert_btn = ttk.Button(self, text="Convert", command=self.do_conversion)
        
        
    def layout_widgets(self):
        self.columnconfigure(0, weight=0)
        self.columnconfigure(1, weight=1)
        self.columnconfigure(2, weight=0)
        
        self.opponent_label.grid(row=0, column=0, sticky=tk.E)
        self.input_label.grid(row=1, column=0, sticky=tk.E)
        self.output_label.grid(row=2, column=0, sticky=tk.E)
        
        self.opponent_entry.grid(row=0, column=1, sticky=tk.E+tk.W)
        self.input_entry.grid(row=1, column=1, sticky=tk.E+tk.W)
        self.output_entry.grid(row=2, column=1, sticky=tk.E+tk.W)
        
        self.opponent_file_btn.grid(row=0, column=2, sticky=tk.W)
        self.input_file_btn.grid(row=1, column=2, sticky=tk.W)
        self.output_file_btn.grid(row=2, column=2, sticky=tk.W)
        
        self.convert_btn.grid(row=3, column=0, sticky=tk.N)
        
    
    def do_conversion(self):
        opp_dir = self.opponent_dir.get().strip()
        infile = self.input_file.get().strip()
        outfile = self.output_file.get().strip()
        
        if len(opp_dir) <= 0 or len(infile) <= 0 or len(outfile) <= 0:
            return
        
        opp_dir = Path(opp_dir).resolve()
        infile = Path(infile).resolve()
        outfile = Path(outfile).resolve()
        
        convert(str(infile), str(outfile), opponent_dir=str(opp_dir))
        
        messagebox.showinfo('Convert', 'Conversion complete.')
        

class Application(tk.Frame):
    def __init__(self, master=None):
        super().__init__(master)
        self.pack()
        self.create_widgets()
        
    def create_widgets(self):
        self.hi_there = ttk.Button(self)
        self.hi_there["text"] = "Hello World\n(click me)"
        self.hi_there["command"] = self.say_hi
        self.hi_there.pack(side="top")

        style = ttk.Style()
        style.configure('App.TButton', foreground='red')

        self.quit = ttk.Button(self, text="QUIT", style="App.TButton", command=root.destroy)
        self.quit.pack(side="bottom")

    def say_hi(self):
        print(askopenfilename())

        
root = tk.Tk()
root.minsize(700, 100)
app = C2XInterface(root)
app.mainloop()
