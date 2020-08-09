using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace SPNATI_Character_Editor
{
	public class BindableTagList
	{
		private Dictionary<string, BindableTag> _bindings = new Dictionary<string, BindableTag>();
		private HashSet<BindableTag> _modifyingTags = new HashSet<BindableTag>();

		public Character _character;

		/// <summary>
		/// Raised when at least one stage is added to a tag
		/// </summary>
		public event EventHandler<BindableTag> TagAdded;
		/// <summary>
		/// Raised when all stages have been removed from a tag
		/// </summary>
		public event EventHandler<BindableTag> TagRemoved;
		/// <summary>
		/// Raised when a stage has been added or removed from a tag but did not trigger TagAdded or TagRemoved
		/// </summary>
		public event EventHandler<BindableTag> TagModified;

		public BindableTagList(Character character)
		{
			_character = character;
		}

		public BindableTag Get(string tag)
		{
			return _bindings.Get(tag);
		}

		public BindableTag Add(string tag)
		{
			BindableTag bindable = new BindableTag(tag);
			_bindings[tag] = bindable;

			bindable.Stages.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
			{
				Stages_CollectionChanged(bindable, e);
			};

			foreach (CharacterTag characterTag in _character.Tags.Where(t => t.Tag == tag))
			{
				int from = -1;
				int to = -1;
				if (!string.IsNullOrEmpty(characterTag.From))
				{
					int.TryParse(characterTag.From, out from);
				}
				else
				{
					from = 0;
				}
				if (!string.IsNullOrEmpty(characterTag.To))
				{
					int.TryParse(characterTag.To, out to);
				}
				else
				{
					to = _character.Layers + Clothing.ExtraStages - 1;
				}
				for (int i = from; i <= to; i++)
				{
					bindable.Stages.Add(i);
				}
			}
			return bindable;
		}

		private void Stages_CollectionChanged(BindableTag tag, NotifyCollectionChangedEventArgs e)
		{
			if (_modifyingTags.Contains(tag))
			{
				return;
			}
			_modifyingTags.Add(tag);
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				Tag tagDefinition = TagDatabase.GetTag(tag.Tag);
				if (tagDefinition != null && (string.IsNullOrEmpty(tagDefinition.Gender) || tagDefinition.Gender == _character.Gender))
				{
					if (tagDefinition.PairedTags.Count > 0)
					{
						//add any paired tags
						foreach (string parentTag in tagDefinition.PairedTags)
						{
							Tag parentDefinition = TagDatabase.GetTag(parentTag);
							if (parentDefinition != null && (string.IsNullOrEmpty(parentDefinition.Gender) || parentDefinition.Gender == _character.Gender))
							{
								BindableTag parent = _bindings.Get(parentTag);
								if (parent != null)
								{
									foreach (int stage in e.NewItems)
									{
										parent.AddStage(stage);
									}
								}
							}
						}
					}

					////remove any mutually-exclusive tags
					//if (!string.IsNullOrEmpty(tagDefinition.Group))
					//{
					//	TagGroup group = TagDatabase.Dictionary.GetGroup(tagDefinition.Group);
					//	if (group != null && !group.MultiSelect)
					//	{
					//		foreach (Tag sibling in group.Tags)
					//		{
					//			//paired tags can stay
					//			if (sibling != tagDefinition && !tagDefinition.PairedTags.Contains(sibling.Value))
					//			{
					//				Tag siblingDefinition = TagDatabase.GetTag(sibling.Value);
					//				if (siblingDefinition != null && siblingDefinition.PairedTags.Contains(tag.Tag))
					//				{
					//					continue;
					//				}

					//				BindableTag siblingBindable = _bindings.Get(sibling.Value);
					//				if (siblingBindable != null && !_modifyingTags.Contains(siblingBindable))
					//				{
					//					foreach (int stage in e.NewItems)
					//					{
					//						siblingBindable.RemoveStage(stage);
					//					}
					//				}

					//			}
					//		}
					//	}
					//}
				}

				if (tag.Stages.Count == 1)
				{
					TagAdded?.Invoke(this, tag);
				}
				else
				{
					TagModified?.Invoke(this, tag);
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				if (tag.Stages.Count == 0)
				{
					TagRemoved?.Invoke(this, tag);
				}
				else
				{
					TagModified?.Invoke(this, tag);
				}
			}
			_modifyingTags.Remove(tag);
		}


		/// <summary>
		/// Converts tags into CharacterTags for the character's XML
		/// </summary>
		/// <returns></returns>
		public void SaveIntoCharacter()
		{
			_character.Tags.Clear();
			foreach (KeyValuePair<string, BindableTag> kvp in _bindings)
			{
				BindableTag bindable = kvp.Value;
				if (bindable.Stages.Count > 0)
				{
					foreach (CharacterTag tag in bindable.GetCharacterTags(_character))
					{
						_character.Tags.Add(tag);
					}
				}
			}
		}

		public IEnumerable<BindableTag> GetPopulated()
		{
			foreach (BindableTag tag in _bindings.Values)
			{
				if (tag.Stages.Count > 0)
				{
					yield return tag;
				}
			}
		}
	}
}
