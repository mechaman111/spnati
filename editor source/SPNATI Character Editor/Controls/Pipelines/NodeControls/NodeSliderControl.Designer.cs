namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	partial class NodeSliderControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.slider = new Desktop.Skinning.SkinnedSlider();
			this.tmrDebounce = new System.Windows.Forms.Timer(this.components);
			this.valValue = new Desktop.Skinning.SkinnedNumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.slider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valValue)).BeginInit();
			this.SuspendLayout();
			// 
			// slider
			// 
			this.slider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.slider.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.slider.Increment = 10;
			this.slider.Location = new System.Drawing.Point(0, 0);
			this.slider.Margin = new System.Windows.Forms.Padding(0);
			this.slider.Maximum = 100;
			this.slider.Minimum = 0;
			this.slider.Name = "slider";
			this.slider.Size = new System.Drawing.Size(110, 21);
			this.slider.TabIndex = 0;
			this.slider.TickFrequency = 25;
			this.slider.Value = 0;
			// 
			// tmrDebounce
			// 
			this.tmrDebounce.Interval = 300;
			this.tmrDebounce.Tick += new System.EventHandler(this.tmrDebounce_Tick);
			// 
			// valValue
			// 
			this.valValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.valValue.BackColor = System.Drawing.Color.White;
			this.valValue.DecimalPlaces = 2;
			this.valValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valValue.ForeColor = System.Drawing.Color.Black;
			this.valValue.Increment = new decimal(new int[] {
			1,
			0,
			0,
			65536});
			this.valValue.Location = new System.Drawing.Point(108, 1);
			this.valValue.Maximum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.valValue.Name = "valValue";
			this.valValue.Size = new System.Drawing.Size(43, 20);
			this.valValue.TabIndex = 1;
			// 
			// NodeSliderControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valValue);
			this.Controls.Add(this.slider);
			this.Name = "NodeSliderControl";
			this.Size = new System.Drawing.Size(151, 21);
			((System.ComponentModel.ISupportInitialize)(this.slider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valValue)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedSlider slider;
		private System.Windows.Forms.Timer tmrDebounce;
		private Desktop.Skinning.SkinnedNumericUpDown valValue;
	}
}
