using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myprj = Terminal;
using myclass = Terminal.Shell;

namespace Terminal
{
    /// <summary>
    /// Shellの抽象クラス
    /// </summary>
    abstract class Shell
    {
        #region enum
        /// <summary>
        /// メッセージの結果状態
        /// </summary>
        public enum MessageResultEnum
        {
            /// <summary>
            /// 処理が成功した
            /// </summary>
            Success,

            /// <summary>
            /// 処理が失敗した
            /// </summary>
            Fail,

            /// <summary>
            /// 不明
            /// </summary>
            Unknown,
        }

        /// <summary>
        /// Shellの状態
        /// </summary>
        public enum ShellStatusEnum
        {
            /// <summary>
            /// メッセージを待っている
            /// </summary>
            Standby,

            /// <summary>
            /// メッセージの処理中
            /// </summary>
            Busy,

            /// <summary>
            /// 不明
            /// </summary>
            Unknown,
        }
        #endregion enum

        /// <summary>
        /// 最新のメッセージ結果
        /// </summary>
        public myclass.MessageResultEnum LatestResult { get; private set; } =
            myclass.MessageResultEnum.Unknown;

        /// <summary>
        /// 現在のShellの状態
        /// </summary>
        public myclass.ShellStatusEnum CurrentStatus { get; private set; } =
            myclass.ShellStatusEnum.Unknown;

        /// <summary>
        /// Shellへのメッセージ送信
        /// </summary>
        public abstract string Message(string p_script);
    }
}
