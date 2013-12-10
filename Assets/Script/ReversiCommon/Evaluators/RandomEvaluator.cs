using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace ReversiCommon.Evaluators
{
    /// <summary>
    /// ランダム評価クラス
    /// </summary>
    public class RandomEvaluator : IEvaluator
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// 乱数の種
        /// </summary>
        private int RANDOM_SEED = 10;
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
            Random rand = new Random();
            return (double)rand.Next(RANDOM_SEED);
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
