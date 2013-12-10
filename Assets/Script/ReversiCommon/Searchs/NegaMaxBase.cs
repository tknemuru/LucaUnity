using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using TKCommon.Utility;
using System.Diagnostics;
using ReversiCommon.Collections;

namespace ReversiCommon.Searchs
{
    /// <summary>
    /// <para>NegaMax法探索ベースクラス</para>
    /// </summary>
    public abstract class NegaMaxBase<T> : ISearch<T>
    {
        #region "定数"
        /// <summary>
        /// <para>初期アルファ値</para>
        /// </summary>
        protected const double DEFAULT_ALPHA = double.MinValue;

        /// <summary>
        /// <para>初期ベータ値</para>
        /// </summary>
        protected const double DEFAULT_BETA = double.MaxValue;
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>探索する深さ</para>
        /// </summary>
        protected int m_LimitDepth;

        /// <summary>
        /// 返却するキー
        /// </summary>
        protected T m_Key;

        /// <summary>
        /// 評価値
        /// </summary>
        public double Value { get; private set; }
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        public NegaMaxBase(int limit)
        {
            this.m_LimitDepth = limit;
            this.m_Key = this.GetDefaultKey();
            this.Value = DEFAULT_ALPHA;
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>最善手を探索して取得する</para>
        /// </summary>
        /// <returns></returns>
        public T SearchBestValue()
        {
            this.Value = this.SearchBestValue(1, DEFAULT_ALPHA, DEFAULT_BETA);
            return this.m_Key;
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// <para>最善手を探索して取得する</para>
        /// </summary>
        /// <returns></returns>
        protected double SearchBestValue(int depth, double alpha, double beta)
        {
            // 深さ制限に達した
            if (this.IsLimit(depth)) { return this.GetEvaluate(); }

            // 可能な手をすべて生成
            List<T> leafList = this.GetAllLeaf();

            double maxKeyValue = DEFAULT_ALPHA;
            if (leafList.Count > 0)
            {
                // ソート
                if (this.IsOrdering(depth)) { leafList = this.MoveOrdering(leafList); }

                foreach (T leaf in leafList)
                {
                    // 前処理
                    this.SearchSetUp(leaf);

                    double value = this.SearchBestValue(depth + 1, -beta, -alpha) * -1.0D;
                    
                    // 後処理
                    this.SearchTearDown();

                    // ベータ刈り
                    if (value >= beta)
                    {
                        this.SetKey(leaf, depth);
                        return value;
                    }

                    if (value > maxKeyValue)
                    {
                        // より良い手が見つかった
                        this.SetKey(leaf, depth);
                        maxKeyValue = value;
                        // α値の更新
                        alpha = MathUtility.Max<double>(alpha, maxKeyValue);
                    }
                }
            }
            else
            {
                // ▼パスの場合▼
                // 前処理
                this.PassSetUp();

                maxKeyValue = this.SearchBestValue(depth + 1, -beta, -alpha) * -1.0D;

                // 後処理
                this.PassTearDown();
            }

            Debug.Assert(((maxKeyValue != DEFAULT_ALPHA) && (maxKeyValue != DEFAULT_BETA)), "デフォルト値のまま返そうとしています。");
            return maxKeyValue;
        }

        /// <summary>
        /// 返却するキーをセットする
        /// </summary>
        /// <param name="leaf"></param>
        private void SetKey(T leaf, int depth)
        {
            if (depth == 1)
            {
                this.m_Key = leaf;
            }
        }

        /// <summary>
        /// 深さ制限に達した場合にはTrueを返す
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        protected abstract bool IsLimit(int limit);

        /// <summary>
        /// 評価値を取得する
        /// </summary>
        /// <returns></returns>
        protected abstract double GetEvaluate();

        /// <summary>
        /// 全てのリーフを取得する
        /// </summary>
        /// <returns></returns>
        protected abstract List<T> GetAllLeaf();

        /// <summary>
        /// ソートをする場合はTrueを返す
        /// </summary>
        /// <returns></returns>
        protected abstract bool IsOrdering(int depth);

        /// <summary>
        /// ソートする
        /// </summary>
        /// <param name="allLeaf"></param>
        /// <returns></returns>
        protected abstract List<T> MoveOrdering(List<T> allLeaf);

        /// <summary>
        /// キーの初期値を取得する
        /// </summary>
        /// <returns></returns>
        protected abstract T GetDefaultKey();

        /// <summary>
        /// 探索の前処理を行う
        /// </summary>
        protected abstract void SearchSetUp(T leaf);

        /// <summary>
        /// 探索の後処理を行う
        /// </summary>
        protected abstract void SearchTearDown();

        /// <summary>
        /// パスの前処理を行う
        /// </summary>
        protected abstract void PassSetUp();

        /// <summary>
        /// パスの後処理を行う
        /// </summary>
        protected abstract void PassTearDown();
        #endregion
        #endregion
    }
}
