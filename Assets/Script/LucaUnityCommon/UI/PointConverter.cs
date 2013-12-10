using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LucaUnityCommon.UI
{
    public static class PointConverter
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// Xの座標リスト
        /// </summary>
        private static List<float> m_XIndex;

        /// <summary>
        /// Zの座標リスト
        /// </summary>
        private static List<float> m_ZIndex;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// コンストラクタ
        /// </summary>
        static PointConverter()
        {
            m_XIndex = new List<float>()
			{
                0.78f,
                1.71f,
                2.65f,
                3.6f,
                4.55f,
                5.5f,
                6.45f,
                7.38f
			};

            m_ZIndex = new List<float>()
			{
				7.37f,
				6.38f,
				5.43f,
				4.48f,
				3.52f,
				2.55f,
				1.61f,
				0.68f
			};
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// X座標.
        /// </summary>
        /// <param name="orgX">Org x.</param>
        public static float X(float orgX)
        {
            Dictionary<int, float> differenceList = new Dictionary<int, float>();
            for (int i = 0; i < m_XIndex.Count; i++)
            {
                differenceList.Add(i, Math.Abs(m_XIndex[i] - orgX));
            }
            var orderdDiff = differenceList.OrderBy(item => item.Value).ToList();
            return m_XIndex[orderdDiff[0].Key];
        }

        /// <summary>
        /// Z座標.
        /// </summary>
        /// <param name="orgX">Org x.</param>
        public static float Z(float orgZ)
        {
            Dictionary<int, float> differenceList = new Dictionary<int, float>();
            for (int i = 0; i < m_ZIndex.Count; i++)
            {
                differenceList.Add(i, Math.Abs(m_ZIndex[i] - orgZ));
            }
            var orderdDiff = differenceList.OrderBy(item => item.Value).ToList();
            return m_ZIndex[orderdDiff[0].Key];
        }

        /// <summary>
        /// UI上の座標からリバーシゲーム上の座標を求める
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static int Point(float orgX, float orgZ)
        {
            // 念のため変換をかけておく
            orgX = X(orgX);
            orgZ = Z(orgZ);

            int x = (m_XIndex.IndexOf(orgX) + 1);
            int z = (m_ZIndex.IndexOf(orgZ) + 1);

            return ((z * 10) + x);
        }

        /// <summary>
        /// リバーシゲーム上の座標からUI上のX座標を求める
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static float PointToX(int point)
        {
            int x = ((point % 10) - 1);
            return m_XIndex[x];
        }

        /// <summary>
        /// リバーシゲーム上の座標からUI上のZ座標を求める
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static float PointToZ(int point)
        {
            int z = ((point / 10) - 1);
            return m_ZIndex[z];
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion        
    }
}
