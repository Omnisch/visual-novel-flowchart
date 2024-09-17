using UnityEditor;
using UnityEngine;

namespace Omnis
{
    public class EditorTweak : Editor
    {
        public static void Script(Object target, System.Type classType)
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)System.Convert.ChangeType(target, classType)), classType, false);
            GUI.enabled = true;
        }
        public static void Header(string header)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
        }
    }
}
