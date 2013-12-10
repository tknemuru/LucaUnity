using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

using ReversiCommon.Collections;

namespace ReversiCommon.Utility
{
    /// <summary>
    /// <para>回転クラス</para>
    /// </summary>
    public static class RotateUtility
    {
        #region "定数"
        /// <summary>
        /// <para>回転方法</para>
        /// </summary>
        private enum ROTATE_METHOD
        {
            ROTATE_56_TO_56,
            ROTATE_65_TO_56,
            ROTATE_34_TO_56,
            ROTATE_43_TO_56,
        }
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>回転方法</para>
        /// </summary>
        private static ROTATE_METHOD m_RotateMethod;
        #endregion

        #region "コンストラクタ"
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>回転方法をセットする</para>
        /// </summary>
        /// <param name="point"></param>
        public static void SetRotateMethod(int point)
        {
            switch (point)
            {
                case 56 :
                    m_RotateMethod = ROTATE_METHOD.ROTATE_56_TO_56;
                    break;
                case 65 :
                    m_RotateMethod = ROTATE_METHOD.ROTATE_65_TO_56;
                    break;
                case 34 :
                    m_RotateMethod = ROTATE_METHOD.ROTATE_34_TO_56;
                    break;
                case 43 :
                    m_RotateMethod = ROTATE_METHOD.ROTATE_43_TO_56;
                    break;
                default :
                    Debug.Assert(1 != 1, "不正な座標が渡されました。場所：RotateUtility SetRotateMethod 座標=" + point.ToString());
                    break;
            }
        }

        /// <summary>
        /// <para>一手目を56に整形した盤を返す</para>
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, Color> GetRotateColors()
        {
            Debug.Assert(IsRotateValidate(), "回転が正しくおこなわれていません。"); 

            Dictionary<int, Color> retColors = new Dictionary<int, Color>();
            for (int h = 1; h <= (int)Board.LENGTH.HIGHT; h++)
            {
                for (int w = 1; w <= (int)Board.LENGTH.WIDTH; w++)
                {
                    int point = ((h * 10) + w);
                    retColors.Add(GetRotateMirrorPoint(point), Board.GetSquareColor(point));
                }
            }

            return retColors;
        }

        /// <summary>
        /// <para>回転した座標を返す</para>
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static int GetRotatePoint(int point)
        {
            Debug.Assert(IsRotateValidate(), "回転が正しくおこなわれていません。"); 

            return GetRotateMirrorPoint(point);
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// <para>縦方向で反転した位置を取得する</para>
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private static int GetMirrorPoint(int point)
        {
            int tempNum = point - ((point % 10) - 1);
            int addNum = (int)Board.LENGTH.WIDTH - ((point % 10));
            return tempNum + addNum;
        }

        /// <summary>
        /// <para>指定した回数だけ回転した位置を取得する</para>
        /// </summary>
        /// <param name="point"></param>
        /// <param name="rotateCount"></param>
        /// <returns></returns>
        private static int GetRotatePoint(int point, int rotateCount)
        {
            rotateCount = rotateCount % 4;
            if (rotateCount < 0)
            {
                rotateCount += 4;
            }

            int oldX = int.Parse(point.ToString().Substring(1, 1));
            int oldY = int.Parse(point.ToString().Substring(0, 1));
            int newX = 0;
            int newY = 0;

            switch (rotateCount)
            {
                case 1 :
                    newX = oldY;
                    newY = (int)Board.LENGTH.WIDTH - oldX + 1;
                    break;
                case 2 :
                    newX = (int)Board.LENGTH.WIDTH - oldX + 1;
                    newY = (int)Board.LENGTH.WIDTH - oldY + 1;
                    break;
                case 3 :
                    newX = (int)Board.LENGTH.WIDTH - oldY + 1;
                    newY = oldX;
                    break;
                default :
                    newX = oldX;
                    newY = oldY;
                    break;
            }

            return ((newY * 10) + newX);
        }

        /// <summary>
        /// <para>回転・反転した座標を取得する</para>
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private static int GetRotateMirrorPoint(int point)
        {
            int retPoint = point;
            switch(m_RotateMethod)
            {
                case ROTATE_METHOD.ROTATE_56_TO_56 :
                    break;
                case ROTATE_METHOD.ROTATE_34_TO_56 :
                    retPoint = GetMirrorPoint(retPoint);
                    retPoint = GetRotatePoint(retPoint, -1);
                    break;
                case ROTATE_METHOD.ROTATE_43_TO_56 :
                    retPoint = GetRotatePoint(retPoint, -2);
                    break;
                case ROTATE_METHOD.ROTATE_65_TO_56 :
                    retPoint = GetMirrorPoint(retPoint);
                    retPoint = GetRotatePoint(retPoint, 1);
                    break;
                default :
                    break;
            }

            return retPoint;
        }

        /// <summary>
        /// <para>回転が正しく行われているかをチェックする</para>
        /// </summary>
        /// <returns></returns>
        private static bool IsRotateValidate()
        {
            int chkPoint = 0;
            switch (m_RotateMethod)
            {
                case ROTATE_METHOD.ROTATE_56_TO_56:
                    chkPoint = GetRotateMirrorPoint(56);
                    break;
                case ROTATE_METHOD.ROTATE_34_TO_56:
                    chkPoint = GetRotateMirrorPoint(34);
                    break;
                case ROTATE_METHOD.ROTATE_43_TO_56:
                    chkPoint = GetRotateMirrorPoint(43);
                    break;
                case ROTATE_METHOD.ROTATE_65_TO_56:
                    chkPoint = GetRotateMirrorPoint(65);
                    break;
                default:
                    break;
            }

            return (chkPoint == 56);
        }
        #endregion
        #endregion
    }
}
