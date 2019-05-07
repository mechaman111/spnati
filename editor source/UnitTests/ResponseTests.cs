using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPNATI_Character_Editor;

namespace UnitTests
{
	[TestClass]
	/// <summary>
	/// Test for making sure the right response tag is given to a spoken tag
	/// </summary>
	public class ResponseTests
	{
		private static Character _female;
		private static Character _male;
		private static Character _bifemale;
		private static Character _bimale;

		[ClassInitialize]
		public static void Init(TestContext context)
		{
			TriggerDatabase.Load();

			_female = new Character() { Gender = "female", FolderName = "female" };
			_male = new Character() { Gender = "male", FolderName = "male" };
			_bifemale = new Character() { Gender = "female", FolderName = "female" };
			_bimale = new Character() { Gender = "male", FolderName = "male" };
			foreach (Character c in new Character[] { _female, _male, _bifemale, _bimale })
			{
				c.AddLayer(new Clothing() { Type = "extra" });
				c.AddLayer(new Clothing() { Type = "minor" });
				c.AddLayer(new Clothing() { Type = "major" });
				c.AddLayer(new Clothing() { Type = "important", Position = "upper" });
				c.AddLayer(new Clothing() { Type = "important", Position = "lower" });
			}
			_bimale.Metadata.CrossGender = true;
			_bifemale.Metadata.CrossGender = true;
		}

