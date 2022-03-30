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
        private Chart mainChart;
        public Chart MainChart
        {
            get => this.mainChart;
            set
            {
                this.mainChart = value;
                OnPropertyChanged("MainChart");
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

        private async void InitChart()
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            this.Users = await proxy.GetUsersAsync();

          //  create chart
                Chart chart;
            switch (this.chartType % 7)
            {
                case 1:
                    chart = new LineChart();
                    break;
                case 2:
                    chart = new PieChart();
                    break;
                case 3:
                    chart = new BarChart();
                    break;
                case 4:
                    chart = new PointChart();
                    break;
                case 5:
                    chart = new RadarChart();
                    break;
                case 6:
                    chart = new DonutChart();
                    break;
                default:
                    chart = new RadialGaugeChart();
                    break;
            }
            List<ChartEntry> chartEntries = new List<ChartEntry>();
            foreach (User U in this.Users)
            {
                var UserRecipes = U.Recipes.Count;
                ChartEntry entry = new ChartEntry(UserRecipes)
                {
                    TextColor = SKColor.Parse("#3498db"),
                    ValueLabelColor = SKColor.FromHsv(UserRecipes % 100, UserRecipes % 100 / 2, UserRecipes % 100 / 4),
                    Color = SKColor.FromHsv(UserRecipes, UserRecipes / 2, UserRecipes / 4),
                    Label = $"{U.Name}",
                    ValueLabel = $"{UserRecipes:N0}"
                };
                chartEntries.Add(entry);
            }
            chart.Entries = chartEntries;
            chart.LabelTextSize += 10;

            this.MainChart = chart;
        }

        public ICommand NextChart => new Command(OnNextChart);
        public void OnNextChart()
        {
            this.chartType++;
            InitChart();
        }

    }


    //private void InitChart()
    //{
    //    //create random data
    //    Random r = new Random();
    //    this.data.Clear();
    //    for (int i = 0; i < 8; i++)
    //        this.data.Add(new Person()
    //        {
    //            Id = i,
    //            Height = r.Next(145, 200)
    //        });

    //    //create chart
    //    Chart chart;
    //    switch (this.chartType % 7)
    //    {
    //        case 1:
    //            chart = new LineChart();
    //            break;
    //        case 2:
    //            chart = new PieChart();
    //            break;
    //        case 3:
    //            chart = new BarChart();
    //            break;
    //        case 4:
    //            chart = new PointChart();
    //            break;
    //        case 5:
    //            chart = new RadarChart();
    //            break;
    //        case 6:
    //            chart = new DonutChart();
    //            break;
    //        default:
    //            chart = new RadialGaugeChart();
    //            break;
    //    }

    //    List<ChartEntry> chartEntries = new List<ChartEntry>();
    //    foreach (Person p in data)
    //    {
    //        ChartEntry entry = new ChartEntry((float)p.Height)
    //        {
    //            TextColor = SKColor.Parse("#3498db"),
    //            ValueLabelColor = SKColor.FromHsv((float)p.Height % 100, (float)p.Height % 100 / 2, (float)p.Height % 100 / 4),
    //            Color = SKColor.FromHsv((float)p.Height, (float)p.Height / 2, (float)p.Height / 4),
    //            Label = $"{p.Id}",
    //            ValueLabel = $"{p.Height:N0}"
    //        };
    //        chartEntries.Add(entry);
    //    }
    //    chart.Entries = chartEntries;
    //    chart.LabelTextSize += 10;

    //    MainChart = chart;
    //}

    //public ICommand NextChart => new Command(OnNextChart);
    //public void OnNextChart()
    //{
    //    this.chartType++;
    //    InitChart();
    //}




        }

