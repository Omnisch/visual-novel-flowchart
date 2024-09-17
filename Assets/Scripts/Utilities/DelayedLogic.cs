namespace Omnis
{
    public class DelayedLogic : Logic
    {
        #region Serialized fields
        [UnityEngine.Min(0)] public float delayTime;
        #endregion

        #region Interfaces
        public override void Invoke() => StartCoroutine(InvokingCoroutine());
        #endregion

        #region Functions
        private System.Collections.IEnumerator InvokingCoroutine()
        {
            yield return new UnityEngine.WaitForSecondsRealtime(delayTime);
            callback.Invoke();
        }
        #endregion
    }
}
