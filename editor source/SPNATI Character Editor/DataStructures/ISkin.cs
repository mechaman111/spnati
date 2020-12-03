using Desktop;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// interface for classes that store pose files somewhere
	/// </summary>
	public interface ISkin : IRecord
	{
		/// Gets the name of the folder containing the images
		/// </summary>
		string FolderName { get; }
		/// <summary>
		/// Gets the full path to the skin's directory
		/// </summary>
		/// <returns></returns>
		string GetDirectory();
		/// <summary>
		/// Gets the full path to the skin's backup directory
		/// </summary>
		/// <returns></returns>
		string GetBackupDirectory();
		/// <summary>
		/// Gets the path where attachments should be stored
		/// </summary>
		/// <returns></returns>
		string GetAttachmentsDirectory();
		/// <summary>
		/// Gets the location of a pose
		/// </summary>
		/// <param name="subfolder"></param>
		/// <param name="poseName"></param>
		/// <param name="asset"></param>
		/// <returns></returns>
		string GetPosePath(string sheetName, string subfolder, string poseName, bool asset);
		/// <summary>
		/// Gets a list of pose names that the skin requires
		/// </summary>
		/// <returns></returns>
		HashSet<string> GetRequiredPoses(bool stageless);
		/// <summary>
		/// Associated character
		/// </summary>
		Character Character { get; }
		/// <summary>
		/// Gets a list of custom poses associated with this skin
		/// </summary>
		/// <returns></returns>
		List<Pose> CustomPoses { get; set; }
		ISkin Skin { get; }
		bool IsDirty { get; set; }
	}
}
