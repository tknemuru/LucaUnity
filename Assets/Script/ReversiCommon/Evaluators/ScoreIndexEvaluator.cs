using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReversiCommon.Collections;
using ReversiCommon.Utility;
using System.Configuration;
using System.Diagnostics;
using TKCommon.Utility;
using UnityEngine;

namespace ReversiCommon.Evaluators
{
    /// <summary>
    /// <para>スコアインデックス評価関数クラス</para>
    /// </summary>
    public class ScoreIndexEvaluator : IEvaluator
    {
        #region "定数"
        /// <summary>
        /// スコアインデックスのファイルパス
        /// </summary>
        private const string SCORE_INDEX_FILE_PATH = @"score.{0}";
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>ターンステージ</para>
        /// </summary>
        private static int m_TurnStage = 0;

        /// <summary>
        /// <para>スコアインデックスディクショナリ</para>
        /// </summary>
        private static Dictionary<string, double> m_IndexDic;

        /// <summary>
        /// 分析する場合はTrue
        /// </summary>
        private bool m_IsAnalytics;

        /// <summary>
        /// インデックスの概要情報
        /// </summary>
        public Dictionary<string, double> IndexDescription;

        /// <summary>
        /// スコアインデックスのファイルパス
        /// </summary>
        private string m_ScoreIndexFilePath;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        public ScoreIndexEvaluator(bool isAnalytics = false)
        {
            this.IndexDescription = new Dictionary<string, double>();
            this.m_IsAnalytics = isAnalytics;
            this.m_ScoreIndexFilePath = SCORE_INDEX_FILE_PATH;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath"></param>
        public ScoreIndexEvaluator(string scoreIndexFilePath)
        {
            this.IndexDescription = new Dictionary<string, double>();
            this.m_ScoreIndexFilePath = scoreIndexFilePath;
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>評価値を取得</para>
        /// </summary>
        /// <returns></returns>
        public double GetEvaluate()
        {
            // スコアインデックスの更新
            this.UpdateScoreIndexTable();

            // 現在のインデックス値を取得
            List<ScoreIndex> scoreIndexList = ScoreIndexGenerator.GetIndexList();

            // 評価値を取得
            double score = 0;
            foreach (ScoreIndex scoreIndex in scoreIndexList)
            {
                if (m_IndexDic.ContainsKey(scoreIndex.Key))
                {
                    score += (m_IndexDic[scoreIndex.Key] * scoreIndex.Value);
                }
            }

            // 拡張情報の評価値を取得
            // パリティ
            score += m_IndexDic["0" + IndexExtraInformation.Parity.Name] * double.Parse(TurnKeeper.Parity);

            // 黒着手可能数
            double blackMobility = Board.GetAllPutablePointerCount(ReversiCommon.Collections.Color.Black);
            double whiteMobility = Board.GetAllPutablePointerCount(ReversiCommon.Collections.Color.White);
            double mobility = blackMobility - whiteMobility;
            score += m_IndexDic["0" + IndexExtraInformation.Mobility.Name] * mobility;
            
            // 全部なしチェック
            score += this.GetAllNothingScore(blackMobility, whiteMobility);

            return score;
        }

        /// <summary>
        /// スコアインデックスのキーを取得する
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="indexNo"></param>
        /// <returns></returns>
        public static string GetScoreIndexKey(string indexName, string indexNo)
        {
            return (indexNo + indexName);
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// <para>スコアインデックスを更新する</para>
        /// </summary>
        private void UpdateScoreIndexTable()
        {
            if(m_TurnStage != TurnKeeper.NowStage)
            {
                m_TurnStage = TurnKeeper.NowStage;
                m_IndexDic = new Dictionary<string, double>();
                //string csv = FileUtility.ReadToEnd(string.Format(this.m_ScoreIndexFilePath, m_TurnStage));
                TextAsset txt = UnityEngine.Object.Instantiate(Resources.Load(string.Format(this.m_ScoreIndexFilePath, m_TurnStage), typeof(TextAsset))) as TextAsset;
                string csv = txt.text;
                string[] csvList = csv.Split(',');
                System.Diagnostics.Debug.Assert((csvList.Length % 2 == 0), "要素数が奇数です。");
                for (int i = 0; i < csvList.Length; i += 2)
                {
                    m_IndexDic.Add(csvList[i], double.Parse(csvList[i + 1]));
                }
            }
        }

        /// <summary>
        /// インデックス概要を作成する
        /// </summary>
        private void MakeIndexDescription()
        {
            // 分析しない場合は何もしない
            if (!this.m_IsAnalytics) { return; }

            List<CostIndexGenerator> costIndexList = CostIndexGenerator.GetIndexList();


            // 評価値を取得
            foreach (CostIndexGenerator costIndex in costIndexList)
            {
                if (m_IndexDic.ContainsKey(NormalizeIndex.Get(costIndex.Name, costIndex.Index) + costIndex.Name))
                {
                    this.IndexDescription.Add(costIndex.Description, (m_IndexDic[NormalizeIndex.Get(costIndex.Name, costIndex.Index) + costIndex.Name] * NormalizeIndex.GetNormalizeType(costIndex.Name, costIndex.Index)));
                }
                else
                {
                    this.IndexDescription.Add(costIndex.Description, 0);
                }
            }

            // 拡張情報の評価値を取得
            // パリティ
            this.IndexDescription.Add("パリティ", m_IndexDic["0" + IndexExtraInformation.Parity.Name] * double.Parse(TurnKeeper.Parity));

            // 黒着手可能数
            double mobility = (Board.GetAllPutablePointerCount(ReversiCommon.Collections.Color.Black)) - Board.GetAllPutablePointerCount(ReversiCommon.Collections.Color.White);
            this.IndexDescription.Add("着手可能数", m_IndexDic["0" + IndexExtraInformation.Mobility.Name] * mobility);

        }

        /// <summary>
        /// 全てなしの場合のスコアを取得
        /// </summary>
        /// <returns></returns>
        private double GetAllNothingScore(double blackMobility, double whiteMobility)
        {
            if (blackMobility == 0 && whiteMobility == 0)
            {
                return (double)((Board.GetBlackCount() - Board.GetWhiteCount()) * 100000);
            }
            else
            {
                return 0;
            }
        }
        #endregion
        #endregion
    }
}
