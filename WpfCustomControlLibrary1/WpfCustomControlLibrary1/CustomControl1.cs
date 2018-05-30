using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCustomControlLibrary1
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1;assembly=WpfCustomControlLibrary1"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CirclePanel : Panel
    {
        protected override Size MeasureOverride(Size p_availableSize)
        {
            double l_maxChildWidth = 0.0;
            double l_maxChildHeight = 0.0;

            // 各子コントロールを測定する
            // この場合、子には成約を適用しない
            foreach(UIElement l_child in InternalChildren)
            {
                l_child.Measure(p_availableSize);
                l_maxChildWidth = Math.Max(l_child.DesiredSize.Width, l_maxChildWidth);
                l_maxChildHeight = Math.Max(l_child.DesiredSize.Height, l_maxChildHeight);
            }

            // すべての幅を満たす円の目的の半径を計算し、高さ分をオフセットする
            //
            double l_idealCircumference = l_maxChildWidth * InternalChildren.Count;
            double l_idealRadius = (l_idealCircumference / (Math.PI * 2) + l_maxChildHeight);

            Size l_ideal = new Size(l_idealRadius * 2, l_idealRadius * 2);

            // サイズを計算する。なお、いずれかの方向が無限になる可能性がある。
            // その場合、理想的なサイズを最大とし、かつ与えられた制約内に収まるサイズを返す

            Size l_desired = l_ideal;
            if(!double.IsInfinity(p_availableSize.Width))
            {
                if(p_availableSize.Width < l_desired.Width)
                {
                    l_desired.Width = p_availableSize.Width; 
                }
            }
            if(!double.IsInfinity(p_availableSize.Height))
            {
                if(p_availableSize.Height < l_desired.Height)
                {
                    l_desired.Height = l_desired.Height;
                }
            }
            return l_desired;
        }

        protected override Size ArrangeOverride(Size p_finalSize)
        {
            // 円の境界を計算する
            //
            Rect l_layoutRect;
            if(p_finalSize.Width > p_finalSize.Height)
            {
                l_layoutRect = new Rect(
                    (p_finalSize.Width - p_finalSize.Height) / 2,
                    0,
                    p_finalSize.Height,
                    p_finalSize.Height);
            }
            else
            {
                l_layoutRect = new Rect(
                    0,
                    (p_finalSize.Height - p_finalSize.Width) / 2,
                    p_finalSize.Width,
                    p_finalSize.Width);
            }

            double l_angleInc = 360.0 / InternalChildren.Count;
            double l_angle = 0;
            foreach(UIElement l_child in InternalChildren)
            {
                Point l_childLocation = new Point(
                    l_layoutRect.Left +
                    (l_layoutRect.Width - l_child.DesiredSize.Width) / 2,
                    l_layoutRect.Top);

                l_child.Arrange(new Rect(l_childLocation,l_child.DesiredSize));

                // 変換の原点は、この配置位置に対して0,0
                // 
                l_child.RenderTransform = new RotateTransform(
                    l_angle,
                    l_child.DesiredSize.Width / 2,
                    p_finalSize.Height / 2 - l_layoutRect.Top);

                l_angle += l_angleInc;
                l_child.Arrange(new Rect(l_childLocation, l_child.DesiredSize));
            }
            return p_finalSize;
        }

        static CirclePanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CirclePanel), new FrameworkPropertyMetadata(typeof(CirclePanel)));
        }
    }
}
