using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Diagnostics;

namespace TKCommon.Utility
{
    /// <summary>
    /// <para>計算ユーティリティクラス</para>
    /// </summary>
    public static class MathUtility
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
        /// <para>べき乗の合算値を返す</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int GetPows(int x, int y)
        {
            int result = 0;
            for(int i = 0; i < y; i++)
            {
                result += (int)Math.Pow(x, i);
            }

            return result;
        }

        /// <summary>
        /// <para>大きい方の数値を返す</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T Max<T>(T x, T y)
            where T : IComparable
        {
            return x.CompareTo(y) > 0 ? x : y;
        }

        /// <summary>
        /// <para>小さい方の数値を返す</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T Min<T>(T x, T y)
            where T : IComparable
        {
            return x.CompareTo(y) < 0 ? x : y;
        }

        /// <summary>
        /// ベクトルに行列を作用させる y = Ax
        /// </summary>
        /// <param name="vectorX"></param>
        /// <param name="matrixA"></param>
        /// <returns></returns>
        public static List<double> VectorByMatrix(List<List<double>> matrixA, List<double> vectorX)
        {
            int height = matrixA.Count();
            int width = matrixA[0].Count();

            Debug.Assert((width == vectorX.Count()), "行列の列数とベクトルの成分数が一致していません。");

            List<double> newVectorY = new List<double>();
            for (int h = 0; h < height; h++)
            {
                double newValue = 0;
                for (int w = 0; w < width; w++)
                {
                    newValue += matrixA[h][w] * vectorX[w];
                }
                newVectorY.Add(newValue);
            }

            return newVectorY;
        }

        /// <summary>
        /// ベクトル同士の積を求める
        /// </summary>
        /// <param name="matrixX"></param>
        /// <param name="matrixY"></param>
        /// <returns></returns>
        public static List<List<double>> MatrixByMatrix(List<List<double>> matrixX, List<List<double>> matrixY)
        {
            // Xの列数とYの行数が同一でなければ積は不可能
            if (matrixX[0].Count() != matrixY.Count())
            {
                throw new ApplicationException("Xの列数とYの行数が異なるため、ベクトルの積が行えません。");
            }
            else if ((matrixX.Count() == 1) || (matrixX[0].Count() == 1) || (matrixY.Count() == 1) || (matrixY[0].Count() == 1))
            {
                throw new ApplicationException("行列とベクトルの積はVectorByMatrixで行ってください。");
            }

            int limitWidthY = matrixY[0].Count();
            int limitHeightY = matrixY.Count();
            int limitHeightX = matrixX.Count();
            List<List<double>> retMatrix = new List<List<double>>();
            for (int heightX = 0; heightX < limitHeightX; heightX++)
            {
                List<double> line = new List<double>();
                for (int widthY = 0; widthY < limitWidthY; widthY++)
                {
                    double value = 0;
                    for (int heightY = 0; heightY < limitHeightY; heightY++)
                    {
                        value += (matrixY[heightY][widthY] * matrixX[heightX][heightY]);
                    }
                    line.Add(value);
                }
                retMatrix.Add(line);
            }

            return retMatrix;
        }

        /// <summary>
        /// ベクトルの内積を求める
        /// </summary>
        /// <param name="vectorX"></param>
        /// <param name="vectorY"></param>
        /// <returns></returns>
        public static double DotProduct(List<double> vectorX, List<double> vectorY)
        {
            Debug.Assert((vectorX.Count() == vectorY.Count()), "ベクトルの成分数が一致していません。");

            int limit = vectorX.Count();
            double retDotProduct = 0;
            for (int i = 0; i < limit; i++)
            {
                retDotProduct += (vectorX[i] * vectorY[i]);
            }

            return retDotProduct;
        }

        /// <summary>
        /// ベクトルノルムを求める
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static double VectorNorm(List<double> vector)
        {
            double norm = 0;
            foreach (double value in vector)
            {
                norm += Math.Abs(value);
            }
            return norm;
        }

        /// <summary>
        /// 行列を転置する
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static List<List<double>> TranspositionMatrix(List<List<double>> matrix)
        {
            List<List<double>> newMatrix = new List<List<double>>();
            for (int width = 0; width < matrix[0].Count(); width++)
            {
                List<double> line = new List<double>();
                for (int height = 0; height < matrix.Count(); height++)
                {
                    line.Add(matrix[height][width]);
                }
                newMatrix.Add(line);
            }
            return newMatrix;
        }

        /// <summary>
        /// mXn列の行列をnXn列の正方行列に変換する
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static List<List<double>> ConvertSquareMatrix(List<List<double>> A)
        {
            // 転置行列を生成
            List<List<double>> At = MathUtility.TranspositionMatrix(A);

            // 転置行列との積を求める
            return MathUtility.MatrixByMatrix(At, A);
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
