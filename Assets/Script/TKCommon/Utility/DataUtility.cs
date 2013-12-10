using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TKCommon.Utility
{
    /// <summary>
    /// <para>データユーティリティクラス</para>
    /// </summary>
    public static class DataUtility
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
        /// <para>指定した数だけ飛ばし、指定した数だけ切り出して取得する</para>
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="enums">対象要素</param>
        /// <param name="skipNum">飛ばす要素数</param>
        /// <param name="takeNum">取得する要素数</param>
        /// <returns></returns>
        public static List<Type> GetSkipTakeList<Type>(List<Type> enums, int skipNum, int takeNum)
            where Type : IEnumerable<Type>
        {
            return enums.Skip(skipNum).Take(takeNum).ToList();
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
