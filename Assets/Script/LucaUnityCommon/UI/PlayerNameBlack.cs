using UnityEngine;
using System.Collections;

using ReversiCommon.Collections;
using LucaUnity;

namespace LucaUnityCommon.UI
{
    public class PlayerNameBlack : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.SetName(Main.BlackPlayerName);
        }

        /// <summary>
        /// 名前をセットする
        /// </summary>
        public void SetName(string name)
        {
            guiText.text = name + "(Black)";
        }
    }
}
