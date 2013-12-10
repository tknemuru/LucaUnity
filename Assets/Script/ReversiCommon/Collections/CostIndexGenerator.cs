using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ReversiCommon.Collections
{
    /// <summary>
    /// <para>評価値インデックスクラス</para>
    /// </summary>
    public class CostIndexGenerator
    {
        #region "定数"
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// <para>インデックス名</para>
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// <para>含まれている座標</para>
        /// </summary>
        private List<int> Points { get; set; }

        /// <summary>
        /// <para>インデックス</para>
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// 概要
        /// </summary>
        public string Description { get; private set; }
        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// <para>コンストラクタ</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="points"></param>
        private CostIndexGenerator(string name, List<int> points, int index, string description)
        {
            this.Name = name;
            this.Points = points;
            this.Index = index;
            this.Description = description;
        }
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        /// <summary>
        /// <para>インデックスリストを取得する</para>
        /// </summary>
        /// <returns></returns>
        public static List<CostIndexGenerator> GetIndexList()
        {
            List<CostIndexGenerator> indexList = new List<CostIndexGenerator>();

            for (int i = 0; i <= 3; i++)
            {
                indexList.Add(GetCostIndex("diag" + (4 + i).ToString(), 41 + ((int)Board.DIRECTION.DOWN * i), Board.DIRECTION.UP_RIGHT, string.Format("diag{0} 左⇒上", (4 + i))));
                indexList.Add(GetCostIndex("diag" + (4 + i).ToString(), 15 + ((int)Board.DIRECTION.LEFT * i), Board.DIRECTION.DOWN_RIGHT, string.Format("diag{0} 上⇒右", (4 + i))));
                indexList.Add(GetCostIndex("diag" + (4 + i).ToString(), 58 + ((int)Board.DIRECTION.UP * i), Board.DIRECTION.DOWN_LEFT, string.Format("diag{0} 右⇒下", (4 + i))));
                indexList.Add(GetCostIndex("diag" + (4 + i).ToString(), 84 + ((int)Board.DIRECTION.RIGHT * i), Board.DIRECTION.UP_LEFT, string.Format("diag{0} 下⇒左", (4 + i))));
            }
            indexList.Add(GetCostIndex("diag8", 81, Board.DIRECTION.UP_RIGHT, "diag8 左下⇒右上"));
            indexList.Add(GetCostIndex("diag8", 11, Board.DIRECTION.DOWN_RIGHT, "diag8 左上⇒右下"));

            for (int i = 0; i <= 2; i++)
            {
                indexList.Add(GetCostIndex("hor_vert" + (2 + i).ToString(), 21 + ((int)Board.DIRECTION.DOWN * i), Board.DIRECTION.RIGHT, string.Format("hor_vert{0} 左⇒右", (2 + i))));
                indexList.Add(GetCostIndex("hor_vert" + (2 + i).ToString(), 17 + ((int)Board.DIRECTION.LEFT * i), Board.DIRECTION.DOWN, string.Format("hor_vert{0} 上⇒下", (2 + i))));
                indexList.Add(GetCostIndex("hor_vert" + (2 + i).ToString(), 78 + ((int)Board.DIRECTION.UP * i), Board.DIRECTION.LEFT, string.Format("hor_vert{0} 右⇒左", (2 + i))));
                indexList.Add(GetCostIndex("hor_vert" + (2 + i).ToString(), 82 + ((int)Board.DIRECTION.RIGHT * i), Board.DIRECTION.UP, string.Format("hor_vert{0} 下⇒上", (2 + i))));
            }

            indexList.Add(GetEdgeCostIndex("edge2X", 11, Board.DIRECTION.RIGHT, 22, 27, "edge2X 上"));
            indexList.Add(GetEdgeCostIndex("edge2X", 18, Board.DIRECTION.DOWN, 27, 77, "edge2X 右"));
            indexList.Add(GetEdgeCostIndex("edge2X", 88, Board.DIRECTION.LEFT, 77, 72, "edge2X 下"));
            indexList.Add(GetEdgeCostIndex("edge2X", 81, Board.DIRECTION.UP, 72, 22, "edge2X 左"));

            //if (TurnKeeper.NowStage <= 7)
            //{
            indexList.Add(GetCornerCostIndex("corner2X5", 11, Board.DIRECTION.RIGHT, 5, Board.DIRECTION.DOWN, 2, "corner2X5 左上⇒右"));
            indexList.Add(GetCornerCostIndex("corner2X5", 11, Board.DIRECTION.DOWN, 5, Board.DIRECTION.RIGHT, 2, "corner2X5 左上⇒下"));
            indexList.Add(GetCornerCostIndex("corner2X5", 18, Board.DIRECTION.LEFT, 5, Board.DIRECTION.DOWN, 2, "corner2X5 右上⇒左"));
            indexList.Add(GetCornerCostIndex("corner2X5", 18, Board.DIRECTION.DOWN, 5, Board.DIRECTION.LEFT, 2, "corner2X5 右上⇒下"));
            indexList.Add(GetCornerCostIndex("corner2X5", 88, Board.DIRECTION.UP, 5, Board.DIRECTION.LEFT, 2, "corner2X5 右下⇒上"));
            indexList.Add(GetCornerCostIndex("corner2X5", 88, Board.DIRECTION.LEFT, 5, Board.DIRECTION.UP, 2, "corner2X5 右下⇒左"));
            indexList.Add(GetCornerCostIndex("corner2X5", 81, Board.DIRECTION.RIGHT, 5, Board.DIRECTION.UP, 2, "corner2X5 左下⇒右"));
            indexList.Add(GetCornerCostIndex("corner2X5", 81, Board.DIRECTION.UP, 5, Board.DIRECTION.RIGHT, 2, "corner2X5 左下⇒上"));
            //}

            indexList.Add(GetCornerCostIndex("corner3X3", 11, Board.DIRECTION.RIGHT, 3, Board.DIRECTION.DOWN, 3, "corner3X3 上"));
            indexList.Add(GetCornerCostIndex("corner3X3", 18, Board.DIRECTION.LEFT, 3, Board.DIRECTION.DOWN, 3, "corner3X3 右"));
            indexList.Add(GetCornerCostIndex("corner3X3", 88, Board.DIRECTION.UP, 3, Board.DIRECTION.LEFT, 3, "corner3X3 下"));
            indexList.Add(GetCornerCostIndex("corner3X3", 81, Board.DIRECTION.RIGHT, 3, Board.DIRECTION.UP, 3, "corner3X3 左"));

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
        private static CostIndexGenerator GetCostIndex(string name, int startPoint, Board.DIRECTION direc, string description)
        {
            List<int> points = new List<int>();
            int tmpPoint = startPoint;
            int index = 0;
            while(Board.IsEnabledPoint(tmpPoint))
            {
                index += Board.GetSquareColor(tmpPoint).ColorNumber;
                points.Add(tmpPoint);
                tmpPoint += (int)direc;
                if (Board.IsEnabledPoint(tmpPoint))
                {
                    index = index * 3;
                }
            }

            return new CostIndexGenerator(name, points, index, description);
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
        private static CostIndexGenerator GetEdgeCostIndex(string name, int startPoint, Board.DIRECTION direc, int wingR, int wingL, string description)
        {
            List<int> points = new List<int>();
            int tmpPoint = startPoint;
            int index = 0;
            while (Board.IsEnabledPoint(tmpPoint))
            {
                index = (index + Board.GetSquareColor(tmpPoint).ColorNumber) * 3;
                points.Add(tmpPoint);
                tmpPoint += (int)direc;
            }
            index = (index + Board.GetSquareColor(wingR).ColorNumber) * 3;
            index = (index + Board.GetSquareColor(wingL).ColorNumber);
            points.Add(wingR);
            points.Add(wingL);

            return new CostIndexGenerator(name, points, index, description);
        }

        /// <summary>
        /// <para>指定したインデックスを作成する（corner用）</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startPoint"></param>
        /// <param name="direc"></param>
        /// <returns></returns>
        private static CostIndexGenerator GetCornerCostIndex(string name, int startPoint, Board.DIRECTION direc, int cnt, Board.DIRECTION incrementDirec, int incrementCnt, string description)
        {
            List<int> points = new List<int>();
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
                    points.Add(tmpPoint);
                    tmpPoint += (int)direc;
                }
                tmpPoint = startPoint + (int)incrementDirec;
            }

            return new CostIndexGenerator(name, points, index, description);
        }
        #endregion
        #endregion
    }
}
