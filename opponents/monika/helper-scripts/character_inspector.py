import csv
from pathlib import Path
import functools as ft
import tkinter as tk
import tkinter.ttk as ttk
from tkinter import filedialog
from tkinter import messagebox

from PIL import Image, ImageTk

import csv2xml as c2x
import csv2xml.behaviour_parser as bp
from csv2xml import stage, utils


class DirectorySelector(tk.Frame):
    def __init__(self, master=None, label_text=""):
        super().__init__(master)
        
        self.label_text = label_text
        
        self.selected_dir = tk.StringVar()
        
        self.create_widgets()
        self.layout_widgets()
        
        default_dir = c2x.find_opponents_directory()
        
        if default_dir is not None:
            self.selected_dir.set(str(default_dir))
        
    def select_callback(self):
        dir_str = filedialog.askdirectory().strip()
        if len(dir_str) <= 0:
            return
            
        p = Path(dir_str).resolve()
        if p.is_dir():
            self.selected_dir.set(str(p))
            
            
    def create_widgets(self):
        file_select_style = ttk.Style()
        file_select_style.configure('C2XDirSelect.TButton', width=3)
    
        self.label = ttk.Label(self, text=self.label_text)
        
        self.subframe = tk.Frame(self)
        
        self.entry = ttk.Entry(self.subframe, textvariable=self.selected_dir)
        self.select_btn = ttk.Button(self.subframe,
            text="...", style="C2XDirSelect.TButton",
            command=self.select_callback
        )
        
        
    def layout_widgets(self):
        self.label.pack(side=tk.TOP)
        self.subframe.pack(side=tk.TOP, fill=tk.X, expand=True)
        
        self.entry.pack(side=tk.LEFT, fill=tk.X, expand=True)
        self.select_btn.pack(side=tk.LEFT)
        
        
class CaseSelector(tk.Frame):
    def __init__(self, master=None, label_text="", values_callback=None, item_change_callback=None):
        super().__init__(master)
        
        self.label_text = label_text
        self.values_callback = values_callback
        
        self.entry_text = tk.StringVar()
        self.cbox_text = tk.StringVar()
        
        self.entry_text.trace_add("write", item_change_callback)
        
        self.create_widgets()
        self.layout_widgets()
        
        
    def selected_items(self):
        yield from filter(lambda s: len(s.strip()) > 0, self.entry_text.get().strip().split(','))
        
        
    def on_item_selected(self, event):
        selected_item = self.cbox_text.get().strip()
        
        if len(selected_item) > 0:
            new_entry_text = self.entry_text.get().strip()
            
            if len(new_entry_text) > 0:
                new_entry_text += ', '
                
            new_entry_text += selected_item
            self.entry_text.set(new_entry_text)
            
    
    def postcommand(self):
        if self.values_callback is not None:
            currently_selected = list(self.selected_items())
            values = list(filter(lambda v: str(v) not in currently_selected, self.values_callback()))
            values.insert(0, '')
            
            self.combobox['values'] = values
        
        
    def create_widgets(self):
        self.label = ttk.Label(self, text=self.label_text)
        self.entry = ttk.Entry(self, textvariable=self.entry_text)
        self.combobox = ttk.Combobox(self, textvariable=self.cbox_text, postcommand=self.postcommand, width=30)
        
        self.combobox.bind("<<ComboboxSelected>>", self.on_item_selected)
        
    
    def layout_widgets(self):
        self.label.pack(side=tk.LEFT)
        self.combobox.pack(side=tk.LEFT)
        self.entry.pack(side=tk.LEFT, fill=tk.X, expand=True)


