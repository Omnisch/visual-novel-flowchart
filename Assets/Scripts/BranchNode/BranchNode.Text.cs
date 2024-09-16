using UnityEngine;
using TMPro;

namespace Omnis.BranchTracker
{
    public partial class BranchNode
    {
        #region Serialized Fields
        [Header("Text")]
        public TMP_Text display;
        [SerializeField] private string description;
        #endregion

        #region Interfaces
        public string Description
        {
            get => description;
            set
            {
                description = value;
                display.text = description;
            }
        }
        #endregion
    }
}
