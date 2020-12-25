using Desktop.DataStructures;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SPNATI_Character_Editor
{
	public class BindableTag : BindableObject
	{
		public string Tag
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public ObservableCollection<int> Stages
		{
			get { return Get<ObservableCollection<int>>(); }
			set { Set(value); }
		}

		public BindableTag()
		{
			Stages = new ObservableCollection<int>();
		}

		public BindableTag(string tag) : this()
		{
			Tag = tag;
		}

		public override string ToString()
		{
			return Tag;
		}

		public bool HasStage(int stage)
		{
			return Stages.Contains(stage);
		}

		public void AddStage(int stage)
		{
			if (!HasStage(stage))
			{
				Stages.Add(stage);
			}
		}

		public void RemoveStage(int stage)
		{
			Stages.Remove(stage);
		}

		/// <summary>
		/// Creates one or more character tags based on the stages
		/// </summary>
		/// <returns></returns>
		public List<CharacterTag> GetCharacterTags(ISkin character)
		{
			List<CharacterTag> list = new List<CharacterTag>();
			int layers = character.Character.Layers + Clothing.ExtraStages;
			if (Stages.Count == layers)
			{
				list.Add(new CharacterTag(Tag));
			}
			else
			{
				if (Stages.Count > 1)
				{
					List<int> stages = new List<int>();
					stages.AddRange(Stages);
					stages.Sort();
					int from = stages[0];
					int to = -1;
					for (int i = 1; i < stages.Count; i++)
					{
						int stage = stages[i];
						int last = stages[i - 1];
						if (stage - last > 1)
						{
							to = last;
							CharacterTag tag = new CharacterTag(Tag);
							tag.From = from.ToString();
							tag.To = to.ToString();
							list.Add(tag);
							from = stage;
						}
						if (i == stages.Count - 1)
						{
							to = stage;
							CharacterTag tag = new CharacterTag(Tag);
							tag.From = from.ToString();
							tag.To = to.ToString();
							list.Add(tag);
						}
					}
				}
				else if (Stages.Count == 1)
				{
					CharacterTag tag = new CharacterTag(Tag);
					tag.From = tag.To = Stages[0].ToString();
					list.Add(tag);
				}
			}
			return list;
		}
	}
}
