using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using ReversiCommon.Collections;
using ReversiCommon.Evaluators;
using ReversiCommon.Utility;

namespace ReversiCommon.Searchs
{
    /// <summary>
    /// 定石探索クラス
    /// </summary>
    public class BookSearch : ISearch<int>
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// 評価値
        /// </summary>
        public double Value { get; private set; }
        #endregion

        #region "コンストラクタ"
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>最善手を探索して取得する</para>
        /// </summary>
        /// <returns></returns>
        public int SearchBestValue()
        {
            List<int> leafList = Board.GetAllPutableIndex();
            Dictionary<int, double> valueList = new Dictionary<int, double>();
            BookEvaluator evaluator = new BookEvaluator(TurnKeeper.NowTurnNumber);

            foreach (int leaf in leafList)
            {
                // 手を打つ
                Board.Reverse(leaf);
                // 初回は回転方法を決める
                if (TurnKeeper.NowTurnNumber == 1)
                {
                    RotateUtility.SetRotateMethod(leaf);
                }

                double value = evaluator.GetEvaluate();
                valueList.Add(leaf, value);

                // 手を戻す
                Board.UndoReverse();
            }

            var orderdValue = (from a in valueList
                               select a).OrderByDescending(item => item.Value).ToList();
            
            this.Value = orderdValue[0].Value;
            return orderdValue[0].Key;
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
