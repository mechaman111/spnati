using System.Collections.Generic;

namespace SPNATI_Character_Editor.Controls.EditControls.VariableControls
{
	[SubVariable("hand.score")]
	public partial class HandQualityControl : PlayerControlBase
	{
		private static readonly KeyValuePair<string, string>[] RankMap = {
			new KeyValuePair<string, string>(null, ""),
			new KeyValuePair<string, string>("0", "high card"),
			new KeyValuePair<string, string>("1", "one pair"),
			new KeyValuePair<string, string>("2", "two pair"),
			new KeyValuePair<string, string>("3", "three of a kind"),
			new KeyValuePair<string, string>("4", "straight"),
			new KeyValuePair<string, string>("5", "flush"),
			new KeyValuePair<string, string>("6", "full house"),
			new KeyValuePair<string, string>("7", "four of a kind"),
			new KeyValuePair<string, string>("8", "straight flush"),
			new KeyValuePair<string, string>("9", "royal flush"),
		};

		private static readonly KeyValuePair<string, string>[] CardMap = {
			new KeyValuePair<string, string>(null, ""),
			new KeyValuePair<string, string>("01", "1"),
			new KeyValuePair<string, string>("02", "2"),
			new KeyValuePair<string, string>("03", "3"),
			new KeyValuePair<string, string>("04", "4"),
			new KeyValuePair<string, string>("05", "5"),
			new KeyValuePair<string, string>("06", "6"),
			new KeyValuePair<string, string>("07", "7"),
			new KeyValuePair<string, string>("08", "8"),
			new KeyValuePair<string, string>("09", "9"),
			new KeyValuePair<string, string>("10", "10"),
			new KeyValuePair<string, string>("11", "Jack"),
			new KeyValuePair<string, string>("12", "Queen"),
			new KeyValuePair<string, string>("13", "King"),
			new KeyValuePair<string, string>("14", "Ace"),
		};

		public HandQualityControl()
		{
			InitializeComponent();
			cboOperator.DataSource = ExpressionTest.Operators;
			cboRank.DataSource = RankMap;
			cboRank.ValueMember = "Key";
			cboRank.DisplayMember = "Value";
			cboCard.DataSource = CardMap;
			cboCard.ValueMember = "Key";
			cboCard.DisplayMember = "Value";
		}

		protected override void OnBoundData()
		{
			base.OnBoundData();
			cboOperator.SelectedItem = Expression.Operator ?? "==";
			string value = Expression.Value;
			if (value != null && value.Length == 3)
			{
				string rank = value[0].ToString();
				string card = value.Substring(1);
				for (int i = 0; i < RankMap.Length; i++)
				{
					KeyValuePair<string, string> item = RankMap[i];
					if (rank == item.Key)
					{
						cboRank.SelectedItem = item;
						break;
					}
				}
				for (int i = 0; i < CardMap.Length; i++)
				{
					KeyValuePair<string, string> item = CardMap[i];
					if (card == item.Key)
					{
						cboCard.SelectedItem = item;
						break;
					}
				}

			}
			else
			{
				cboCard.SelectedIndex = 0;
				cboRank.SelectedIndex = 0;
			}
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Hand quality");
		}

		protected override void AddHandlers()
		{
			base.AddHandlers();
			cboOperator.SelectedIndexChanged += CboOperator_ValueChanged;
			cboCard.SelectedIndexChanged += CboOperator_ValueChanged;
			cboRank.SelectedIndexChanged += CboOperator_ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			base.RemoveHandlers();
			cboOperator.SelectedIndexChanged -= CboOperator_ValueChanged;
			cboCard.SelectedIndexChanged -= CboOperator_ValueChanged;
			cboRank.SelectedIndexChanged -= CboOperator_ValueChanged;
		}

		private void CboOperator_ValueChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		protected override void OnSave()
		{
			base.OnSave();
			Expression.Operator = cboOperator.Text;
			object rankObj = cboRank.SelectedItem;
			object cardObj = cboCard.SelectedItem;
			if (rankObj != null && cardObj != null)
			{
				string rank = ((KeyValuePair<string, string>)rankObj).Key;
				string card = ((KeyValuePair<string, string>)cardObj).Key;
				if (rank != null && card != null)
				{
					Expression.Value = rank + card;
					return;
				}
			}

			Expression.Value = null;

		}

		protected override string GetVariable()
		{
			return "hand.score";
		}
	}
}
