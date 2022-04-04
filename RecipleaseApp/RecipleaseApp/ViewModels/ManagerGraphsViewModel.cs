using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using System.Windows.Input;
using Xamarin.Forms;
using RecipleaseApp.Services;
using RecipleaseApp.Models;
using RecipleaseApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using SkiaSharp;
using Microcharts;
using System.Threading.Tasks;

namespace RecipleaseApp.ViewModels
{
    class ManagerGraphsViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private int Recipes;
        private List<User> Users;
        private Chart recipeChart;
        public Chart RecipeChart
        {
            get => this.recipeChart;
            set
            {
                this.recipeChart = value;
                OnPropertyChanged("RecipeChart");
            }
        }


        private Chart gendersChart;
        public Chart GendersChart
        {
            get => this.gendersChart;
            set
            {
                this.gendersChart = value;
                OnPropertyChanged("GendersChart");
            }
        }

        private Chart signUpChart;
        public Chart SignUpChart
        {
            get => this.signUpChart;
            set
            {
                this.signUpChart = value;
                OnPropertyChanged("SignUpChart");
            }
        }

        private string title;
        public string Title
        {
            get => this.title;
            set
            {
                this.title = value;
                OnPropertyChanged("Title");
            }

        }

        private int chartType;

        public ManagerGraphsViewModel()
        {

            this.chartType = 1;
            this.Users = new List<User>();

            InitChart();
        }

        private async void InitGenders()
        {
            
           
           
        }
        private async void InitChart()
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            this.Users = await proxy.GetUsersAsync();

          //  create chart
            //    Chart chart;
            //switch (this.chartType % 7)
            //{
            //    case 1:
            //        chart = new LineChart();
            //        break;
            //    case 2:
            //        chart = new PieChart();
            //        break;
            //    case 3:
            //        chart = new BarChart();
            //        break;
            //    case 4:
            //        chart = new PointChart();
            //        break;
            //    case 5:
            //        chart = new RadarChart();
            //        break;
            //    case 6:
            //        chart = new DonutChart();
            //        break;
            //    default:
            //        chart = new RadialGaugeChart();
            //        break;
            //}
            Chart chart = new LineChart();
            List<ChartEntry> chartEntries = new List<ChartEntry>();
            List<Recipe> lst = await GetRecipes();
            DateTime? prevDate = lst[0].DateOfUpload;
            int counter = 0;
            foreach (Recipe r in lst)
            {
                counter++;
                if (r.DateOfUpload?.Year != prevDate?.Year ||
                    r.DateOfUpload?.Month != prevDate?.Month ||
                    r.DateOfUpload?.Day != prevDate?.Day)
                {
                    ChartEntry entry = new ChartEntry(counter)
                    {
                        TextColor = SKColor.Parse("#3498db"),
                        ValueLabelColor = SKColor.FromHsl(100, 100, 100),
                        Color = SKColor.FromHsv(100, 100, 100),
                        Label = $"{r.DateOfUpload?.Day}-{r.DateOfUpload?.Month}-{r.DateOfUpload?.Year}",
                        ValueLabel = $"{counter:N0}"
                    };
                    chartEntries.Add(entry);
                    prevDate = r.DateOfUpload;
                }
                
            }
            chart.Entries = chartEntries;
            chart.LabelTextSize += 10;

            this.RecipeChart = chart;


            Chart chart2 = new PieChart();
            List<ChartEntry> chartEntriess = new List<ChartEntry>();

            List<User> lstUsers = await GetUsers();
            int Gender = 1;
            int countGenders = 0;
            int[] tally = new int[3];
            foreach (User U in lstUsers)
            {

                switch (U.GenderId)
                {
                    case 1: tally[0]++;
                        break;
                    case 2: tally[1]++;
                        break;
                    case 3: tally[2]++;
                        break;
                    default: break;
                }

               
              
            }
            ChartEntry entryFemale = new ChartEntry(tally[0])
            {
                TextColor = SKColor.Parse("#3498db"),
                ValueLabelColor = SKColor.FromHsl(266, 38, 29),
                Color = SKColor.FromHsv(255 / tally[0] * 10, 255, 230),
                Label = $"Female",
                ValueLabel = $"{tally[0]:N0}"
            };
            ChartEntry entryMale = new ChartEntry(tally[1])
            {
                TextColor = SKColor.Parse("#3498db"),
                ValueLabelColor = SKColor.FromHsl(300, 76, 673),
                Color = SKColor.FromHsv(55, 74, 190),
                Label = $"Male",
                ValueLabel = $"{tally[1]:N0}"
            };
            ChartEntry entryOther = new ChartEntry(tally[2])
            {
                TextColor = SKColor.Parse("#3498db"),
                ValueLabelColor = SKColor.FromHsl(646, 44, 22),
                Color = SKColor.FromHsv(575, 78, 88),
                Label = $"Other",
                ValueLabel = $"{tally[2]:N0}"
            };
            chartEntriess.Add(entryOther);
            chartEntriess.Add(entryMale);
            chartEntriess.Add(entryFemale);
            chart2.Entries = chartEntriess;
            chart2.LabelTextSize += 10;
            this.GendersChart = chart2;




            Chart chartSignUp = new LineChart();
            List<ChartEntry> chartEntriesSignUp = new List<ChartEntry>();
            List<User> lstSignUp = await GetUsersDate();
            DateTime? prevDateSignUp = lstSignUp[0].SignUpTime;
            int counterS = 0;
            foreach (User U in lstSignUp)
            {
                counterS++;

               
                if (U.SignUpTime?.Year != prevDateSignUp?.Year ||
                    U.SignUpTime?.Month != prevDateSignUp?.Month ||
                    U.SignUpTime?.Day != prevDateSignUp?.Day)
                {
                    ChartEntry entrySignUpTime = new ChartEntry(counterS)
                    {
                        TextColor = SKColor.Parse("#3498db"),
                        ValueLabelColor = SKColor.FromHsl(40,40,60),
                        Color = SKColor.FromHsv(200,50,30),
                        Label = $"{U.SignUpTime?.Day}-{U.SignUpTime?.Month}-{U.SignUpTime?.Year}",
                        ValueLabel = $"{counterS:N0}"
                    };
                    chartEntriesSignUp.Add(entrySignUpTime);
                    prevDateSignUp = U.SignUpTime;
                }

            }
            chartSignUp.Entries = chartEntriesSignUp;
            chart.LabelTextSize += 10;

            this.SignUpChart = chartSignUp;
        }



        public ICommand NextChart => new Command(OnNextChart);
        public void OnNextChart()
        {
            
            InitGenders();
        }

        private async Task<List<Recipe>> GetRecipes()
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            List<Recipe> lst = await proxy.GetRecepiesAsync();
            lst = lst.OrderBy(r => r.DateOfUpload).ToList();

            return lst;
        }

        private async Task<List<User>> GetUsers()
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            List<User> lst = await proxy.GetUsersAsync();
            lst = lst.OrderBy(u=> u.GenderId).ToList();

            return lst;
        }

        private async Task<List<User>> GetUsersDate()
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            List<User> lst = await proxy.GetUsersAsync();
            lst = lst.OrderBy(u => u.SignUpTime).ToList();

            return lst;
        }
    }


  




        }

