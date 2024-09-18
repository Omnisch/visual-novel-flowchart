using UnityEngine;
using TMPro;

namespace Omnis.Flowchart
{
    public partial class Node
    {
        #region Serialized Fields
        public TMP_Text display;
        public string description;
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