using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Omnis.Flowchart
{
    public class NodeContextMenu : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private GameObject itemPrefab;
        #endregion

        #region Fields
        #endregion

        #region Interfaces
        #endregion

        #region Functions
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            foreach (var entry in GameManager.Instance.gameSettings.contextMenuRegistry)
            {
                if (GameManager.Instance.ActiveNode)
                    if (GameManager.Instance.ActiveNode.TryGetComponent(System.Type.GetType(entry.typeName), out var target))
                    {
                        var item = Instantiate(itemPrefab, transform).GetComponent<Button>();
                        item.GetComponentInChildren<TMP_Text>().text = entry.label;
                        item.onClick.AddListener(() => { target.SendMessage(entry.message, SendMessageOptions.DontRequireReceiver); });
                    }
            }
        }
        private void OnDisable()
        {
            for (int i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
        }
        #endregion
    }
}
