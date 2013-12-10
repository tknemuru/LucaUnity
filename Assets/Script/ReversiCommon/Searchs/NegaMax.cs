using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReversiCommon.Collections;
using ReversiCommon.Evaluators;
using TKCommon.Utility;
using System.Diagnostics;
using ReversiCommon.Utility;

namespace ReversiCommon.Searchs
{
    /// <summary>
    /// <para>NegaMax法探索クラス</para>
    /// </summary>
    public class NegaMax : NegaMaxBase<int>
    {
        #region "定数"
        /// <summary>
        /// キーの初期値
        /// </summary>
        private const int DEFAULT_KEY = 99;
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>評価クラスのインスタンスリスト</para>
        /// </summary>
        private static List<IEvaluator> m_Evaluators;

        /// <summary>
        /// 位置評価インスタンス
        /// </summary>
        private static PointEvaluator m_PointEvaluator = new PointEvaluator(false);
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        public NegaMax(int limit, List<IEvaluator> evaluator)
            : base(limit)
        {
            m_Evaluators = evaluator;
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        public NegaMax(int limit, List<IEvaluator> evaluator, PointEvaluator pointEv)
            : base(limit)
        {
            m_Evaluators = evaluator;
            m_PointEvaluator = pointEv;
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// 深さ制限に達した場合にはTrueを返す
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        protected override bool IsLimit(int limit)
        {
            return (limit == this.m_LimitDepth || TurnKeeper.IsGameEnd());
        }

        /// <summary>
        /// 評価値を取得する
        /// </summary>
        /// <returns></returns>
        protected override double GetEvaluate()
        {
            double score = 0;
            foreach (IEvaluator ev in m_Evaluators)
            {
                score += ev.GetEvaluate();
            }

            return (score * this.Parity);
        }

        /// <summary>
        /// 全てのリーフを取得する
        /// </summary>
        /// <returns></returns>
        protected override List<int> GetAllLeaf()
        {
            return Board.GetAllPutableIndex();
        }

        /// <summary>
        /// ソートをする場合はTrueを返す
        /// </summary>
        /// <returns></returns>
        protected override bool IsOrdering(int depth)
        {
            return (depth <= 3);
        }

        /// <summary>
        /// ソートする
        /// </summary>
        /// <param name="allLeaf"></param>
        /// <returns></returns>
        protected override List<int> MoveOrdering(List<int> allLeaf)
        {
            Dictionary<int, int> mobilityDic = new Dictionary<int, int>();
            foreach (int leaf in allLeaf)
            {
                Board.Reverse(leaf);
                mobilityDic.Add(leaf, (Board.GetAllPutablePointerCount(TurnKeeper.NowTurnColor) + (int)(m_PointEvaluator.GetEvaluate() * this.Parity)));
                Board.UndoReverse();
            }

            return allLeaf.OrderByDescending(item => mobilityDic[item]).ToList();
        }

        /// <summary>
        /// キーの初期値を取得する
        /// </summary>
        /// <returns></returns>
        protected override int GetDefaultKey()
        {
            return DEFAULT_KEY;
        }

        /// <summary>
        /// 探索の前処理を行う
        /// </summary>
        protected override void SearchSetUp(int leaf)
        {
            // 手を打つ
            Board.Reverse(leaf);
            // 初回は回転方法を決める
            if (TurnKeeper.NowTurnNumber == 1)
            {
                RotateUtility.SetRotateMethod(leaf);
            }

            // ターンの変更
            TurnKeeper.ChangeTurn();
        }

        /// <summary>
        /// 探索の後処理を行う
        /// </summary>
        protected override void SearchTearDown()
        {
            // 手を戻す
            Board.UndoReverse();
            // ターンの変更
            TurnKeeper.UndoTurn();
        }

        /// <summary>
        /// パスの前処理を行う
        /// </summary>
        protected override void PassSetUp()
        {
            // 色の変更
            TurnKeeper.ChangeOnlyColor();
        }

        /// <summary>
        /// パスの後処理を行う
        /// </summary>
        protected override void PassTearDown()
        {
            // 色を戻しておく
            TurnKeeper.ChangeOnlyColor();
        }

        /// <summary>
        /// パリティ
        /// </summary>
        private double Parity
        {
            get
            {
                return (TurnKeeper.NowTurnColor == Color.Black) ? 1D : -1D;
            }
        }
        #endregion
        #endregion
    }
}
