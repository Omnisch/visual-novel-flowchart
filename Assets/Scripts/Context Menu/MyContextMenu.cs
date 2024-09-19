using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Omnis.Flowchart
{
    [RequireComponent(typeof(RectTransform))]
    public class MyContextMenu : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private GameObject splitLinePrefab;
        public List<GameObject> targets;
        #endregion

        #region Functions
        private void UpdateRT()
        {
            RectTransform rt = GetComponent<RectTransform>();
            float menuHeight = 4f;
            for (int i = 0; i < transform.childCount; i++)
                menuHeight += transform.GetChild(i).GetComponent<RectTransform>().rect.height;
            rt.sizeDelta = new Vector2(200f, Mathf.Max(20f, menuHeight));
            var targetPos = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
            rt.pivot = new(
                targetPos.x > Screen.width - rt.sizeDelta.x ? 1f : 0f,
                targetPos.y < rt.sizeDelta.y ? 0f : 1f);
            transform.position = targetPos;
        }
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            foreach (var target in targets)
            {
                if (target != targets.First())
                    Instantiate(splitLinePrefab, transform);
                foreach (var entry in GameManager.Instance.gameSettings.contextMenuRegistry)
                    if (target && target.TryGetComponent(System.Type.GetType(entry.typeName), out _))
                    {
                        var item = Instantiate(itemPrefab, transform).GetComponent<Button>();
                        item.GetComponentInChildren<TMPro.TMP_Text>().text = entry.label;
                        item.onClick.AddListener(() => { target.SendMessage(entry.message, SendMessageOptions.DontRequireReceiver); });
                    }
            }
            UpdateRT();
        }
        private void OnDisable()
        {
            for (int i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
        }
        #endregion
    }
}
