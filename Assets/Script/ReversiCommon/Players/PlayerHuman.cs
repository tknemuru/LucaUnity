using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReversiCommon.Collections;
using ReversiCommon.Strategys;

namespace ReversiCommon.Players
{
    /// <summary>
    /// <para>人間プレイヤークラス</para>
    /// </summary>
    public class PlayerHuman : Player
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>次に打つ手</para>
        /// </summary>
        private int m_NextPoint;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="strategy"></param>
        public PlayerHuman(string name, Color color) : base(name, color)
        {
            this.m_Strategy = new StrategyHuman();
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
            return new Disc(this.m_NextPoint, this.Color);
        }

        /// <summary>
        /// <para>次の打ち手をセットする</para>
        /// </summary>
        public override void SetNextPointer(int point)
        {
            this.m_NextPoint = point;
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
