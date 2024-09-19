using UnityEngine;

namespace Omnis
{
    public abstract class VectorTweak
    {
        #region Vector2 to Vector3
        /// <returns>(x, y, 0)</returns>
        public static Vector3 V2ToV3xy(Vector2 v2) => new(v2.x, v2.y, 0f);
        /// <returns>(x, 0, y)</returns>
        public static Vector3 V2ToV3xz(Vector2 v2) => new(v2.x, 0f, v2.y);
        #endregion

        #region Change One Value in Vector3
        /// <returns>(x, y, n)</returns>
        public static Vector3 xyn(Vector3 v, float n) => new(v.x, v.y, n);
        /// <returns>(x1, y1, z2)</returns>
        public static Vector3 xyn(Vector3 v1, Vector3 v2) => new(v1.x, v1.y, v2.z);
        /// <returns>(x, n, z)</returns>
        public static Vector3 xnz(Vector3 v, float n) => new(v.x, n, v.z);
        /// <returns>(x1, y2, z1)</returns>
        public static Vector3 xnz(Vector3 v1, Vector3 v2) => new(v1.x, v2.y, v1.z);
        /// <returns>(n, y, z)</returns>
        public static Vector3 nyz(Vector3 v, float n) => new(n, v.y, v.z);
        /// <returns>(x2, y1, z1)</returns>
        public static Vector3 nyz(Vector3 v1, Vector3 v2) => new(v2.x, v1.y, v1.z);
        /// <returns>(x, y, 0)</returns>
        public static Vector3 xyo(Vector3 v) => xyn(v, 0);
        /// <returns>(x, 0, y)</returns>
        public static Vector3 xoz(Vector3 v) => xnz(v, 0);
        /// <returns>(0, y, z)</returns>
        public static Vector3 oyz(Vector3 v) => nyz(v, 0);
        #endregion

        #region Round
        public static Vector3 Round(Vector3 v) => new(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
        public static Vector3 GridSnap(Vector3 v, float increment = 1f) => increment == 0f ? v : increment * Round(v / increment);
        #endregion
    }
}
