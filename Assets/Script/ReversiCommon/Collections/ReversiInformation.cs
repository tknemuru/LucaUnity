using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ReversiCommon.Collections
{
    /// <summary>
    /// リバーシゲーム情報クラス
    /// </summary>
    public static class ReversiInformation
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// リバーシゲーム情報
        /// </summary>
        private static List<string> m_Information;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// コンストラクタ
        /// </summary>
        static ReversiInformation()
        {
            m_Information = new List<string>();
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// 情報を追加する
        /// </summary>
        /// <param name="information"></param>
        public static void Add(string information)
        {
            m_Information.Add(information);
        }

        /// <summary>
        /// 情報を文字列にして返す
        /// </summary>
        /// <param name="isClear">trueの場合は返却後に情報をクリアする</param>
        /// <returns></returns>
        public static string ToString(bool isClear = true)
        {
            string retInfo = string.Empty;
            foreach (string infomation in m_Information)
            {
                if (string.IsNullOrEmpty(retInfo))
                {
                    retInfo = infomation;
                }
                else
                {
                    retInfo += Environment.NewLine + infomation;
                }
            }

            if (isClear)
            {
                m_Information = new List<string>();
            }

            return retInfo;
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
