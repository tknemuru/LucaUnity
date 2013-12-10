using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReversiCommon.Collections;

namespace ReversiCommon.Searchs
{
    /// <summary>
    /// <para>探索アルゴリズムインターフェイス</para>
    /// </summary>
    public interface ISearch<T>
    {
        /// <summary>
        /// <para>最善手を探索して取得する</para>
        /// </summary>
        /// <returns></returns>
        T SearchBestValue();

        /// <summary>
        /// 評価値
        /// </summary>
        double Value { get; }
    }
}
