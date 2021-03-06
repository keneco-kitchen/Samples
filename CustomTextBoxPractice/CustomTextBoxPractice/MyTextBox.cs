﻿using System;
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

namespace CustomTextBoxPractice
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomTextBoxPractice"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomTextBoxPractice;assembly=CustomTextBoxPractice"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:MyTextBox/>
    ///
    /// </summary>
    public class MyTextBox : TextBox
    {


        public int CaretPosition
        {
            get { return (int)GetValue(CaretPositionProperty); }
            set { SetValue(CaretPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CaretPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaretPositionProperty =
            DependencyProperty.Register("CaretPosition", typeof(int), typeof(MyTextBox),
                 new FrameworkPropertyMetadata(0,
                     FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                     OnCaretPositionChanged));

        public static void OnCaretPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           if(e.NewValue == e.OldValue) { return; }
           (d as MyTextBox).CaretIndex = (int)e.NewValue;
        }

        public MyTextBox():base()
        {
           this.SelectionChanged += new RoutedEventHandler(MyTextBox_SelectionChanged);
        }

        public void MyTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            this.CaretPosition = (sender as MyTextBox).CaretIndex;
        }

        //static MyTextBox()
        //{
        //    //DefaultStyleKeyProperty.OverrideMetadata(typeof(MyTextBox), new FrameworkPropertyMetadata(typeof(MyTextBox)));
        //}
    }
}
