using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReversiCommon.Collections;

namespace ReversiCommon.Strategys
{
    /// <summary>
    /// <para>人間用戦略</para>
    /// </summary>
    public class StrategyHuman : IStrategy
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
        public Disc GetNextDisc()
        {
            return new Disc(0, Color.Space);
        }
        #endregion
    }
}