class CharacterSelector(tk.Frame):
    def __init__(self, master=None, load_callback=None):
        super().__init__(master)
        
        self.load_callback = load_callback
        self.cbox_text = tk.StringVar()
        
        self.opponents = []
        
        self.create_widgets()
        self.layout_widgets()
        
        
    def load_opponents(self):
        self.opponents = list(c2x.list_opponents(online=True, testing=True, offline=True))
        
        
    def on_load(self):
        self.load_callback(self.cbox_text.get())
        
        
    def postcommand(self):
        self.combobox['values'] = self.opponents
        
        
    def create_widgets(self):
        self.label = ttk.Label(self, text="Character:")
        self.combobox = ttk.Combobox(self, textvariable=self.cbox_text, postcommand=self.postcommand, width=20)
        self.button = ttk.Button(self, text="Load...", command=self.on_load)
        
    
    def layout_widgets(self):
        self.label.pack(side=tk.TOP)
        self.combobox.pack(side=tk.TOP)
        self.button.pack(side=tk.TOP)
        

class ImageView(tk.Canvas):
    def __init__(self, master=None, **kwargs):
        super().__init__(master, highlightthickness=0, **kwargs)
        
        self.image = None
        self.photo_image = ImageTk.PhotoImage('RGBA', size=(0, 0))
        self.image_on_canvas = self.create_image(0, 0, anchor=tk.NW, image=self.photo_image, tags='img')
        
        self.bind("<Configure>", self.on_resize)
        
    def resize_image(self, width, height):
        size = (width, height)
        
        resized = self.image.copy()
        resized.thumbnail(size)
        self.photo_image = ImageTk.PhotoImage(resized)
        
        pos_x = int(width // 2) - int(resized.width // 2)
        pos_y = int(height // 2) - int(resized.height // 2)
        
        self.delete('img')
        self.create_image(pos_x, pos_y, anchor=tk.NW, image=self.photo_image, tags='img')
        
    def change_image(self, file):
        if self.image is not None:
            self.image.close()
            
        if not file.is_file():
            return
            
        self.image = Image.open(file).convert('RGBA')
        self.resize_image(self.winfo_width(), self.winfo_height())
    
    def on_resize(self, event):
        if self.image is not None:
            self.resize_image(event.width, event.height)
        

class DialogueView(tk.Frame):
    def __init__(self, master=None):
        super().__init__(master)
        
        self.selected_image = None
        self.opponent_id = None
        self.opponent_meta = None
        
        self.create_widgets()
        self.layout_widgets()
        
    def on_select(self, *args):
        selected = self.tv.focus()
        item = self.tv.item(selected)
        
        st = item['values'][0]
        pose = item['values'][2]
        if isinstance(st, str):
            stageset = stage.parse_stage_selector(st, self.opponent_meta)
            stages = sorted(filter(lambda x: isinstance(x, int), stageset))
        
            if len(stages) <= 0:
                return
                
            st = stages[0]
        
        img_dir = c2x.find_opponents_directory() / self.opponent_id
        img_file = img_dir / utils.find_image(pose, st, img_dir)
        
        if self.selected_image is not None:
            self.selected_image.close()
        
        self.canvas.change_image(img_file)
        
    def create_widgets(self):
        self.canvas = ImageView(self)
        
        self.tv = ttk.Treeview(self, columns=['stage', 'conditions', 'image', 'text'])
        self.tv.heading('#0', text='Case')
        self.tv.heading('#1', text='Stages')
        self.tv.heading('#2', text='Conditions')
        self.tv.heading('#3', text='Image')
        self.tv.heading('#4', text='Text')
        
        self.tv.column('#0', width=200, stretch=False)
        self.tv.column('#1', width=50, stretch=False)
        self.tv.column('#2', width=200, stretch=False)
        self.tv.column('#3', width=150, stretch=False)
        self.tv.column('#4', minwidth=400, stretch=True)
        
        self.scroll = ttk.Scrollbar(self, orient='vertical', command=self.tv.yview)
        self.tv.configure(yscrollcommand=self.scroll.set)
        
        self.tv.bind('<<TreeviewSelect>>', self.on_select)
        
        
    def layout_widgets(self):
        self.tv.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        self.scroll.pack(side=tk.LEFT, fill=tk.Y, expand=False)
        self.canvas.pack(side=tk.LEFT, fill=tk.BOTH, expand=False)
        
        
    def update_view(self, opponent_id, opponent_meta, values):
        # values is an iterable of (stageset, case) tuples
        
        self.opponent_id = opponent_id
        self.opponent_meta = opponent_meta
        
        # clear the treeview:
        self.tv.delete(*self.tv.get_children())
        
        for stageset, case in values:
            stages = stage.format_stage_set(stageset)
            fmt_cond = case.format_conditions()
            
            for state in case.states:
                values = [
                    stages,
                    fmt_cond,
                    state.image,
                    state.text
                ]
                
                self.tv.insert('', 'end', open=False, text=case.tag, values=values)

class App(tk.Frame):
    def __init__(self, master=None):
        super().__init__(master)
        
        self.pack(fill=tk.BOTH, expand=True)
        
        self.lineset = None
        self.opponent_meta = None
        self.opponent_id = None
        
        self.create_widgets()
        self.layout_widgets()
        
        self.char_select.load_opponents()
        
    def get_cases(self):
        if self.lineset is not None:
            s = set(c.tag for c in c2x.all_cases(self.lineset))
            return sorted(s, key=lambda tag: c2x.Case.ALL_TAGS.index(tag))
        return []
        
    def get_stages(self):
        if self.lineset is not None:
            s = set()
            for stageset in self.lineset.keys():
                s.update(stageset)
                
            return s
        return []
        
    def create_widgets(self):
        self.dir_select = DirectorySelector(self, "Path to SPNATI opponents/ directory:")
        
        self.middle_frame = tk.Frame(self)
        
        self.cs_selector_frame = tk.Frame(self.middle_frame)
        self.case_selector = CaseSelector(self.cs_selector_frame, "Filter Cases: ", self.get_cases, self.filter_changed)
        self.stage_selector = CaseSelector(self.cs_selector_frame, "Filter Stages: ", self.get_stages, self.filter_changed)
        
        self.char_select = CharacterSelector(self.middle_frame, self.update_opponent)
        self.dialogue_view = DialogueView(self)
        
    def layout_widgets(self):
        self.case_selector.pack(side=tk.TOP, fill=tk.X, expand=True)
        self.stage_selector.pack(side=tk.TOP, fill=tk.X, expand=True)
        
        self.cs_selector_frame.pack(side=tk.LEFT, fill=tk.X, expand=True)
        self.char_select.pack(side=tk.LEFT)
        
        self.dir_select.pack(side=tk.TOP, fill=tk.X, anchor=tk.N, expand=False)
        self.middle_frame.pack(side=tk.TOP, fill=tk.X, anchor=tk.N, expand=False)
        self.dialogue_view.pack(side=tk.TOP, fill=tk.BOTH, anchor=tk.NW, expand=True)
        
        
    def update_view(self):
        if self.lineset is None or self.opponent_id is None or self.opponent_meta is None:
            return
            
        filter_cases = set(self.case_selector.selected_items())
        filter_stages = set(self.stage_selector.selected_items())

        def iterate_cases():
            for stageset, case_list in self.lineset.items():
                stageset = set(map(str, stageset))
                
                if len(filter_stages) > 0 and filter_stages.isdisjoint(stageset):
                    continue
                    
                for case in case_list:
                    if len(filter_cases) > 0 and case.tag not in filter_cases:
                        continue
                        
                    yield (stageset, case)
        
        self.dialogue_view.update_view(self.opponent_id, self.opponent_meta, iterate_cases())
    
    def filter_changed(self, *args):
        self.update_view()
    
    def update_opponent(self, opponent_id):
        self.opponent_id = opponent_id
        self.lineset, self.opponent_meta = c2x.load_character(opponent_id)
        
        self.update_view()

root = tk.Tk()
root.minsize(800, 800)
app = App(root)
app.mainloop()
