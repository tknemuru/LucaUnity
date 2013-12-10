using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using ReversiCommon.Utility;

namespace ReversiCommon.Collections
{
    /// <summary>
    /// <para>盤クラス</para>
    /// </summary>
    public static class Board
    {
        #region "定数"
        /// <summary>
        /// <para>黒で初期化する座標位置</para>
        /// </summary>
        private static readonly List<int> INITIALIZE_BLACK_POINT = new List<int> { 45, 54 };

        /// <summary>
        /// <para>白で初期化する座標位置</para>
        /// </summary>
        private static readonly List<int> INITIALIZE_WHITE_POINT = new List<int> { 44, 55 };

        /// <summary>
        /// <para>裏返す方向</para>
        /// </summary>
        public enum DIRECTION
        {
            UP = -10,
            UP_RIGHT = -9,
            RIGHT = 1,
            DOWN_RIGHT = 11,
            DOWN = 10,
            DOWN_LEFT = 9,
            LEFT = -1,
            UP_LEFT = -11
        }

        /// <summary>
        /// <para>マス目の合計</para>
        /// </summary>
        public const int SQURE_NUMBER_SUM = 64;

        /// <summary>
        /// <para>盤の幅</para>
        /// </summary>
        public enum LENGTH
        {
            WIDTH = 8,
            HIGHT = 8
        }

        /// <summary>
        /// <para>イメージファイルURL：配置可能</para>
        /// </summary>
        private const string IMAGE_FILE_URL_MOBILITY = @"C:\work\visualstudio\Luca_Append\ReversiCommon\Images\Mobility.gif";
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>カラーリスト</para>
        /// </summary>
        private static Dictionary<int, Color> m_Colors;
        public static Dictionary<int, Color> Colors
        {
            get { return m_Colors; }
        }

        /// <summary>
        /// <para>スコアリスト</para>
        /// </summary>
        public static Dictionary<int, int> m_Scores;
        public static Dictionary<int, int> Scores
        {
            get { return m_Scores; }
            set { m_Scores = value; }
        }

