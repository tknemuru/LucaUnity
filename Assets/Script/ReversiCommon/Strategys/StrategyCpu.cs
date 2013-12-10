using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReversiCommon.Collections;
using ReversiCommon.Searchs;
using ReversiCommon.Evaluators;
using ReversiCommon.Utility;

namespace ReversiCommon.Strategys
{
    /// <summary>
    /// <para>CPU用戦略</para>
    /// </summary>
    public class StrategyCpu : IStrategy
    {
        #region "定数"
        /// <summary>
        /// <para>探索する深さの初期設定値</para>
        /// </summary>
        private const int FIRST_SEARCH_LIMIT_DEPTH = 6;

        /// <summary>
        /// <para>探索する深さの２段階目設定値</para>
        /// </summary>
        private const int SECOND_SEARCH_LIMIT_DEPTH = 6;

        /// <summary>
        /// <para>探索する深さの最終段階設定値</para>
        /// </summary>
        private const int FINAL_SEARCH_LIMIT_DEPTH = 99;

        /// <summary>
        /// <para>完全読みを開始するターン</para>
        /// </summary>
        public const int FINAL_SEARCH_START_TURN = 48;
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>探索する深さ</para>
        /// </summary>
        protected int m_SearchLimitDepth;

        /// <summary>
        /// パリティ
        /// </summary>
        private double Parity
        {
            get { return (TurnKeeper.NowTurnColor == Color.Black) ? 1D : -1D; }
        }
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        public StrategyCpu()
        {
            this.SetSearchLimitDepth();
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>次に打つ手を取得する</para>
        /// </summary>
        /// <returns></returns>
        public virtual Disc GetNextDisc()
        {
            this.SetSearchLimitDepth();
            double score = BookEvaluator.NOTHING_VALUE;
            int ret = 99;
            //if (TurnKeeper.NowTurnNumber > 1 && TurnKeeper.NowTurnNumber <= 27)
            //{
            //    BookSearch bookSearch = new BookSearch();
            //    ret = bookSearch.SearchBestValue();
            //    score = bookSearch.Value;
            //    if (score != BookEvaluator.NOTHING_VALUE)
            //    {
            //        ReversiInformation.Add("定石を使用しました。");
            //    }
            //}

            ISearch<int> search = new NegaMax(this.m_SearchLimitDepth, this.GetEvaluators());
            if (score == BookEvaluator.NOTHING_VALUE)
            {
                ret = search.SearchBestValue();
                score = search.Value;
            }
            
            if (TurnKeeper.NowTurnNumber >= FINAL_SEARCH_START_TURN)
            {
                ReversiInformation.Add("黒：" + ((int)score * (int)this.Parity).ToString() + "石でゲーム終了予定です。");
            }
            else
            {
                ReversiInformation.Add("スコア：" + score.ToString("R") + "点の場所に打ちました。");
            }
            return new Disc(ret, TurnKeeper.NowTurnColor);
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// <para>探索する深さをセットする</para>
        /// </summary>
        protected void SetSearchLimitDepth()
        {
            if (TurnKeeper.NowTurnNumber <= 14)
            {
                this.m_SearchLimitDepth = FIRST_SEARCH_LIMIT_DEPTH;
            }
            else if (TurnKeeper.NowTurnNumber <= FINAL_SEARCH_START_TURN)
            {
                this.m_SearchLimitDepth = SECOND_SEARCH_LIMIT_DEPTH;
            }
            else
            {
                this.m_SearchLimitDepth = FINAL_SEARCH_LIMIT_DEPTH;
            }
        }

        /// <summary>
        /// <para>使用する評価関数のインスタンスを取得する</para>
        /// </summary>
        /// <returns></returns>
        protected virtual List<IEvaluator> GetEvaluators()
        {
            List<IEvaluator> evs = new List<IEvaluator>();
            if (TurnKeeper.NowTurnNumber < FINAL_SEARCH_START_TURN)
            {
                evs.Add(new ScoreIndexEvaluator());
                return evs;
            }
            else
            {
                evs.Add(new FinalEvaluator());
                return evs;
            }
        }
        #endregion
        #endregion
    }
}
