using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ReversiCommon.Collections
{
    public class Color
    {
        #region "定数"
        /// <summary>
        /// <para>色</para>
        /// </summary>
        private enum COLOR_ID
        {
            WHITE = 0,
            SPACE = 1,
            BLACK = 2
        }

        /// <summary>
        /// <para>イメージファイルURL：白</para>
        /// </summary>
        private const string IMAGE_FILE_URL_WHITE = @"C:\work\visualstudio\Luca_Append\ReversiCommon\Images\White.gif";

        /// <summary>
        /// <para>イメージファイルURL：スペース</para>
        /// </summary>
        private const string IMAGE_FILE_URL_SPACE = @"C:\work\visualstudio\Luca_Append\ReversiCommon\Images\Space.gif";

        /// <summary>
        /// <para>イメージファイルURL：黒</para>
        /// </summary>
        private const string IMAGE_FILE_URL_BLACK = @"C:\work\visualstudio\Luca_Append\ReversiCommon\Images\Black.gif";
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>白色</para>
        /// </summary>
        private static Color m_White = new Color(COLOR_ID.WHITE, IMAGE_FILE_URL_WHITE);
        public static Color White
        {
            get { return m_White; }
        }

        /// <summary>
        /// <para>黒色</para>
        /// </summary>
        private static Color m_Black = new Color(COLOR_ID.BLACK, IMAGE_FILE_URL_BLACK);
        public static Color Black
        {
            get { return m_Black; }
        }

        /// <summary>
        /// <para>スペース</para>
        /// </summary>
        private static Color m_Space = new Color(COLOR_ID.SPACE, IMAGE_FILE_URL_SPACE);
        public static Color Space
        {
            get { return m_Space; }
        }

        /// <summary>
        /// <para>内部保持色</para>
        /// </summary>
        private COLOR_ID m_Color;
        public string ColorId
        {
            get { return m_Color.ToString(); }
        }
        public int ColorNumber
        {
            get { return (int)m_Color; }
        }

        /// <summary>
        /// <para>イメージファイルのURL</para>
        /// </summary>
        private string m_ImageUrl;
        public string ImageUrl
        {
            get { return m_ImageUrl; }
        }
        #endregion

        #region "プロパティ"
        public Color OppositeColor
        {
            get { return this.GetOppositeColor(); }
        }
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        /// <param name="color"></param>
        private Color(COLOR_ID color, string imageUrl)
        {
            m_Color = color;
            m_ImageUrl = imageUrl;
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>数値に対応した色を返す</para>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Color ToColor(int i)
        {
            switch (i)
            {
                case (int)Color.COLOR_ID.BLACK:
                    return Color.Black;
                case (int)Color.COLOR_ID.WHITE:
                    return Color.White;
                case (int)Color.COLOR_ID.SPACE:
                    return Color.Space;
                default:
                    return Color.Space;
            }
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// <para>反対の色を返す</para>
        /// </summary>
        /// <returns></returns>
        private Color GetOppositeColor()
        {
            switch (m_Color)
            {
                case COLOR_ID.WHITE:
                    return m_Black;
                case COLOR_ID.BLACK:
                    return m_White;
                case COLOR_ID.SPACE:
                    return m_Space;
                default:
                    return m_Space;
            }
        }
        #endregion
        #endregion
    }
}
