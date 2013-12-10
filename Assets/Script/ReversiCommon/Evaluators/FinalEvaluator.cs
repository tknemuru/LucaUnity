using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using ReversiCommon.Collections;
using ReversiCommon.Utility;

namespace ReversiCommon.Evaluators
{
    /// <summary>
    /// <para>完全読み評価クラス</para>
    /// </summary>
    public class FinalEvaluator : IEvaluator
    {
        #region "定数"
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
            // デバッグ
            //DebugUtility.OutputBoardToConsole();

            return Board.GetBlackCount();
            //if (Board.GetBlackCount() > 32)
            //{
            //    return 1;
            //}
            //else if (Board.GetBlackCount() < 32)
            //{
            //    return -1;
            //}
            //else
            //{
            //    return 0;
            //}
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
