using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using LucaUnityCommon.UI;
using ReversiCommon.Collections;
using ReversiCommon.Players;
using ReversiCommon.Utility;
using ReversiCommon.Strategys;

namespace LucaUnity
{
    public class Main : MonoBehaviour
    {
        #region "定数"
        /// <summary>
        /// 石のY座標
        /// </summary>
        private const float DISC_Y_POSITION = 1f;

        /// <summary>
        /// 石が置かれるまでの秒数
        /// </summary>
        private const float DISC_SETTED_TIME = 0.1f;

        /// <summary>
        /// 着手可能マークのY座標
        /// </summary>
        private const float MOBILITY_MARK_Y_POSITION = 1f;

        /// <summary>
        /// Z座標
        /// </summary>
        private const float SCREEN_POINT_Z_POSITION = 8.94f;
        #endregion

        #region "メンバ変数"
        /// <summary>
        /// 石プレハブ
        /// </summary>
        public GameObject DiscPrefab;

        /// <summary>
        /// 着手可能マークプレハブ
        /// </summary>
        public GameObject MobilityMarkPrefab;

        /// <summary>
        /// 結果を表示するプレハブ
        /// </summary>
        public GameObject ResultPlanePrefab;

        /// <summary>
        /// <para>プレイヤーリスト</para>
        /// </summary>
        private static Dictionary<ReversiCommon.Collections.Color, Player> m_PlayerList;

        /// <summary>
        /// コンピュータが思考中の場合はtrue
        /// </summary>
        private bool m_IsComThinking;

        /// <summary>
        /// コンピュータの思考中表示に使用するタイマー
        /// </summary>
        private float m_ThinkTimer;
        #endregion

        #region "コンストラクタ"
        #endregion

        #region "プロパティ"
        #region "公開プロパティ"
        /// <summary>
        /// 黒のプレイヤ名
        /// </summary>
        public static string BlackPlayerName
        {
            get
            {
                return m_PlayerList[ReversiCommon.Collections.Color.Black].Name;
            }
        }

        /// <summary>
        /// 白のプレイヤ名
        /// </summary>
        public static string WhitePlayerName
        {
            get
            {
                return m_PlayerList[ReversiCommon.Collections.Color.White].Name;
            }
        }
        #endregion

        #region "内部プロパティ"
        /// <summary>
        /// リバーシゲーム上の座標
        /// </summary>
        private int Point
        {
            get
            {
                var screenPoint = Input.mousePosition;
                screenPoint.z = SCREEN_POINT_Z_POSITION;
                var v = camera.ScreenToWorldPoint(screenPoint);
                var cnvX = PointConverter.X(v.x);
                var cnvZ = PointConverter.Z(v.z);
                
                return PointConverter.Point(cnvX, cnvZ);
            }
        }
        #endregion
        #endregion

        #region "メソッド"
        #region "公開メソッド"
        // Use this for initialization
        void Start()
        {
            // コンピュータは思考していない
            this.m_IsComThinking = false;
            this.m_ThinkTimer = 0f;

            // プレイヤーの作成
            this.SetPlayer();

            // UI盤の初期化
            UIBoard.InitializeUIBoard(this.LoadDisc, this.LoadMobilityMark);

            // ゲーム開始
            this.RunGame();
        }

        // Update is called once per frame
        void Update()
        {
            if (m_PlayerList[TurnKeeper.NowTurnColor] is PlayerHuman)
            {
                if (!Input.GetButtonDown("Fire1")) { return; }

                int point = this.Point;
                // 配置不可能なら何もしない
                if (!Board.IsReversibleThisPoint(point)) { return; }

                System.Diagnostics.Debug.Assert(m_PlayerList[TurnKeeper.NowTurnColor] is PlayerHuman, "人間プレイヤー以外を操作しようとしました。");
                // 次に置く場所を確定する
                m_PlayerList[TurnKeeper.NowTurnColor].SetNextPointer(point);

                // ターンをまわす
                this.NextTurn();

                // 画面情報を更新する
                this.UpdateDisplay();
            }
            else
            {
                // コンピュータの思考
                this.ComputerThink();
            }            
        }

        /// <summary>
        /// GUIオブジェクトの処理
        /// </summary>
        void OnGUI()
        {
            if (!this.m_IsComThinking) { return; }

            GUI.Box(new Rect(((Screen.width / 2f) - 200), ((Screen.height / 2f) - 15), 300, 30), "thinking...");
            //TextAsset txt = Instantiate(Resources.Load("hoge", typeof(TextAsset))) as TextAsset;
            //GUI.Box(new Rect(100, 250, 300, 30), txt.text);
        }
        #endregion

        #region "内部メソッド"
        /// <summary>
        /// コンピュータの思考メソッド
        /// </summary>
        private void ComputerThink()
        {
            // 相手の石が置かれてから思考開始
            this.m_ThinkTimer += Time.deltaTime;
            if (this.m_ThinkTimer < DISC_SETTED_TIME)
            {
                return;
            }
            else if (!this.m_IsComThinking)
            {
                this.m_IsComThinking = true;
            }
            else
            {
                this.RunGame();
                this.m_ThinkTimer = 0f;
                this.m_IsComThinking = false;
            }
        }

