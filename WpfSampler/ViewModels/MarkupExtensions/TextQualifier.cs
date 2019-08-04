using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xaml;

namespace WpfSampler.ViewModels.MarkupExtensions
{
    class TextQualifierExtension : MarkupExtension
    {
        public string Message { get; set; }

        public TextQualifierExtension()
        {
            Message = null;
        }

        public TextQualifierExtension(string message)
        {
            Message = message;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Message))
                throw new InvalidOperationException("Required property Message is invalid.");

            if (serviceProvider == null)
                return Message;

            var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            FrameworkElement targetControl = null;
            if (target != null)
            {
                targetControl = target.TargetObject as FrameworkElement;
            }
            if (targetControl == null)
                return Message;

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(targetControl))
                return Message;

            string qualifiedName = null;
            IRootObjectProvider rootProvider = null;
            if (serviceProvider != null)
            {
                rootProvider = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
            }
            string rootName = null;
            if (rootProvider != null)
            {
                var rootObject = rootProvider.RootObject;
                if (rootObject != null)
                {
                    rootName = rootObject.GetType().ToString();
                }
            }
            if (!string.IsNullOrEmpty(rootName))
            {
                var controlName = targetControl.Name;
                if (!string.IsNullOrEmpty(controlName))
                    qualifiedName = string.Format("{0}:{1}", rootName, controlName);
            }

            string value = (qualifiedName == null) ? Message : $"[{qualifiedName}] {Message}";
            return value;
        }
    }
}
