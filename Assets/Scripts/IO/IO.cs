// author: Omnistudio
// version: 2024.10.28

#if !UNITY_WEBGL
using AnotherFileBrowser.Windows;
#else
using System;
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
    public partial class IO : MonoBehaviour
    {
        #region Fields
        private static readonly string extensionFilter = "Visual novel flowchart saves (*.json) | *.json";
        #endregion

        #region Interfaces
#if UNITY_WEBGL
        // JS to Unity
        private static byte[] rawData;
        private void SetRawDataFromJavaScript(string rawData) => IO.rawData = System.Convert.FromBase64String(rawData);


        // Unity to JS
        [DllImport("__Internal")]
        private static extern void HtmlOpenFilePicker();
        [DllImport("__Internal")]
        private static extern void HtmlSaveFile(string fileName, IntPtr dataPtr, int dataLength);


        public static void OpenBrowserAndSaveFile<T>(T dataT)
        {
            byte[] data = SerializeData(dataT);

            IntPtr dataPtr = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, dataPtr, data.Length);

            HtmlSaveFile($"save-{DateTime.Now:yyyyMMdd-HHmmss}.json", dataPtr, data.Length);

            Marshal.FreeHGlobal(dataPtr);
        }
        public static void OpenBrowserAndLoadFile<T>(UnityAction<T> callback)
        {
            Instance.StartCoroutine(IOpenBrowserAndLoadFile(callback));
        }
        private static IEnumerator IOpenBrowserAndLoadFile<T>(UnityAction<T> callback)
        {
            rawData = null;
            HtmlOpenFilePicker();
            yield return new WaitUntil(() => rawData != null);
            callback?.Invoke(DeserializeData<T>(rawData));
        }
#else
        /// <param name="extensionFilter">Format: "description (*.ext1, *.ext2) | *.ext1; *.ext2"</param>
        public static void OpenBrowserAndSaveFile<T>(T dataToSave)
        {
            BrowserProperties bp = new() { filter = extensionFilter, filterIndex = 0 };

            new FileBrowser().OpenFileBrowser(bp, path => SaveFile(dataToSave, path));
        }
        /// <param name="extensionFilter">Format: "description (*.ext1, *.ext2) | *.ext1; *.ext2"</param>
        public static void OpenBrowserAndLoadFile<T>(UnityAction<T> callback)
        {
            BrowserProperties bp = new() { filter = extensionFilter, filterIndex = 0 };

            T data = default;
            new FileBrowser().OpenFileBrowser(bp, path => data = LoadFile<T>(path));

            callback?.Invoke(data);
        }
#endif
        #endregion

        #region Functions
        private static void SaveFile<T>(T dataT, string path) => File.WriteAllBytes(path, SerializeData(dataT));
        private static T LoadFile<T>(string path) => DeserializeData<T>(File.ReadAllBytes(path));
        private static byte[] SerializeData<T>(T dataT) => SerializationUtility.SerializeValue(dataT, DataFormat.JSON);
        private static T DeserializeData<T>(byte[] dataB) => SerializationUtility.DeserializeValue<T>(dataB, DataFormat.JSON);
        #endregion
    }
}
