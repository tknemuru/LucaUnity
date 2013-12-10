using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReversiCommon.Collections;

namespace ReversiCommon.Evaluators
{
    /// <summary>
    /// <para>評価関数クラスインターフェイス</para>
    /// </summary>
    public interface IEvaluator
    {
        /// <summary>
        /// <para>評価値を取得</para>
        /// </summary>
        /// <returns></returns>
        double GetEvaluate();
    }
}
