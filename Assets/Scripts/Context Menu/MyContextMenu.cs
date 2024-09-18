using UnityEngine;
using UnityEngine.UI;

namespace Omnis.Flowchart
{
    public class MyContextMenu : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private GameObject itemPrefab;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            foreach (var entry in GameManager.Instance.gameSettings.contextMenuRegistry)
                if (GameManager.Instance.ActiveNode)
                    if (GameManager.Instance.ActiveNode.TryGetComponent(System.Type.GetType(entry.typeName), out var target))
                    {
                        var item = Instantiate(itemPrefab, transform).GetComponent<Button>();
                        item.GetComponentInChildren<TMPro.TMP_Text>().text = entry.label;
                        item.onClick.AddListener(() => { target.SendMessage(entry.message, SendMessageOptions.DontRequireReceiver); });
                    }
            GetComponent<RectTransform>().sizeDelta = new Vector2(200f, Mathf.Max(20f, transform.childCount * 40f + 4f));
        }
        private void OnDisable()
        {
            for (int i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
        }
        #endregion
    }
}
