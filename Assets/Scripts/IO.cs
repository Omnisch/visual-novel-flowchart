using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnis.Flowchart
{
    public class IO : MonoBehaviour
    {
        #region Serialized Fields
        #endregion

        #region Fields
        private string savePath;
        private string loadPath;
        #endregion

        #region Interfaces
        public void LoadFilePath()
        {
            var bp = new AnotherFileBrowser.Windows.BrowserProperties
            {
                filter = "Visual novel flowchart files (*.txt, *.vnf) | *.txt; *.vnf",
                filterIndex = 0
            };

            new AnotherFileBrowser.Windows.FileBrowser().OpenFileBrowser(bp, path => loadPath = path);
        }
        #endregion

        #region Functions
        #endregion
    }
}
