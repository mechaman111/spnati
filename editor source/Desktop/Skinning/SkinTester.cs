using Desktop.CommonControls;
using System.ComponentModel;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public partial class SkinTester : UserControl
	{
		public SkinTester()
		{
			InitializeComponent();

			skinnedComboBox1.Items.Add("Item 1");
			skinnedComboBox1.Items.Add("Item 2");
			skinnedComboBox3.Items.Add("Item 1");
			skinnedComboBox4.Items.Add("Item 1");
			skinnedComboBox5.Items.Add("Item 1");

			skinnedComboBox2.Items.Add("Item 1");
			skinnedComboBox2.Items.Add("Item 2");
			skinnedComboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
			skinnedComboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

			skinnedComboBox1.SelectedIndex = 0;
			skinnedComboBox2.SelectedIndex = 0;
			skinnedComboBox3.SelectedIndex = 0;
			skinnedComboBox4.SelectedIndex = 0;
			skinnedComboBox5.SelectedIndex = 0;

			skinnedDataGridView1.Rows.Add(new object[] { "Cell 1", "Cell 2", "Cell 3", true });
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (DesignMode) { return; }


			AccordionColumn column = new AccordionColumn("Name", "Name");
			column.FillWeight = 1;
			accordionListView1.AddColumn(column);
			column = new AccordionColumn("Number", "#");
			column.Width = 60;
			column.TextAlign = HorizontalAlignment.Right;
			accordionListView1.AddColumn(column);
			accordionListView1.RebuildColumns();

			GroupedList<TestObject> accordionGroup = new GroupedList<TestObject>();
			accordionGroup.AddItem(new TestObject("Apple"));
			accordionGroup.AddItem(new TestObject("Ants"));
			accordionGroup.AddItem(new TestObject("Banana"));
			accordionListView1.DataSource = accordionGroup;
		}

		private void cmdDisable_Click(object sender, System.EventArgs e)
		{
			panel1.Enabled = !panel1.Enabled;
		}

		private class TestObject : IGroupedItem, INotifyPropertyChanged
		{
			public string Name { get; set; }

			public TestObject(string name)
			{
				Name = name;
			}

			public event PropertyChangedEventHandler PropertyChanged
			{
				add { }
				remove { }
			}

			public string GetGroupKey()
			{
				return string.IsNullOrEmpty(Name) ? "Default" : Name[0].ToString();
			}
		}
	}
}
