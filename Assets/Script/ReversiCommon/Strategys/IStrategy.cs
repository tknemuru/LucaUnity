using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReversiCommon.Collections;

namespace ReversiCommon.Strategys
{
    /// <summary>
    /// <para>戦略インターフェイス</para>
    /// </summary>
    public interface IStrategy
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        #endregion

        #region "コンストラクタ"
        #endregion

        #region "メソッド"
        /// <summary>
        /// <para>次に打つ手を取得する</para>
        /// </summary>
        /// <returns></returns>
        Disc GetNextDisc();
        #endregion
    }
}
