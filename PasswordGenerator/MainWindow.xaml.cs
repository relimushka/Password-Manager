using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace PasswordGenerator
{
    /*!
    \brief Главный класс, отвечающий за взаимодействие с графическим интерфейсом
    \author relimushka
    \version 1.0.0
    \date Июнь 2022 года
    */
    public partial class MainWindow : Window
    {
        public ObservableCollection<Data> data;
        public string editingService;

        public MainWindow()
        {
            InitializeComponent();
            UpdateListView();
        }

        /*!
        Обновляет таблицу ListView
        */
        public void UpdateListView()
        {
            data = PasswordDatabase.GetData();
            InformationList.ItemsSource = data;
        }

        /*!
        При нажатии кнопки, копирует в буфер обмена имя аккаунта
        */
        private void CopyLogin_Click(object sender, RoutedEventArgs e)
        {
            var boundData = (Data)((Button)sender).DataContext;
            Clipboard.SetText(boundData.Login);
        }

        /*!
        При нажатии кнопки, копирует в буфер обмена пароль аккаунта
        */
        private void CopyPasswordClick(object sender, RoutedEventArgs e)
        {
            var boundData = (Data)((Button)sender).DataContext;
            Clipboard.SetText(boundData.Password);
        }

        /*!
        При вызове диалогового окна, заполняет текстовые поля данными аккаунта
        */
        private void EditClick(object sender, RoutedEventArgs e)
        {
            var boundData = (Data)((Button)sender).DataContext;
            editingService = boundData.Service + "-" + boundData.Login;
            editServiceTextBox.Text = boundData.Service;
            editLoginTextBox.Text = boundData.Login;
            editPasswordTextBox.Text = boundData.Password;
        }

        /*!
        При закрытии диалогового окна, либо не выполняет никаких действий, либо изменяет данные аккаунта
        */
        private void EditConfirmClick(object sender, DialogClosingEventArgs eventArgs)
        {
            if (!Equals(eventArgs.Parameter, true))
                return;
            if (!string.IsNullOrWhiteSpace(editServiceTextBox.Text) && !string.IsNullOrWhiteSpace(editPasswordTextBox.Text))
            {
                PasswordDatabase.Edit(editingService, editServiceTextBox.Text, editLoginTextBox.Text, editPasswordTextBox.Text);
                UpdateListView();
            }
        }

        /*!
        При нажатии кнопки, удаляет данные о аккаунте
        */
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            var boundData = (Data)((Button)sender).DataContext;
            PasswordDatabase.Delete(boundData.Service, boundData.Login);
            UpdateListView();
        }

        /*!
        При нажатии кнопки, либо отображает текстовое поле для поиска, либо скрывает текстовое поле для поиска
        */
        private void ShowSearch(object sender, RoutedEventArgs e)
        {
            if (search_TextBox.Visibility == Visibility.Hidden)
            {
                search_TextBox.Visibility = Visibility.Visible;
                search_TextBox.Focus();
            }
            else
            {
                search_TextBox.Visibility = Visibility.Hidden;
            }
        }

        /*!
        При изменении текста в текстовом поле, осуществляет поиск по названию сервиса
        */
        private void SearchFilter(object sender, TextChangedEventArgs e)
        {
            UpdateListView();
            PasswordDatabase.Search(data, search_TextBox.Text);
        }

        /*!
        При нажатии кнопки, очищает текстовые поля, отвечающие за данные нового аккаунта
        */
        private void ClearCreateClick(object sender, RoutedEventArgs e)
        {
            NewServiceTextBox.Clear();
            NewLoginTextBox.Clear();
            NewPasswordTextBox.Clear();
        }

        /*!
        При закрытии диалогового окна, либо не выполняет никаких действий, либо вносит данные нового аккаунта
        */
        private void CreateClick(object sender, DialogClosingEventArgs eventArgs)
        {
            if (!Equals(eventArgs.Parameter, true))
                return;
            if (!string.IsNullOrWhiteSpace(NewServiceTextBox.Text) || !string.IsNullOrWhiteSpace(NewLoginTextBox.Text) || !string.IsNullOrWhiteSpace(NewPasswordTextBox.Text))
            {
                PasswordDatabase.Create(NewServiceTextBox.Text, NewLoginTextBox.Text, NewPasswordTextBox.Text);
                UpdateListView();
            }
        }

        /*!
        При изменении значения ползунка, отвечающего за длину пароля, изменяет текст, отвечающий за длину пароля
        */
        private void CreateSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CreateSliderValue.Content = e.NewValue;
        }

        /*!
        При нажатии кнопки, генерирует пароль
        */
        private void CreateGenerateClick(object sender, RoutedEventArgs e)
        {
            NewPasswordTextBox.Text = GeneratePassword.Generate(Create_Digits_CheckBox.IsChecked, Create_Uppercase_CheckBox.IsChecked, Create_SpecialCharacters_CheckBox.IsChecked, Convert.ToInt16(CreateSlider.Value));
        }

        /*!
        При изменении текста в текстовом поле для пароля, осуществляет расчёт надежности пароля
        */
        private void CreateCheckStrength(object sender, TextChangedEventArgs e)
        {
            CreatePasswordStrench.Content = PasswordStrength.Check(NewPasswordTextBox.Text);
        }

        // Edit Dialog

        /*!
        При изменении значения ползунка, отвечающего за длину пароля, изменяет текст, отвечающий за длину пароля
        */
        private void EditSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            editSliderValue.Content = e.NewValue;
        }

        /*!
        При нажатии кнопки, генерирует пароль
        */
        private void EditGenerateClick(object sender, RoutedEventArgs e)
        {
            editPasswordTextBox.Text = GeneratePassword.Generate(editDigitsCheckBox.IsChecked, editUppercaseCheckBox.IsChecked, editSpecialCharactersCheckBox.IsChecked, Convert.ToInt16(editSlider.Value));
        }

        /*!
        При изменении текста в текстовом поле для пароля, осуществляет расчёт надежности пароля
        */
        private void EditCheckStrength(object sender, TextChangedEventArgs e)
        {
            editPasswordStrench.Content = PasswordStrength.Check(editPasswordTextBox.Text);
        }
    }
}