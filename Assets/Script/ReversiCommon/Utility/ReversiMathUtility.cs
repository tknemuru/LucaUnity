using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ReversiCommon.Utility
{
    /// <summary>
    /// リバーシ用計算ユーティリティ
    /// </summary>
    public static class ReversiMathUtility
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        #endregion

        #region "コンストラクタ"
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// 桁数に応じたインデックスの最大値を返す
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int GetMaxIndex(int digit)
        {
            int result = 2;
            for (int i = 0; i < (digit - 1); i++)
            {
                result = ((result * 3) + 2);
            }
            return result;
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