		[TestMethod]
		public void SwapCards()
		{
			Case c = new Case("swap_cards");
			c.Stages.Add(0);
			Assert.AreEqual("swap_cards", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void MustStrip_Winning_Female()
		{
			Case c = new Case("must_strip_winning");
			c.Stages.Add(0);
			Assert.AreEqual("female_must_strip", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void MustStrip_Normal_Female()
		{
			Case c = new Case("must_strip_normal");
			c.Stages.Add(0);
			Assert.AreEqual("female_must_strip", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void MustStrip_Losing_Female()
		{
			Case c = new Case("must_strip_losing");
			c.Stages.Add(0);
			Assert.AreEqual("female_must_strip", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void MustStrip_Winning_Male()
		{
			Case c = new Case("must_strip_winning");
			c.Stages.Add(0);
			Assert.AreEqual("male_must_strip", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void MustStrip_Normal_Male()
		{
			Case c = new Case("must_strip_normal");
			c.Stages.Add(0);
			Assert.AreEqual("male_must_strip", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void MustStrip_Losing_Male()
		{
			Case c = new Case("must_strip_losing");
			c.Stages.Add(0);
			Assert.AreEqual("male_must_strip", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripping_Extra_Female()
		{
			Case c = new Case("stripping");
			c.Stages.Add(0);
			Assert.AreEqual("female_removing_accessory", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Stripping_Minor_Female()
		{
			Case c = new Case("stripping");
			c.Stages.Add(1);
			Assert.AreEqual("female_removing_minor", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Stripping_Major_Female()
		{
			Case c = new Case("stripping");
			c.Stages.Add(2);
			Assert.AreEqual("female_removing_major", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Stripping_Upper_Female()
		{
			Case c = new Case("stripping");
			c.Stages.Add(3);
			Assert.AreEqual("female_chest_will_be_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Stripping_Lower_Female()
		{
			Case c = new Case("stripping");
			c.Stages.Add(4);
			Assert.AreEqual("female_crotch_will_be_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_Stripping_Extra_Male()
		{
			Case c = new Case("stripping");
			c.Stages.Add(0);
			Assert.AreEqual("male_removing_accessory", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripping_Minor_Male()
		{
			Case c = new Case("stripping");
			c.Stages.Add(1);
			Assert.AreEqual("male_removing_minor", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripping_Major_Male()
		{
			Case c = new Case("stripping");
			c.Stages.Add(2);
			Assert.AreEqual("male_removing_major", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripping_Upper_Male()
		{
			Case c = new Case("stripping");
			c.Stages.Add(3);
			Assert.AreEqual("male_chest_will_be_visible", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripping_Lower_Male()
		{
			Case c = new Case("stripping");
			c.Stages.Add(4);
			Assert.AreEqual("male_crotch_will_be_visible", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripped_Extra_Female()
		{
			Case c = new Case("stripped");
			c.Stages.Add(1);
			Assert.AreEqual("female_removed_accessory", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Stripped_Minor_Female()
		{
			Case c = new Case("stripped");
			c.Stages.Add(2);
			Assert.AreEqual("female_removed_minor", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Stripped_Major_Female()
		{
			Case c = new Case("stripped");
			c.Stages.Add(3);
			Assert.AreEqual("female_removed_major", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Stripped_Upper_Large_Female()
		{
			_female.Size = "large";
			Case c = new Case("stripped");
			c.Stages.Add(4);
			Assert.AreEqual("female_large_chest_is_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Stripped_Upper_Medium_Female()
		{
			_female.Size = "medium";
			Case c = new Case("stripped");
			c.Stages.Add(4);
			Assert.AreEqual("female_medium_chest_is_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Stripped_Upper_Female()
		{
			_female.Size = "small";
			Case c = new Case("stripped");
			c.Stages.Add(4);
			Assert.AreEqual("female_small_chest_is_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Stripped_Lower_Female()
		{
			Case c = new Case("stripped");
			c.Stages.Add(5);
			Assert.AreEqual("female_crotch_is_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Stripped_Extra_Male()
		{
			Case c = new Case("stripped");
			c.Stages.Add(1);
			Assert.AreEqual("male_removed_accessory", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripped_Minor_Male()
		{
			Case c = new Case("stripped");
			c.Stages.Add(2);
			Assert.AreEqual("male_removed_minor", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripped_Major_Male()
		{
			Case c = new Case("stripped");
			c.Stages.Add(3);
			Assert.AreEqual("male_removed_major", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripped_Upper_Male()
		{
			Case c = new Case("stripped");
			c.Stages.Add(4);
			Assert.AreEqual("male_chest_is_visible", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripped_Lower_Large_Male()
		{
			_male.Size = "large";
			Case c = new Case("stripped");
			c.Stages.Add(5);
			Assert.AreEqual("male_large_crotch_is_visible", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripped_Lower_Medium_Male()
		{
			_male.Size = "medium";
			Case c = new Case("stripped");
			c.Stages.Add(5);
			Assert.AreEqual("male_medium_crotch_is_visible", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripped_Lower_Small_Male()
		{
			_male.Size = "small";
			Case c = new Case("stripped");
			c.Stages.Add(5);
			Assert.AreEqual("male_small_crotch_is_visible", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Stripped_Extra_Opponent()
		{
			Case c = new Case("stripped");
			c.Stages.Add(1);
			Assert.AreEqual("opponent_stripped", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void Stripped_Minor_Opponent()
		{
			Case c = new Case("stripped");
			c.Stages.Add(2);
			Assert.AreEqual("opponent_stripped", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void Stripped_Major_Opponent()
		{
			Case c = new Case("stripped");
			c.Stages.Add(3);
			Assert.AreEqual("opponent_stripped", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void Stripped_Upper_Opponent()
		{
			Case c = new Case("stripped");
			c.Stages.Add(4);
			Assert.AreEqual("opponent_chest_is_visible", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void Stripped_Lower_Opponent()
		{
			Case c = new Case("stripped");
			c.Stages.Add(5);
			Assert.AreEqual("opponent_crotch_is_visible", c.GetResponseTag(_bifemale, _male));
		}

		[TestMethod]
		public void Stripped_Lower_Large_Opponent()
		{
			_bimale.Size = "large";
			Case c = new Case("stripped");
			c.Stages.Add(5);
			Assert.AreEqual("opponent_crotch_is_visible", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void Stripped_Lower_Medium_Opponent()
		{
			_bimale.Size = "medium";
			Case c = new Case("stripped");
			c.Stages.Add(5);
			Assert.AreEqual("opponent_crotch_is_visible", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void Stripped_Lower_Small_Opponent()
		{
			_bimale.Size = "small";
			Case c = new Case("stripped");
			c.Stages.Add(5);
			Assert.AreEqual("opponent_crotch_is_visible", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void MustMasturbateFirst_Female()
		{
			Case c = new Case("must_masturbate_first");
			Assert.AreEqual("female_must_masturbate", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void MustMasturbate_Female()
		{
			Case c = new Case("must_masturbate");
			Assert.AreEqual("female_must_masturbate", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void StartMasturbating_Female()
		{
			Case c = new Case("start_masturbating");
			Assert.AreEqual("female_start_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Masturbating_Female()
		{
			Case c = new Case("masturbating");
			Assert.AreEqual("female_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Heavy_Masturbating_Female()
		{
			Case c = new Case("heavy_masturbating");
			Assert.AreEqual("female_heavy_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Finished_Masturbating_Female()
		{
			Case c = new Case("finished_masturbating");
			Assert.AreEqual("female_finished_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void MustMasturbateFirst_Male()
		{
			Case c = new Case("must_masturbate_first");
			Assert.AreEqual("male_must_masturbate", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void MustMasturbate_Male()
		{
			Case c = new Case("must_masturbate");
			Assert.AreEqual("male_must_masturbate", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void StartMasturbating_Male()
		{
			Case c = new Case("start_masturbating");
			Assert.AreEqual("male_start_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Masturbating_Male()
		{
			Case c = new Case("masturbating");
			Assert.AreEqual("male_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Heavy_Masturbating_Male()
		{
			Case c = new Case("heavy_masturbating");
			Assert.AreEqual("male_heavy_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Finished_Masturbating_Male()
		{
			Case c = new Case("finished_masturbating");
			Assert.AreEqual("male_finished_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void MustMasturbateFirst_Opponent()
		{
			Case c = new Case("must_masturbate_first");
			Assert.AreEqual("opponent_lost", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void MustMasturbate_Opponent()
		{
			Case c = new Case("must_masturbate");
			Assert.AreEqual("opponent_lost", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void StartMasturbating_Opponent()
		{
			Case c = new Case("start_masturbating");
			Assert.AreEqual("opponent_start_masturbating", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void Masturbating_Opponent()
		{
			Case c = new Case("masturbating");
			Assert.AreEqual("opponent_masturbating", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void Heavy_Masturbating_Opponent()
		{
			Case c = new Case("heavy_masturbating");
			Assert.AreEqual("opponent_heavy_masturbating", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void Finished_Masturbating_Opponent()
		{
			Case c = new Case("finished_masturbating");
			Assert.AreEqual("opponent_finished_masturbating", c.GetResponseTag(_bimale, _female));
		}

		[TestMethod]
		public void GameOver_Victory()
		{
			Case c = new Case("game_over_victory");
			Assert.AreEqual("game_over_defeat", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Hand_Good()
		{
			Case c = new Case("good_hand");
			Assert.AreEqual("hand", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Hand_Bad()
		{
			Case c = new Case("bad_hand");
			Assert.AreEqual("hand", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Hand_Okay()
		{
			Case c = new Case("okay_hand");
			Assert.AreEqual("hand", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Hand_Any()
		{
			Case c = new Case("hand");
			Assert.AreEqual("hand", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Male_MustStrip_Other()
		{
			Case c = new Case("male_must_strip");
			Assert.AreEqual("male_must_strip", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_MustStrip_Target()
		{
			Case c = new Case("male_must_strip");
			c.Target = _male.FolderName;
			Assert.AreEqual("must_strip", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_RemovingAccessory_Other()
		{
			Case c = new Case("male_removing_accessory");
			Assert.AreEqual("male_removing_accessory", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_RemovingAccessory_Target()
		{
			Case c = new Case("male_removing_accessory");
			c.Target = _male.FolderName;
			Assert.AreEqual("stripping", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_RemovingMinor_Other()
		{
			Case c = new Case("male_removing_minor");
			Assert.AreEqual("male_removing_minor", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_RemovingMinor_Target()
		{
			Case c = new Case("male_removing_minor");
			c.Target = _male.FolderName;
			Assert.AreEqual("stripping", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_RemovingMajor_Other()
		{
			Case c = new Case("male_removing_major");
			Assert.AreEqual("male_removing_major", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_RemovingMajor_Target()
		{
			Case c = new Case("male_removing_major");
			c.Target = _male.FolderName;
			Assert.AreEqual("stripping", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Female_MustStrip_Other()
		{
			Case c = new Case("female_must_strip");
			Assert.AreEqual("female_must_strip", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_MustStrip_Target()
		{
			Case c = new Case("female_must_strip");
			c.Target = _female.FolderName;
			Assert.AreEqual("must_strip", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_RemovingAccessory_Other()
		{
			Case c = new Case("female_removing_accessory");
			Assert.AreEqual("female_removing_accessory", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_RemovingAccessory_Target()
		{
			Case c = new Case("female_removing_accessory");
			c.Target = _female.FolderName;
			Assert.AreEqual("stripping", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_RemovingMinor_Other()
		{
			Case c = new Case("female_removing_minor");
			Assert.AreEqual("female_removing_minor", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_RemovingMinor_Target()
		{
			Case c = new Case("female_removing_minor");
			c.Target = _female.FolderName;
			Assert.AreEqual("stripping", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_RemovingMajor_Other()
		{
			Case c = new Case("female_removing_major");
			Assert.AreEqual("female_removing_major", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_RemovingMajor_Target()
		{
			Case c = new Case("female_removing_major");
			c.Target = _female.FolderName;
			Assert.AreEqual("stripping", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Male_ChestWillBeVisible_Other()
		{
			Case c = new Case("male_chest_will_be_visible");
			Assert.AreEqual("male_chest_will_be_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_ChestWillBeVisible_Target()
		{
			Case c = new Case("male_chest_will_be_visible");
			c.Target = _male.FolderName;
			Assert.AreEqual("stripping", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Female_ChestWillBeVisible_Other()
		{
			Case c = new Case("female_chest_will_be_visible");
			Assert.AreEqual("female_chest_will_be_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Female_ChestWillBeVisible_Target()
		{
			Case c = new Case("female_chest_will_be_visible");
			c.Target = _female.FolderName;
			Assert.AreEqual("stripping", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Male_ChestIsVisible_Other()
		{
			Case c = new Case("male_chest_is_visible");
			Assert.AreEqual("male_chest_is_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_ChestIsVisible_Target()
		{
			Case c = new Case("male_chest_is_visible");
			c.Target = _male.FolderName;
			Assert.AreEqual("stripped", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Female_SmallChestIsVisible_Other()
		{
			Case c = new Case("female_small_chest_is_visible");
			Assert.AreEqual("female_small_chest_is_visible", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_SmallChestIsVisible_Target()
		{
			Case c = new Case("female_small_chest_is_visible");
			c.Target = _female.FolderName;
			_female.Size = "small";
			Assert.AreEqual("stripped", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_MediumChestIsVisible_Other()
		{
			Case c = new Case("female_medium_chest_is_visible");
			Assert.AreEqual("female_medium_chest_is_visible", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_MediumChestIsVisible_Target()
		{
			Case c = new Case("female_medium_chest_is_visible");
			c.Target = _female.FolderName;
			_female.Size = "medium";
			Assert.AreEqual("stripped", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_LargeChestIsVisible_Other()
		{
			Case c = new Case("female_large_chest_is_visible");
			Assert.AreEqual("female_large_chest_is_visible", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_LargeChestIsVisible_Target()
		{
			Case c = new Case("female_large_chest_is_visible");
			c.Target = _female.FolderName;
			_female.Size = "large";
			Assert.AreEqual("stripped", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_ChestIsVisible_Other()
		{
			Case c = new Case("female_chest_is_visible");
			Assert.AreEqual("female_chest_is_visible", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_ChestIsVisible_Target()
		{
			Case c = new Case("female_chest_is_visible");
			c.Target = _female.FolderName;
			Assert.AreEqual("stripped", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Opponent_ChestIsVisible_Other()
		{
			Case c = new Case("opponent_chest_is_visible");
			Assert.AreEqual("opponent_chest_is_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Opponent_ChestIsVisible_Target()
		{
			Case c = new Case("opponent_chest_is_visible");
			c.Target = _male.FolderName;
			Assert.AreEqual("stripped", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_CrotchWillBeVisible_Other()
		{
			Case c = new Case("male_crotch_will_be_visible");
			Assert.AreEqual("male_crotch_will_be_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_CrotchWillBeVisible_Target()
		{
			Case c = new Case("male_crotch_will_be_visible");
			c.Target = _male.FolderName;
			Assert.AreEqual("stripping", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Female_CrotchWillBeVisible_Other()
		{
			Case c = new Case("female_crotch_will_be_visible");
			Assert.AreEqual("female_crotch_will_be_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Female_CrotchWillBeVisible_Target()
		{
			Case c = new Case("female_crotch_will_be_visible");
			c.Target = _female.FolderName;
			Assert.AreEqual("stripping", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Opponent_CrotchWillBeVisible_Other()
		{
			Case c = new Case("opponent_crotch_will_be_visible");
			Assert.AreEqual("opponent_crotch_will_be_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Opponent_CrotchWillBeVisible_Target()
		{
			Case c = new Case("opponent_crotch_will_be_visible");
			c.Target = _female.FolderName;
			Assert.AreEqual("stripping", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_CrotchIsVisible_Other()
		{
			Case c = new Case("female_crotch_is_visible");
			Assert.AreEqual("female_crotch_is_visible", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_CrotchIsVisible_Target()
		{
			Case c = new Case("female_crotch_is_visible");
			c.Target = _female.FolderName;
			Assert.AreEqual("stripped", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Male_SmallCrotchIsVisible_Other()
		{
			Case c = new Case("male_small_crotch_is_visible");
			Assert.AreEqual("male_small_crotch_is_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_SmallCrotchIsVisible_Target()
		{
			Case c = new Case("male_small_crotch_is_visible");
			c.Target = _male.FolderName;
			_male.Size = "small";
			Assert.AreEqual("stripped", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_MediumCrotchIsVisible_Other()
		{
			Case c = new Case("male_medium_crotch_is_visible");
			Assert.AreEqual("male_medium_crotch_is_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_MediumCrotchIsVisible_Target()
		{
			Case c = new Case("male_medium_crotch_is_visible");
			c.Target = _male.FolderName;
			_male.Size = "medium";
			Assert.AreEqual("stripped", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_LargeCrotchIsVisible_Other()
		{
			Case c = new Case("male_large_crotch_is_visible");
			Assert.AreEqual("male_large_crotch_is_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_LargeCrotchIsVisible_Target()
		{
			Case c = new Case("male_large_crotch_is_visible");
			c.Target = _male.FolderName;
			_male.Size = "large";
			Assert.AreEqual("stripped", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_CrotchIsVisible_Other()
		{
			Case c = new Case("male_crotch_is_visible");
			Assert.AreEqual("male_crotch_is_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_CrotchIsVisible_Target()
		{
			Case c = new Case("male_crotch_is_visible");
			c.Target = _male.FolderName;
			Assert.AreEqual("stripped", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Opponent_CrotchIsVisible_Other()
		{
			Case c = new Case("opponent_crotch_is_visible");
			Assert.AreEqual("opponent_crotch_is_visible", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Opponent_CrotchIsVisible_Target()
		{
			Case c = new Case("opponent_crotch_is_visible");
			c.Target = _male.FolderName;
			Assert.AreEqual("stripped", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_MustMasturbate_Other()
		{
			Case c = new Case("male_must_masturbate");
			Assert.AreEqual("male_must_masturbate", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_MustMasturbate_Target()
		{
			Case c = new Case("male_must_masturbate");
			c.Target = _male.FolderName;
			Assert.AreEqual("must_masturbate", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Female_MustMasturbate_Other()
		{
			Case c = new Case("female_must_masturbate");
			Assert.AreEqual("female_must_masturbate", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_MustMasturbate_Target()
		{
			Case c = new Case("female_must_masturbate");
			c.Target = _female.FolderName;
			Assert.AreEqual("must_masturbate", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Male_StartMasturbating_Other()
		{
			Case c = new Case("male_start_masturbating");
			Assert.AreEqual("male_start_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_StartMasturbating_Target()
		{
			Case c = new Case("male_start_masturbating");
			c.Target = _male.FolderName;
			Assert.AreEqual("start_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Female_StartMasturbating_Other()
		{
			Case c = new Case("female_start_masturbating");
			Assert.AreEqual("female_start_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_StartMasturbating_Target()
		{
			Case c = new Case("female_start_masturbating");
			c.Target = _female.FolderName;
			Assert.AreEqual("start_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Opponent_StartMasturbating_Other()
		{
			Case c = new Case("opponent_start_masturbating");
			Assert.AreEqual("opponent_start_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Opponent_StartMasturbating_Target()
		{
			Case c = new Case("opponent_start_masturbating");
			c.Target = _male.FolderName;
			Assert.AreEqual("start_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_Masturbating_Other()
		{
			Case c = new Case("male_masturbating");
			Assert.AreEqual("male_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_Masturbating_Target()
		{
			Case c = new Case("male_masturbating");
			c.Target = _male.FolderName;
			Assert.AreEqual("masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Female_Masturbating_Other()
		{
			Case c = new Case("female_masturbating");
			Assert.AreEqual("female_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_Masturbating_Target()
		{
			Case c = new Case("female_masturbating");
			c.Target = _female.FolderName;
			Assert.AreEqual("masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Opponent_Masturbating_Other()
		{
			Case c = new Case("opponent_masturbating");
			Assert.AreEqual("opponent_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Opponent_Masturbating_Target()
		{
			Case c = new Case("opponent_masturbating");
			c.Target = _female.FolderName;
			Assert.AreEqual("masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Male_Heavy_Masturbating_Other()
		{
			Case c = new Case("male_heavy_masturbating");
			Assert.AreEqual("male_heavy_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_Heavy_Masturbating_Target()
		{
			Case c = new Case("male_heavy_masturbating");
			c.Target = _male.FolderName;
			Assert.AreEqual("heavy_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Female_Heavy_Masturbating_Other()
		{
			Case c = new Case("female_heavy_masturbating");
			Assert.AreEqual("female_heavy_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_Heavy_Masturbating_Target()
		{
			Case c = new Case("female_heavy_masturbating");
			c.Target = _female.FolderName;
			Assert.AreEqual("heavy_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Opponent_Heavy_Masturbating_Other()
		{
			Case c = new Case("opponent_heavy_masturbating");
			Assert.AreEqual("opponent_heavy_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Opponent_Heavy_Masturbating_Target()
		{
			Case c = new Case("opponent_heavy_masturbating");
			c.Target = _female.FolderName;
			Assert.AreEqual("heavy_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Male_Finished_Masturbating_Other()
		{
			Case c = new Case("male_finished_masturbating");
			Assert.AreEqual("male_finished_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Male_Finished_Masturbating_Target()
		{
			Case c = new Case("male_finished_masturbating");
			c.Target = _male.FolderName;
			Assert.AreEqual("finished_masturbating", c.GetResponseTag(_female, _male));
		}

		[TestMethod]
		public void Female_Finished_Masturbating_Other()
		{
			Case c = new Case("female_finished_masturbating");
			Assert.AreEqual("female_finished_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Female_Finished_Masturbating_Target()
		{
			Case c = new Case("female_finished_masturbating");
			c.Target = _female.FolderName;
			Assert.AreEqual("finished_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Opponent_Finished_Masturbating_Other()
		{
			Case c = new Case("opponent_finished_masturbating");
			Assert.AreEqual("opponent_finished_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void Opponent_Finished_Masturbating_Target()
		{
			Case c = new Case("opponent_finished_masturbating");
			c.Target = _female.FolderName;
			Assert.AreEqual("finished_masturbating", c.GetResponseTag(_male, _female));
		}

		[TestMethod]
		public void StageLimit_RemovingExtra()
		{
			Case c = new Case("female_removing_accessory");
			c.Target = _female.FolderName;
			Case response = c.CreateResponse(_male, _female);
			Assert.AreEqual(1, response.Stages.Count);
			Assert.AreEqual(0, response.Stages[0]);
		}

		[TestMethod]
		public void StageLimit_RemovedExtra()
		{
			Case c = new Case("female_removed_accessory");
			c.Target = _female.FolderName;
			Case response = c.CreateResponse(_male, _female);
			Assert.AreEqual(1, response.Stages.Count);
			Assert.AreEqual(1, response.Stages[0]);
		}

		[TestMethod]
		public void StageLimit_RemovingMinor()
		{
			Case c = new Case("female_removing_minor");
			c.Target = _female.FolderName;
			Case response = c.CreateResponse(_male, _female);
			Assert.AreEqual(1, response.Stages.Count);
			Assert.AreEqual(1, response.Stages[0]);
		}

		[TestMethod]
		public void StageLimit_RemovedMinor()
		{
			Case c = new Case("female_removed_minor");
			c.Target = _female.FolderName;
			Case response = c.CreateResponse(_male, _female);
			Assert.AreEqual(1, response.Stages.Count);
			Assert.AreEqual(2, response.Stages[0]);
		}

		[TestMethod]
		public void StageLimit_RemovingMajor()
		{
			Case c = new Case("female_removing_major");
			c.Target = _female.FolderName;
			Case response = c.CreateResponse(_male, _female);
			Assert.AreEqual(1, response.Stages.Count);
			Assert.AreEqual(2, response.Stages[0]);
		}

		[TestMethod]
		public void StageLimit_RemovedMajor()
		{
			Case c = new Case("female_removed_major");
			c.Target = _female.FolderName;
			Case response = c.CreateResponse(_male, _female);
			Assert.AreEqual(1, response.Stages.Count);
			Assert.AreEqual(3, response.Stages[0]);
		}

		[TestMethod]
		public void StageLimit_ChestWillBeVisible()
		{
			Case c = new Case("female_chest_will_be_visible");
			c.Target = _female.FolderName;
			Case response = c.CreateResponse(_male, _female);
			Assert.AreEqual(1, response.Stages.Count);
			Assert.AreEqual(3, response.Stages[0]);
		}

		[TestMethod]
		public void StageLimit_ChestIsVisible()
		{
			Case c = new Case("female_large_chest_is_visible");
			c.Target = _female.FolderName;
			Case response = c.CreateResponse(_male, _female);
			Assert.AreEqual(1, response.Stages.Count);
			Assert.AreEqual(4, response.Stages[0]);
		}

		[TestMethod]
		public void StageLimit_CrotchWillBeVisible()
		{
			Case c = new Case("female_crotch_will_be_visible");
			c.Target = _female.FolderName;
			Case response = c.CreateResponse(_male, _female);
			Assert.AreEqual(1, response.Stages.Count);
			Assert.AreEqual(4, response.Stages[0]);
		}

		[TestMethod]
		public void StageLimit_CrotchIsVisible()
		{
			Case c = new Case("female_crotch_is_visible");
			c.Target = _female.FolderName;
			Case response = c.CreateResponse(_male, _female);
			Assert.AreEqual(1, response.Stages.Count);
			Assert.AreEqual(5, response.Stages[0]);
		}

		#region Testing the various response transfers. Too lazy to make individual property tests, so doing everything in bulk for all major combinations
		[TestMethod]
		public void NotTargetable_NoTarget_WithAlsoPlaying()
		{
			Case c = new Case("good_hand");
			c.AlsoPlaying = "other";
			Case response = c.CreateResponse(_male, _female);
			Assert.IsTrue(response.Conditions.Count == 1);
			Assert.AreEqual("other", response.Conditions[0].FilterTag);
		}

		[TestMethod]
		public void Targetable_NoTarget_WithAlsoPlaying()
		{
			Case c = new Case("female_removing_accessory");
			c.AlsoPlaying = "other";
			Case response = c.CreateResponse(_male, _female);
			Assert.IsTrue(response.Conditions.Count == 1);
			Assert.AreEqual("other", response.Conditions[0].FilterTag);
		}

		[TestMethod]
		public void Targetable_TargetResponder_WithAlsoPlaying()
		{
			Case c = new Case("female_removing_accessory");
			c.Target = _female.FolderName;
			c.AlsoPlaying = "other";
			Case response = c.CreateResponse(_male, _female);
			Assert.IsTrue(response.Conditions.Count == 1);
			Assert.AreEqual("other", response.Conditions[0].FilterTag);
		}

		[TestMethod]
		public void Targetable_Target_WithAlsoPlaying()
		{
			Case c = new Case("female_removing_accessory");
			c.Target = "other1";
			c.AlsoPlaying = "other2";
			Case response = c.CreateResponse(_male, _female);
			Assert.IsTrue(response.Conditions.Count == 1);
			Assert.AreEqual("other2", response.Conditions[0].FilterTag);
		}

		[TestMethod]
		public void Targetable_NoTarget_NoAlsoPlaying()
		{
			Case c = new Case("female_removing_accessory");
			c.TotalExposed = "5";
			c.TotalFemales = "4";
			c.TotalMales = "3-5";
			c.TotalFinished = "2";
			Case response = c.CreateResponse(_male, _female);

			Assert.AreEqual(_female.Layers + Clothing.ExtraStages, response.Stages.Count);
			Assert.AreEqual(_male.FolderName, response.AlsoPlaying);
			Assert.AreEqual(c.TotalFinished, response.TotalFinished);
			Assert.AreEqual(c.TotalFemales, response.TotalFemales);
			Assert.AreEqual(c.TotalMales, response.TotalMales);
			Assert.AreEqual(c.TotalExposed, response.TotalExposed);
		}

		[TestMethod]
		public void NotTargetable_NoTarget_NoAlsoPlaying()
		{
			Case c = new Case("good_hand");
			c.TotalExposed = "5";
			c.TotalFemales = "4";
			c.TotalMales = "3-5";
			c.TotalFinished = "2";
			Case response = c.CreateResponse(_male, _female);

			Assert.AreEqual(_female.Layers + 1, response.Stages.Count);
			Assert.AreEqual(_male.FolderName, response.AlsoPlaying);
			Assert.AreEqual(c.TotalFinished, response.TotalFinished);
			Assert.AreEqual(c.TotalFemales, response.TotalFemales);
			Assert.AreEqual(c.TotalMales, response.TotalMales);
			Assert.AreEqual(c.TotalExposed, response.TotalExposed);
		}

		[TestMethod]
		public void Targetable_NoTarget_AlsoPlayingResponder()
		{
			Case c = new Case("female_removing_accessory");
			c.TimeInStage = "5";
			c.Stages.Add(2);
			c.Stages.Add(3);
			c.SaidMarker = "foo";
			c.NotSaidMarker = "bar";
			c.HasHand = "hand";

			c.AlsoPlaying = _female.FolderName;
			c.AlsoPlayingHand = "good";
			c.AlsoPlayingStage = "2-4";
			c.AlsoPlayingSaidMarker = "foo2";
			c.AlsoPlayingNotSaidMarker = "bar2";
			c.AlsoPlayingTimeInStage = "5";

			Case response = c.CreateResponse(_male, _female);

			Assert.AreEqual(_male.FolderName, response.AlsoPlaying);
			Assert.AreEqual(c.TimeInStage, response.AlsoPlayingTimeInStage);
			Assert.AreEqual("2-3", response.AlsoPlayingStage);
			Assert.AreEqual(c.SaidMarker, response.AlsoPlayingSaidMarker);
			Assert.AreEqual(c.NotSaidMarker, response.AlsoPlayingNotSaidMarker);
			Assert.AreEqual(c.HasHand, response.AlsoPlayingHand);

			Assert.AreEqual(3, response.Stages.Count);
			Assert.IsTrue(response.Stages.Contains(2));
			Assert.IsTrue(response.Stages.Contains(3));
			Assert.IsTrue(response.Stages.Contains(4));
			Assert.AreEqual(c.AlsoPlayingHand, response.HasHand);
			Assert.AreEqual(c.AlsoPlayingSaidMarker, response.SaidMarker);
			Assert.AreEqual(c.AlsoPlayingNotSaidMarker, response.NotSaidMarker);
			Assert.AreEqual(c.AlsoPlayingTimeInStage, response.TimeInStage);
		}

		[TestMethod]
		public void NotTargetable_NoTarget_AlsoPlayingResponder()
		{
			Case c = new Case("good_hand");
			c.TimeInStage = "5";
			c.Stages.Add(2);
			c.Stages.Add(3);
			c.SaidMarker = "foo";
			c.NotSaidMarker = "bar";
			c.HasHand = "hand";

			c.AlsoPlaying = _female.FolderName;
			c.AlsoPlayingHand = "good";
			c.AlsoPlayingStage = "2-4";
			c.AlsoPlayingSaidMarker = "foo2";
			c.AlsoPlayingNotSaidMarker = "bar2";
			c.AlsoPlayingTimeInStage = "5";

			Case response = c.CreateResponse(_male, _female);

			Assert.AreEqual(_male.FolderName, response.AlsoPlaying);
			Assert.AreEqual(c.TimeInStage, response.AlsoPlayingTimeInStage);
			Assert.AreEqual("2-3", response.AlsoPlayingStage);
			Assert.AreEqual(c.SaidMarker, response.AlsoPlayingSaidMarker);
			Assert.AreEqual(c.NotSaidMarker, response.AlsoPlayingNotSaidMarker);
			Assert.AreEqual(c.HasHand, response.AlsoPlayingHand);

			Assert.AreEqual(3, response.Stages.Count);
			Assert.IsTrue(response.Stages.Contains(2));
			Assert.IsTrue(response.Stages.Contains(3));
			Assert.IsTrue(response.Stages.Contains(4));
			Assert.AreEqual(c.AlsoPlayingHand, response.HasHand);
			Assert.AreEqual(c.AlsoPlayingSaidMarker, response.SaidMarker);
			Assert.AreEqual(c.AlsoPlayingNotSaidMarker, response.NotSaidMarker);
			Assert.AreEqual(c.AlsoPlayingTimeInStage, response.TimeInStage);
		}

		[TestMethod]
		public void NotTargetable_TargetableResponse_NoAlsoPlaying()
		{
			Case c = new Case("must_strip_normal");
			c.TimeInStage = "5";
			c.Stages.Add(2);
			c.Stages.Add(3);
			c.SaidMarker = "foo";
			c.NotSaidMarker = "bar";
			c.HasHand = "hand";

			Case response = c.CreateResponse(_male, _female);

			Assert.AreEqual(_male.FolderName, response.Target);
			Assert.AreEqual(_female.Layers + Clothing.ExtraStages, response.Stages.Count);
			Assert.AreEqual(c.TimeInStage, response.TargetTimeInStage);
			Assert.AreEqual("2-3", response.TargetStage);
			Assert.AreEqual(c.SaidMarker, response.TargetSaidMarker);
			Assert.AreEqual(c.NotSaidMarker, response.TargetNotSaidMarker);
			Assert.AreEqual(c.HasHand, response.TargetHand);

			Assert.AreEqual(_female.Layers + Clothing.ExtraStages, response.Stages.Count);
			Assert.IsNull(response.AlsoPlaying);
		}

		[TestMethod]
		public void NotTargetable_TargetableResponse_WithAlsoPlaying()
		{
			Case c = new Case("must_strip_normal");
			c.TimeInStage = "5";
			c.Stages.Add(2);
			c.Stages.Add(3);
			c.SaidMarker = "foo";
			c.NotSaidMarker = "bar";
			c.HasHand = "hand";

			c.AlsoPlaying = "other";
			c.AlsoPlayingHand = "good";
			c.AlsoPlayingStage = "2-4";
			c.AlsoPlayingSaidMarker = "foo2";
			c.AlsoPlayingNotSaidMarker = "bar2";
			c.AlsoPlayingTimeInStage = "5";

			Case response = c.CreateResponse(_male, _female);

			Assert.AreEqual(_male.FolderName, response.Target);
			Assert.AreEqual(_female.Layers + Clothing.ExtraStages, response.Stages.Count);
			Assert.AreEqual(c.TimeInStage, response.TargetTimeInStage);
			Assert.AreEqual("2-3", response.TargetStage);
			Assert.AreEqual(c.SaidMarker, response.TargetSaidMarker);
			Assert.AreEqual(c.NotSaidMarker, response.TargetNotSaidMarker);
			Assert.AreEqual(c.HasHand, response.TargetHand);

			Assert.AreEqual(c.AlsoPlaying, response.AlsoPlaying);
			Assert.AreEqual(c.AlsoPlayingStage, response.AlsoPlayingStage);
			Assert.AreEqual(c.AlsoPlayingHand, response.AlsoPlayingHand);
			Assert.AreEqual(c.AlsoPlayingSaidMarker, response.AlsoPlayingSaidMarker);
			Assert.AreEqual(c.AlsoPlayingNotSaidMarker, response.AlsoPlayingNotSaidMarker);
			Assert.AreEqual(c.AlsoPlayingTimeInStage, response.AlsoPlayingTimeInStage);
		}

		[TestMethod]
		public void Targetable_Target_NoAlsoPlaying()
		{
			Case c = new Case("female_removing_accessory");
			c.TimeInStage = "5";
			c.Stages.Add(2);
			c.Stages.Add(3);
			c.SaidMarker = "foo";
			c.NotSaidMarker = "bar";
			c.HasHand = "hand";

			c.Target = "other";
			c.TargetHand = "good";
			c.TargetStage = "2-4";
			c.TargetSaidMarker = "foo2";
			c.TargetNotSaidMarker = "bar2";
			c.TargetTimeInStage = "5";
			c.TargetStatus = "blah";
			c.TargetLayers = "2-4";
			c.TargetStartingLayers = "1-3";

			Case response = c.CreateResponse(_male, _female);

			Assert.AreEqual(_female.Layers + Clothing.ExtraStages, response.Stages.Count);

			Assert.AreEqual(_male.FolderName, response.AlsoPlaying);
			Assert.AreEqual(c.TimeInStage, response.AlsoPlayingTimeInStage);
			Assert.AreEqual("2-3", response.AlsoPlayingStage);
			Assert.AreEqual(c.SaidMarker, response.AlsoPlayingSaidMarker);
			Assert.AreEqual(c.NotSaidMarker, response.AlsoPlayingNotSaidMarker);
			Assert.AreEqual(c.HasHand, response.AlsoPlayingHand);

			Assert.AreEqual(c.Target, response.Target);
			Assert.AreEqual(c.TargetStage, response.TargetStage);
			Assert.AreEqual(c.TargetHand, response.TargetHand);
			Assert.AreEqual(c.TargetSaidMarker, response.TargetSaidMarker);
			Assert.AreEqual(c.TargetNotSaidMarker, response.TargetNotSaidMarker);
			Assert.AreEqual(c.TargetTimeInStage, response.TargetTimeInStage);
			Assert.AreEqual(c.TargetStatus, response.TargetStatus);
			Assert.AreEqual(c.TargetLayers, response.TargetLayers);
			Assert.AreEqual(c.TargetStartingLayers, response.TargetStartingLayers);
		}

		[TestMethod]
		public void Targetable_TargetResponder_NoAlsoPlaying()
		{
			Case c = new Case("female_removing_accessory");
			c.TimeInStage = "5";
			c.Stages.Add(2);
			c.Stages.Add(3);
			c.SaidMarker = "foo";
			c.NotSaidMarker = "bar";
			c.HasHand = "hand";

			c.Target = _female.FolderName;
			c.TargetHand = "good";
			c.TargetStage = "2-4";
			c.TargetSaidMarker = "foo2";
			c.TargetNotSaidMarker = "bar2";
			c.TargetTimeInStage = "5";
			c.TargetStatus = "blah";
			c.TargetLayers = "2-4";
			c.TargetStartingLayers = "1-3";

			Case response = c.CreateResponse(_male, _female);
			
			Assert.AreEqual(3, response.Stages.Count);
			Assert.IsTrue(response.Stages.Contains(2));
			Assert.IsTrue(response.Stages.Contains(3));
			Assert.IsTrue(response.Stages.Contains(4));
			Assert.AreEqual(c.TargetHand, response.HasHand);
			Assert.AreEqual(c.TargetSaidMarker, response.SaidMarker);
			Assert.AreEqual(c.TargetNotSaidMarker, response.NotSaidMarker);
			Assert.AreEqual(c.TimeInStage, response.TimeInStage);

			Assert.AreEqual(_male.FolderName, response.AlsoPlaying);
			Assert.AreEqual(c.TimeInStage, response.AlsoPlayingTimeInStage);
			Assert.AreEqual("2-3", response.AlsoPlayingStage);
			Assert.AreEqual(c.SaidMarker, response.AlsoPlayingSaidMarker);
			Assert.AreEqual(c.NotSaidMarker, response.AlsoPlayingNotSaidMarker);
			Assert.AreEqual(c.HasHand, response.AlsoPlayingHand);
		}

		[TestMethod]
		public void Targetable_Target_AlsoPlayingResponder()
		{
			Case c = new Case("female_removing_accessory");
			c.TimeInStage = "5";
			c.Stages.Add(2);
			c.Stages.Add(3);
			c.SaidMarker = "foo";
			c.NotSaidMarker = "bar";
			c.HasHand = "hand";

			c.Target = "other";
			c.TargetHand = "good";
			c.TargetStage = "2-4";
			c.TargetSaidMarker = "foo2";
			c.TargetNotSaidMarker = "bar2";
			c.TargetTimeInStage = "5";
			c.TargetStatus = "blah";
			c.TargetLayers = "2-4";
			c.TargetStartingLayers = "1-3";

			c.AlsoPlaying = _female.FolderName;
			c.AlsoPlayingHand = "good";
			c.AlsoPlayingStage = "2-4";
			c.AlsoPlayingSaidMarker = "foo2";
			c.AlsoPlayingNotSaidMarker = "bar2";
			c.AlsoPlayingTimeInStage = "5";

			Case response = c.CreateResponse(_male, _female);
			
			Assert.AreEqual(_male.FolderName, response.AlsoPlaying);
			Assert.AreEqual(c.TimeInStage, response.AlsoPlayingTimeInStage);
			Assert.AreEqual("2-3", response.AlsoPlayingStage);
			Assert.AreEqual(c.SaidMarker, response.AlsoPlayingSaidMarker);
			Assert.AreEqual(c.NotSaidMarker, response.AlsoPlayingNotSaidMarker);
			Assert.AreEqual(c.HasHand, response.AlsoPlayingHand);

			Assert.AreEqual(c.Target, response.Target);
			Assert.AreEqual(c.TargetStage, response.TargetStage);
			Assert.AreEqual(c.TargetHand, response.TargetHand);
			Assert.AreEqual(c.TargetSaidMarker, response.TargetSaidMarker);
			Assert.AreEqual(c.TargetNotSaidMarker, response.TargetNotSaidMarker);
			Assert.AreEqual(c.TargetTimeInStage, response.TargetTimeInStage);
			Assert.AreEqual(c.TargetStatus, response.TargetStatus);
			Assert.AreEqual(c.TargetLayers, response.TargetLayers);
			Assert.AreEqual(c.TargetStartingLayers, response.TargetStartingLayers);

			Assert.AreEqual(3, response.Stages.Count);
			Assert.IsTrue(response.Stages.Contains(2));
			Assert.IsTrue(response.Stages.Contains(3));
			Assert.IsTrue(response.Stages.Contains(4));
			Assert.AreEqual(c.AlsoPlayingHand, response.HasHand);
			Assert.AreEqual(c.AlsoPlayingSaidMarker, response.SaidMarker);
			Assert.AreEqual(c.AlsoPlayingNotSaidMarker, response.NotSaidMarker);
			Assert.AreEqual(c.AlsoPlayingTimeInStage, response.TimeInStage);
		}
		#endregion
	}
}
