using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using ReversiCommon.Collections;
using ReversiCommon.Utility;

namespace ReversiCommon.Evaluators
{
    /// <summary>
    /// <para>開放度理論評価クラス</para>
    /// </summary>
    public class OpenTheoryEvaluator : IEvaluator
    {
        #region "定数"
        /// <summary>
        /// <para>角の座標</para>
        /// </summary>
        private static readonly List<int> CORNER_POINT = new List<int>() { 11, 18, 81, 88 };

        /// <summary>
        /// <para>開放度スコア</para>
        /// </summary>
        private const int OPEN_SCORE = -1000;
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>開放度リスト</para>
        /// </summary>
        private Dictionary<int, int> m_OpenScoreDic;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        public OpenTheoryEvaluator()
        {
            // 開放度リストの初期化
            this.InitializeOpenScoreDic();
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
            // 開放度リストの初期化
            this.InitializeOpenScoreDic();

            // 開放度リストを最新に更新
            this.UpdateOpenScoreDic();

            // デバッグ
            //DebugUtility.OutputBoardOpenScoreToConsole(this.m_OpenScoreDic);

            // 開放度（黒）を取得
            int blackScore = this.GetOpenScore(Color.Black);

            // 開放度（白）を取得
            int whiteScore = this.GetOpenScore(Color.White);

            return (blackScore - whiteScore) * OPEN_SCORE;
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// <para>開放度リストを初期化する</para>
        /// </summary>
        private void InitializeOpenScoreDic()
        {
            this.m_OpenScoreDic = new Dictionary<int, int>();
            for (int h = 1; h <= 8; h++)
            {
                for (int w = 1; w <= 8; w++)
                {
                    int point = ((h * 10) + w);
                    this.m_OpenScoreDic.Add(point, GetInitializeScore(point));
                }
            }
        }

        /// <summary>
        /// <para>初期化する開放度値を取得する</para>
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private int GetInitializeScore(int point)
        {
            if (CORNER_POINT.Contains(point))
            {
                // 角は開放度の初期値 = 3
                return 3;
            }
            else if (((point % 10) == 1) || ((point % 10) == 8)
                     || (point <= 18) || (point >= 81))
            {
                // 辺は開放度の初期値 = 5
                return 5;
            }
            else
            {
                // 通常は8
                return 8;
            }
        }

        /// <summary>
        /// <para>開放度リストを更新する</para>
        /// </summary>
        private void UpdateOpenScoreDic()
        {
            foreach (KeyValuePair<int, Color> point in Board.Colors)
            {
                if (point.Value != Color.Space)
                {
                    this.DiscPointed(point.Key);
                }
            }
        }

        /// <summary>
        /// <para>石を置いて開放度リストを更新する</para>
        /// </summary>
        /// <param name="point"></param>
        private void DiscPointed(int point)
        {
            if(this.m_OpenScoreDic.ContainsKey(point + (int)Board.DIRECTION.DOWN))
            {
                this.m_OpenScoreDic[point + (int)Board.DIRECTION.DOWN] -= 1;
            }

            if (this.m_OpenScoreDic.ContainsKey(point + (int)Board.DIRECTION.DOWN_LEFT))
            {
                this.m_OpenScoreDic[point + (int)Board.DIRECTION.DOWN_LEFT] -= 1;
            }

            if (this.m_OpenScoreDic.ContainsKey(point + (int)Board.DIRECTION.DOWN_RIGHT))
            {
                this.m_OpenScoreDic[point + (int)Board.DIRECTION.DOWN_RIGHT] -= 1;
            }

            if (this.m_OpenScoreDic.ContainsKey(point + (int)Board.DIRECTION.LEFT))
            {
                this.m_OpenScoreDic[point + (int)Board.DIRECTION.LEFT] -= 1;
            }

            if (this.m_OpenScoreDic.ContainsKey(point + (int)Board.DIRECTION.RIGHT))
            {
                this.m_OpenScoreDic[point + (int)Board.DIRECTION.RIGHT] -= 1;
            }

            if (this.m_OpenScoreDic.ContainsKey(point + (int)Board.DIRECTION.UP))
            {
                this.m_OpenScoreDic[point + (int)Board.DIRECTION.UP] -= 1;
            }

            if (this.m_OpenScoreDic.ContainsKey(point + (int)Board.DIRECTION.UP_LEFT))
            {
                this.m_OpenScoreDic[point + (int)Board.DIRECTION.UP_LEFT] -= 1;
            }

            if (this.m_OpenScoreDic.ContainsKey(point + (int)Board.DIRECTION.UP_RIGHT))
            {
                this.m_OpenScoreDic[point + (int)Board.DIRECTION.UP_RIGHT] -= 1;
            }
        }

        /// <summary>
        /// <para>開放度を取得する</para>
        /// </summary>
        private int GetOpenScore(Color color)
        {
            int score = 0;
            foreach (KeyValuePair<int, Color> point in Board.Colors)
            {
                if (point.Value == color)
                {
                    score += this.m_OpenScoreDic[point.Key];
                }
            }
            return score;
        }
        #endregion
        #endregion
    }
}
