using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Terminal
{
    public class Console : RichTextBox
    {
        private Talk currentTalk;

        public Brush UserForeground
        {
            get { return (Brush)GetValue(UserForegroundProperty); }
            set { SetValue(UserForegroundProperty, value); }
        }

        public static readonly DependencyProperty UserForegroundProperty =
            DependencyProperty.Register(
                nameof(UserForeground),
                typeof(Brush),
                typeof(Console),
                new PropertyMetadata(default(Brush)));

        public Brush PathForeground
        {
            get { return (Brush)GetValue(PathForegroundProperty); }
            set { SetValue(PathForegroundProperty, value); }
        }

        public static readonly DependencyProperty PathForegroundProperty =
            DependencyProperty.Register(
                nameof(PathForeground),
                typeof(Brush),
                typeof(Console),
                new PropertyMetadata(default(Brush)));

        static Console()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Console),
                new FrameworkPropertyMetadata(typeof(Console)));
        }

        private bool CaretIsEditArea()
        {
            this.currentTalk != null && this.currentTalk
        }

        public void MakeTalk(string p_user, string p_path)
        {
            this.currentTalk = new Talk(p_user, this.UserForeground, 
                p_path, this.PathForeground);
            this.Document.Blocks.Add(this.currentTalk);
        }

    }
}
