using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReversiCommon.Collections;

namespace ReversiCommon.Strategys
{
    /// <summary>
    /// <para>戦略クラス</para>
    /// </summary>
    public abstract class Strategy : IStrategy
    {
        #region "定数"
        /// <summary>
        /// <para>戦略</para>
        /// </summary>
        private enum STRATEGY_ID
        {
            HUMAN,
            CPU,
        }
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>人間用戦略</para>
        /// </summary>
        private static IStrategy m_HumanStrategy = new StrategyHuman();
        public static IStrategy HumanStrategy
        {
            get { return m_HumanStrategy; }
        }

        /// <summary>
        /// <para>CPU用戦略</para>
        /// </summary>
        private static IStrategy m_CpuStrategy = new StrategyCpu();
        public static IStrategy CpuStrategy
        {
            get { return m_CpuStrategy; }
        }
        #endregion

        #region "コンストラクタ"
        #endregion

        #region "メソッド"
        /// <summary>
        /// <para>次に打つ手を取得する</para>
        /// </summary>
        /// <returns></returns>
        public abstract Disc GetNextDisc();
        #endregion
    }
}
