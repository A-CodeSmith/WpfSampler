using log4net.Core;
using System.Collections.Generic;
using System.IO;

namespace WpfSampler.Logging
{
    class LogExcerptAppender : AppenderBase
    {
        private List<string> _entries = new List<string>();
        private string _excerpt;
        private int _size = 20;

        public string Excerpt
        {
            get => _excerpt;
            set => SetObservableProperty(ref _excerpt, value);
        }

        public int Size
        {
            get => _size;
            set => SetObservableProperty(ref _size, value);
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            StringWriter writer = new StringWriter();
            Layout.Format(writer, loggingEvent);

            _entries.Add(writer.ToString());
            if (_entries.Count > _size)
                _entries.RemoveAt(0);

            Excerpt = string.Join(null, _entries);
        }
    }
}
