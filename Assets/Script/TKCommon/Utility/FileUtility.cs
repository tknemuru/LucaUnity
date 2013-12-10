using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.IO;

namespace TKCommon.Utility
{
    /// <summary>
    /// <para>ファイルユーティリティクラス</para>
    /// </summary>
    public static class FileUtility
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
        /// <para>ファイルからバイト配列のデータを取得する</para>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] GetByteData(string filePath)
        {
            //ファイルを開く
            System.IO.FileStream fs = new System.IO.FileStream(filePath
                                                              , System.IO.FileMode.Open
                                                              , System.IO.FileAccess.Read);

            byte[] bs;
            using (fs)
            {
                //ファイルを読み込むバイト型配列を作成する
                bs = new byte[fs.Length];
                //ファイルの内容をすべて読み込む
                fs.Read(bs, 0, bs.Length);
            }
            return bs;
        }

        /// <summary>
        /// <para>ファイルからバイト配列のデータを取得する</para>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<byte> GetByteListData(string filePath)
        {
            return GetByteData(filePath).ToList();
        }

        /// <summary>
        /// <para>ファイルから文字列配列のデータを取得する</para>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> ReadListData(string filePath, Encoding encoding = null)
        {
            if (encoding == null) { encoding = Encoding.GetEncoding("Shift_JIS"); }

            List<string> list = new List<string>();
            string line;
            using (StreamReader sr = new StreamReader(filePath, encoding))
            {
                while ((line = sr.ReadLine()) != null) {
                    list.Add(line);
                }
            }

            return list;
        }

        /// <summary>
        /// <para>ファイルを読み取る</para>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadToEnd(string filePath, Encoding encoding = null)
        {
            if (encoding == null) { encoding = Encoding.GetEncoding("Shift_JIS"); }

            using (StreamReader sr = new StreamReader(filePath, encoding))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// <para>文字列のリストをファイルに出力する</para>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static void WriteListData(List<string> data, string filePath, bool append = false)
        {
            CreateDirectory(GetFileDirectory(filePath));

            using (StreamWriter w = new StreamWriter(filePath, append, System.Text.Encoding.GetEncoding("Shift_JIS")))
            {
                foreach(string line in data)
                {
                    w.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// <para>文字列のリストをファイルに出力する</para>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static void WriteLine(string line, string filePath, bool append = true)
        {
            CreateDirectory(GetFileDirectory(filePath));

            using (System.IO.StreamWriter sr = new System.IO.StreamWriter(filePath, append, System.Text.Encoding.GetEncoding("Shift_JIS")))
            {
                sr.WriteLine(line);
            }
        }

        /// <summary>
        /// <para>文字列をファイルに出力する</para>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static void Write(string str, string filePath, bool append = true)
        {
            CreateDirectory(GetFileDirectory(filePath));

            using (System.IO.StreamWriter sr = new System.IO.StreamWriter(filePath, append, System.Text.Encoding.GetEncoding("Shift_JIS")))
            {
                sr.Write(str);
            }
        }

        /// <summary>
        /// ディレクトリを作成する
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            if (File.Exists(path)) { return; }
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// ファイル名を含めたフルパスからファイル名を除いたパスを返す
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string GetFileDirectory(string fullPath, char delimiter = '/')
        {
            List<string> directorys = fullPath.Replace('\\', delimiter).Split(delimiter).ToList();
            if (directorys.Count == 1)
            {
                return fullPath;
            }

            string retPath = "";
            for (int i = 0; i < (directorys.Count - 1); i++)
            {
                retPath += directorys[i] + delimiter;
            }

            return retPath.Substring(0, (retPath.Length - 1));
        }

        /// <summary>
        /// ファイル名を含めたフルパスからファイル名のみを返す
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string GetFileName(string fullPath, char delimiter = '/')
        {
            List<string> directorys = fullPath.Replace('\\', delimiter).Split(delimiter).ToList();
            if (directorys.Count == 1)
            {
                return fullPath;
            }

            return directorys.Last();
        }
        #endregion

        #region "内部メソッド"
        #endregion
        #endregion
    }
}
