using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace TKCommon.Collections
{
    /// <summary>
    /// 疎行列クラス
    /// </summary>
    public class SparseMatrix<T>
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// 疎行列の大半の成分を構成する要素
        /// </summary>
        private T m_SparseItem;

        /// <summary>
        /// 要素を構成するディクショナリ
        /// </summary>
        private Dictionary<int, T> m_ItemDictionary;

        /// <summary>
        /// 要素を平行方向に保持するディクショナリ
        /// </summary>
        private Dictionary<int, Dictionary<int, T>> m_ItemDictionaryHorizontal;

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
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sparseItem"> 疎ベクターの大半の成分を構成する要素</param>
        public SparseMatrix(int y, int x, T sparseItem)
        {
            this.m_SparseItem = sparseItem;
            this.m_ItemDictionary = new Dictionary<int, T>();
            this.m_ItemDictionaryHorizontal = new Dictionary<int, Dictionary<int, T>>();
            this.m_Width = x;
            this.m_Height = y;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="json"></param>
        public SparseMatrix(List<SparseVector<T>> vectorList, T sparseItem)
        {
            this.m_SparseItem = sparseItem;
            this.m_Width = vectorList[0].Count();
            this.m_Height = vectorList.Count();
            this.m_ItemDictionary = new Dictionary<int, T>();
            this.m_ItemDictionaryHorizontal = new Dictionary<int, Dictionary<int, T>>();

            for (int y = 0; y < vectorList.Count(); y++)
            {
                Debug.Assert(vectorList[y].Count() == this.m_Width, "ベクトルの要素数が異なります。");
                foreach (KeyValuePair<int, T> vector in vectorList[y].NoSparseKeyValues)
                {
                    this[y, vector.Key] = vector.Value;
                    if (!this.m_ItemDictionaryHorizontal.ContainsKey(y))
                    {
                        this.m_ItemDictionaryHorizontal.Add(y, new Dictionary<int, T>());
                    }
                    this.m_ItemDictionaryHorizontal[y][this.SerializeXY(y, vector.Key)] = vector.Value;
                }
            }
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public T this[int y, int x]
        {
            set
            {
                if (this.m_Height <= y || this.m_Width <= x)
                {
                    throw new ApplicationException(string.Format("存在しないインデックスです。H{0} W{1} Y:{2} X:{3}", this.m_Height, this.m_Width, y, x));
                }

                if (!value.Equals(this.m_SparseItem))
                {
                    this.m_ItemDictionary[this.SerializeXY(y, x)] = value;

                    // 平行方向のディクショナリにも保存しておく
                    if (!this.m_ItemDictionaryHorizontal.ContainsKey(y))
                    {
                        this.m_ItemDictionaryHorizontal.Add(y, new Dictionary<int,T>());
                    }
                    this.m_ItemDictionaryHorizontal[y][this.SerializeXY(y, x)] = value;
                }
            }
            get
            {
                if (this.m_Height <= y || this.m_Width <= x)
                {
                    throw new ApplicationException(string.Format("存在しないインデックスです。H{0} W{1} Y:{2} X:{3}", this.m_Height, this.m_Width, y, x));
                }
                return (this.m_ItemDictionary.ContainsKey(this.SerializeXY(y, x)) ? this.m_ItemDictionary[this.SerializeXY(y, x)] : this.m_SparseItem);
            }
        }

        /// <summary>
        /// 疎な部分を除いた列挙子。
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, T> NoSparseKeyValues
        {
            get
            {
                return this.m_ItemDictionary;
            }
        }

        /// <summary>
        /// 疎な部分を除いた特定の行。
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<int, T>> NoSparseKeyValuesOneRow(int y)
        {
            //var row = from a in this.m_ItemDictionary
            //          where ((a.Key / this.m_Width) == y)
            //          select a;
            if (!this.m_ItemDictionaryHorizontal.ContainsKey(y))
            {
                this.m_ItemDictionaryHorizontal.Add(y, new Dictionary<int, T>());
            }
            return this.m_ItemDictionaryHorizontal[y];
        }

        /// <summary>
        /// 疎な部分を除いた特定の列。
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<int, T>> NoSparseKeyValuesOneColumn(int x)
        {
            var column = from a in this.m_ItemDictionary
                         where ((a.Key % this.m_Width) == x)
                         select a;

            return column;
        }

        /// <summary>
        /// XY合算の座標からXの座標を取得する
        /// </summary>
        /// <param name="xy"></param>
        /// <returns></returns>
        public int DeserializeX(int xy)
        {
            return (xy % this.m_Width);
        }

        /// <summary>
        /// XY合算の座標からYの座標を取得する
        /// </summary>
        /// <param name="xy"></param>
        /// <returns></returns>
        public int DeserializeY(int xy)
        {
            return (xy / this.m_Width);
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// X、Yの座標から合算した座標を取得する
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private int SerializeXY(int y, int x)
        {
            return ((this.m_Width * y) + x);
        }
        #endregion
        #endregion
    }
}
