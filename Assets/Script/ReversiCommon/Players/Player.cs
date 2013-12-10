using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using ReversiCommon.Collections;
using ReversiCommon.Strategys;

namespace ReversiCommon.Players
{
    /// <summary>
    /// <para>プレイヤーベースクラス</para>
    /// </summary>
    public abstract class Player
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>戦略</para>
        /// </summary>
        protected IStrategy m_Strategy;

        /// <summary>
        /// <para>名前</para>
        /// </summary>
        protected string m_Name;
        public string Name
        {
            get { return m_Name; }
        }

        /// <summary>
        /// <para>色</para>
        /// </summary>
        protected Color m_Color;
        public Color Color
        {
            get { return m_Color; }
        }
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="strategy"></param>
        public Player(string name, Color color)
        {
            m_Name = name;
            m_Color = color;
        }
        #endregion

        #region "メソッド"
        /// <summary>
        /// <para>次の打ち手を取得する</para>
        /// </summary>
        /// <returns></returns>
        public abstract Disc GetNextPointer();

        /// <summary>
        /// <para>次の打ち手をセットする</para>
        /// </summary>
        public abstract void SetNextPointer(int point);
        #endregion
    }
}
