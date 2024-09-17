using UnityEditor;
using UnityEngine;

namespace Omnis.BranchTracker
{
    [CustomEditor(typeof(Node))]
    [CanEditMultipleObjects]
    public class NodeCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorTweak.Script(target, typeof(Node));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("opaque"), true);
            var mode = serializedObject.FindProperty("mode");
            EditorGUILayout.PropertyField(mode, true);
            EditorGUI.indentLevel++;
            switch ((NodeMode)mode.enumValueIndex)
            {
                case NodeMode.Island:
                    break;
                case NodeMode.Root:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("outSlot"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("addChildButton"), true);
                    break;
                case NodeMode.Branch:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("inSlot"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("removeButton"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("outSlot"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("addChildButton"), true);
                    break;
                case NodeMode.Leaf:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("inSlot"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("removeButton"), true);
                    break;
            }
            if (serializedObject.FindProperty("nodeSprites").arraySize < System.Enum.GetNames(typeof(NodeMode)).Length)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("nodeSprites"), true);
                EditorGUILayout.HelpBox("NodeSprites must have at least " + System.Enum.GetNames(typeof(NodeMode)).Length + " elements.", MessageType.Error);
            }
            else
                EditorGUILayout.ObjectField("Sprite", serializedObject.FindProperty("nodeSprites").GetArrayElementAtIndex(mode.enumValueIndex).objectReferenceValue, typeof(Sprite), false);
            EditorGUI.indentLevel--;

            EditorTweak.Header("Offset");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("childOffset"), true);

            EditorTweak.Header("Text");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("display"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("description"), true);

            serializedObject.ApplyModifiedProperties();
            (target as Node).UpdateMode();
        }
    }
}