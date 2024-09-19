using AnotherFileBrowser.Windows;
using OdinSerializer;
using System.IO;

namespace Omnis.Flowchart
{
    public class IO
    {
        #region Serialized Fields
        public static FlowchartData data;
        #endregion

        #region Interfaces
        public static void OpenFileBrowser(OpenFileBrowserOperation op)
        {
            var bp = new BrowserProperties
            {
                filter = "Visual novel flowchart files (*.vnfl, *.json) | *.vnfl; *.json",
                filterIndex = 0
            };

            new FileBrowser().OpenFileBrowser(bp, path => {
                switch (op) {
                    case OpenFileBrowserOperation.Save:
                        SaveFile(path); break;
                    case OpenFileBrowserOperation.Load:
                        LoadFile(path); break;
                }
            });
        }
        #endregion

        #region Functions
        private static void SaveFile(string path)
        {
            byte[] bytes = SerializationUtility.SerializeValue(GameManager.Instance.registry.Data, DataFormat.JSON);
            File.WriteAllBytes(path, bytes);
        }
        private static void LoadFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            data = SerializationUtility.DeserializeValue<FlowchartData>(bytes, DataFormat.JSON);
        }
        #endregion
    }

    public enum OpenFileBrowserOperation { Save, Load }
}
