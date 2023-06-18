using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Controls;
using System.IO.Pipes;

namespace Wpftutorialsamples.Rich_text_controls
{
    public partial class Richtexteditorsample : Window
    {
        public Richtexteditorsample()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26,
28, 36, 48, 72 };
        }
        private void rtbeditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbeditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnbold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = rtbeditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnitalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = rtbeditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnunderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));
            temp = rtbeditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = rtbeditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|all files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream filestream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbeditor.Document.ContentStart,
                rtbeditor.Document.ContentEnd); range.Load(filestream, DataFormats.Rtf);
            }
        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|all files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream filestream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbeditor.Document.ContentStart,
                rtbeditor.Document.ContentEnd); range.Save(filestream, DataFormats.Rtf);
            }
        }
        private void cmbfontfamily_Selectionchanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
                rtbeditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }
        private void cmbfontsize_Textchanged(object sender, TextChangedEventArgs e)
        {
            rtbeditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
        }
    }
}