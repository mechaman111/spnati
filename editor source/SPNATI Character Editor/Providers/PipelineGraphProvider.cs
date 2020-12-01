using Desktop;
using ImagePipeline;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SPNATI_Character_Editor.DataStructures;

namespace SPNATI_Character_Editor.Providers
{
	public class PipelineGraphProvider : IRecordProvider<PipelineGraph>
	{
		private PoseMatrix _matrix;

		public bool AllowsNew
		{
			get { return true; }
		}
		public bool AllowsDelete { get { return true; } }

		public bool TrackRecent
		{
			get { return false; }
		}

		public IRecord Create(string key)
		{
			PipelineGraph graph = new PipelineGraph()
			{
				Key = key,
				Name = key,
				Character = _matrix.Character
			};

			//make a default working graph
			PipelineNode node = graph.AddNode("cell");
			graph.Connect(node, 0, graph.MasterNode, 0);

			_matrix.Pipelines.Add(graph);

			return graph;
		}

		public void Delete(IRecord record)
		{
			PipelineGraph graph = record as PipelineGraph;
			foreach (PoseSheet sheet in _matrix.Sheets)
			{
				foreach (PoseStage stage in sheet.Stages)
				{
					if (stage.Pipeline == graph.Key)
					{
						MessageBox.Show($"Cannot delete pipeline {graph.Name} because it is in use by sheet {sheet.Name}.", "Delete Pipeline", MessageBoxButtons.OK);
						return;
					}
					foreach (PoseEntry entry in stage.Poses)
					{
						if (entry.Pipeline == graph.Key)
						{
							MessageBox.Show($"Cannot delete pipeline {graph.Name} because it is in use by sheet {sheet.Name}.", "Delete Pipeline", MessageBoxButtons.OK);
							return;
						}
					}
				}
			}

			_matrix.Pipelines.Remove(graph);
		}

		public bool FilterFromUI(IRecord record)
		{
			return false;
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			if (_matrix == null)
			{
				throw new InvalidOperationException("Pipeline graph record lookup requires a PoseMatrix context.");
			}
			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (PipelineGraph record in _matrix.Pipelines)
			{
				if (record.Key.ToLower().Contains(text) || record.Name.ToLower().Contains(text))
				{
					//partial match
					list.Add(record);
				}
			}
			return list;
		}

		public void SetContext(object context)
		{
			_matrix = context as PoseMatrix;
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Choose a Pipeline";
			info.Columns = new string[] { "Name" };
		}

		public ListViewItem FormatItem(IRecord record)
		{
			PipelineGraph pipeline = record as PipelineGraph;
			return new ListViewItem(new string[] { pipeline.Name });
		}


		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}
	}
}
