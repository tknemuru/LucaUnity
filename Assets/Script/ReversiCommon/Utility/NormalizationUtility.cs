using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using ReversiCommon.Collections;

namespace ReversiCommon.Utility
{
    /// <summary>
    /// インデックス正規化ユーティリティ
    /// </summary>
    public static class NormalizationUtility
    {
        #region "定数"
        /// <summary>
        /// 一時的に変換させておくダミー色
        /// </summary>
        private const string DUMMY_COLOR = "9";
        #endregion

        #region "メンバ変数"
        #endregion

        #region "コンストラクタ"
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// インデックスの石の色を変える
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int ChangeColor(int index, int width)
        {
            string cnvIndexStr = RadixConvertUtility.ToString(index, 3, width, true);
            cnvIndexStr = cnvIndexStr.Replace(Color.Black.ColorNumber.ToString(), DUMMY_COLOR);
            cnvIndexStr = cnvIndexStr.Replace(Color.White.ColorNumber.ToString(), Color.Black.ColorNumber.ToString());
            cnvIndexStr = cnvIndexStr.Replace(DUMMY_COLOR, Color.White.ColorNumber.ToString());
            int newIndex = RadixConvertUtility.ToInt32(cnvIndexStr, 3);
            return newIndex;
        }

        /// <summary>
        /// インデックスの順序を逆にする
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int Reverse(int index, int width)
        {
            string cnvIndexStr = RadixConvertUtility.ToString(index, 3, width, true);
            string reverseIndexStr = string.Empty;
            foreach (char cr in cnvIndexStr.ToCharArray().Reverse())
            {
                reverseIndexStr += cr;
            }
            int newIndex = RadixConvertUtility.ToInt32(reverseIndexStr, 3);
            return newIndex;
        }

        /// <summary>
        /// インデックスの色を変換して順序を逆にする
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int ChangeColorReverse(int index, int width)
        {
            return NormalizationUtility.Reverse(NormalizationUtility.ChangeColor(index, width), width);
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
