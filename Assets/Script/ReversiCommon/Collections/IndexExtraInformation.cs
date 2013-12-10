using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ReversiCommon.Collections
{
    /// <summary>
    /// インデックス拡張情報
    /// </summary>
    public class IndexExtraInformation
    {
        #region "定数"
        /// <summary>
        /// 拡張情報
        /// </summary>
        public enum EXTRA_INFO_INDEX
        {
            PARITY = 0,
            MOBILITY,
            //WHITE_MOBILITY
        }
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// インデックス
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// パリティ
        /// </summary>
        public static IndexExtraInformation Parity
        {
            get { return m_Parity; }
        }
        private static readonly IndexExtraInformation m_Parity = new IndexExtraInformation("parity", EXTRA_INFO_INDEX.PARITY);

        /// <summary>
        /// 黒着手可能数
        /// </summary>
        public static IndexExtraInformation Mobility
        {
            get { return m_Mobility; }
        }
        private static readonly IndexExtraInformation m_Mobility = new IndexExtraInformation("mobility", EXTRA_INFO_INDEX.MOBILITY);

        /// <summary>
        /// 白着手可能数
        /// </summary>
        //public static IndexExtraInformation WhiteMobility
        //{
        //    get { return m_WhiteMobility; }
        //}
        //private static readonly IndexExtraInformation m_WhiteMobility = new IndexExtraInformation("white_mobility", EXTRA_INFO_INDEX.WHITE_MOBILITY);

        /// <summary>
        /// 全ての拡張情報
        /// </summary>
        public static List<IndexExtraInformation> All
        {
            get { return new List<IndexExtraInformation>() { m_Parity, m_Mobility }; }
        }
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        private IndexExtraInformation(string name, EXTRA_INFO_INDEX index)
        {
            this.Name = name;
            this.Index = (int)index;
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
