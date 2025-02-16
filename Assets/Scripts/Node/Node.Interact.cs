using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.Flowchart
{
    public partial class Node
    {
        #region Fields
        private Vector3 cursorOffset;
        #endregion

        #region Interfaces
        public override bool IsLeftPressed
        {
            get => base.IsLeftPressed;
            set
            {
                base.IsLeftPressed = value;
                if (value)
                {
                    cursorOffset = transform.position - Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                }
            }
        }
        public void OnUpdate()
        {
            if (IsLeftPressed)
            {
                transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + cursorOffset;
                transform.position = VectorTweaker.xyn(VectorTweaker.GridSnap(transform.position, GameManager.Instance.settings.gridSnapIncrement), transform.position);
                inSlots.ForEach((slot) => slot.UpdateLinks());
                outSlots.ForEach((slot) => slot.UpdateLinks());
            }
        }
        #endregion

        #region Functions
        protected override void OnInteracted(List<Collider> hits)
        {
            GameManager.Instance.registry.Prioritize(this);
            GameManager.Instance.ActiveNode = this;
        }
        #endregion
    }
}
