#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Omnis.Flowchart.Relationship
{
    [CustomEditor(typeof(RelNode))]
    [CanEditMultipleObjects]
    public class RelNodeCustomEditor : NodeCustomEditor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorTweak.Script(target, typeof(RelNode));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("opaque"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("inSlots"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("outSlots"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("nodeSprites"), true);
            var mode = serializedObject.FindProperty("mode");
            EditorGUILayout.PropertyField(mode, true);
            (target as Node).Mode = (NodeMode)mode.enumValueIndex;
            EditorGUI.indentLevel++;
            {
                if (serializedObject.FindProperty("nodeSprites").arraySize < System.Enum.GetNames(typeof(NodeMode)).Length)
                    EditorGUILayout.HelpBox("NodeSprites must have at least " + System.Enum.GetNames(typeof(NodeMode)).Length + " elements.", MessageType.Error);
                else
                    EditorGUILayout.ObjectField("Sprite", serializedObject.FindProperty("nodeSprites").GetArrayElementAtIndex(mode.enumValueIndex).objectReferenceValue, typeof(Sprite), false);
            }
            EditorGUI.indentLevel--;

            EditorTweak.Header("Text");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("display"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("description"), true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
