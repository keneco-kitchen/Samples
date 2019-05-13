using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using myclass = Terminal.Talk;

namespace Terminal
{
    class Talk : Section
    {
        #region private field

        /// <summary>
        /// Promptの文字
        /// </summary>
        private static readonly string prompt = "$ ";

        #endregion private field

        #region property

        /// <summary>
        /// 入出力メッセージ
        /// </summary>
        public Paragraph Message { get; } = new Paragraph();

        /// <summary>
        /// Prompt用表示
        /// </summary>
        public Span Prompt { get; } = new Span();

        public bool CanMessaging { get; private set; } = false;
        #endregion property

        #region initialization

        public Talk(
            string p_user, 
            Brush p_userForeground,
            string p_current,
            Brush p_currentForeground)
        {
            //
            // promptの作成
            //
            this.Prompt.Inlines.Add( new Run(p_user)
                { Foreground = p_userForeground });
            this.Prompt.Inlines.Add( new Run(p_current)
                { Foreground = p_currentForeground });
            this.Prompt.Inlines.Add(new LineBreak());
            this.Prompt.Inlines.Add(prompt);

            // messageの作成
            this.Next();
        }

        #endregion initialization

        #region public method

        public bool CaretIsInMessageAera(TextPointer p_caretPosition)
        {
            return this.CanMessaging &&
                 this.Message.ContentStart.IsInSameDocument(p_caretPosition) &&
                 this.Message.ContentStart.GetOffsetToPosition(p_caretPosition) - 1 >= myclass.prompt.Length;

                
        }
        
        public bool CaretIsInPromptArea(TextPointer p_caretPosition)
        {
            return this.CanMessaging &&
                 this.Message.ContentStart.IsInSameDocument(p_caretPosition) &&
                 this.Message.ContentStart.GetOffsetToPosition(p_caretPosition) - 1 == myclass.prompt.Length;

        }


        /// <summary>
        /// 次のMessageの作成と表示
        /// </summary>
        public void Next()
        {
            // Promptを含めたmessageを作成して
            this.Message.Inlines.Add(this.Prompt);
            // Consoleに表示する
            this.Blocks.Add(this.Message);
        }

        #endregion public method
    }
}
