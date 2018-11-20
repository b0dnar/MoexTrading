using System;
using System.IO;
using ru.micexrts.cgate;

namespace MoexTrading.Plaza.Schemes
{
    public class fut_sess_contents
    {
        public const int table_index = 1;
        private UnmanagedMemoryStream stream;
        private int offset;
        private BinaryReader reader;
        private BinaryWriter writer;
        
        public fut_sess_contents(UnmanagedMemoryStream stream_)
        {
            Data = stream_;
            offset = 0;
        }

        public fut_sess_contents(UnmanagedMemoryStream stream_, int offset_)
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

        public string short_isin
        {
            get
            {
                checkReader();
                stream.Position = offset + 32;
                return P2TypeParser.ParseCXX(reader, 25);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 32;
                P2TypeComposer.ComposeCXX(writer, value, 25);
            }
        }

        public string isin
        {
            get
            {
                checkReader();
                stream.Position = offset + 58;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 25);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 58;
                P2TypeComposer.ComposeCXX(writer, value, 25);
            }
        }

        public string name
        {
            get
            {
                checkReader();
                stream.Position = offset + 84;
                return P2TypeParser.ParseCXX(reader, 75);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 84;
                P2TypeComposer.ComposeCXX(writer, value, 75);
            }
        }

        public int inst_term
        {
            get
            {
                checkReader();
                stream.Position = offset + 160;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 160;
                writer.Write(value);
            }
        }

        public string code_vcb
        {
            get
            {
                checkReader();
                stream.Position = offset + 164;
                return P2TypeParser.ParseCXX(reader, 25);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 164;
                P2TypeComposer.ComposeCXX(writer, value, 25);
            }
        }

        public sbyte is_limited
        {
            get
            {
                checkReader();
                stream.Position = offset + 190;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 190;
                writer.Write(value);
            }
        }

        public decimal limit_up
        {
            get
            {
                checkReader();
                stream.Position = offset + 191;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 191;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte limit_up_scale
        {
            get
            {
                stream.Position = offset + 191;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal limit_down
        {
            get
            {
                checkReader();
                stream.Position = offset + 202;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 202;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte limit_down_scale
        {
            get
            {
                stream.Position = offset + 202;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal old_kotir
        {
            get
            {
                checkReader();
                stream.Position = offset + 213;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 213;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte old_kotir_scale
        {
            get
            {
                stream.Position = offset + 213;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal buy_deposit
        {
            get
            {
                checkReader();
                stream.Position = offset + 224;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 224;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.2", (decimal)value);
            }
        }

        public byte buy_deposit_scale
        {
            get
            {
                stream.Position = offset + 224;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal sell_deposit
        {
            get
            {
                checkReader();
                stream.Position = offset + 234;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 234;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.2", (decimal)value);
            }
        }

        public byte sell_deposit_scale
        {
            get
            {
                stream.Position = offset + 234;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public int roundto
        {
            get
            {
                checkReader();
                stream.Position = offset + 244;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 244;
                writer.Write(value);
            }
        }

        public decimal min_step
        {
            get
            {
                checkReader();
                stream.Position = offset + 248;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 248;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte min_step_scale
        {
            get
            {
                stream.Position = offset + 248;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public int lot_volume
        {
            get
            {
                checkReader();
                stream.Position = offset + 260;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 260;
                writer.Write(value);
            }
        }

        public decimal step_price
        {
            get
            {
                checkReader();
                stream.Position = offset + 264;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 264;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte step_price_scale
        {
            get
            {
                stream.Position = offset + 264;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public DateTime d_pg
        {
            get
            {
                checkReader();
                stream.Position = offset + 276;
                return P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 276;
                P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }

        public sbyte is_spread
        {
            get
            {
                checkReader();
                stream.Position = offset + 286;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 286;
                writer.Write(value);
            }
        }

        public DateTime d_exp
        {
            get
            {
                checkReader();
                stream.Position = offset + 288;
                return P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 288;
                P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }

        public sbyte is_percent
        {
            get
            {
                checkReader();
                stream.Position = offset + 298;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 298;
                writer.Write(value);
            }
        }

        public decimal percent_rate
        {
            get
            {
                checkReader();
                stream.Position = offset + 299;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 299;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d6.2", (decimal)value);
            }
        }

        public byte percent_rate_scale
        {
            get
            {
                stream.Position = offset + 299;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal last_cl_quote
        {
            get
            {
                checkReader();
                stream.Position = offset + 304;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 304;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte last_cl_quote_scale
        {
            get
            {
                stream.Position = offset + 304;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public int signs
        {
            get
            {
                checkReader();
                stream.Position = offset + 316;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 316;
                writer.Write(value);
            }
        }

        public sbyte is_trade_evening
        {
            get
            {
                checkReader();
                stream.Position = offset + 320;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 320;
                writer.Write(value);
            }
        }

        public int ticker
        {
            get
            {
                checkReader();
                stream.Position = offset + 324;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 324;
                writer.Write(value);
            }
        }

        public int state
        {
            get
            {
                checkReader();
                stream.Position = offset + 328;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 328;
                writer.Write(value);
            }
        }

        public sbyte price_dir
        {
            get
            {
                checkReader();
                stream.Position = offset + 332;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 332;
                writer.Write(value);
            }
        }

        public int multileg_type
        {
            get
            {
                checkReader();
                stream.Position = offset + 336;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 336;
                writer.Write(value);
            }
        }

        public int legs_qty
        {
            get
            {
                checkReader();
                stream.Position = offset + 340;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 340;
                writer.Write(value);
            }
        }

        public decimal step_price_clr
        {
            get
            {
                checkReader();
                stream.Position = offset + 344;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 344;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte step_price_clr_scale
        {
            get
            {
                stream.Position = offset + 344;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal step_price_interclr
        {
            get
            {
                checkReader();
                stream.Position = offset + 355;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 355;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte step_price_interclr_scale
        {
            get
            {
                stream.Position = offset + 355;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal step_price_curr
        {
            get
            {
                checkReader();
                stream.Position = offset + 366;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 366;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte step_price_curr_scale
        {
            get
            {
                stream.Position = offset + 366;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public DateTime d_start
        {
            get
            {
                checkReader();
                stream.Position = offset + 378;
                return P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 378;
                P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }

        public decimal exch_pay
        {
            get
            {
                checkReader();
                stream.Position = offset + 388;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 388;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte exch_pay_scale
        {
            get
            {
                stream.Position = offset + 388;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal pctyield_coeff
        {
            get
            {
                checkReader();
                stream.Position = offset + 399;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 399;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte pctyield_coeff_scale
        {
            get
            {
                stream.Position = offset + 399;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal pctyield_total
        {
            get
            {
                checkReader();
                stream.Position = offset + 410;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 410;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte pctyield_total_scale
        {
            get
            {
                stream.Position = offset + 410;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }
    }
}