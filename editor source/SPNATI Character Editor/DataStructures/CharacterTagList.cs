using Desktop.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
    /// <summary>
    /// Data representation of tags.xml
    /// </summary>
    [XmlRoot("opponent")]
    public class CharacterTagList : BindableObject, IHookSerialization
    {
        [XmlArray("tags")]
        [XmlArrayItem("tag")]
        public List<CharacterTag> Tags
        {
            get { return Get<List<CharacterTag>>(); }
            set { Set(value); }
        }

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize(string source)
        {

        }
    }
}
