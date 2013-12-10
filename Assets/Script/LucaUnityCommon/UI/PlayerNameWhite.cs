using UnityEngine;
using System.Collections;

using ReversiCommon.Collections;
using LucaUnity;

namespace LucaUnityCommon.UI
{
    public class PlayerNameWhite : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.SetName(Main.WhitePlayerName);
        }

        /// <summary>
        /// 名前をセットする
        /// </summary>
        public void SetName(string name)
        {
            guiText.text = name + "(White)";
        }
    }
}
