using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.Flowchart
{
    public class FloatLinkSlot : Linkable
    {
        #region Interfaces
        public void TryPassLink()
        {
            bool succeeded = false;
            if (GameManager.Instance.TargetSlot)
            {
                if (inLinks.Count > 0)
                    succeeded = GameManager.Instance.TargetSlot.TryAcceptInLink(inLinks[0]);
                else if (outLinks.Count > 0)
                    succeeded = GameManager.Instance.TargetSlot.TryAcceptOutLink(outLinks[0]);
            }
            else
            {
                var newNode = GameManager.Instance.nodeRegistry.NewNode();
                if (inLinks.Count > 0)
                {
                    newNode.transform.position = transform.position - newNode.inSlot.transform.localPosition;
                    succeeded = (newNode.inSlot as LinkSlot).TryAcceptInLink(inLinks[0]);
                }
                else if (outLinks.Count > 0)
                {
                    newNode.transform.position = transform.position - newNode.outSlot.transform.localPosition;
                    succeeded = (newNode.outSlot as LinkSlot).TryAcceptOutLink(outLinks[0]);
                }
            }

            if (!succeeded) BreakAll();
            Destroy(gameObject);
        }
        #endregion

        #region Unity Methods
        private void Update()
        {
            transform.position = VectorTweak.xyn(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), transform.position.z);
            UpdateLinks();
        }
        #endregion
    }
}
