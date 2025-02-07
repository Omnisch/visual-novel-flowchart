using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Omnis.Flowchart.Relationship
{
    public class RelNode : Node
    {
        #region Serialized fields
        public Linkable leftSlot;
        public Linkable rightSlot;
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Public functions
        public override NodeMode Mode
        {
            get => base.Mode;
            set
            {
                base.Mode = value;
                if (leftSlot)
                    leftSlot.gameObject.SetActive(value switch
                    {
                        NodeMode.LeftOut => true,
                        _ => false
                    });
                if (rightSlot)
                    rightSlot.gameObject.SetActive(value switch
                    {
                        NodeMode.RightOut => true,
                        _ => false
                    });
            }
        }

        public void LeftOutMode()
        {
            rightSlot.BreakAll();
            Mode = NodeMode.LeftOut;
        }
        public void RightOutMode()
        {
            leftSlot.BreakAll();
            Mode = NodeMode.RightOut;
        }
        #endregion

        #region Functions
        [DllImport("__Internal")]
        private static extern void relationship();
        #endregion
    }
}
