// author: Omnistudio
// version: 2024.10.28

#if !UNITY_WEBGL
using AnotherFileBrowser.Windows;
#else
using System.Collections;
using System.Runtime.InteropServices;
#endif
using OdinSerializer;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Omnis
{
    /// <summary>
    /// Requiring packages: AnotherFileBrowser, OdinSerializer.
    /// </summary>
    public class IO : MonoBehaviour
    {
        #region Interfaces
#if UNITY_WEBGL
        // JS to Unity
        private static byte[] rawData;
        public void SetRawData(string rawData) => IO.rawData = System.Convert.FromBase64String(rawData);


        // Unity to JS
        [DllImport("__Internal")]
        private static extern void OpenHTMLFilePicker();


        public static void OpenBrowserAndSaveFile<T>(T dataToSave, string extensionFilter) {}
        public static void OpenBrowserAndLoadFile<T>(UnityAction<T> callback, string extensionFilter)
        {
            StartCoroutine(IOpenBrowserAndLoadFile(callback, extensionFilter));
        }
        private static IEnumerator IOpenBrowserAndLoadFile<T>(UnityAction<T> callback, string extensionFilter)
        {
            rawData = null;
            OpenHTMLFilePicker();
            yield return new WaitUntil(() => rawData != null);
            callback?.Invoke(DeserializeData<T>(rawData));
        }
#else
        /// <param name="extensionFilter">Format: "description (*.ext1, *.ext2) | *.ext1; *.ext2"</param>
        public static void OpenBrowserAndSaveFile<T>(T dataToSave, string extensionFilter)
        {
            BrowserProperties bp = new() { filter = extensionFilter, filterIndex = 0 };

            new FileBrowser().OpenFileBrowser(bp, path => SaveFile(dataToSave, path));
        }
        /// <param name="extensionFilter">Format: "description (*.ext1, *.ext2) | *.ext1; *.ext2"</param>
        public static void OpenBrowserAndLoadFile<T>(UnityAction<T> callback, string extensionFilter)
        {
            BrowserProperties bp = new() { filter = extensionFilter, filterIndex = 0 };

            T data = default;
            new FileBrowser().OpenFileBrowser(bp, path => data = LoadFile<T>(path));

            callback?.Invoke(data);
        }
#endif
        #endregion

        #region Functions
        private static void SaveFile<T>(T dataToSave, string path)
        {
            byte[] bytes = SerializationUtility.SerializeValue(dataToSave, DataFormat.JSON);
            File.WriteAllBytes(path, bytes);
        }
        private static T LoadFile<T>(string path) => DeserializeData<T>(File.ReadAllBytes(path));
        private static T DeserializeData<T>(byte[] bytes) => SerializationUtility.DeserializeValue<T>(bytes, DataFormat.JSON);
        #endregion
    }
}
