using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMiniPhotoshop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Встановити режим Ink в якості стандартного
            this.MylnkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            this.inkRadio.IsChecked = true;
            this.ComboColors.SelectedIndex = 0;
        }


        private void ColorChanged(object sender, SelectionChangedEventArgs e)
        {
            // Отримати властивість Tag вибраного елемента StackPanel
            string colorToUse = (this.ComboColors.SelectedItem
            as StackPanel).Tag.ToString();
            
            // Змінити колір, який використовується для візуалізиції почерку
            this.MylnkCanvas.DefaultDrawingAttributes.Color =
            (Color)ColorConverter.ConvertFromString(colorToUse);

        }

        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {

           
            // В залежності від того, яка кнопка відправила подію, 
            // розташувати InkCanvas в потрібний режим
            switch ((sender as RadioButton)?.Content.ToString())
            {
                // Ці рядки повінні співпадати із значеннями властивості Content 
                // кожного елемента RadioButton
                case "Ink Mode!":
                    this.MylnkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Erase Mode!":
                    this.MylnkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case "Select Mode!":
                    this.MylnkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Зберегти всі дані InkCanvas в локальний файл
            using (FileStream fs = new FileStream("StrokeData.bin", FileMode.Create))
            {
                this.MylnkCanvas.Strokes.Save(fs);
                fs.Close();
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            // Завантажити дані із файла
            using (FileStream fs = new FileStream("StrokeData.bin",
            FileMode.Open, FileAccess.Read))
            {
                StrokeCollection strokes = new StrokeCollection(fs);
                this.MylnkCanvas.Strokes = strokes;

            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

            // Очистити всі дані
            this.MylnkCanvas.Strokes.Clear();
        }
    }
}
