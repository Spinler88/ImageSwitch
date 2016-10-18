using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ImageSwitch
{
    public partial class ImageView : ContentPage
    {
        private bool _isAnmiating = false;
        private bool _isEndless = false;

        private double _endPositionX;
        private double _startPositionX;
        private double _endPositionY;
        private double _startPositionY;
        private int _moveSpeed;

        private int _loopCount;
        public ImageView()
        {
            InitializeComponent();
            StartButton.Clicked += OnStartClicked;
            HideButton.Clicked += OnHideClicked;
            matchButton.Clicked += OnMatchClicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DifferenceLabel.Text = "0";
        }

        async void OnStartClicked(object sender, EventArgs e)
        {
            if (_isAnmiating) return;
            setMoveValues();
            AnimateLoop();
        }

        void OnMatchClicked(object sender, EventArgs e)
        {
            int xValue = int.Parse(MovingImage.X.ToString());
            int yValue = int.Parse(MovingImage.Y.ToString());

            string newText = xValue.ToString() + "-" + xValue.ToString();
            DifferenceLabel.Text = newText;
        }

        async void OnHideClicked(object sender, EventArgs e)
        {
            ShowFullImage();
        }

        private void setMoveValues()
        {
            _startPositionX = 0 - MovingImage.Width;
            _startPositionY = 0;
            _endPositionX = imageGrid.Width + MovingImage.Width;
            _endPositionY = 0;
            _moveSpeed = 1000;
        }
        private async void AnimateLoop()
        {
            HideFullImage();
            _loopCount = 0;
            
            if (!_isAnmiating)
            {
                _isAnmiating = !_isAnmiating;
                _isEndless = true;

                while (_isEndless)
                {
                    await MovingImage.TranslateTo(_startPositionX, _startPositionY, 0);
                    await MovingImage.TranslateTo(_endPositionX, _endPositionY, (uint)_moveSpeed);

                    if (_loopCount > 5)
                    {
                        ShowFullImage();
                        _isEndless = false;
                    }
                    else
                    {
                        _loopCount++;
                    }
                }
                _isAnmiating = !_isAnmiating;
            }
        }

        private void ShowFullImage()
        {
            FullImage.IsVisible = true;
            MovingImage.IsVisible = false;
            BackgroundImage.IsVisible = false;
        }

        private void HideFullImage()
        {
            FullImage.IsVisible = false;
            MovingImage.IsVisible = true;
            BackgroundImage.IsVisible = true;
        }
    }
}
