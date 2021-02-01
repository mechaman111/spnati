using Desktop;
using Desktop.Skinning;
using System;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class AddRecipeForm : SkinnedForm
	{
		public bool NeedEdit = false;
		public Recipe Recipe;

		public Case Case;

		public AddRecipeForm()
		{
			InitializeComponent();
		}

		private void cmdEdit_Click(object sender, System.EventArgs e)
		{
			if (!CreateRecipe()) { return; }
			NeedEdit = true;
			DialogResult = System.Windows.Forms.DialogResult.OK;
			Close();
		}

		private void cmdCreate_Click(object sender, System.EventArgs e)
		{
			if (!CreateRecipe()) { return; }
			DialogResult = System.Windows.Forms.DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}

		private bool CreateRecipe()
		{
			if (string.IsNullOrEmpty(txtName.Text))
			{
				MessageBox.Show("Please fill out a name.");
				return false;
			}

			RecipeProvider provider = new RecipeProvider();
			Recipe = provider.Create("") as Recipe;
			Recipe.Name = txtName.Text;
			Recipe.Description = txtDescription.Text;

			try
			{
				string caseJson = Json.Serialize(Case);
				Case recipeCase = Json.Deserialize<Case>(caseJson);
				Recipe.Case = recipeCase;
				Recipe.Case.Lines.Clear();

				string file = Recipe.GetFilePath();
				string json = Json.Serialize(Recipe);
				File.WriteAllText(file, json);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to save recipe: " + ex.Message);
				return false;
			}
			return true;
		}
	}
}
