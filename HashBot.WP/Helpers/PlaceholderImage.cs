using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace HashBot.WP.Helpers
{
    public class PlaceholderImage : Control
    {
        private static string TemplateString
        {
            get
            {
                return
                    "<ControlTemplate " +
                        "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" " +
                        "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">" +
                        "<Grid>" +
                            "<Image x:Name=\"FrontImage\"/>" +
                            "<Image x:Name=\"BackImage\"/>" +
                        "</Grid>" +
                    "</ControlTemplate>";
            }
        }

        private Image _backImage;
        private Image _frontImage;
        private bool _frontImageFailed;

        public ImageSource PlaceholderSource
        {
            get { return (ImageSource)GetValue(PlaceholderSourceProperty); }
            set { SetValue(PlaceholderSourceProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderSourceProperty = DependencyProperty.Register("PlaceholderSource", typeof(ImageSource), typeof(PlaceholderImage), null);

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(PlaceholderImage), new PropertyMetadata(OnSourcePropertyChanged));

        private static void OnSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((PlaceholderImage)o).OnSourcePropertyChanged((ImageSource)e.OldValue, (ImageSource)e.NewValue);
        }

        private void OnSourcePropertyChanged(ImageSource oldValue, ImageSource newValue)
        {
            oldValue = newValue;
            newValue = oldValue;

            _frontImageFailed = false;
            UpdateBackVisibility();
        }

        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register("Stretch", typeof(Stretch), typeof(PlaceholderImage), new PropertyMetadata(Stretch.Uniform));

        public PlaceholderImage()
        {
            Template = (ControlTemplate)XamlReader.Load(TemplateString);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (null != _frontImage)
            {
                _frontImage.ImageOpened -= ImageOpenedOrDownloadCompleted;
            }

            _backImage = GetTemplateChild("BackImage") as Image;
            _frontImage = GetTemplateChild("FrontImage") as Image;
            _frontImageFailed = false;

            if (null != _backImage)
            {
                _backImage.SetBinding(Image.SourceProperty, new Binding("PlaceholderSource") { Source = this });
                _backImage.SetBinding(Image.StretchProperty, new Binding("Stretch") { Source = this });
            }
            if (null != _frontImage)
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    _frontImage.SetBinding(Image.SourceProperty, new Binding("Source") { Source = this });
                }
                _frontImage.SetBinding(Image.StretchProperty, new Binding("Stretch") { Source = this });
                _frontImage.ImageOpened += ImageOpenedOrDownloadCompleted;
                _frontImage.ImageFailed += ImageFailedCompleted;
            }
            UpdateBackVisibility();
        }

        private void ImageOpenedOrDownloadCompleted(object sender, EventArgs e)
        {
            _frontImageFailed = false;
            UpdateBackVisibility();
        }

        void ImageFailedCompleted(object sender, ExceptionRoutedEventArgs e)
        {
            _frontImageFailed = true;
            UpdateBackVisibility();
        }

        private void UpdateBackVisibility()
        {
            if (_backImage != null)
            {
                _backImage.Visibility = _frontImageFailed ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
