using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using ReversiCommon.Collections;

namespace LucaUnityCommon.UI
{
    /// <summary>
    /// UIリバーシ盤
    /// </summary>
    public static class UIBoard
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// 石リスト
        /// </summary>
        private static Dictionary<int, GameObject> m_DiscList;

        /// <summary>
        /// 色リスト
        /// </summary>
        private static Dictionary<int, ReversiCommon.Collections.Color> m_ColorList;

        /// <summary>
        /// 着手可能マークリスト
        /// </summary>
        private static Dictionary<int, GameObject> m_MobilityList;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// コンストラクタ
        /// </summary>
        static UIBoard()
        {
            m_DiscList = new Dictionary<int, GameObject>();
            m_ColorList = new Dictionary<int, ReversiCommon.Collections.Color>();
            m_MobilityList = new Dictionary<int, GameObject>();
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// UI盤を初期化する
        /// </summary>
        public static void InitializeUIBoard(Func<ReversiCommon.Collections.Color, int, GameObject> loadDisc, Func<int, GameObject> loadMobilityMark)
        {
            UpdateUIBoard(loadDisc, loadMobilityMark, Board.GetInitializeColor);
        }

        /// <summary>
        /// UI盤をアップデートする
        /// </summary>
        public static void UpdateUIBoard(Func<ReversiCommon.Collections.Color, int, GameObject> loadDisc, Func<int, GameObject> loadMobilityMark)
        {
            UpdateUIBoard(loadDisc, loadMobilityMark, Board.GetSquareColor);
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// 石を画面上にロードする
        /// </summary>
        /// <param name="point"></param>
        private static bool AddDisc(int point, Func<ReversiCommon.Collections.Color, int, GameObject> loadDisc, ReversiCommon.Collections.Color color)
        {
            if (m_DiscList.ContainsKey(point)) { return false; }

            m_DiscList.Add(point, loadDisc(color, point));
            m_ColorList.Add(point, color);
            return true;
        }

        /// <summary>
        /// 石を反転させる
        /// </summary>
        /// <param name="point"></param>
        private static void ReverseDisc(int point, Func<ReversiCommon.Collections.Color, int, GameObject> loadDisc)
        {
            if (Board.GetSquareColor(point) != m_ColorList[point])
            {
                float rotate = (Board.GetSquareColor(point) == ReversiCommon.Collections.Color.White) ? 180f : 0f;
                m_DiscList[point].transform.rotation = Quaternion.Euler(rotate, 0, 0);
                m_ColorList[point] = m_ColorList[point].OppositeColor;
            }
        }

        /// <summary>
        /// 着手可能マークをクリアする
        /// </summary>
        private static void ClearMobilityMark()
        {
            foreach (KeyValuePair<int, GameObject> mark in m_MobilityList)
            {
                GameObject.Destroy(mark.Value);
            }
            m_MobilityList = new Dictionary<int, GameObject>();
        }

        /// <summary>
        /// 着手可能マークを画面上にロードする
        /// </summary>
        /// <param name="point"></param>
        private static void AddMobilityMark(int point, Func<int, GameObject> loadMobilityMark)
        {
            m_MobilityList.Add(point, loadMobilityMark(point));
        }

        /// <summary>
        /// UI盤をアップデートする
        /// </summary>
        private static void UpdateUIBoard(Func<ReversiCommon.Collections.Color, int, GameObject> loadDisc, Func<int, GameObject> loadMobilityMark, Func<int, ReversiCommon.Collections.Color> getColor)
        {
            UIBoard.ClearMobilityMark();
            for (int h = 1; h <= 8; h++)
            {
                for (int w = 1; w <= 8; w++)
                {
                    int point = ((h * 10) + w);
                    if (getColor(point) != ReversiCommon.Collections.Color.Space)
                    {
                        AddDisc(point, loadDisc, getColor(point));
                        ReverseDisc(point, loadDisc);
                    }
                    else if (Board.IsReversibleThisPoint(point))
                    {
                        AddMobilityMark(point, loadMobilityMark);
                    }
                }
            }
        }
        #endregion
        #endregion
    }
}
