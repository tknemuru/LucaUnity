using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoAncare
{
    /// <summary>
    /// <para>座標クラス</para>
    /// </summary>
    public class Pointer
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>数値型の座標値</para>
        /// </summary>
        private int m_NumberPoint;
        public int NumberPoint
        {
            get { return m_NumberPoint; }
            set { m_NumberPoint = value; }
        }

        /// <summary>
        /// <para>座標がNullならばTrueを返す</para>
        /// </summary>
        private bool m_IsNullPointer;
        public bool IsNullPointer
        {
            get { return m_IsNullPointer; }
            set { m_IsNullPointer = value; }
        }

        /// <summary>
        /// <para>座標の評価値を返す</para>
        /// </summary>
        private int m_evaluate;
        public int Evaluate
        {
            get { return m_evaluate; }
            set { m_evaluate = value; }
        }
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        private Pointer()
        {
            m_IsNullPointer = true;
            m_evaluate = 0;
        }
        #endregion

        #region "メソッド"
        /// <summary>
        /// <para>評価値の符号を逆にして返す</para>
        /// </summary>
        /// <param name="pointer"></param>
        /// <returns></returns>
        public Pointer OppositeSignEvaluate()
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
        public static Pointer MaxEvaluate(Pointer p1, Pointer p2)
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
        #endregion
    }
}
