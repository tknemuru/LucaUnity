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
    /// ランダム性を持たせたCPU戦略クラス
    /// </summary>
    public class StrategyMeasurementCpu : StrategyCpu
    {
        #region "定数"
        /// <summary>
        /// ランダム評価を行うターン数
        /// </summary>
        private const int RANDOM_TURN = 3;
        #endregion

        #region "メンバ変数"
        ///// <summary>
        ///// スコアインデックスファイルパス
        ///// </summary>
        //private string m_ScoreIndexFilePath;

        /// <summary>
        /// スコアインデックス評価クラス
        /// </summary>
        private ScoreIndexEvaluator m_ScoreIndexEv;

        /// <summary>
        /// 座標評価クラス
        /// </summary>
        private PointEvaluator m_PointEv;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="scoreIndexFilePath"></param>
        public StrategyMeasurementCpu(string scoreIndexFilePath, double cornerScore, double xScore)
        {
            this.m_ScoreIndexEv = new ScoreIndexEvaluator(scoreIndexFilePath);
            this.m_PointEv = new PointEvaluator(cornerScore, xScore);
        }
        #endregion

        #region "メソッド"
        /// <summary>
        /// <para>次に打つ手を取得する</para>
        /// </summary>
        /// <returns></returns>
        public override Disc GetNextDisc()
        {
            this.SetSearchLimitDepth();
            double score = BookEvaluator.NOTHING_VALUE;
            int ret = 99;

            ISearch<int> search = new NegaMax(this.m_SearchLimitDepth, this.GetEvaluators(), this.m_PointEv);
            if (score == BookEvaluator.NOTHING_VALUE)
            {
                ret = search.SearchBestValue();
                score = search.Value;
            }

            if (TurnKeeper.NowTurnNumber >= FINAL_SEARCH_START_TURN)
            {
                ReversiInformation.Add("黒：" + ((int)score).ToString() + "石でゲーム終了予定です。");
            }
            else
            {
                ReversiInformation.Add("スコア：" + ((int)(score * 1000)).ToString() + "点の場所に打ちました。");
            }
            return new Disc(ret, TurnKeeper.NowTurnColor);
        }

        /// <summary>
        /// <para>使用する評価関数のインスタンスを取得する</para>
        /// </summary>
        /// <returns></returns>
        protected override List<IEvaluator> GetEvaluators()
        {
            List<IEvaluator> evs = new List<IEvaluator>();
            if (TurnKeeper.NowTurnNumber <= RANDOM_TURN)
            {
                evs.Add(this.m_PointEv);
                evs.Add(this.m_ScoreIndexEv);
                evs.Add(new RandomEvaluator());
                return evs;
            }
            else if (TurnKeeper.NowTurnNumber < StrategyCpu.FINAL_SEARCH_START_TURN)
            {
                evs.Add(this.m_PointEv);
                evs.Add(this.m_ScoreIndexEv);
                return evs;
            }
            else
            {
                evs.Add(new FinalEvaluator());
                return evs;
            }
        }
        #endregion
    }
}
