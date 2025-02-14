using System.Runtime.InteropServices;
using UnityEngine;

namespace Omnis.Flowchart.Kinship
{
    public class KinshipHandler : MonoBehaviour
    {
        #region Serialized fields
        [SerializeField] private string query;
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Public functions
        public void GetRelationship()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                Debug.Log(Marshal.PtrToStringAuto(Relationship(query)));
            }
            else
                Debug.Log("Not supported on this platform.");
        }
        #endregion

        #region Functions
        [DllImport("__Internal")]
        private static extern System.IntPtr Relationship(string query);
        #endregion

        #region Unity methods
        #endregion
    }
}
