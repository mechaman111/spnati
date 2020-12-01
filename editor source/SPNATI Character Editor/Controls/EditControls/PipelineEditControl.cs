using Desktop;
using Desktop.CommonControls;
using ImagePipeline;
using System;
using System.Collections.Generic;
using SPNATI_Character_Editor.DataStructures;
using SPNATI_Character_Editor.Forms;

namespace SPNATI_Character_Editor.Controls.EditControls
{
	public partial class PipelineEditControl : PropertyEditControl
	{
		private PoseEntry _cell;
		private PoseMatrix _matrix;
		private PoseSheet _sheet;
		private PoseStage _stage;
		private HashSet<string> _knownGraphs = new HashSet<string>();

		public PipelineEditControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			PoseEntry cell = Data as PoseEntry;
			PoseStage stage;
			if (cell == null)
			{
				stage = Data as PoseStage;
				if (stage.Poses.Count > 0)
				{
					cell = stage.Poses[0];
				}
			}
			else
			{
				stage = cell.Stage;
			}
			_stage = stage;
			_sheet = stage.Sheet;
			_matrix = stage.Sheet.Matrix;
			_cell = cell;

			foreach (PipelineGraph graph in _matrix.Pipelines)
			{
				_knownGraphs.Add(graph.Key);
			}

			recField.AllowCreate = true;
			recField.RecordType = typeof(PipelineGraph);
			recField.RecordContext = _matrix;
			recField.RecordKey = GetValue()?.ToString();
			cmdEdit.Visible = !string.IsNullOrEmpty(recField.RecordKey);
		}

		protected override void OnClear()
		{
			recField.Record = null;
		}

		protected override void AddHandlers()
		{
			recField.RecordChanged += RecField_RecordChanged;
		}

		protected override void RemoveHandlers()
		{
			recField.RecordChanged -= RecField_RecordChanged;
		}

		protected override void OnSave()
		{
			IRecord record = recField.Record;
			if (record == null)
			{
				SetValue(null);
			}
			else
			{
				SetValue(record.Key);
			}
		}

		private void RecField_RecordChanged(object sender, RecordEventArgs e)
		{
			cmdEdit.Visible = !string.IsNullOrEmpty(recField.RecordKey);
			Save();

			//if this is a new graph, open it
			PipelineGraph graph = recField.Record as PipelineGraph;
			if (graph != null && !_knownGraphs.Contains(graph.Key))
			{
				_knownGraphs.Add(graph.Key);
				OpenPipelineEditor();
			}
		}

		private void cmdEdit_Click(object sender, EventArgs e)
		{
			OpenPipelineEditor();
		}

		private void OpenPipelineEditor()
		{
			PipelineGraph graph = recField.Record as PipelineGraph;

			PipelineEditor form = new PipelineEditor(_matrix.Character, _stage, _cell, graph);
			form.ShowDialog();
		}

		private void cmdParams_Click(object sender, EventArgs e)
		{
			List<string> parameters = new List<string>();
			PoseEntry cell = Data as PoseEntry;
			PoseStage stage = Data as PoseStage;
			if (cell != null)
			{
				parameters = cell.PipelineParameters;
			}
			else if (stage != null)
			{
				parameters = stage.PipelineParameters;
			}

			PipelineParametersForm form = new PipelineParametersForm();
			form.SetData(parameters);
			if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				parameters = form.Parameters;
				if (cell != null)
				{
					cell.PipelineParameters = parameters;
				}
				else if (stage != null)
				{
					stage.PipelineParameters = parameters;
				}
			}
		}
	}

	public class PipelineSelectAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(PipelineEditControl); }
		}
	}
}
