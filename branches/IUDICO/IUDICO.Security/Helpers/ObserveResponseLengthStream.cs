using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace IUDICO.Security.Helpers
{
    public class ObserveResponseLengthStream : Stream
    {
        private Stream stream = null;
        private long length = 0;
        public ObserveResponseLengthStream(Stream sstream)
        {
            this.stream = sstream;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        public override void Flush()
        {
            this.stream.Flush();
        }

        public override void SetLength(long value)
        {
            this.stream.SetLength(value);
            this.length = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);
            this.length += (long)count;
        }

        public override void Close()
        {
            try
            {
                this.stream.Close();
            }
            catch { 
            
            }

            base.Close();

            try
            {
                /* now you know the actual length 
                  of the response in this.length,
                  so do whatever you want with it */
            }
            catch (Exception)
            {
                // make sure your exceptions are handled!
            }
        }

        /*...all the other code simply exposes the 
          stream's properties and methods...*/

        public override bool CanRead
        {
            get { return this.stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return this.stream.CanWrite; }
        }

        public override long Length
        {
            get { return this.length; }
        }

        public override long Position
        {
            get
            {
                return this.stream.Position;
            }
            set
            {
                this.stream.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.stream.Read(buffer, offset, count);
        }
    }
}