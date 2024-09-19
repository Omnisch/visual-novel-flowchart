using OdinSerializer;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Omnis.Flowchart
{
    public class IO : MonoBehaviour
    {
        #region Serialized Fields
        #endregion

        #region Fields
        private string filePath;
        #endregion

        #region Interfaces
        public void GetFilePath()
        {
            var bp = new AnotherFileBrowser.Windows.BrowserProperties
            {
                filter = "Visual novel flowchart files (*.txt, *.vnf) | *.txt; *.vnf",
                filterIndex = 0
            };

            new AnotherFileBrowser.Windows.FileBrowser().OpenFileBrowser(bp, path => filePath = path);
        }
        
        public void SaveFile()
        {
            byte[] bytes = SerializationUtility.SerializeValue(GameManager.Instance.nodeRegistry.NodeData, DataFormat.JSON);
            File.WriteAllBytes(filePath, bytes);
        }
        public void LoadFile()
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            SerializationUtility.DeserializeValue<List<NodeData>>(bytes, DataFormat.JSON);
        }
        #endregion

        #region Functions
        #endregion
    }
}
