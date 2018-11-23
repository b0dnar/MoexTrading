using System;
using System.IO;
using ru.micexrts.cgate;

namespace MoexTrading.Plaza.Schemes
{
    public class orders_aggr
    {
        public const int table_index = 0;
        private UnmanagedMemoryStream stream;
        private int offset;
        private BinaryReader reader;
        private BinaryWriter writer;
     
        public orders_aggr(UnmanagedMemoryStream stream_)
        {
            Data = stream_;
            offset = 0;
        }

        public orders_aggr(UnmanagedMemoryStream stream_, int offset_)
        {
            Data = stream_;
            offset = offset_;
        }

        public UnmanagedMemoryStream Data
        {
            set
            {
                stream = value;
                reader = null;
                writer = null;
            }
            get
            {
                return stream;
            }
        }

        public int Offset
        {
            get
            {
                return offset;
            }
        }

        private void checkReader()
        {
            if (reader == null)
                reader = new BinaryReader(stream);
        }

        private void checkWriter()
        {
            if (writer == null)
                writer = new BinaryWriter(stream);
        }

        public long replID
        {
            get
            {
                checkReader();
                stream.Position = offset + 0;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 0;
                writer.Write(value);
            }
        }

        public long replRev
        {
            get
            {
                checkReader();
                stream.Position = offset + 8;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 8;
                writer.Write(value);
            }
        }

        public long replAct
        {
            get
            {
                checkReader();
                stream.Position = offset + 16;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 16;
                writer.Write(value);
            }
        }

        public int isin_id
        {
            get
            {
                checkReader();
                stream.Position = offset + 24;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 24;
                writer.Write(value);
            }
        }

        public decimal price
        {
            get
            {
                checkReader();
                stream.Position = offset + 28;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 28;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte price_scale
        {
            get
            {
                stream.Position = offset + 28;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public long volume
        {
            get
            {
                checkReader();
                stream.Position = offset + 40;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 40;
                writer.Write(value);
            }
        }

        public DateTime moment
        {
            get
            {
                checkReader();
                stream.Position = offset + 48;
                return P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 48;
                P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }

        public ulong moment_ns
        {
            get
            {
                checkReader();
                stream.Position = offset + 60;
                return reader.ReadUInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 60;
                writer.Write(value);
            }
        }

        public sbyte dir
        {
            get
            {
                checkReader();
                stream.Position = offset + 68;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 68;
                writer.Write(value);
            }
        }

        public DateTime timestamp
        {
            get
            {
                checkReader();
                stream.Position = offset + 70;
                return P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 70;
                P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }

        public int sess_id
        {
            get
            {
                checkReader();
                stream.Position = offset + 80;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 80;
                writer.Write(value);
            }
        }
    }
}