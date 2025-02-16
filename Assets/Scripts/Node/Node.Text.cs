using UnityEngine;

namespace Omnis.Flowchart
{
    public partial class Node
    {
        #region Serialized Fields
        public TMPro.TMP_Text display;
        [SerializeField] private string description;
        #endregion

        #region Properties
        public string Description
        {
            get => description;
            set
            {
                if (syncked == true) return;
                syncked = true;

                description = value;
                display.text = description;

                if (copy) copy.Description = value;
                syncked = false;
            }
        }
        #endregion
    }
}
