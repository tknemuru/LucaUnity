using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using TKCommon.Collections;
using System.Diagnostics;
using System.Threading;

namespace TKCommon.Utility
{
    public class SparseVectorUtility
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
        /// ベクトルに行列を作用させる y = Ax
        /// </summary>
        /// <param name="vectorX"></param>
        /// <param name="matrixA"></param>
        /// <returns></returns>
        public static SparseVector<double> VectorByMatrix(SparseMatrix<double> matrixA, SparseVector<double> vectorX)
        {
            Debug.Assert((matrixA.Width == vectorX.Count()), "行列の列数とベクトルの成分数が一致していません。");

            SparseVector<double> newVectorY = new SparseVector<double>(matrixA.Height, 0);
            foreach (KeyValuePair<int, double> keyValue in matrixA.NoSparseKeyValues)
            {
                // TODO:vectorXは0の可能性がある。その場合は計算が無駄になる。もっといい方法があるかもしれない。
                newVectorY[matrixA.DeserializeY(keyValue.Key)] += keyValue.Value * vectorX[matrixA.DeserializeX(keyValue.Key)];
            }

            return newVectorY;
        }

        /// <summary>
        /// ベクトル同士の積を求める
        /// </summary>
        /// <param name="matrixX"></param>
        /// <param name="matrixY"></param>
        /// <returns></returns>
        public static SparseMatrix<double> MatrixByMatrix(SparseMatrix<double> matrixX, SparseMatrix<double> matrixY)
        {
            // Xの列数とYの行数が同一でなければ積は不可能
            if (matrixX.Width != matrixY.Height)
            {
                throw new ApplicationException("Xの列数とYの行数が異なるため、ベクトルの積が行えません。");
            }
            else if ((matrixX.Height == 1) || (matrixX.Width == 1) || (matrixY.Height == 1) || (matrixY.Width == 1))
            {
                throw new ApplicationException("行列とベクトルの積はVectorByMatrixで行ってください。");
            }

            SparseMatrix<double> retMatrix = new SparseMatrix<double>(matrixX.Height, matrixY.Width, 0);
            foreach (KeyValuePair<int, double> keyValueX in matrixX.NoSparseKeyValues)
            {
                foreach (KeyValuePair<int, double> keyValueY in matrixY.NoSparseKeyValuesOneRow(matrixX.DeserializeX(keyValueX.Key)))
                {
                    retMatrix[matrixX.DeserializeY(keyValueX.Key), matrixY.DeserializeX(keyValueY.Key)] += (keyValueX.Value * keyValueY.Value);
                }
            }

            return retMatrix;
        }

        private static Dictionary<int, double> GetTestDic()
        {
            Dictionary<int, double> test = new Dictionary<int, double>();

            for (int i = 0; i < 30; i++)
            {
                test.Add(i, i);
            }
            return test;
        }

        ///// <summary>
        ///// ベクトル同士の積を求める
        ///// </summary>
        ///// <param name="matrixX"></param>
        ///// <param name="matrixY"></param>
        ///// <returns></returns>
        //public static List<SparseVector<double>> MatrixByMatrix(List<SparseVector<double>> matrixX, List<SparseVector<double>> matrixY)
        //{
        //    // Xの列数とYの行数が同一でなければ積は不可能
        //    if (matrixX[0].Count() != matrixY.Count())
        //    {
        //        throw new ApplicationException("Xの列数とYの行数が異なるため、ベクトルの積が行えません。");
        //    }
        //    else if ((matrixX.Count() == 1) || (matrixX[0].Count() == 1) || (matrixY.Count() == 1) || (matrixY[0].Count() == 1))
        //    {
        //        throw new ApplicationException("行列とベクトルの積はVectorByMatrixで行ってください。");
        //    }

        //    long limitWidthY = matrixY[0].Count();
        //    int limitHeightY = matrixY.Count();
        //    int limitHeightX = matrixX.Count();
        //    List<SparseVector<double>> retMatrix = new List<SparseVector<double>>();
        //    for (int heightX = 0; heightX < limitHeightX; heightX++)
        //    {
        //        SparseVector<double> line = new SparseVector<double>(0);
        //        for (int widthY = 0; widthY < limitWidthY; widthY++)
        //        {
        //            double value = 0;
        //            foreach (KeyValuePair<int, double> keyValue in matrixX[heightX].NoSparseKeyValues)
        //            {
        //                value += (matrixY[keyValue.Key][widthY] * keyValue.Value);
        //            }
        //            line.Add(value);
        //        }
        //        retMatrix.Add(line);
        //    }

        //    return retMatrix;
        //}

        /// <summary>
        /// ベクトルの内積を求める
        /// </summary>
        /// <param name="vectorX"></param>
        /// <param name="vectorY"></param>
        /// <returns></returns>
        public static double DotProduct(SparseVector<double> vectorX, SparseVector<double> vectorY)
        {
            Debug.Assert((vectorX.Count() == vectorY.Count()), "ベクトルの成分数が一致していません。");

            long limit = vectorX.Count();
            double retDotProduct = 0;
            foreach (KeyValuePair<int, double> keyValue in vectorY.NoSparseKeyValues)
            {
                retDotProduct += (vectorX[keyValue.Key] * keyValue.Value);
            }

            return retDotProduct;
        }

        /// <summary>
        /// ベクトルノルムを求める
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static double VectorNorm(SparseVector<double> vector)
        {
            double norm = 0;
            foreach(KeyValuePair<int, double> keyValue in vector.NoSparseKeyValues)
            {
                norm += Math.Abs(keyValue.Value);
            }

            return norm;
        }

        /// <summary>
        /// 行列を転置する
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static SparseMatrix<double> TranspositionMatrix(SparseMatrix<double> matrix)
        {
            SparseMatrix<double> newMatrix = new SparseMatrix<double>(matrix.Width, matrix.Height, 0);

            foreach (KeyValuePair<int, double> keyValue in matrix.NoSparseKeyValues)
            {
                newMatrix[matrix.DeserializeX(keyValue.Key), matrix.DeserializeY(keyValue.Key)] = keyValue.Value;
            }

            return newMatrix;
        }

        /// <summary>
        /// mXn列の行列をnXn列の正方行列に変換する
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static SparseMatrix<double> ConvertSquareMatrix(SparseMatrix<double> A)
        {
            // 横長はNG
            Debug.Assert((A.Height >= A.Width), "横長の行列を正方行列にすることはできません。");

            // 転置行列を生成
            SparseMatrix<double> At = SparseVectorUtility.TranspositionMatrix(A);

            // 転置行列との積を求める
            return SparseVectorUtility.MatrixByMatrix(At, A);
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
