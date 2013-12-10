using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using ReversiCommon.Collections;
using ReversiCommon.Utility;

namespace ReversiCommon.Evaluators
{
    /// <summary>
    /// <para>着手可能数評価ロジッククラス</para>
    /// </summary>
    public class MobilityEvaluator : IEvaluator
    {
        #region "定数"
        /// <summary>
        /// <para>着手可能数の配点割合</para>
        /// </summary>
        private const int RATIO_MOBILITY_SCORE = 10000;
        #endregion

        #region "メンバ変数"
        #endregion

        #region "コンストラクタ"
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

            // 着手可能数のスコア
            //int score = (Board.GetAllPutablePointer().Count() * RATIO_MOBILITY_SCORE) / TurnKeeper.NowTurnNumber;
            int score = (Board.GetAllPutablePointer().Count() * RATIO_MOBILITY_SCORE);

            // スコアは必ず黒基準にして返す
            int ret = (TurnKeeper.NowTurnColor == Color.Black) ? score : score * -1;

            //DebugUtility.StopWatch.Stop();
            //DebugUtility.AddEventTimes("MobilityEvaluator");

            return ret;
            //return score;
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
