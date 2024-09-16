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
                if (value.Length > 50) description = value.Substring(0, 50);
                else description = value;
                display.text = description;
            }
        }
        #endregion
    }
}
