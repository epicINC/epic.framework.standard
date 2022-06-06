using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Epic
{
    public class Tracer
    {
        // TextWriterTraceListener


        public Tracer()
        {
        }

        public Tracer(string filename) : this(new TextWriterTraceListener(filename))
        {
        }

        public Tracer(Stream stream) : this(new TextWriterTraceListener(stream))
        {
        }

        public Tracer(TextWriter write) : this(new TextWriterTraceListener(write))
        {

        }

        public Tracer(TraceListener item)
        {
            this.Listeners.Add(item);
        }



        IList<TraceListener> Listeners { get; set; } = new List<TraceListener>();



        public void Assert(bool condition)
        {
            if (condition) return;
            this.Fail("Fail.");
        }

        public void Assert(bool condition, string message)
        {
            if (condition) return;
            this.Fail(message);
        }

        public void Assert(bool condition, string message, string detailMessage)
        {
            if (condition) return;
            this.Fail(message, detailMessage);
        }

        public void Fail(string message)
        {
            this.Each(e => e.Fail(message));
        }

        public void Fail(string message, string detailMessage)
        {
            this.Each(e => e.Fail(message, detailMessage));
        }


        public void Write(string message)
        {
            Each(e => e.Write(message));
        }

        public void Write(string message, string category)
        {
            Each(e => e.Write(message, category));
        }

        public void Write(object value)
        {
            Each(e => e.Write(value));
        }

        public void Write(object value, string category)
        {
            Each(e => e.Write(value, category));
        }

        public void WriteLine(string message)
        {
            Each(e => e.WriteLine(message));
        }

        public void WriteLine(string message, string category)
        {
            Each(e => e.WriteLine(message, category));
        }

        public void WriteLine(object value)
        {
            Each(e => e.WriteLine(value));
        }

        public void WriteLine(object value, string category)
        {
            Each(e => e.WriteLine(value, category));
        }

        public void WriteIf(bool condition, string message)
        {
            if (!condition) return;
            this.Write(message);
        }

        public void WriteIf(bool condition, string message, string category)
        {
            if (!condition) return;
            this.Write(message, category);
        }

        public void WriteIf(bool condition, object value)
        {
            if (!condition) return;
            this.Write(value);
        }

        public void WriteIf(bool condition, object value, string category)
        {
            if (!condition) return;
            this.Write(value, category);
        }

        public void WriteLineIf(bool condition, string message)
        {
            if (!condition) return;
            this.WriteLine(message);
        }

        public void WriteLineIf(bool condition, string message, string category)
        {
            if (!condition) return;
            this.WriteLine(message, category);
        }

        public void WriteLineIf(bool condition, object value)
        {
            if (!condition) return;
            this.WriteLine(value);
        }

        public void WriteLineIf(bool condition, object value, string category)
        {
            if (!condition) return;
            this.WriteLine(value, category);
        }

        public void Close()
        {
            this.Each(e => e.Close());
        }


        void Each(Action<TraceListener> action)
        {
            if (this.Listeners.Count == 0) return;
            for (var i = 0; i < this.Listeners.Count; i++)
                action(this.Listeners[i]);
        }




    }
}
