using UnityEngine;
using System.Collections;

using ReversiCommon.Collections;
using LucaUnity;

namespace LucaUnityCommon.UI
{
    public class ResultText : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.DisplayResult();
        }

        /// <summary>
        /// <para>結果を表示する</para>
        /// </summary>
        private void DisplayResult()
        {
            if (Board.IsGameEnd())
            {
                if (Board.GetBlackCount() > Board.GetWhiteCount())
                {
                    guiText.text = Main.BlackPlayerName + "(Black) Win";
                }
                else if (Board.GetWhiteCount() > Board.GetBlackCount())
                {
                    guiText.text = Main.WhitePlayerName + "(White) Win";
                }
                else
                {
                    guiText.text = "Draw";
                }
            }
            else
            {
                guiText.text = "";
            }
        }
    }
}
