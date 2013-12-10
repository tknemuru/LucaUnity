using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using ReversiCommon.Collections;
using System.Diagnostics;
using ReversiCommon.Utility;

namespace ReversiCommon.Evaluators
{
    /// <summary>
    /// <para>座標位置評価クラス</para>
    /// </summary>
    public class PointEvaluator : IEvaluator
    {
        #region "定数"
        /// <summary>
        /// <para>角の座標</para>
        /// </summary>
        private static readonly List<int> CORNER_POINT = new List<int>() { 11, 18, 81, 88 };

        /// <summary>
        /// <para>C打ち、X打ちの座標</para>
        /// </summary>
        //private static readonly List<List<int>> CX_POINT = new List<List<int>>() { new List<int>() { 12, 21, 22 }, new List<int>() { 17, 27, 28 }, new List<int>() { 71, 72, 82 }, new List<int>() { 78, 77, 87 } };
        private static readonly List<int> X_POINT = new List<int>() { 22, 27, 72, 77 };

        /// <summary>
        /// <para>角のスコア</para>
        /// </summary>
        private double CORNER_SCORE = 1;

        /// <summary>
        /// <para>C打ち、X打ちのスコア</para>
        /// </summary>
        private double CX_SCORE = 0;

        /// <summary>
        /// <para>一手目の調整レート</para>
        /// </summary>
        private const double FIRST_POINT_RATE = 1;
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>一手目を評価する場合はTrue</para>
        /// </summary>
        private bool m_IsFirstPoint;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        /// <param name="isFirstPoint"></param>
        public PointEvaluator(bool isFirstPoint)
        {
            m_IsFirstPoint = isFirstPoint;
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        /// <param name="isFirstPoint"></param>
        public PointEvaluator(double cornerScore, double xScore)
        {
            this.CORNER_SCORE = cornerScore;
            this.CX_SCORE = xScore;
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>評価値を取得</para>
        /// </summary>
        /// <returns></returns>
        public double GetEvaluate()
        {
            //DebugUtility.StopWatch.Start();

            double retScore = 0;
            for (int i = 0; i < CORNER_POINT.Count; i++)
            {
                var cornerBlack = from a in Board.Colors
                                  where CORNER_POINT[i] == a.Key
                                  && a.Value == Color.Black
                                  select a;

                var cornerWhite = from a in Board.Colors
                                  where CORNER_POINT[i] == a.Key
                                  && a.Value == Color.White
                                  select a;

                var cxBlack = from a in Board.Colors
                              where X_POINT[i] == a.Key
                              && a.Value == Color.Black
                              select a;

                var cxWhite = from a in Board.Colors
                              where X_POINT[i] == a.Key
                              && a.Value == Color.White
                              select a;

                Debug.Assert(((cornerBlack.Count() == 1 || cornerBlack.Count() == 0) && (cornerWhite.Count() == 1 || cornerWhite.Count() == 0)), "角の数が不正です。");

                //double blackScore = (cornerBlack.Count() * CORNER_SCORE) + (cxBlack.Count() * CX_SCORE * (cornerBlack.Count() - 1));
                //double whiteScore = ((cornerWhite.Count() * CORNER_SCORE) + (cxWhite.Count() * CX_SCORE * (cornerWhite.Count() - 1))) * -1;
                double blackScore = (((double)cornerBlack.Count() * CORNER_SCORE) + ((double)cxBlack.Count() * CX_SCORE));
                double whiteScore = (((double)cornerWhite.Count() * CORNER_SCORE) + ((double)cxWhite.Count() * CX_SCORE));
                retScore += (blackScore - whiteScore);
            }

            //DebugUtility.StopWatch.Stop();
            //DebugUtility.AddEventTimes("PointEvaluator");

            //if (this.m_IsFirstPoint)
            //{
            //    // 一手目の場合はスコアを上げる
            //    retScore = retScore * FIRST_POINT_RATE;
            //}

            return retScore;
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
