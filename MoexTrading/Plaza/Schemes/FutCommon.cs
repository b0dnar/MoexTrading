using System;
using System.IO;
using ru.micexrts.cgate;

namespace MoexTrading.Plaza.Schemes
{
    public class common
    {
        public const int table_index = 0;
        private UnmanagedMemoryStream stream;
        private int offset;
        private BinaryReader reader;
        private BinaryWriter writer;

        public common(UnmanagedMemoryStream stream_)
        {
            Data = stream_;
            offset = 0;
        }

        public common(UnmanagedMemoryStream stream_, int offset_)
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

        public int sess_id
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

        public int isin_id
        {
            get
            {
                checkReader();
                stream.Position = offset + 28;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 28;
                writer.Write(value);
            }
        }

        public decimal best_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 32;
                decimal buy;
                try
                {
                    buy = (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
                }
                catch
                {
                    buy = 0;
                }
                return buy;
            }
            set
            {
                checkWriter();
                stream.Position = offset + 32;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte best_buy_scale
        {
            get
            {
                stream.Position = offset + 32;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public long xamount_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 44;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 44;
                writer.Write(value);
            }
        }

        public int orders_buy_qty
        {
            get
            {
                checkReader();
                stream.Position = offset + 52;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 52;
                writer.Write(value);
            }
        }

        public long xorders_buy_amount
        {
            get
            {
                checkReader();
                stream.Position = offset + 56;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 56;
                writer.Write(value);
            }
        }

        public decimal best_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 64;
                decimal sel;
                try
                {
                    sel = (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
                }
                catch
                {
                    sel = 0;
                }
                return sel;
            }
            set
            {
                checkWriter();
                stream.Position = offset + 64;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte best_sell_scale
        {
            get
            {
                stream.Position = offset + 64;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public long xamount_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 76;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 76;
                writer.Write(value);
            }
        }

        public int orders_sell_qty
        {
            get
            {
                checkReader();
                stream.Position = offset + 84;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 84;
                writer.Write(value);
            }
        }

        public long xorders_sell_amount
        {
            get
            {
                checkReader();
                stream.Position = offset + 88;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 88;
                writer.Write(value);
            }
        }

        public decimal open_price
        {
            get
            {
                checkReader();
                stream.Position = offset + 96;
                decimal op;
                try
                {
                    op = (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
                }
                catch
                {
                    op = 0;
                }
                return op;
            }
            set
            {
                checkWriter();
                stream.Position = offset + 96;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte open_price_scale
        {
            get
            {
                stream.Position = offset + 96;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal close_price
        {
            get
            {
                checkReader();
                stream.Position = offset + 107;
                decimal cl;
                try
                {
                    cl = (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
                }
                catch
                {
                    cl = 0;
                }
                return cl;
            }
            set
            {
                checkWriter();
                stream.Position = offset + 107;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte close_price_scale
        {
            get
            {
                stream.Position = offset + 107;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal price
        {
            get
            {
                checkReader();
                stream.Position = offset + 118;
                decimal rez;
                try
                {
                    rez = (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
                }
                catch
                {
                    rez = 0;
                }

                return rez;
            }
            set
            {
                checkWriter();
                stream.Position = offset + 118;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte price_scale
        {
            get
            {
                stream.Position = offset + 118;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal trend
        {
            get
            {
                checkReader();
                stream.Position = offset + 129;
                decimal tr;
                try
                {
                    tr = (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
                }
                catch
                {
                    tr = 0;
                }
                return tr;
            }
            set
            {
                checkWriter();
                stream.Position = offset + 129;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte trend_scale
        {
            get
            {
                stream.Position = offset + 129;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public long xamount
        {
            get
            {
                checkReader();
                stream.Position = offset + 140;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 140;
                writer.Write(value);
            }
        }

        public DateTime deal_time
        {
            get
            {
                checkReader();
                stream.Position = offset + 148;
                return P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 148;
                P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }

        public ulong deal_time_ns
        {
            get
            {
                checkReader();
                stream.Position = offset + 160;
                return reader.ReadUInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 160;
                writer.Write(value);
            }
        }

        public decimal min_price
        {
            get
            {
                checkReader();
                stream.Position = offset + 168;
                decimal pr;
                try
                {
                    pr = (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
                }
                catch
                {
                    pr = 0;
                }
                return pr;
            }
            set
            {
                checkWriter();
                stream.Position = offset + 168;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte min_price_scale
        {
            get
            {
                stream.Position = offset + 168;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal max_price
        {
            get
            {
                checkReader();
                stream.Position = offset + 179;
                decimal pr;
                try
                {
                    pr = (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
                }
                catch
                {
                    pr = 0;
                }
                return pr;
            }
            set
            {
                checkWriter();
                stream.Position = offset + 179;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte max_price_scale
        {
            get
            {
                stream.Position = offset + 179;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal avr_price
        {
            get
            {
                checkReader();
                stream.Position = offset + 190;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 190;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte avr_price_scale
        {
            get
            {
                stream.Position = offset + 190;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public long xcontr_count
        {
            get
            {
                checkReader();
                stream.Position = offset + 204;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 204;
                writer.Write(value);
            }
        }

        public decimal capital
        {
            get
            {
                checkReader();
                stream.Position = offset + 212;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 212;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte capital_scale
        {
            get
            {
                stream.Position = offset + 212;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public int deal_count
        {
            get
            {
                checkReader();
                stream.Position = offset + 228;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 228;
                writer.Write(value);
            }
        }

        public decimal old_kotir
        {
            get
            {
                checkReader();
                stream.Position = offset + 232;
                decimal kot;
                try
                {
                    kot = (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
                }
                catch
                {
                    kot = 0;
                }
                return kot;
            }
            set
            {
                checkWriter();
                stream.Position = offset + 232;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte old_kotir_scale
        {
            get
            {
                stream.Position = offset + 232;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public long xpos
        {
            get
            {
                checkReader();
                stream.Position = offset + 244;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 244;
                writer.Write(value);
            }
        }

        public DateTime mod_time
        {
            get
            {
                checkReader();
                stream.Position = offset + 252;
                DateTime time;
                try
                {
                    time = P2TypeParser.ParseTimeAsDate(reader);
                }
                catch
                {
                    time = DateTime.Now;
                }
                return time;
            }
            set
            {
                checkWriter();
                stream.Position = offset + 252;
                P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }

        public ulong mod_time_ns
        {
            get
            {
                checkReader();
                stream.Position = offset + 264;
                return reader.ReadUInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 264;
                writer.Write(value);
            }
        }

        public decimal cur_kotir
        {
            get
            {
                checkReader();
                stream.Position = offset + 272;
                decimal dec;
                try
                {
                    dec = (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
                }
                catch
                {
                    dec = old_kotir;
                }
                return dec;
            }
            set
            {
                checkWriter();
                stream.Position = offset + 272;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte cur_kotir_scale
        {
            get
            {
                stream.Position = offset + 272;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal cur_kotir_real
        {
            get
            {
                checkReader();
                stream.Position = offset + 283;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 283;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte cur_kotir_real_scale
        {
            get
            {
                stream.Position = offset + 283;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public DateTime local_time
        {
            get
            {
                checkReader();
                stream.Position = offset + 294;
                return P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 294;
                P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }
    }
}