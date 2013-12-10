using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ReversiCommon.Collections
{
    /// <summary>
    /// スコアインデックスクラス
    /// </summary>
    public class ScoreIndex
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// キー
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// バリュー
        /// </summary>
        public double Value { get; private set; }
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public ScoreIndex(string key, double value)
        {
            this.Key = key;
            this.Value = value;
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
