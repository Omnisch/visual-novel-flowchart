using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis
{
    public class PlayerInputHandler : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Logic debugLogic;
        [SerializeField] private bool stopAtFirstHit;
        #endregion

        #region Fields
        private List<Collider> PointerHits;
        #endregion

        #region Interfaces
        public void SetInputEnabled(bool value)
        {
            enabled = value;
            Cursor.visible = value;
        }
        #endregion

        #region Functions
        private void FlushInput() {}
        #endregion

        #region Unity Methods
        private void Start()
        {
            foreach (var map in playerInput.actions.actionMaps)
                map.Enable();

            PointerHits = new();
        }

        private void OnEnable()
        {
            playerInput.enabled = true;
            FlushInput();
        }

        private void OnDisable()
        {
            playerInput.enabled = false;
        }
        #endregion

        #region Handlers
        protected void OnInteract() => PointerHits.ForEach(hit => hit.SendMessage("OnInteract", options: SendMessageOptions.DontRequireReceiver));

        protected void OnPress() => PointerHits.ForEach(hit => hit.SendMessage("OnPress", options: SendMessageOptions.DontRequireReceiver));

        protected void OnRelease() => PointerHits.ForEach(hit => hit.SendMessage("OnRelease", options: SendMessageOptions.DontRequireReceiver));

        protected void OnScroll(InputValue value) => PointerHits.ForEach(hit => hit.SendMessage("OnScroll", value.Get<float>(), options: SendMessageOptions.DontRequireReceiver));

        protected void OnDebugTest() => debugLogic.Invoke();

        protected void OnPointer(InputValue value)
        {
            Ray r = Camera.main.ScreenPointToRay(value.Get<Vector2>());
            List<Collider> rawHits;
            if (stopAtFirstHit)
            {
                rawHits = new();
                if (Physics.Raycast(r, out RaycastHit hit))
                    rawHits.Add(hit.collider);
            }
            else
                rawHits = Physics.RaycastAll(r).Select(hit => hit.collider).ToList();

            foreach (var hit in PointerHits.Except(rawHits).ToList())
                if (hit) hit.SendMessage("OnPointerExit", options: SendMessageOptions.DontRequireReceiver);
            foreach (var hit in rawHits.Except(PointerHits).ToList())
                if (hit) hit.SendMessage("OnPointerEnter", options: SendMessageOptions.DontRequireReceiver);
            PointerHits = rawHits;
        }

        protected void OnEscape()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        #endregion
        }
    }
