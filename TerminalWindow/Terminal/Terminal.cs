using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Terminal
{
    /// <summary>
    /// Terminal
    /// </summary>
    public class Terminal : Control
    {
        private Console console;
        private readonly string user = "developer ";
        private readonly string path = "usr/local/bin";

        static Terminal()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Terminal),
                new FrameworkPropertyMetadata(typeof(Terminal)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // テンプレートからコントロールの取得
            this.console = this.GetTemplateChild("PART_Console") as Console;
            this.console.MakeTalk(this.user,this.path);

        }
    }
}
