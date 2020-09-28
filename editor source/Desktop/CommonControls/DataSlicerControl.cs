using Desktop.Providers;
using Desktop.Reporting;
using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class DataSlicerControl : UserControl, ISkinControl
	{
		private DataSlicer _slicer;
		private IEnumerable<ISliceable> _dataSet;
		private Control _currentEditControl;

		private const int BucketPadding = 10;
		private const int MaxBarWidth = 200;

		private SolidBrush[] _barBrushes;
		private SolidBrush _labelBrush = new SolidBrush(Color.Black);

		private List<DataBucket> _buckets = null;

		public DataSlicerControl()
		{
			_barBrushes = new SolidBrush[8];
			for (int i = 0; i < _barBrushes.Length; i++)
			{
				_barBrushes[i] = new SolidBrush(Color.Black);
			}
			InitializeComponent();
		}

		public void OnUpdateSkin(Skin skin)
		{
			_barBrushes[0].Color = skin.Blue;
			_barBrushes[1].Color = skin.Red;
			_barBrushes[2].Color = skin.Green;
			_barBrushes[3].Color = skin.Orange;
			_barBrushes[4].Color = skin.Purple;
			_barBrushes[5].Color = skin.Gray;
			_barBrushes[6].Color = skin.Pink;
			_barBrushes[7].Color = skin.LightGray;
			_labelBrush.Color = skin.Surface.ForeColor;
		}

		public void SetSlicer(DataSlicer slicer, IEnumerable<ISliceable> dataSet)
		{
			_slicer = slicer;
			_slicer.PropertyChanged += _slicer_PropertyChanged;
			_slicer.Slicers.CollectionChanged += Slicers_CollectionChanged;
			_dataSet = dataSet;
			SliceData();
		}

		private void Slicers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
					IDataSlicer slicer = e.NewItems[0] as IDataSlicer;
					lstSlices.Items.Insert(e.NewStartingIndex, slicer);
					lstSlices.SelectedItem = slicer;
					SliceData();
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
					lstSlices.Items.RemoveAt(e.OldStartingIndex);
					break;
			}
		}

		private void SliceData()
		{
			tmrRefresh.Start();
		}

		private void graph_Paint(object sender, PaintEventArgs e)
		{
			if (_buckets == null || _buckets.Count == 0)
			{
				return;
			}

			Graphics g = e.Graphics;
			g.Clear(SkinManager.Instance.CurrentSkin.FieldBackColor);
			int width = (graph.Width - BucketPadding * 2) / _buckets.Count;
			int height = graph.Height;
			int x = BucketPadding;
			int y = 0;
			int maxCount = 0;
			foreach (DataBucket b in _buckets)
			{
				if (b.Count > maxCount)
				{
					maxCount = b.Count;
				}
			}

			for (int i = 0; i < _buckets.Count; i++)
			{
				DrawBucket(g, _buckets[i], x, y, width, height, maxCount, _buckets);
				x += width;
			}
		}

		private void DrawBucket(Graphics g, DataBucket bucket, int x, int y, int width, int height, int maxCount, List<DataBucket> siblings)
		{
			//label
			StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter };
			Rectangle rect = new Rectangle(x, y, width, height);
			g.DrawString(bucket.Name, Skin.TextFont, _labelBrush, rect, sf);

			int lineHeight = (int)g.MeasureString("A", Skin.TextFont).Height;
			//bars
			if (bucket.SubBuckets.Count > 0)
			{
				//recursive labels
				height -= lineHeight;
				width = (width - BucketPadding * 2) / bucket.SubBuckets.Count;
				x += BucketPadding;
				for (int i = 0; i < bucket.SubBuckets.Count; i++)
				{
					DataBucket sub = bucket.SubBuckets[i];
					//label
					DrawBucket(g, sub, x, y, width, height, maxCount, bucket.SubBuckets);

					x += width;
				}
			}
			else
			{
				int i = siblings.IndexOf(bucket);
				SolidBrush barBrush = _barBrushes[i % _barBrushes.Length];
				int availableHeight = height - lineHeight * 2;
				float pct = maxCount == 0 ? 0 : bucket.Count / (float)maxCount;
				int barHeight = Math.Max(1, (int)(pct * availableHeight));
				int barWidth = Math.Min(MaxBarWidth, width);
				int left = x + width / 2 - barWidth / 2;
				g.FillRectangle(barBrush, left, availableHeight - barHeight + lineHeight, barWidth, barHeight);
				g.DrawString(bucket.Count.ToString(), Skin.TextFont, barBrush, new Rectangle(left, availableHeight - barHeight, barWidth, lineHeight), sf);
			}
		}

		private void tsAddSlice_Click(object sender, EventArgs e)
		{
			SlicerDefinition def = RecordLookup.DoLookup(typeof(SlicerDefinition), "", false, null) as SlicerDefinition;
			if (def != null)
			{
				IDataSlicer slicer = def.CreateInstance(_slicer.Context);
				_slicer.AddSlicer(slicer);
			}
		}

		private void tsRemoveSlice_Click(object sender, EventArgs e)
		{
			IDataSlicer slicer = lstSlices.SelectedItem as IDataSlicer;
			if (slicer != null)
			{
				_slicer.RemoveSlicer(slicer);
			}
		}

		private void tsDown_Click(object sender, EventArgs e)
		{
			IDataSlicer slicer = lstSlices.SelectedItem as IDataSlicer;
			if (slicer != null)
			{
				_slicer.MoveSlicer(slicer, 1);
			}
		}

		private void tsUp_Click(object sender, EventArgs e)
		{
			IDataSlicer slicer = lstSlices.SelectedItem as IDataSlicer;
			if (slicer != null)
			{
				_slicer.MoveSlicer(slicer, -1);
			}
		}

		private void _slicer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			SliceData();
		}

		private void lstSlices_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_currentEditControl != null)
			{
				pnlSlice.Controls.Remove(_currentEditControl);
				_currentEditControl = null;
			}
			IDataSlicer slicer = lstSlices.SelectedItem as IDataSlicer;
			if (slicer != null)
			{
				grpProperties.Text = $"{slicer.DisplayName} Properties";
				Type controlType = DataSlicerControlMap.GetControlType(slicer.GetType());
				if (controlType != null)
				{
					Control ctl = Activator.CreateInstance(controlType) as Control;
					ISlicerControl slicerCtl = ctl as ISlicerControl;
					if (ctl != null && slicerCtl != null)
					{
						_currentEditControl = ctl;
						ctl.Dock = DockStyle.Fill;
						pnlSlice.Controls.Add(ctl);
						slicerCtl.SetSlicer(slicer);
					}
				}
			}
			else
			{
				grpProperties.Text = "Slice Properties";
			}
		}

		private void tmrRefresh_Tick(object sender, EventArgs e)
		{
			tmrRefresh.Stop();
			_buckets = _slicer.Slice(_dataSet);
			graph.Invalidate();
		}
	}
}