        /// <summary>
        /// <para>ログスタック</para>
        /// </summary>
        private static Stack<Disc> m_LogStack;
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        static Board()
        {
            InitializeBoard();
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>指定したマスの色を取得する</para>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Color GetSquareColor(int key)
        {
            return m_Colors[key];
        }

        /// <summary>
        /// <para>ボードを初期化する</para>
        /// </summary>
        public static void InitializeBoard()
        {
            m_LogStack = new Stack<Disc>();
            m_Scores = new Dictionary<int, int>();

            m_Colors = new Dictionary<int, Color>();
            for (int h = 1; h <= 8; h++)
            {
                for (int w = 1; w <= 8; w++)
                {
                    int point = ((h * 10) + w);
                    m_Colors.Add(point, GetInitializeColor(point));
                }
            }
        }

        /// <summary>
        /// 盤面状態をセットする（テスト用）
        /// </summary>
        /// <param name="colors"></param>
        public static void SetBoardState(Dictionary<int, Color> colors)
        {
            m_Colors = colors;
        }

        /// <summary>
        /// <para>石を裏返す</para>
        /// </summary>
        /// <param name="point"></param>
        public static void Reverse(int point)
        {
            Debug.Assert(m_Colors.ContainsKey(point), "範囲外の座標が指定されました。");
            Debug.Assert(m_Colors[point] == Color.Space, "スペース以外の座標が指定されました。");

            // 置いた場所をログスタックに格納
            m_LogStack.Push(new Disc(point, Color.Space));

            List<bool> isReverse = new List<bool>();
            isReverse.Add(ReverseOneDirection(point, DIRECTION.DOWN));
            isReverse.Add(ReverseOneDirection(point, DIRECTION.DOWN_LEFT));
            isReverse.Add(ReverseOneDirection(point, DIRECTION.DOWN_RIGHT));
            isReverse.Add(ReverseOneDirection(point, DIRECTION.LEFT));
            isReverse.Add(ReverseOneDirection(point, DIRECTION.RIGHT));
            isReverse.Add(ReverseOneDirection(point, DIRECTION.UP));
            isReverse.Add(ReverseOneDirection(point, DIRECTION.UP_LEFT));
            isReverse.Add(ReverseOneDirection(point, DIRECTION.UP_RIGHT));
            Debug.Assert(isReverse.Contains(true), "ひとつも裏返せないところに置かれました。");
        }

        /// <summary>
        /// <para>ひとつでも石がおける場所がある場合はTrueを返す</para>
        /// </summary>
        /// <returns></returns>
        public static bool IsReversible()
        {
            List<bool> isReversible = new List<bool>();
            for (int i = 10; i <= 80; i += 10)
            {
                string outLine = string.Empty;
                for (int s = 1; s <= 8; s++)
                {
                    isReversible.Add(IsReversibleThisPoint(s + i));
                }
            }
            return isReversible.Contains(true);
        }

        /// <summary>
        /// <para>全ての着手可能な手を返す</para>
        /// </summary>
        /// <returns></returns>
        [Obsolete("Discじゃなくてintを返すようにする")]
        public static List<Disc> GetAllPutablePointer()
        {
            List<Disc> retDiscs = new List<Disc>();
            for (int i = 10; i <= 80; i += 10)
            {
                string outLine = string.Empty;
                for (int s = 1; s <= 8; s++)
                {
                    if (IsReversibleThisPoint(s + i))
                    {
                        retDiscs.Add(new Disc((s + i), TurnKeeper.NowTurnColor));
                    }
                }
            }

            return retDiscs;
        }

        /// <summary>
        /// <para>全ての着手可能な手を返す</para>
        /// </summary>
        /// <returns></returns>
        public static List<int> GetAllPutableIndex()
        {
            List<int> retDiscs = new List<int>();
            for (int i = 10; i <= 80; i += 10)
            {
                string outLine = string.Empty;
                for (int s = 1; s <= 8; s++)
                {
                    if (IsReversibleThisPoint(s + i))
                    {
                        retDiscs.Add(s + i);
                    }
                }
            }

            return retDiscs;
        }

        /// <summary>
        /// <para>指定した色の着手可能な手数を返す</para>
        /// </summary>
        /// <returns></returns>
        public static int GetAllPutablePointerCount(Color color)
        {
            bool isOppositeTurn = (TurnKeeper.NowTurnColor != color);

            if (isOppositeTurn) { TurnKeeper.ChangeOnlyColor(); }
            int count = 0;
            for (int i = 10; i <= 80; i += 10)
            {
                string outLine = string.Empty;
                for (int s = 1; s <= 8; s++)
                {
                    if (IsReversibleThisPoint(s + i))
                    {
                        count++;
                    }
                }
            }
            if (isOppositeTurn) { TurnKeeper.ChangeOnlyColor(); }

            return count;
        }

        /// <summary>
        /// <para>手を戻す</para>
        /// </summary>
        public static bool UndoReverse()
        {
            // ひとつもログがない場合は処理終了
            if (m_LogStack.Count < 1) { return false; }

            // スタックからログを取得
            Disc disc;
            while (m_LogStack.Count > 0 && m_LogStack.Peek().Color != Color.Space)
            {
                disc = m_LogStack.Pop();
                m_Colors[disc.Point] = disc.Color;
            }

            // 最後のログを反映
            disc = m_LogStack.Pop();
            m_Colors[disc.Point] = disc.Color;

            return true;
        }

        /// <summary>
        /// <para>石を裏返せる場合はTrueを返す</para>
        /// </summary>
        /// <param name="point"></param>
        public static bool IsReversibleThisPoint(int point)
        {
            //Debug.Assert(m_Colors.ContainsKey(point), "範囲外の座標が指定されました。");
            if (m_Colors[point] != Color.Space) { return false; }

            //List<bool> isReverse = new List<bool>();
            //isReverse.Add(IsReverseOneDirection(point, DIRECTION.DOWN));
            //isReverse.Add(IsReverseOneDirection(point, DIRECTION.DOWN_LEFT));
            //isReverse.Add(IsReverseOneDirection(point, DIRECTION.DOWN_RIGHT));
            //isReverse.Add(IsReverseOneDirection(point, DIRECTION.LEFT));
            //isReverse.Add(IsReverseOneDirection(point, DIRECTION.RIGHT));
            //isReverse.Add(IsReverseOneDirection(point, DIRECTION.UP));
            //isReverse.Add(IsReverseOneDirection(point, DIRECTION.UP_LEFT));
            //isReverse.Add(IsReverseOneDirection(point, DIRECTION.UP_RIGHT));
            //return isReverse.Contains(true);
            if (IsReverseOneDirection(point, DIRECTION.DOWN)) { return true; }
            if (IsReverseOneDirection(point, DIRECTION.DOWN_LEFT)) { return true; }
            if (IsReverseOneDirection(point, DIRECTION.DOWN_RIGHT)) { return true; }
            if (IsReverseOneDirection(point, DIRECTION.LEFT)) { return true; }
            if (IsReverseOneDirection(point, DIRECTION.RIGHT)) { return true; }
            if (IsReverseOneDirection(point, DIRECTION.UP)) { return true; }
            if (IsReverseOneDirection(point, DIRECTION.UP_LEFT)) { return true; }
            if (IsReverseOneDirection(point, DIRECTION.UP_RIGHT)) { return true; }
            return false;
        }

        /// <summary>
        /// <para>指定した座標のイメージファイルURLを取得する</para>
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static string GetPointImageUrl(int point)
        {
            return (IsReversibleThisPoint(point)) ? IMAGE_FILE_URL_MOBILITY : m_Colors[point].ImageUrl;
        }

        /// <summary>
        /// <para>ゲームが終了したかどうかを判断する</para>
        /// </summary>
        /// <returns></returns>
        public static bool IsGameEnd()
        {
            // 全部置き終わってたら終了
            if (TurnKeeper.IsGameEnd()) { return true; }

            // 両ユーザ置く場所がなかったら終了
            if (!IsReversible())
            {
                TurnKeeper.ChangeTurn();
                if (!IsReversible())
                {
                    TurnKeeper.UndoTurn();
                    return true;
                }
                else
                {
                    TurnKeeper.UndoTurn();
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// <para>黒の石数を取得する</para>
        /// </summary>
        /// <returns></returns>
        public static int GetBlackCount()
        {
            var cnt = from a in m_Colors
                      where a.Value == Color.Black
                      select a;

            return cnt.Count();
        }

        /// <summary>
        /// <para>白の石数を取得する</para>
        /// </summary>
        /// <returns></returns>
        public static int GetWhiteCount()
        {
            var cnt = from a in m_Colors
                      where a.Value == Color.White
                      select a.Key.ToString().Count();

            return cnt.Count();
        }

        /// <summary>
        /// <para>回転したログスタックの文字列を返す</para>
        /// </summary>
        /// <returns></returns>
        public static string GetRotateLogStack()
        {
            List<Disc> rotateLogStack = m_LogStack.ToList();
            var logs = from a in rotateLogStack
                       where a.Color == Color.Space
                       select a;
            string retLog = string.Empty;
            foreach (Disc disc in logs.ToList())
            {
                retLog = RotateUtility.GetRotatePoint(disc.Point).ToString() + retLog;
            }

            return retLog;
        }

        /// <summary>
        /// <para>スコアリストをクリアする</para>
        /// </summary>
        public static void ClearScores()
        {
            m_Scores = new Dictionary<int, int>();
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// <para>範囲内の座標ならばTrueを返す</para>
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool IsEnabledPoint(int point)
        {
            return m_Colors.ContainsKey(point);
        }

        /// <summary>
        /// <para>初期化する色を返す</para>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Color GetInitializeColor(int key)
        {
            if (INITIALIZE_BLACK_POINT.Contains(key))
            {
                return Color.Black;
            }
            else if (INITIALIZE_WHITE_POINT.Contains(key))
            {
                return Color.White;
            }
            else
            {
                return Color.Space;
            }
        }

        /// <summary>
        /// <para>指定した方向の石を裏返す</para>
        /// </summary>
        /// <param name="point"></param>
        /// <param name="direc"></param>
        private static bool ReverseOneDirection(int point, DIRECTION direc)
        {
            int tmpPoint = point + (int)direc;
            List<int> archieve = new List<int>();
            bool isReversed = false;

            while(m_Colors.ContainsKey(tmpPoint))
            {
                if (m_Colors[tmpPoint] == TurnKeeper.NowTurnColor.OppositeColor)
                {
                    // 成功するかわからないけど、一応裏返していく
                    m_Colors[tmpPoint] = TurnKeeper.NowTurnColor;
                    archieve.Add(tmpPoint);
                    tmpPoint += (int)direc;
                    isReversed = true;
                }
                else if (m_Colors[tmpPoint] == TurnKeeper.NowTurnColor)
                {
                    if (isReversed)
                    {
                        // 成功
                        m_Colors[point] = TurnKeeper.NowTurnColor;

                        // ログスタックに記録
                        foreach (int num in archieve)
                        {
                            m_LogStack.Push(new Disc(num, TurnKeeper.NowTurnColor.OppositeColor));
                        }
                        return true;
                    }
                    else
                    {
                        // 失敗：隣が自分の色だった
                        return false;
                    }
                }
                else if (m_Colors[tmpPoint] == Color.Space)
                {
                    // 失敗
                    foreach (int num in archieve)
                    {
                        m_Colors[num] = TurnKeeper.NowTurnColor.OppositeColor;
                    }
                    return false;
                }
                else
                {
                    Debug.Assert(m_Colors.ContainsKey(point), "色が特定できませんでした。場所：ReverseOneDirection");
                }
            }

            // 失敗
            foreach (int num in archieve)
            {
                m_Colors[num] = TurnKeeper.NowTurnColor.OppositeColor;
            }
            return false;
        }

        /// <summary>
        /// <para>指定した方向の石を裏返せる場合はTrueを返す</para>
        /// </summary>
        /// <param name="point"></param>
        /// <param name="direc"></param>
        private static bool IsReverseOneDirection(int point, DIRECTION direc)
        {
            int tmpPoint = point + (int)direc;
            bool isReversed = false;

            while (m_Colors.ContainsKey(tmpPoint))
            {
                if (m_Colors[tmpPoint] == Color.Space)
                {
                    // 失敗
                    return false;
                }
                if (m_Colors[tmpPoint] == TurnKeeper.NowTurnColor.OppositeColor)
                {
                    tmpPoint += (int)direc;
                    isReversed = true;
                }
                else if (m_Colors[tmpPoint] == TurnKeeper.NowTurnColor)
                {
                    if (isReversed)
                    {
                        // 成功
                        return true;
                    }
                    else
                    {
                        // 失敗：隣が自分の色だった
                        return false;
                    }
                }
                else
                {
                    Debug.Assert(m_Colors.ContainsKey(point), "色が特定できませんでした。場所：ReverseOneDirection");
                }
            }

            // 失敗
            return false;
        }

        /// <summary>
        /// 盤内に収まっている座標の場合はtrueを返す
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private static bool IsValidatePoint(int point)
        {
            int rest = (point % 10);
            int mod = (point / 10);
            return (rest != 0) && (rest != 9) && (mod != 0) && (mod < 9); 
        }
        #endregion
        #endregion
    }
}
