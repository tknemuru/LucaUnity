using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Diagnostics;
using TKCommon.Utility;

namespace ReversiCommon.Utility
{
    /// <summary>
    /// リバーシ用変換クラス
    /// </summary>
    public static class ReversiConvertUtility
    {
        #region "定数"
        /// <summary>
        /// アルファベットの座標
        /// </summary>
        private static readonly List<string> ALPHABET_POINT = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" };

        /// <summary>
        /// チェック用の番号
        /// </summary>
        private static readonly List<string> CHECK_NUMBER_POINT = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" };
        #endregion

        #region "メンバ変数"
        #endregion

        #region "コンストラクタ"
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// F5形式の座標を56などの形式に変換する
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static string ConvertToThorPoint(string point)
        {
            Debug.Assert((point.Length == 2), "座標の桁数が２桁ではありません。");
            List<string> points = StringUtility.ToListSplitCount(point, 1);

            Debug.Assert(ALPHABET_POINT.Contains(points[0].ToUpper()), "不正なアルファベットです。");
            Debug.Assert(CHECK_NUMBER_POINT.Contains(points[1]), "不正な番号です。 番号:" + points[1]);
            int alpha = (ALPHABET_POINT.IndexOf(points[0].ToUpper()) + 1);
            return (int.Parse(points[1]) * 10 + alpha).ToString();
        }

        /// <summary>
        /// F5形式の座標を56などの形式に変換できる場合はTrueを返す
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool IsConvertableToThorPoint(string point)
        {
            if (point.Length != 2)
            {
                return false;
            }

            List<string> points = StringUtility.ToListSplitCount(point, 1);

            if(!ALPHABET_POINT.Contains(points[0].ToUpper()))
            {
                return false;
            }

            if (!CHECK_NUMBER_POINT.Contains(points[1]))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
