using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.Flowchart
{
    public class FloatLinkSlot : Linkable
    {
        #region Fields
        private LinkSlot targetSlot;
        #endregion

        #region Interfaces
        public override bool IsLeftPressed
        {
            get => base.IsLeftPressed;
            set
            {
                base.IsLeftPressed = value;
                if (!value) TryPassLink();
            }
        }
        #endregion

        #region Functions
        protected override void OnInteracted(List<Collider> hits)
        {
            hits.Select(hit => hit.gameObject).ToList().Find(hit => hit.TryGetComponent(out targetSlot));
        }
        private void TryPassLink()
        {
            bool succeeded = false;
            if (targetSlot && (targetSlot.key == key))
            {
                if (inLinks.Count > 0)
                    succeeded = targetSlot.TryAcceptInLink(inLinks[0]);
                else if (outLinks.Count > 0)
                    succeeded = targetSlot.TryAcceptOutLink(outLinks[0]);
            }
            else if (!targetSlot)
            {
                var newNode = GameManager.Instance.registry.NewNode();
                if (inLinks.Count > 0)
                {
                    newNode.transform.position = transform.position - newNode.inSlots[key].transform.localPosition;
                    succeeded = (newNode.inSlots[key] as LinkSlot).TryAcceptInLink(inLinks[0]);
                }
                else if (outLinks.Count > 0)
                {
                    newNode.transform.position = transform.position - newNode.outSlots[key].transform.localPosition;
                    succeeded = (newNode.outSlots[key] as LinkSlot).TryAcceptOutLink(outLinks[0]);
                }
            }

            if (!succeeded) BreakAll();
            Destroy(gameObject);
        }
        #endregion

        #region Unity Methods
        private void Update()
        {
            transform.position = VectorTweaker.xyn(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), transform.position.z);
            UpdateLinks();
        }
        #endregion
    }
}
