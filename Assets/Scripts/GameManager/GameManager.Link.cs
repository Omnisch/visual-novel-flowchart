using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnis.BranchTracker
{
    public partial class GameManager
    {
        #region Fields
        private Linkable activeSlot;
        private bool slotTargeted;
        #endregion

        #region Interfaces
        public Linkable ActiveSlot
        {
            get => activeSlot;
            set
            {
                activeSlot = value;
            }
        }
        public bool SlotTargeted { get => slotTargeted; set => slotTargeted = value; }
        public void FreeActiveSlot()
        {
            if (!ActiveSlot) return;
            if (slotTargeted) return;

            
        }
        #endregion

        #region Functions
        #endregion
    }
}