        /// <summary>
        /// ゲームオブジェクトを読み込む
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private GameObject LoadGameObject(GameObject orgObj)
        {
            var obj = GameObject.Instantiate(orgObj) as GameObject;
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;

            return obj;
        }

        /// <summary>
        /// 盤の上にゲームオブジェクトをセットする
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private GameObject SetOnBoard(GameObject obj, int point, float y)
        {
            Vector3 dir = new Vector3(PointConverter.PointToX(point), y, PointConverter.PointToZ(point));
            obj.transform.position = dir;
            return obj;
        }

        /// <summary>
        /// 石を読み込む
        /// </summary>
        /// <returns></returns>
        public GameObject LoadDisc(ReversiCommon.Collections.Color color, int point)
        {
            var disc = this.LoadGameObject(DiscPrefab);
            if (color == ReversiCommon.Collections.Color.White)
            {
                disc.transform.rotation = Quaternion.Euler(180, 0, 0);
            }

            return this.SetOnBoard(disc, point, DISC_Y_POSITION);
        }

        /// <summary>
        /// 着手可能マークを読み込む
        /// </summary>
        /// <returns></returns>
        public GameObject LoadMobilityMark(int point)
        {
            var mark = this.LoadGameObject(MobilityMarkPrefab);

            return this.SetOnBoard(mark, point, MOBILITY_MARK_Y_POSITION);
        }

        /// <summary>
        /// <para>プレイヤーをセットする</para>
        /// </summary>
        private void SetPlayer()
        {
            m_PlayerList = new Dictionary<ReversiCommon.Collections.Color, Player>();
            m_PlayerList.Add(ReversiCommon.Collections.Color.White, new PlayerCpu("Com", ReversiCommon.Collections.Color.White, new StrategyCpu()));
            m_PlayerList.Add(ReversiCommon.Collections.Color.Black, new PlayerHuman("You", ReversiCommon.Collections.Color.Black));

            // [TODO]名前ラベルの設定
            //this.lblBlackUserName.Text = m_PlayerList[ReversiCommon.Collections.Color.Black].Name;
            //this.lblWhiteUserName.Text = m_PlayerList[ReversiCommon.Collections.Color.White].Name;
        }

        /// <summary>
        /// <para>画面情報を更新する</para>
        /// </summary>
        private void UpdateDisplay()
        {
            // [TODO]石数情報の更新
            //this.lblBlackCount.Text = Board.GetBlackCount().ToString();
            //this.lblWhiteCount.Text = Board.GetWhiteCount().ToString();

            // [TODO]ターン情報の更新
            //this.lblBlackTurn.Text = string.Empty;
            //this.lblWhiteTurn.Text = string.Empty;
            //if (TurnKeeper.NowTurnColor == ReversiCommon.Collections.Color.Black)
            //{
            //    this.lblBlackTurn.Text = TURN_STR;
            //}
            //else
            //{
            //    this.lblWhiteTurn.Text = TURN_STR;
            //}

            UIBoard.UpdateUIBoard(this.LoadDisc, this.LoadMobilityMark);

            // [TODO]情報ラベルの更新
            //this.lblInfo.Text = ReversiInformation.ToString();
        }

        /// <summary>
        /// <para>ゲームを実行する</para>
        /// </summary>
        private void RunGame()
        {
            if (!Board.IsGameEnd())
            {
                // 置けるところがなかったらパス
                if (Board.IsReversible())
                {
                    if (m_PlayerList[TurnKeeper.NowTurnColor] is PlayerHuman)
                    {
                        return;
                    }
                    else
                    {
                        // ターンをまわす
                        this.NextTurn();

                        // 画面情報を更新する
                        this.UpdateDisplay();

                        // ゲーム続行
                        this.RunGame();
                    }
                }
                else
                {
                    ReversiInformation.Add(m_PlayerList[TurnKeeper.NowTurnColor].Name + "はパスしました。");

                    // ターンをまわす
                    TurnKeeper.ChangeOnlyColor();

                    // 画面情報を更新する
                    this.UpdateDisplay();

                    // ゲーム続行
                    this.RunGame();
                }
            }
            else
            {
                // 結果表示
                this.DisplayResult();
            }
        }

        /// <summary>
        /// <para>ターンをまわす</para>
        /// </summary>
        private void NextTurn()
        {
            // 石を置く位置を取得
            Disc pointer = m_PlayerList[TurnKeeper.NowTurnColor].GetNextPointer();

            // 石を置いて裏返す
            Board.Reverse(pointer.Point);

            // 初回は回転方法を決める
            if (TurnKeeper.NowTurnNumber == 1)
            {
                RotateUtility.SetRotateMethod(pointer.Point);
            }

            // ターンをまわす
            TurnKeeper.ChangeTurn();
        }

        /// <summary>
        /// <para>結果を表示する</para>
        /// </summary>
        private void DisplayResult()
        {
            this.m_IsComThinking = false;

            GameObject resultPlane = this.LoadGameObject(this.ResultPlanePrefab);
            Vector3 dir = new Vector3(4f, 2f, 4f);
            resultPlane.transform.position = dir;
        }
        #endregion
        #endregion
    }
}

