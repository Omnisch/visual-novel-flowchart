// author: Omnistudio
// version: 2024.10.28

using UnityEngine;

namespace Omnis
{
    /// <summary>
    /// Auxiliary functions of UnityEngine.Vector2 and UnityEngine.Vector3
    /// </summary>
    public abstract class VectorTweak
    {
        #region Vector2 to Angle
        public static float V2ToRadians(Vector2 v2) => Mathf.Atan2(v2.y, v2.x);
        public static float V2ToDegrees(Vector2 v2) => Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        #endregion

        #region Vector2 to Vector3
        /// <returns>(x, y, 0)</returns>
        public static Vector3 V2ToV3xy(Vector2 v2) => new(v2.x, v2.y, 0f);
        /// <returns>(x, 0, y)</returns>
        public static Vector3 V2ToV3xz(Vector2 v2) => new(v2.x, 0f, v2.y);
        #endregion

        #region Vector3 to Vector2
        /// <returns>(x, y)</returns>
        public static Vector2 XY(Vector3 v3) => new(v3.x, v3.y);
        /// <returns>(x, z)</returns>
        public static Vector2 XZ(Vector3 v3) => new(v3.x, v3.z);
        /// <returns>(y, z)</returns>
        public static Vector2 YZ(Vector3 v3) => new(v3.y, v3.z);
        #endregion

        #region Change One Value in Vector3
        /// <returns>(x, y, n)</returns>
        public static Vector3 XYN(Vector3 v, float n) => new(v.x, v.y, n);
        /// <returns>(x1, y1, z2)</returns>
        public static Vector3 XYN(Vector3 v1, Vector3 v2) => new(v1.x, v1.y, v2.z);
        /// <returns>(x, n, z)</returns>
        public static Vector3 XNZ(Vector3 v, float n) => new(v.x, n, v.z);
        /// <returns>(x1, y2, z1)</returns>
        public static Vector3 XNZ(Vector3 v1, Vector3 v2) => new(v1.x, v2.y, v1.z);
        /// <returns>(n, y, z)</returns>
        public static Vector3 NYZ(Vector3 v, float n) => new(n, v.y, v.z);
        /// <returns>(x2, y1, z1)</returns>
        public static Vector3 NYZ(Vector3 v1, Vector3 v2) => new(v2.x, v1.y, v1.z);
        /// <returns>(x, y, 0)</returns>
        public static Vector3 XYO(Vector3 v) => XYN(v, 0);
        /// <returns>(x, 0, y)</returns>
        public static Vector3 XOZ(Vector3 v) => XNZ(v, 0);
        /// <returns>(0, y, z)</returns>
        public static Vector3 OYZ(Vector3 v) => NYZ(v, 0);
        #endregion

        #region Round
        public static Vector3 Round(Vector3 v) => new(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
        public static Vector3 GridSnap(Vector3 v, float increment = 1f) => increment == 0f ? v : increment * Round(v / increment);
        #endregion
    }
}
