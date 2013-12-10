using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using TKCommon.Utility;
using ReversiCommon.Collections;
using System.Diagnostics;

namespace ReversiCommon.Evaluators
{
    /// <summary>
    /// 定石評価クラス
    /// </summary>
    public class BookEvaluator : IEvaluator
    {
        #region "定数"
        /// <summary>
        /// ファイルパス
        /// </summary>
        private const string FILE_PATH = @"C:\work\visualstudio\Luca_TRUNK\LucaDevelop\csv\joseki\joseki.black.{0}";

        /// <summary>
        /// 定石が存在しなかった場合の値
        /// </summary>
        public const double NOTHING_VALUE = -99D;
        #endregion

        #region "メンバ変数"   
        private Dictionary<string, double> m_Book;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="turn"></param>
        public BookEvaluator(int turn)
        {
            this.m_Book = new Dictionary<string, double>();

            string csv = FileUtility.ReadToEnd(string.Format(FILE_PATH, turn));
            string[] csvList = csv.Split(',');
            Debug.Assert((csvList.Length % 2 == 0), "要素数が奇数です。");
            for (int i = 0; i < csvList.Length; i += 2)
            {
                this.m_Book.Add(csvList[i], double.Parse(csvList[i + 1]));
            }
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
            // 現在までの打ち筋を取得
            string pattern = Board.GetRotateLogStack();
            double score = NOTHING_VALUE;
            if (this.m_Book.ContainsKey(pattern))
            {
                score = (double)this.m_Book[pattern];
            }
            return score;
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
