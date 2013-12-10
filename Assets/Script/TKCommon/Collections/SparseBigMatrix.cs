using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TKCommon.Utility;
using System.IO;
using System.Diagnostics;

namespace TKCommon.Collections
{
    /// <summary>
    /// 巨大疎行列クラス
    /// </summary>
    public class SparseBigMatrix
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// 疎行列の大半の成分を構成する要素
        /// </summary>
        private double m_SparseItem;

        /// <summary>
        /// 要素を構成するディクショナリ
        /// </summary>
        private Dictionary<int, double> m_ItemDictionary;

        /// <summary>
        /// 幅
        /// </summary>
        public int Width
        {
            get { return this.m_Width; }
        }
        private int m_Width;

        /// <summary>
        /// 高さ
        /// </summary>
        public int Height
        {
            get { return this.m_Height; }
        }
        private int m_Height;

        /// <summary>
        /// ファイル名
        /// </summary>
        private IEnumerable<string> m_Files;

        /// <summary>
        /// 高さが未知数のときはtrue
        /// </summary>
        private bool m_IsHeightUnknown;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sparseItem"> 疎ベクターの大半の成分を構成する要素</param>
        public SparseBigMatrix(IEnumerable<string> files, int x, double sparseItem)
        {
            this.m_SparseItem = sparseItem;
            this.m_ItemDictionary = new Dictionary<int, double>();
            this.m_Width = x;
            this.m_Height = 0;
            this.m_Files = files;
            this.m_IsHeightUnknown = true;
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// ファイルから行を読み込む
        /// </summary>
        public IEnumerator<SparseVector<double>> GetEnumerator()
        {
            foreach (string file in this.m_Files)
            {
                using (StreamReader sr = new StreamReader(file, Encoding.GetEncoding("Shift_JIS")))
                {
                    string line;
                    while (true)
                    {
                        //StopWatchLogger.StartEventWatch("GetEnumerator");
                        line = sr.ReadLine();
                        if (line == null)
                        {
                            if (file == this.m_Files.Last())
                            {
                                this.m_IsHeightUnknown = false;
                            }
                            break;
                        }
                        if (this.m_IsHeightUnknown) { this.m_Height++; }
                        SparseVector<double> vector = new SparseVector<double>(this.m_Width, this.GetVectorDictionary(line), 0);
                        //StopWatchLogger.StopEventWatch("GetEnumerator");
                        yield return vector;
                    }
                }
            }
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// ベクトル用のディクショナリを取得する
        /// </summary>
        /// <param name="csv"></param>
        /// <returns></returns>
        private Dictionary<int, double> GetVectorDictionary(string csv)
        {
            List<string> itemList = csv.Split(',').ToList();
            Debug.Assert((itemList.Count % 2) == 0, "要素数が奇数です。");

            Dictionary<int, double> vectorDic = new Dictionary<int, double>();
            for (int i = 0; i < itemList.Count; i += 2)
            {
                vectorDic.Add(StringUtility.ToInt(itemList[i]), StringUtility.ToDouble(itemList[i + 1]));
            }

            return vectorDic;
        }
        #endregion
        #endregion
    }
}
