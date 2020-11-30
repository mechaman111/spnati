using System.IO;
using System.Threading.Tasks;
using SPNATI_Character_Editor;

namespace ImagePipeline
{
	public class FileNode : NodeDefinition
	{
		public override string Group
		{
			get { return "Input"; }
		}

		public override string Description
		{
			get { return "Obtains an image from a file"; }
		}

		public override PortDefinition[] GetInputs() { return null; }

		public override PortDefinition[] GetOutputs()
		{
			return new PortDefinition[]
			{
				new PortDefinition(PortType.Bitmap, "image")
			};
		}

		public override NodeProperty[] GetProperties()
		{
			return new NodeProperty[]
			{
				new NodeProperty(NodePropertyType.ImageFile, "Src")
			};
		}

		public override string Name
		{
			get { return "Image"; }
		}

		public override string Key
		{
			get { return "file"; }
			set { }
		}

		public override Task<PipelineResult> Process(PipelineArgs args)
		{
			if (!args.HasProperties())
			{
				return Task.FromResult(new PipelineResult(null));
			}
			else
			{
				string filename = args.GetProperty<string>(0);
				if (string.IsNullOrEmpty(filename))
				{
					return Task.FromResult(new PipelineResult(null));
				}
				string path = Path.Combine(Config.SpnatiDirectory, filename);

				if (!File.Exists(path))
				{
					return Task.FromResult(new PipelineResult(null));
				}

				return Task.FromResult(new PipelineResult(new DirectBitmap(path)));
			}
		}
	}
}
