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
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Animation;
using System.Speech.Synthesis;
using MahApps.Metro;
using System.Xml;
using System.Net;
using System.Web;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;


namespace NOtepad
{    
    public partial class MainWindow 
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimer3 = new System.Windows.Threading.DispatcherTimer();


        #region ON STARTUP
        void InitializeObjects()
        {

            Main.WindowStartupLocation = WindowStartupLocation.Manual;
            Main.Height = 0.6 * System.Windows.SystemParameters.PrimaryScreenHeight;
            Main.Width = 0.6 * System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Top = 0.5 * (SystemParameters.PrimaryScreenHeight - Main.Height); this.Left = 0.5 * (SystemParameters.PrimaryScreenWidth - Main.Width);
            FadeOut.Margin = new Thickness(0, 0, 0, 0);
            

            //LeftNav.Height = window.Height;
            LeftNavFlyout.Margin = new Thickness(0, 0, 0, 0);
            //SettingsFlyout.Height = Main.Height;
            SettingsFlyout.Margin = new Thickness(0, 0, 0, 0);

            //SettingsCanvas.Height = 4000;
            // Design.DropShadowEffect(ContentCanvas, 100, 127, 127, 127, 1, 1, 3, 0.8);
            //Design.BorderDropShadowEffect(ContentBorder, 100, 127, 127, 127, 1, 1, 5, 0.5);


            var theme = ThemeManager.DetectAppStyle(System.Windows.Application.Current);

            // now set the Green accent and dark theme
            new PaletteHelper().ReplacePrimaryColor("Blue");
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent("Cobalt"),
                                        ThemeManager.GetAppTheme("BaseLight"));
            ChangeThemeColour("#125ACD", "#91B7F5");
            
        }
        public MainWindow()

        {
            InitializeComponent();

           
            dispatcherTimer2.Tick += dispatcherTimer2_Tick;
            dispatcherTimer2.Interval = new TimeSpan(0, 0, 0, 1);


            #region WEATHER
            GetParameters();
            #endregion

            #region SPEECH
            synthesizer = new SpeechSynthesizer();
            synthesizer.StateChanged += new EventHandler<StateChangedEventArgs>(synthesizer_StateChanged);
            synthesizer.SpeakStarted += new EventHandler<SpeakStartedEventArgs>(synthesizer_SpeakStarted);
            synthesizer.SpeakProgress += new EventHandler<SpeakProgressEventArgs>(synthesizer_SpeakProgress);
            synthesizer.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(synthesizer_SpeakCompleted);
            LoadInstalledVoices();
            #endregion

            #region CREATE REQ FILES + INITIALISE FILES AND OBJECTS
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            if (!Directory.Exists(filePath2))
                Directory.CreateDirectory(filePath2);

            InitFiles();
            InitializeObjects();

            #endregion

            CloseLeftNav();
            CloseSettings();

        }
        #endregion

        #region DESIGN  


        void OpenLeftNav()
        {
            Design.CanvasAnimateOpacity(FadeOut, 180, 1, 0, 0.34);
            //Design.CanvasAnimateOpacity(LeftNav, 180, 1, 0.34, 1);
            //Design.MoveTo(LeftNav, LeftNav.Width,1);
        }
        void CloseLeftNav()
        {
            Design.CanvasAnimateOpacity(FadeOut, 200, 0.5, 0.34, 0);
            //Design.CanvasAnimateOpacity(LeftNav, 100, 0.5, 1, 0.34);
            LeftNavFlyout.IsOpen = false;
            MenuButton.IsChecked = false;
            //Design.ReverseMoveTo(LeftNav, LeftNav.Width,1);       
        }
        void OpenSettings()
        {
            SettingsFlyout.IsOpen = true;
            Design.CanvasAnimateOpacity(FadeOut, 180, 1, 0, 0.34);
            //Design.ReverseMoveTo(SettingsCanvas, Main.Height, 2);
            //Design.CanvasAnimateOpacity(SettingsCanvas,100,0.5, 0, 1);
            AddButton.Visibility = Visibility.Collapsed;
        }
        void CloseSettings()
        {
            SettingsFlyout.IsOpen = false;
            Design.CanvasAnimateOpacity(FadeOut, 200, 0.5, 0.34, 0);
            //Design.MoveTo(SettingsCanvas, Main.Height, 2);
            //Design.CanvasAnimateOpacity(SettingsCanvas, 80, 0.5, 1,0);
            AddButton.Visibility = Visibility.Visible;
        }    
       
        #endregion

        #region ACCOUNTS
        void NewAccount()
        {

            int check = 1;
            for (int i = 1; i <= accounts.AccountNumber && check == 1; i++)
            {
                if (accounts.Users[i].Email == emailTxt.Text)
                    check = 0;
            }

            if (check == 0) InfoLabel.Content = "Email already in use.";
            else
            {
                if (pwTxt.Password != pwRepTxt.Password)
                    InfoLabel.Content = "Passwords do not match.";
                else
                {
                    accounts.AccountNumber++;
                    accounts.Users[accounts.AccountNumber].FirstName = firstNameTxt.Text;
                    accounts.Users[accounts.AccountNumber].LastName = lastNameTxt.Text;
                    accounts.Users[accounts.AccountNumber].Email = emailTxt.Text;
                    accounts.Users[accounts.AccountNumber].Password = pwTxt.Password;
                    InfoLabel.Content = "Sign up succesfull!";
                }
            }
        }
        void LogIn()
        {
            int check = 0;
            int pos = 0;
            for (int i = 1; i <= accounts.AccountNumber && check == 0; i++)
            {
                if (accounts.Users[i].Email == emailLog.Text)
                {
                    check = 1;
                    pos = i;
                }
            }
            if (check == 0 || accounts.Users[pos].Password != pwLog.Password) InfoLabel.Content = "Wrong log in information.";
            else
            {
                InfoLabel.Content = "Log in succesfull!";
                firstNameTxt.Text = accounts.Users[pos].FirstName;
                lastNameTxt.Text = accounts.Users[pos].LastName;
                emailTxt.Text = accounts.Users[pos].Email;
                pwTxt.Password = accounts.Users[pos].Password;
            }
        }

        #endregion

        #region FILE MANIPULATION
        String filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyNotes\";
        String filePath2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyNotes\Resources\";

        Note note = new Note();
        Account accounts = new Account();

      
        int CurrentFile = 1;
        int FileNumber = 0;

        void InitFiles()
        {
           
            if (Directory.GetFiles(filePath) != null)
            {
                bool FoundFiles = false;
                foreach (String file in Directory.GetFiles(filePath))
                {
                    FoundFiles = true;
                }
                if (FoundFiles == false)
                {

                    NewFile();
                }
                else
                {
                    foreach (String file in Directory.GetFiles(filePath))
                    {
                        FileNumber++;

                        StreamReader sr = new StreamReader(file);
                        note.Date[FileNumber].Year = Convert.ToInt32(sr.ReadLine());
                        note.Date[FileNumber].Month = Convert.ToInt32(sr.ReadLine());
                        note.Date[FileNumber].Day = Convert.ToInt32(sr.ReadLine());
                        note.Date[FileNumber].Hour = Convert.ToInt32(sr.ReadLine());
                        note.Date[FileNumber].Minute = Convert.ToInt32(sr.ReadLine());
                        note.Title[FileNumber] = sr.ReadLine();
                        //note.Content[FileNumber] = sr.ReadToEnd();
                    }

                }
                PageNumber.Content = CurrentFile + " / " + FileNumber;
                ShowContent(CurrentFile);
            }


        }
        public void ListTitles()
        {
            ListItems.Items.Clear();
            for (int i = 1; i <= FileNumber; i++)
            {
                ListItems.Items.Add(note.Title[i]);

            }
        }
        private void SaveDocument(string FileName)
        {
            TextRange t = new TextRange(ContentBox.Document.ContentStart,
                                    ContentBox.Document.ContentEnd);
            FileStream file = new FileStream(filePath2 + FileName + ".xaml", FileMode.Create);
            t.Save(file, System.Windows.DataFormats.XamlPackage);
            file.Close();
        }
        private void LoadDocument(string FileName)
        {
            TextRange t = new TextRange(ContentBox.Document.ContentStart,
                                ContentBox.Document.ContentEnd);
            FileStream file = new FileStream(filePath2 + FileName + ".xaml", FileMode.Open);
            t.Load(file, System.Windows.DataFormats.XamlPackage);
            file.Close();
        }
        void NewFile()
        {
            FileNumber++;
            note.Date[FileNumber].Year = DateTime.Now.Year;
            note.Date[FileNumber].Month = DateTime.Now.Month;
            note.Date[FileNumber].Day = DateTime.Now.Day;
            note.Date[FileNumber].Hour = DateTime.Now.Hour;
            note.Date[FileNumber].Minute = DateTime.Now.Minute;
            note.Title[FileNumber] = "Your title!";
            ContentBox.Document.Blocks.Clear();
            ContentBox.Document.Blocks.Add(new Paragraph(new Run("Your Content")));
           
            SaveDocument(FileNumber.ToString());
            ShowContent(FileNumber);
            
            CurrentFile = FileNumber;
            PageNumber.Content = CurrentFile + " / " + FileNumber;
            
        }
        void DeleteFile()
        {
            if (FileNumber == 1)
            {
                note.Date[FileNumber].Year = DateTime.Now.Year;
                note.Date[FileNumber].Month = DateTime.Now.Month;
                note.Date[FileNumber].Day = DateTime.Now.Day;
                note.Date[FileNumber].Hour = DateTime.Now.Hour;
                note.Date[FileNumber].Minute = DateTime.Now.Minute;
                note.Title[FileNumber] = "Your title!";
                note.Content[FileNumber] = "Your Content!";
                Title.Text = "Your title!";
                ContentBox.Document.Blocks.Clear();
                ContentBox.Document.Blocks.Add(new Paragraph(new Run("Your Content")));
                SaveContent();
                
                ShowContent(FileNumber);
                
            }
            else
            {
                try
                {
                    File.Delete(filePath + CurrentFile + ".txt");
                    File.Delete(filePath2 + CurrentFile + ".xaml");
                }
                catch (IOException iox)
                {
                    System.Windows.MessageBox.Show(Convert.ToString(iox)); 
                }

                for (int i = CurrentFile; i < FileNumber ; i++)
                {
                    try
                    {
                        File.Move(filePath + (i + 1) + ".txt", filePath + i + ".txt");
                        File.Move(filePath2 + (i + 1) + ".xaml", filePath2 + i + ".xaml");
                    }
                    catch (IOException iox)
                    {
                        System.Windows.MessageBox.Show(Convert.ToString(iox));
                    }
                }
                FileNumber = 0;
                CurrentFile = 1;
                InitFiles();
                
            }
        }
        void ShowContent(int FileNumber)
        {
            Title.Text = note.Title[FileNumber];
            
            
            if(note.Date[FileNumber].Hour<10)
            Date.Content = "Last Edited " + note.Date[FileNumber].Day + "." + 
                            note.Date[FileNumber].Month + "." + 
                            note.Date[FileNumber].Year + "  " + 
                            "0"+note.Date[FileNumber].Hour + ":" + 
                            note.Date[FileNumber].Minute;

            if (note.Date[FileNumber].Hour >9)
                Date.Content = "Edited " + note.Date[FileNumber].Day + "." +
                                note.Date[FileNumber].Month + "." +
                                note.Date[FileNumber].Year + "  " +
                                note.Date[FileNumber].Hour + ":" +
                                note.Date[FileNumber].Minute;
            if (note.Date[FileNumber].Minute < 10)
                Date.Content = "Edited " + note.Date[FileNumber].Day + "." +
                                note.Date[FileNumber].Month + "." +
                                note.Date[FileNumber].Year + "  " +
                                note.Date[FileNumber].Hour + ":" +
                                "0" + note.Date[FileNumber].Minute;

            if (note.Date[FileNumber].Minute > 9)
                Date.Content = "Edited " + note.Date[FileNumber].Day + "." +
                                note.Date[FileNumber].Month + "." +
                                note.Date[FileNumber].Year + "  " +
                                note.Date[FileNumber].Hour + ":" +
                                note.Date[FileNumber].Minute;

            LoadDocument(FileNumber.ToString());

            ListTitles();
            synthesizer.SpeakAsyncCancelAll();

        }
        void SaveContent()
        {
            note.Date[CurrentFile].Year = DateTime.Now.Year;
            note.Date[CurrentFile].Month = DateTime.Now.Month;
            note.Date[CurrentFile].Day = DateTime.Now.Day;
            note.Date[CurrentFile].Hour = DateTime.Now.Hour;
            note.Date[CurrentFile].Minute = DateTime.Now.Minute;
            note.Title[CurrentFile] = Title.Text;
            
            String filename = filePath + CurrentFile + ".txt";
            StreamWriter sw = new StreamWriter(filename, false);
            sw.WriteLine(note.Date[CurrentFile].Year);
            sw.WriteLine(note.Date[CurrentFile].Month);
            sw.WriteLine(note.Date[CurrentFile].Day);
            sw.WriteLine(note.Date[CurrentFile].Hour);
            sw.WriteLine(note.Date[CurrentFile].Minute);
            sw.WriteLine(note.Title[CurrentFile]);
            sw.Close();
            
            SaveDocument(CurrentFile.ToString());

            ListTitles();
        }

        void addImage()
        {
            // Configure open file dialog box 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Image Files (*.png, *.jpg)|*.png;*.jpg";

            // Show open file dialog box 
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string fileName = dlg.FileName;
                Paragraph para = new Paragraph();
                BitmapImage bitmap = new BitmapImage(new Uri(@fileName, UriKind.Relative));
                Image image = new Image();
                image.Source = bitmap;
                image.Width = 400;
                image.Height = 400;
                para.Inlines.Add(image);

                FlowDoc.Blocks.Add(para);
            }
        }

        #endregion

        #region SEARCH 
        private void searchBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ListItems.Items.Clear();

            for (int i = 1; i <= FileNumber; i++)
            {
                if (note.Title[i] != null)
                    if ( note.Title[i].ToLower().Contains(searchBox.Text.ToString().ToLower()) ) ListItems.Items.Add(note.Title[i]);
            }

        }


      
        #endregion

        #region WEATHER

        public string AccountLocation = "Satu%20Mare";
        public string WeatherURL;



        public struct WeatherDetails
        {
            public double Temperature;
            public string Clouds;
            public string Wind;
            public string Precipitation;
        };
        public WeatherDetails Parameters = new WeatherDetails();

        public async void GetParameters()
        {
            WeatherURL = "http://api.openweathermap.org/data/2.5/weather?q=" + AccountLocation + "&appid=b1b15e88fa797225412429c1c50c122a&mode=xml";
            
            try
            {
                var xml = await new WebClient().DownloadStringTaskAsync(new Uri(WeatherURL));
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string szTemp = doc.DocumentElement.SelectSingleNode("temperature").Attributes["value"].Value;
                double temp = Math.Round(double.Parse(szTemp) - 273.16);
                Parameters.Temperature = temp;
                Parameters.Clouds = doc.DocumentElement.SelectSingleNode("clouds").Attributes["name"].Value;
                //Parameters.Wind = doc.DocumentElement.SelectSingleNode("wind").Attributes["speed"].Attributes["name"].Value;
                Parameters.Precipitation = doc.DocumentElement.SelectSingleNode("precipitation").Attributes["mode"].Value;
                TemperatureLabel.Content = Parameters.Temperature + "°, "+ Parameters.Clouds+ ", "+ Parameters.Precipitation;

            }
            catch (Exception e)
            {
                TemperatureLabel.Content = "- °, - ";

                //System.Windows.MessageBox.Show("Something went wrong.");
            }          

              
        }

        public bool CheckConnection ( String URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Timeout = 5000;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK) return true;
            else return false; 
        }
        #endregion

        #region TEXT TO SPEECH

        private SpeechSynthesizer synthesizer;
        bool SpeechButtonIsPressed = false;
        private void LoadInstalledVoices()
        {
            comboVoice.DataContext = (from e in synthesizer.GetInstalledVoices()
                                      select e.VoiceInfo.Name);
        }

        private void synthesizer_StateChanged(object sender, StateChangedEventArgs e)
        {
            //show the synthesizer's current state
            labelState.Content = e.State.ToString();
        }
        private void synthesizer_SpeakStarted(object sender, SpeakStartedEventArgs e)
        {
        }
        void synthesizer_SpeakProgress(object sender, SpeakProgressEventArgs e)
        {
            //show the synthesizer's current progress
            //labelProgress.Content = e.Text;
        }
        private void synthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            //reset when complete
            //ButtonSpeak.Content = "Speak";
            //labelProgress.Content = "";
        }
        int count2 = 0;
        bool SpeechButtonIsHeld = false;


        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {
            labelState.Content = "Wait.";
            if (count2 == 1)
            {
                synthesizer.SpeakAsyncCancelAll();

                SpeechButtonIsHeld = true;
                count2 = 0;
                dispatcherTimer2.Stop();
            }
            else
                count2++;
        }
        #endregion    

        #region EVENTS

        int count3 = 0;
        bool PalleteButtonIsPressed = false;



        #region ADD BUTTON EVENTS



        private void AddNotePlus2_Click(object sender, RoutedEventArgs e)
        {
            NewFile();
        }

        private void AddImg2_Click(object sender, RoutedEventArgs e)
        {
            addImage();
        }


        #endregion


        #endregion

        #region SPEAK/DELETE/SAVE BUTTON EVENTS

        private void SaveButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SaveContent();
            SaveButton.Opacity = 0.7;
        }
        private void SaveButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SaveButton.Opacity = 0.8;
        }
        private void SaveButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SaveButton.Opacity = 0.7;
        }
        private void SaveButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveButton.Opacity = 0.9;
        }

        private void delete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeleteFile();
            DeleteButton.Opacity = 0.7;
        }
        private void DeleteButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DeleteButton.Opacity = 0.8;
           
        }
        private void DeleteButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DeleteButton.Opacity = 0.7;
        }
        private void DeleteButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DeleteButton.Opacity = 0.9;
        }

        private void SpeakButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SpeakButton.Opacity = 0.8;
        }
        private void SpeakButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SpeakButton.Opacity = 0.7;
        }
        private void SpeakButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SpeechButtonIsPressed = true;
            dispatcherTimer2.Start();
            SpeakButton.Opacity = 0.9;
        }
        private void SpeakButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SpeakButton.Opacity = 0.8;
            dispatcherTimer2.Stop(); count2 = 0;          

            if (comboVoice.SelectedItem != null)
                synthesizer.SelectVoice(comboVoice.SelectedItem.ToString());
            synthesizer.Volume = 100;
            synthesizer.Rate = Convert.ToInt32(sliderRate.Value);
            switch (synthesizer.State)
            {
                //if synthesizer is ready
                case SynthesizerState.Ready:
                    
                    if (SpeechButtonIsHeld ==false) synthesizer.SpeakAsync(new TextRange(ContentBox.Document.ContentStart, ContentBox.Document.ContentEnd).Text);
                    SpeechButtonIsHeld = false;
                    break;
                //if synthesizer is paused
                case SynthesizerState.Paused:
                    synthesizer.Resume();

                    break;
                //if synthesizer is speaking
                case SynthesizerState.Speaking:
                    synthesizer.Pause();

                    break;
            }
           
        }
        #endregion

        #region NEXT/PREVIOUS BUTTON EVENTS

        private void PreviousButton_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            if (CurrentFile > 1)
            {
                CurrentFile--;
                ShowContent(CurrentFile);
                PageNumber.Content = CurrentFile + " / " + FileNumber;
            }
        }

        private void NextButton_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            if (CurrentFile < FileNumber)
            {
                CurrentFile++;
                ShowContent(CurrentFile);
                PageNumber.Content = CurrentFile + " / " + FileNumber;
            }
        }
        #endregion

        #region LEFT NAV EVENTS

        private void MenuButton_Checked(object sender, RoutedEventArgs e)
        {
            OpenLeftNav();
            FadeOut.Opacity = 0.34;

            LeftNavFlyout.IsOpen = true;
            Keyboard.ClearFocus();
        }

        private void MenuButton_Unchecked(object sender, RoutedEventArgs e)
        {
            CloseLeftNav();

        }



        private void FadeOut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (FadeOut.Opacity != 0)
            {

                CloseLeftNav();
                CloseSettings();
            }
     
        }
      

        private void MenuButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MenuButton.Opacity = 1;
        }

        private void MenuButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MenuButton.Opacity = 0.8;
        }

        private void ListItems_Click(object sender, MouseButtonEventArgs e)
        {
            if(ListItems.SelectedItem!=null)
            {
                CloseLeftNav();
                for (int i = 1; i <= FileNumber;i++)
                {
                    if(note.Title[i]==ListItems.SelectedItem.ToString())
                    {
                        CurrentFile = i;
                    }
                }
                    
                ShowContent(CurrentFile);
                PageNumber.Content = CurrentFile + " / " + FileNumber;
            

            }
        }
        #endregion

        #region SETTINGS EVENTS

      
        private void OptionsButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            OptionsButton.Opacity = 0.9;
        }

        private void OptionsButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            OptionsButton.Opacity = 0.7;
        }
        private void OptionsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenSettings();
            SettingsFlyout.IsOpen = true;
        }

      
        #endregion

        #region THEME EVENTS
        public int ThemeActiveColor=2;
        
        private void ChangeThemeColour(String colour, String accent)
        {
            var bc = new BrushConverter();

            //Design.BorderChangeColor(TopBar, colour);
            TopBar.Background=(Brush)bc.ConvertFrom(colour);
            //Design.BorderChangeColor(BottomBar, colour);
            //Design.GridChangeColor(ContentTopBar, colour);
            //BottomBar.Background = TopBar.Background;
            //Design.EllipseChangeColor(button, colour);
            //Design.EllipseChangeColor(button2, colour);
            //Design.EllipseChangeColor(button3, colour);
            
            //Design.EllipseChangeColor(Elli, colour);
            //Design.BorderChangeColor(ContentBarBorder, colour);/\
            //button.Fill = TopBar.Background;
            //button2.Fill = button.Fill;
            //button3.Fill = button.Fill;
            //Design.GridChangeColor(ContentCanvas, accent);
            //Design.BorderChangeColor(ContentBorder, accent);
           
            //previousLabel.Background = TopBar.Background;
           // nextLabel.Background = TopBar.Background;

        }

        private void CheckActiveColor()
        {
            switch (ThemeActiveColor)
            {
                case 1:
                   
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                    
                    break;
                case 5:
                    
                    break;
                case 6:
                    
                    break;
              


            }

        }
       
        private void color6_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            if (ThemeActiveColor != 6)
            {
                var theme = ThemeManager.DetectAppStyle(System.Windows.Application.Current);

                // change Metro Theme colours
                ThemeManager.ChangeAppStyle(this,
                                            ThemeManager.GetAccent("Red"),
                                            ThemeManager.GetAppTheme("BaseLight"));
                new PaletteHelper().ReplacePrimaryColor("Red");
                ChangeThemeColour("#D32F2F", "#E57373");
                
                CheckActiveColor();
                ThemeActiveColor = 6;
            }
        }

        private void NoteColor5_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeActiveColor != 5)
            {
                var theme = ThemeManager.DetectAppStyle(System.Windows.Application.Current);

                // change Metro Theme colours
                ThemeManager.ChangeAppStyle(this,
                                            ThemeManager.GetAccent("Green"),
                                            ThemeManager.GetAppTheme("BaseLight"));
                new PaletteHelper().ReplacePrimaryColor("Green");
                ChangeThemeColour("#43A047", "#A5D6A7");
                
                CheckActiveColor();
                ThemeActiveColor = 5;
            }
        }

        private void NoteColor4_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeActiveColor != 4)
            {
                var theme = ThemeManager.DetectAppStyle(System.Windows.Application.Current);

                // change Metro Theme colours
                ThemeManager.ChangeAppStyle(this,
                                            ThemeManager.GetAccent("Orange"),
                                            ThemeManager.GetAppTheme("BaseLight"));
                new PaletteHelper().ReplacePrimaryColor("Orange");
                ChangeThemeColour("#F57C00", "#FFCC80");
               
                CheckActiveColor();
                ThemeActiveColor = 4;
            }
        }

        private void NoteColor3_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeActiveColor != 3)
            {
                var theme = ThemeManager.DetectAppStyle(System.Windows.Application.Current);

                // change Metro Theme colours
                ThemeManager.ChangeAppStyle(this,
                                            ThemeManager.GetAccent("Cyan"),
                                            ThemeManager.GetAppTheme("BaseLight"));
                new PaletteHelper().ReplacePrimaryColor("Blue");
                ChangeThemeColour("#039BE5", "#90CAF9");
                
                CheckActiveColor();
                ThemeActiveColor = 3;
            }
        }

        private void NoteColor2_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeActiveColor != 2)
            {
                var theme = ThemeManager.DetectAppStyle(System.Windows.Application.Current);

                // now set the Green accent and dark theme
                ThemeManager.ChangeAppStyle(this,
                                            ThemeManager.GetAccent("Cobalt"),
                                            ThemeManager.GetAppTheme("BaseLight"));
                new PaletteHelper().ReplacePrimaryColor("Indigo");
                ChangeThemeColour("#125ACD", "#91B7F5");
                
                CheckActiveColor();
                ThemeActiveColor = 2;
            }
        }

        private void NoteColor1_Click(object sender, RoutedEventArgs e)
        {
            ChangeThemeColour("#5A50CB", "#ACA5FC");
            var theme = ThemeManager.DetectAppStyle(System.Windows.Application.Current);

            // change Metro Theme colours
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent("Purple"),
                                        ThemeManager.GetAppTheme("BaseLight"));
            new PaletteHelper().ReplacePrimaryColor("DeepPurple");

            if (ThemeActiveColor != 1)
            {
               

                CheckActiveColor();
                ThemeActiveColor = 1;
            }
        }

        private void ThemeMode_Checked(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().SetLightDark(true);
            ContentBox.Foreground = Brushes.White;
            ContentBox.SelectionBrush = Brushes.White;
            Title.SelectionBrush = Brushes.White;
        }

        private void ThemeMode_Unchecked(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().SetLightDark(false);
            ContentBox.Foreground = Brushes.Black;
            ContentBox.SelectionBrush = Brushes.Gray;
            Title.SelectionBrush = Brushes.Gray;
        }
        #endregion


        private void logBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void signBtn_Click(object sender, RoutedEventArgs e)
        {

        }

     
    
    }
}
