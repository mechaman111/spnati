using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class StageSpecificGrid : UserControl
	{
		public string Label
		{
			get { return ColValue.HeaderText; }
			set { ColValue.HeaderText = value; }
		}

		public StageSpecificGrid()
		{
			InitializeComponent();
		}
	}
}
