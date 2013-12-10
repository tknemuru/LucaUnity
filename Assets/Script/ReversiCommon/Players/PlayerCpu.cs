using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReversiCommon.Collections;
using ReversiCommon.Strategys;

namespace ReversiCommon.Players
{
    /// <summary>
    /// <para>CPUプレイヤークラス</para>
    /// </summary>
    public class PlayerCpu : Player
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="strategy"></param>
        public PlayerCpu(string name, Color color, IStrategy strategy) : base(name, color)
        {
            this.m_Strategy = strategy;
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>次の打ち手を取得する</para>
        /// </summary>
        /// <returns></returns>
        public override Disc GetNextPointer()
        {
            // 戦略に沿った手を取得
            return this.m_Strategy.GetNextDisc();
        }

        /// <summary>
        /// <para>次の打ち手をセットする</para>
        /// </summary>
        /// <param name="point"></param>
        public override void SetNextPointer(int point)
        {
            // TODO あとで実装するかも
            throw new NotImplementedException();
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
