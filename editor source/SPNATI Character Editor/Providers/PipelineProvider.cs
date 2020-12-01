using Desktop;
using ImagePipeline;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class PipelineProvider : IRecordProvider<IPipelineNode>
	{
		public bool AllowsNew { get { return false; } }
		public bool AllowsDelete { get { return false; } }
		public bool TrackRecent { get { return false; } }

		private static Dictionary<string, IPipelineNode> _nodes;

		public IRecord Create(string key)
		{
			throw new NotImplementedException();
		}
		public void Delete(IRecord record)
		{
			throw new NotImplementedException();
		}
		public bool FilterFromUI(IRecord record)
		{
			return false;
		}
		public ListViewItem FormatItem(IRecord record)
		{
			IPipelineNode node = record as IPipelineNode;
			return new ListViewItem(new string[] { node.Name });
		}

		public void SetContext(object context)
		{

		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Choose a Node";
			info.Columns = new string[] { "Name" };
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			if (_nodes == null)
			{
				BuildPipelineDefinitions();
			}
			text = text.ToLower();
			var list = new List<IRecord>();

			foreach (IPipelineNode node in _nodes.Values)
			{
				if (node.Key.ToLower().Contains(text) || node.Name.ToLower().Contains(text))
				{
					list.Add(node);
				}
			}
			return list;
		}

		private static void BuildPipelineDefinitions()
		{
			_nodes = new Dictionary<string, IPipelineNode>();
			Type pipelineDef = typeof(IPipelineNode);

			foreach (Type type in typeof(ShellLogic).Assembly.GetTypes())
			{
				if (!type.IsInterface && !type.IsAbstract && pipelineDef.IsAssignableFrom(type))
				{
					IPipelineNode node = Activator.CreateInstance(type) as IPipelineNode;
					_nodes[node.Key.ToLower()] = node;
				}
			}
		}

		public static IPipelineNode GetDefinition(string key)
		{
			if (_nodes == null)
			{
				BuildPipelineDefinitions();
			}
			IPipelineNode node;
			_nodes.TryGetValue(key.ToLower(), out node);
			return node;
		}
	}
}
