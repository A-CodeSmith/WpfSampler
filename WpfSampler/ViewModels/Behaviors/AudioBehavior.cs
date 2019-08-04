using System.Media;
using System.Windows;
using System.Windows.Controls;

namespace WpfSampler.ViewModels.Behaviors
{
    static class AudioBehavior
    {
        public static readonly DependencyProperty DingOnClickProperty = DependencyProperty.RegisterAttached("DingOnClick", typeof(bool), typeof(AudioBehavior), new PropertyMetadata(false, OnDingOnClickChanged));

        public static bool GetDingOnClick(DependencyObject obj)
        {
            return (bool)obj.GetValue(DingOnClickProperty);
        }
        public static void SetDingOnClick(DependencyObject obj, bool value)
        {
            obj.SetValue(DingOnClickProperty, value);
        }

        private static void PlayDingSound(object sender, RoutedEventArgs e)
        {
            using (SoundPlayer player = new SoundPlayer(@"Resources\ding.wav"))
            {
                player.Load();
                player.Play();
            }
        }

        private static void OnDingOnClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Button;
            if (element != null)
            {
                if ((bool)e.NewValue)
                {
                    element.Click += PlayDingSound;
                }
                else
                    element.Click -= PlayDingSound;
            }
        }
    }
}
