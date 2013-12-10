using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using TKCommon.Utility;

namespace ReversiCommon.Collections
{
    /// <summary>
    /// スコアインデックス生成クラス
    /// </summary>
    public static class ScoreIndexGenerator
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        #endregion

        #region "コンストラクタ"
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>インデックスリストを取得する</para>
        /// </summary>
        /// <returns></returns>
        public static List<ScoreIndex> GetIndexList()
        {
            List<ScoreIndex> indexList = new List<ScoreIndex>();

            for (int i = 0; i <= 3; i++)
            {
                string digit = (4 + i).ToString();
                indexList.Add(GetCostIndex("diag" + digit, 41 + ((int)Board.DIRECTION.DOWN * i), Board.DIRECTION.UP_RIGHT, digit));
                indexList.Add(GetCostIndex("diag" + digit, 15 + ((int)Board.DIRECTION.LEFT * i), Board.DIRECTION.DOWN_RIGHT, digit));
                indexList.Add(GetCostIndex("diag" + digit, 58 + ((int)Board.DIRECTION.UP * i), Board.DIRECTION.DOWN_LEFT, digit));
                indexList.Add(GetCostIndex("diag" + digit, 84 + ((int)Board.DIRECTION.RIGHT * i), Board.DIRECTION.UP_LEFT, digit));
            }
            indexList.Add(GetCostIndex("diag8", 81, Board.DIRECTION.UP_RIGHT, "8"));
            indexList.Add(GetCostIndex("diag8", 11, Board.DIRECTION.DOWN_RIGHT, "8"));

            for (int i = 0; i <= 2; i++)
            {
                string digit = (2 + i).ToString();
                indexList.Add(GetCostIndex("hor_vert" + digit, 21 + ((int)Board.DIRECTION.DOWN * i), Board.DIRECTION.RIGHT, "8"));
                indexList.Add(GetCostIndex("hor_vert" + digit, 17 + ((int)Board.DIRECTION.LEFT * i), Board.DIRECTION.DOWN, "8"));
                indexList.Add(GetCostIndex("hor_vert" + digit, 78 + ((int)Board.DIRECTION.UP * i), Board.DIRECTION.LEFT, "8"));
                indexList.Add(GetCostIndex("hor_vert" + digit, 82 + ((int)Board.DIRECTION.RIGHT * i), Board.DIRECTION.UP, "8"));
            }

            indexList.Add(GetEdgeCostIndex("edge2X", 11, Board.DIRECTION.RIGHT, 22, 27, "10"));
            indexList.Add(GetEdgeCostIndex("edge2X", 18, Board.DIRECTION.DOWN, 27, 77, "10"));
            indexList.Add(GetEdgeCostIndex("edge2X", 88, Board.DIRECTION.LEFT, 77, 72, "10"));
            indexList.Add(GetEdgeCostIndex("edge2X", 81, Board.DIRECTION.UP, 72, 22, "10"));

            indexList.Add(GetCornerCostIndex("corner2X5", 11, Board.DIRECTION.RIGHT, 5, Board.DIRECTION.DOWN, 2, "10"));
            indexList.Add(GetCornerCostIndex("corner2X5", 11, Board.DIRECTION.DOWN, 5, Board.DIRECTION.RIGHT, 2, "10"));
            indexList.Add(GetCornerCostIndex("corner2X5", 18, Board.DIRECTION.LEFT, 5, Board.DIRECTION.DOWN, 2, "10"));
            indexList.Add(GetCornerCostIndex("corner2X5", 18, Board.DIRECTION.DOWN, 5, Board.DIRECTION.LEFT, 2, "10"));
            indexList.Add(GetCornerCostIndex("corner2X5", 88, Board.DIRECTION.UP, 5, Board.DIRECTION.LEFT, 2, "10"));
            indexList.Add(GetCornerCostIndex("corner2X5", 88, Board.DIRECTION.LEFT, 5, Board.DIRECTION.UP, 2, "10"));
            indexList.Add(GetCornerCostIndex("corner2X5", 81, Board.DIRECTION.RIGHT, 5, Board.DIRECTION.UP, 2, "10"));
            indexList.Add(GetCornerCostIndex("corner2X5", 81, Board.DIRECTION.UP, 5, Board.DIRECTION.RIGHT, 2, "10"));

            indexList.Add(GetCornerCostIndex("corner3X3", 11, Board.DIRECTION.RIGHT, 3, Board.DIRECTION.DOWN, 3, "9"));
            indexList.Add(GetCornerCostIndex("corner3X3", 18, Board.DIRECTION.LEFT, 3, Board.DIRECTION.DOWN, 3, "9"));
            indexList.Add(GetCornerCostIndex("corner3X3", 88, Board.DIRECTION.UP, 3, Board.DIRECTION.LEFT, 3, "9"));
            indexList.Add(GetCornerCostIndex("corner3X3", 81, Board.DIRECTION.RIGHT, 3, Board.DIRECTION.UP, 3, "9"));

            return indexList;
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// <para>指定したインデックスを作成する</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startPoint"></param>
        /// <param name="direc"></param>
        /// <returns></returns>
        private static ScoreIndex GetCostIndex(string name, int startPoint, Board.DIRECTION direc, string digit)
        {
            List<int> points = new List<int>();
            int tmpPoint = startPoint;
            int index = 0;
            while(true)
            {
                index += Board.GetSquareColor(tmpPoint).ColorNumber;
                tmpPoint += (int)direc;
                if (Board.IsEnabledPoint(tmpPoint))
                {
                    index = index * 3;
                }
                else
                {
                    break;
                }
            }

            string key = NormalizeIndex.GetIndexKey(index.ToString(), digit);
            return new ScoreIndex(NormalizeIndex.DirectGet(key).ToString() + name, NormalizeIndex.DirectGetNormalizeType(key));
        }

        /// <summary>
        /// <para>指定したインデックスを作成する（edge用）</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startPoint"></param>
        /// <param name="direc"></param>
        /// <param name="wingR"></param>
        /// <param name="wingL"></param>
        /// <returns></returns>
        private static ScoreIndex GetEdgeCostIndex(string name, int startPoint, Board.DIRECTION direc, int wingR, int wingL, string digit)
        {
            int tmpPoint = startPoint;
            int index = 0;
            while (Board.IsEnabledPoint(tmpPoint))
            {
                index = (index + Board.GetSquareColor(tmpPoint).ColorNumber) * 3;
                tmpPoint += (int)direc;
            }
            index = (index + Board.GetSquareColor(wingR).ColorNumber) * 3;
            index = (index + Board.GetSquareColor(wingL).ColorNumber);

            string key = NormalizeIndex.GetIndexKey(index.ToString(), digit);
            return new ScoreIndex(NormalizeIndex.DirectGet(key).ToString() + name, NormalizeIndex.DirectGetNormalizeType(key));
        }

        /// <summary>
        /// <para>指定したインデックスを作成する（corner用）</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startPoint"></param>
        /// <param name="direc"></param>
        /// <returns></returns>
        private static ScoreIndex GetCornerCostIndex(string name, int startPoint, Board.DIRECTION direc, int cnt, Board.DIRECTION incrementDirec, int incrementCnt, string digit)
        {
            int tmpPoint = startPoint;
            int index = 0;

            for (int i = 0; i < incrementCnt; i++)
            {
                for (int s = 0; s < cnt; s++)
                {
                    if (i == incrementCnt - 1 && s == cnt - 1)
                    {
                        index = (index + Board.GetSquareColor(tmpPoint).ColorNumber);
                    }
                    else
                    {
                        index = (index + Board.GetSquareColor(tmpPoint).ColorNumber) * 3;
                    }
                    tmpPoint += (int)direc;
                }
                tmpPoint = startPoint + (int)incrementDirec;
            }

            string key = NormalizeIndex.GetIndexKey(index.ToString(), digit);
            return new ScoreIndex(NormalizeIndex.DirectGet(key).ToString() + name, NormalizeIndex.DirectGetNormalizeType(key));
        }
        #endregion
        #endregion
    }
}
