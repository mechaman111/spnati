using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	/// <summary>
	/// Interface for a view of the tree
	/// </summary>
	public interface IDialogueTreeView
	{
		/// <summary>
		/// Informs the host that it should delete a node
		/// </summary>
		event EventHandler<TreeNode> DeleteNode;
		/// <summary>
		/// Informs the host that it should save a node
		/// </summary>
		event EventHandler SaveNode;

		/// <summary>
		/// Sets the data for the tree
		/// </summary>
		/// <param name="tree">The tree control this is affecting</param>
		/// <param name="character">The character whose data should be populated</param>
		void Initialize(TreeView tree,  Character character);

		/// <summary>
		/// Gets the context menu to use for the Copy Tools menu
		/// </summary>
		/// <returns></returns>
		ContextMenuStrip GetCopyMenu();

		/// <summary>
		/// Clear and rebuild the tree
		/// </summary>
		void BuildTree();

		/// <summary>
		/// Sets the node filter
		/// </summary>
		/// <param name="mode"></param>
		void SetFilter(TreeFilterMode mode, string key);

		/// <summary>
		/// Selects a node in the tree
		/// </summary>
		/// <param name="stage"></param>
		/// <param name="stageCase"></param>
		void SelectNode(int stage, Case stageCase);

		/// <summary>
		/// Called when a case was changed (ex. a stage added or removed)
		/// </summary>
		/// <param name="modifiedCase"></param>
		void ModifyCase(Case modifiedCase);

		/// <summary>
		/// Called when the Add button is clicked
		/// </summary>
		/// <returns>A tag for a new case to add, "" to open the Add dropdown, or null to do nothing</returns>
		string AddingCase();

		/// <summary>
		/// Adds a brand new case to the tree
		/// </summary>
		/// <param name="newCase"></param>
		void AddCase(Case newCase);

		/// <summary>
		/// Removes a case from the tree
		/// </summary>
		/// <param name="removedCase"></param>
		void RemoveCase(Case removedCase);
	}

	public enum TreeFilterMode
	{
		All = 0,
		NonTargeted = 1,
		Targeted = 2
	}
}
