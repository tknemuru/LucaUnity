using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using TKCommon.Constant;

namespace ReversiCommon.Collections
{
    /// <summary>
    /// <para>ターンキーパークラス</para>
    /// </summary>
    public static class TurnKeeper
    {
        #region "定数"
        /// <summary>
        /// <para>最大ターン数</para>
        /// </summary>
        public const int MAX_TURN = 60;

        /// <summary>
        /// <para>デバッグフラグ</para>
        /// </summary>
        public const bool IS_DEBUG_GAME = false;
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>現在のターンカラー</para>
        /// </summary>
        private static Color m_NowTurnColor;
        public static Color NowTurnColor
        {
            get { return m_NowTurnColor; }
        }

        /// <summary>
        /// パリティ
        /// </summary>
        public static string Parity
        {
            get
            {
                return (TurnKeeper.NowTurnColor == Color.Black) ? Const.SQL_FLG_YES : Const.SQL_FLG_NO;
            }
        }

        /// <summary>
        /// <para>現在のターン数</para>
        /// </summary>
        private static int m_NowTurnNumber;
        public static int NowTurnNumber
        {
            get { return m_NowTurnNumber; }
        }

        /// <summary>
        /// <para>ステージリスト</para>
        /// </summary>
        private static Dictionary<int, int> m_StageList;

        /// <summary>
        /// ステージ
        /// </summary>
        public static List<int> Stage { get; private set; } 

        /// <summary>
        /// <para>現在のステージ</para>
        /// </summary>
        private static int m_NowStage;
        public static int NowStage
        {
            get { return m_NowStage; }
        }
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        static TurnKeeper()
        {
            InitializeTurnKeeper();
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>初期化処理</para>
        /// </summary>
        public static void InitializeTurnKeeper()
        {
            m_NowTurnColor = Color.Black;
            m_NowTurnNumber = 1;

            // ステージリストの登録
            if (m_StageList == null)
            {
                m_StageList = new Dictionary<int, int>();
                int stage = 1;
                for (int turn = 1; turn <= 60; turn++)
                {
                    m_StageList.Add(turn, stage);
                    if ((turn % 4) == 0)
                    {
                        stage++;
                    }
                }
            }
            Stage = (from a in m_StageList
                     select a.Value).Distinct().OrderBy(key => key).ToList();

            m_NowStage = m_StageList[m_NowTurnNumber];
        }

        /// <summary>
        /// <para>ターンをまわす</para>
        /// </summary>
        public static void ChangeTurn()
        {
            // 色を変える
            m_NowTurnColor = m_NowTurnColor.OppositeColor;

            // ターン数を増やす
            m_NowTurnNumber++;

            // ステージを更新
            if (m_NowTurnNumber <= MAX_TURN)
            {
                m_NowStage = m_StageList[m_NowTurnNumber];
            }

            // 最大ターン数を超えることはあり得ない
            //Debug.Assert(m_NowTurnNumber <= MAX_TURN, "最大ターン数を超えました。");
        }

        /// <summary>
        /// <para>ターンを戻す</para>
        /// </summary>
        public static void UndoTurn()
        {
            // 色を変える
            m_NowTurnColor = m_NowTurnColor.OppositeColor;

            // ターン数を減らす
            m_NowTurnNumber--;

            // 0未満になることはあり得ない
            Debug.Assert(m_NowTurnNumber >= 0, "現在のターン数が０未満になりました。");
        }

        /// <summary>
        /// <para>現在の色のみを変える</para>
        /// <para>※使用注意※</para>
        /// </summary>
        public static void ChangeOnlyColor()
        {
            // 色を変える
            m_NowTurnColor = m_NowTurnColor.OppositeColor;
        }

        /// <summary>
        /// <para>すべてのターンが終わっている場合はTrueを返す</para>
        /// </summary>
        /// <returns></returns>
        public static bool IsGameEnd()
        {
            return (m_NowTurnNumber > MAX_TURN);
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
