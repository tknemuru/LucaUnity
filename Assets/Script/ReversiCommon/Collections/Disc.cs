using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiCommon.Collections
{
    /// <summary>
    /// <para>石クラス</para>
    /// </summary>
    public class Disc
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>座標</para>
        /// </summary>
        private int m_Point;
        public int Point
        {
            get { return this.m_Point; }
        }

        /// <summary>
        /// <para>石の色</para>
        /// </summary>
        private Color m_Color;
        public Color Color
        {
            get { return this.m_Color; }
        }

        /// <summary>
        /// <para>盤の状態</para>
        /// <para>※本番では消すこと！</para>
        /// </summary>
        private Dictionary<int, Color> m_BoardColors;
        public Dictionary<int, Color> BoardColors
        {
            get { return this.m_BoardColors; }
            set { this.m_BoardColors = value; }
        }

        /// <summary>
        /// <para>座標の評価値</para>
        /// </summary>
        public int Evaluate { get; set; }
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        /// <param name="point"></param>
        /// <param name="color"></param>
        public Disc(int point, Color color)
        {
            this.m_Point = point;
            this.m_Color = color;
            this.Evaluate = 0;
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        /// <param name="point"></param>
        /// <param name="color"></param>
        public Disc(int point, Color color, int evaluate)
        {
            this.m_Point = point;
            this.m_Color = color;
            this.Evaluate = evaluate;
        }
        #endregion

        #region "メソッド"
        /// <summary>
        /// <para>評価値の符号を逆にして返す</para>
        /// </summary>
        /// <param name="pointer"></param>
        /// <returns></returns>
        public Disc OppositeSignEvaluate()
        {
            this.Evaluate = this.Evaluate * -1;
            return this;
        }

        /// <summary>
        /// <para>評価値が大きい方を返す</para>
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Disc MaxEvaluate(Disc p1, Disc p2)
        {
            if (p1.Evaluate > p2.Evaluate)
            {
                return p1;
            }
            else
            {
                return p2;
            }
        }

        /// <summary>
        /// <para>評価値をセットする</para>
        /// </summary>
        /// <param name="target"></param>
        public void SetEvaluate(Disc target)
        {
            this.Evaluate += target.Evaluate;
        }

        /// <summary>
        /// <para>盤の状態をセットする</para>
        /// </summary>
        public void SetBoardColors()
        {
            this.m_BoardColors = new Dictionary<int, Color>(Board.Colors);
        }
        #endregion
    }
}
