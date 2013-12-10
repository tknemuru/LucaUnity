﻿using UnityEngine;
using System.Collections;

using ReversiCommon.Collections;

namespace LucaUnityCommon.UI
{
    public class ScoreWhite : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.SetScore();
        }

        /// <summary>
        /// スコアをセットする
        /// </summary>
        public void SetScore()
        {
            guiText.text = Board.GetWhiteCount().ToString("00");
        }
    }
}
