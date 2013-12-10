using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Diagnostics;
using TKCommon.Utility;
using UnityEngine;

namespace ReversiCommon.Collections
{
    /// <summary>
    /// 正規化インデックスクラス
    /// </summary>
    public static class NormalizeIndex
    {
        #region "定数"
        /// <summary>
        /// 接続記号
        /// </summary>
        private const string CONJUNCTION = "$";

        /// <summary>
        /// 正規化インデックスのファイルパス
        /// </summary>
        private const string NORMALIZE_INDEX_FILE_PATH = @"normalize_index";

        /// <summary>
        /// 正規化インデックスタイプのファイルパス
        /// </summary>
        private const string NORMALIZE_INDEX_TYPE_FILE_PATH = @"normalize_type";
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// 正規化インデックスディクショナリ
        /// </summary>
        private static Dictionary<string, int> m_NormalizeIndexDic;

        /// <summary>
        /// 正規化タイプディクショナリ
        /// </summary>
        private static Dictionary<string, double> m_NormalizeTypeDic;

        /// <summary>
        /// インデックス桁数ディクショナリ
        /// </summary>
        private static Dictionary<string, string> m_IndexDigitDic;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// コンストラクタ
        /// </summary>
        static NormalizeIndex()
        {
            SetNormalizeIndexDic();
            SetNormalizeIndexTypeDic();
            SetIndexDigitDic();
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// インデックス
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="indexNo"></param>
        /// <returns></returns>
        public static int Get(string indexName, int indexNo)
        {
            string key = GetIndexKey(indexNo.ToString(), m_IndexDigitDic[indexName]);
            return m_NormalizeIndexDic[key];
            //StopWatchLogger.StartEventWatch("Get");
            //string key = GetIndexKey(indexNo.ToString(), m_IndexDigitDic[indexName]);
            //int test = m_NormalizeIndexDic[key];
            //StopWatchLogger.StopEventWatch("Get");
            //return test;
        }

        /// <summary>
        /// インデックスをダイレクトに取得する
        /// </summary>
        /// <param name="indexNo"></param>
        /// <param name="digit"></param>
        /// <returns></returns>
        public static int DirectGet(string key)
        {
            return m_NormalizeIndexDic[key];
        }

        /// <summary>
        /// 正規化タイプ
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="indexNo"></param>
        /// <returns></returns>
        public static double GetNormalizeType(string indexName, int indexNo)
        {
            string key = GetIndexKey(indexNo.ToString(), m_IndexDigitDic[indexName]);
            return m_NormalizeTypeDic[key];
            //StopWatchLogger.StartEventWatch("GetNormalizeType");
            //string key = GetIndexKey(indexNo.ToString(), m_IndexDigitDic[indexName]);
            //double test = m_NormalizeTypeDic[key];
            //StopWatchLogger.StopEventWatch("GetNormalizeType");
            //return test;
        }

        /// <summary>
        /// 正規化タイプをダイレクトに取得する
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="indexNo"></param>
        /// <returns></returns>
        public static double DirectGetNormalizeType(string key)
        {
            return m_NormalizeTypeDic[key];
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// 正規化インデックスディクショナリをセットする
        /// </summary>
        private static void SetNormalizeIndexDic()
        {
            m_NormalizeIndexDic = new Dictionary<string, int>();

            //string csv = FileUtility.ReadToEnd(UnityEngine.Application.dataPath + NORMALIZE_INDEX_FILE_PATH);
            TextAsset txt = UnityEngine.Object.Instantiate(Resources.Load(NORMALIZE_INDEX_FILE_PATH, typeof(TextAsset))) as TextAsset;
            string csv = txt.text;
            string[] csvList = csv.Split(',');
            System.Diagnostics.Debug.Assert((csvList.Length % 2 == 0), "要素数が奇数です。");
            for (int i = 0; i < csvList.Length; i += 2)
            {
                m_NormalizeIndexDic.Add(csvList[i], int.Parse(csvList[i + 1]));
            }
        }

        /// <summary>
        /// 正規化インデックスタイプディクショナリをセットする
        /// </summary>
        private static void SetNormalizeIndexTypeDic()
        {
            m_NormalizeTypeDic = new Dictionary<string, double>();

            //string csv = FileUtility.ReadToEnd(UnityEngine.Application.dataPath + NORMALIZE_INDEX_TYPE_FILE_PATH);
            TextAsset txt = UnityEngine.Object.Instantiate(Resources.Load(NORMALIZE_INDEX_TYPE_FILE_PATH, typeof(TextAsset))) as TextAsset;
            string csv = txt.text;
            string[] csvList = csv.Split(',');
            System.Diagnostics.Debug.Assert((csvList.Length % 2 == 0), "要素数が奇数です。");
            for (int i = 0; i < csvList.Length; i += 2)
            {
                m_NormalizeTypeDic.Add(csvList[i], double.Parse(csvList[i + 1]));
            }
        }

        /// <summary>
        /// 正規化インデックスディクショナリのキーを取得する
        /// </summary>
        /// <param name="indexNo"></param>
        /// <param name="digit"></param>
        /// <returns></returns>
        public static string GetIndexKey(string indexNo, string digit)
        {
            return indexNo + CONJUNCTION + digit;
        }

        /// <summary>
        /// インデックス桁数表を作成する
        /// </summary>
        /// <returns></returns>
        private static void SetIndexDigitDic()
        {
            m_IndexDigitDic = new Dictionary<string, string>();
            
            for(int i = 4; i <= 8; i++)
            {
                m_IndexDigitDic.Add("diag" + i.ToString(), i.ToString());
            }

            for (int i = 2; i <= 4; i++)
            {
                m_IndexDigitDic.Add("hor_vert" + i.ToString(), "8");
            }

            m_IndexDigitDic.Add("edge2X", "10");
            m_IndexDigitDic.Add("corner3X3", "9");
            m_IndexDigitDic.Add("corner2X5", "10");
        }
        #endregion
        #endregion
    }
}
