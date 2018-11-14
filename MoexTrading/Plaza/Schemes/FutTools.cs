using System;
using System.IO;
using ru.micexrts.cgate;

namespace MoexTrading.Plaza.Schemes
{
    public class fut_instruments
    {
        public const int table_index = 3;
        private UnmanagedMemoryStream stream;
        private int offset;
        private BinaryReader reader;
        private BinaryWriter writer;

        public fut_instruments(UnmanagedMemoryStream stream_)
        {
            Data = stream_;
            offset = 0;
        }

        public fut_instruments(UnmanagedMemoryStream stream_, int offset_)
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

        public string short_isin
        {
            get
            {
                checkReader();
                stream.Position = offset + 28;
                return P2TypeParser.ParseCXX(reader, 25);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 28;
                P2TypeComposer.ComposeCXX(writer, value, 25);
            }
        }

        public string isin
        {
            get
            {
                checkReader();
                stream.Position = offset + 54;
                return P2TypeParser.ParseCXX(reader, 25);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 54;
                P2TypeComposer.ComposeCXX(writer, value, 25);
            }
        }

        public string name
        {
            get
            {
                checkReader();
                stream.Position = offset + 80;
                return P2TypeParser.ParseCXX(reader, 75);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 80;
                P2TypeComposer.ComposeCXX(writer, value, 75);
            }
        }

        public int inst_term
        {
            get
            {
                checkReader();
                stream.Position = offset + 156;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 156;
                writer.Write(value);
            }
        }

        public string code_vcb
        {
            get
            {
                checkReader();
                stream.Position = offset + 160;
                return P2TypeParser.ParseCXX(reader, 25);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 160;
                P2TypeComposer.ComposeCXX(writer, value, 25);
            }
        }

        public sbyte is_limited
        {
            get
            {
                checkReader();
                stream.Position = offset + 186;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 186;
                writer.Write(value);
            }
        }

        public decimal old_kotir
        {
            get
            {
                checkReader();
                stream.Position = offset + 187;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 187;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte old_kotir_scale
        {
            get
            {
                stream.Position = offset + 187;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public int roundto
        {
            get
            {
                checkReader();
                stream.Position = offset + 200;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 200;
                writer.Write(value);
            }
        }

        public decimal min_step
        {
            get
            {
                checkReader();
                stream.Position = offset + 204;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 204;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte min_step_scale
        {
            get
            {
                stream.Position = offset + 204;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public int lot_volume
        {
            get
            {
                checkReader();
                stream.Position = offset + 216;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 216;
                writer.Write(value);
            }
        }

        public decimal step_price
        {
            get
            {
                checkReader();
                stream.Position = offset + 220;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 220;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte step_price_scale
        {
            get
            {
                stream.Position = offset + 220;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public DateTime d_pg
        {
            get
            {
                checkReader();
                stream.Position = offset + 232;
                return P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 232;
                P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }

        public sbyte is_spread
        {
            get
            {
                checkReader();
                stream.Position = offset + 242;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 242;
                writer.Write(value);
            }
        }

        public DateTime d_exp
        {
            get
            {
                checkReader();
                stream.Position = offset + 244;
                return P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 244;
                P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }

        public sbyte is_percent
        {
            get
            {
                checkReader();
                stream.Position = offset + 254;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 254;
                writer.Write(value);
            }
        }

        public decimal percent_rate
        {
            get
            {
                checkReader();
                stream.Position = offset + 255;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 255;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d6.2", (decimal)value);
            }
        }

        public byte percent_rate_scale
        {
            get
            {
                stream.Position = offset + 255;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal last_cl_quote
        {
            get
            {
                checkReader();
                stream.Position = offset + 260;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 260;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte last_cl_quote_scale
        {
            get
            {
                stream.Position = offset + 260;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public int signs
        {
            get
            {
                checkReader();
                stream.Position = offset + 272;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 272;
                writer.Write(value);
            }
        }

        public decimal volat_min
        {
            get
            {
                checkReader();
                stream.Position = offset + 276;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 276;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d20.15", (decimal)value);
            }
        }

        public byte volat_min_scale
        {
            get
            {
                stream.Position = offset + 276;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal volat_max
        {
            get
            {
                checkReader();
                stream.Position = offset + 289;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 289;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d20.15", (decimal)value);
            }
        }

        public byte volat_max_scale
        {
            get
            {
                stream.Position = offset + 289;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public sbyte price_dir
        {
            get
            {
                checkReader();
                stream.Position = offset + 302;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 302;
                writer.Write(value);
            }
        }

        public int multileg_type
        {
            get
            {
                checkReader();
                stream.Position = offset + 304;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 304;
                writer.Write(value);
            }
        }

        public int legs_qty
        {
            get
            {
                checkReader();
                stream.Position = offset + 308;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 308;
                writer.Write(value);
            }
        }

        public decimal step_price_clr
        {
            get
            {
                checkReader();
                stream.Position = offset + 312;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 312;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte step_price_clr_scale
        {
            get
            {
                stream.Position = offset + 312;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal step_price_interclr
        {
            get
            {
                checkReader();
                stream.Position = offset + 323;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 323;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte step_price_interclr_scale
        {
            get
            {
                stream.Position = offset + 323;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal step_price_curr
        {
            get
            {
                checkReader();
                stream.Position = offset + 334;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 334;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte step_price_curr_scale
        {
            get
            {
                stream.Position = offset + 334;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public DateTime d_start
        {
            get
            {
                checkReader();
                stream.Position = offset + 346;
                return P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 346;
                P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }

        public sbyte is_limit_opt
        {
            get
            {
                checkReader();
                stream.Position = offset + 356;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 356;
                writer.Write(value);
            }
        }

        public decimal limit_up_opt
        {
            get
            {
                checkReader();
                stream.Position = offset + 357;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 357;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d5.2", (decimal)value);
            }
        }

        public byte limit_up_opt_scale
        {
            get
            {
                stream.Position = offset + 357;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal limit_down_opt
        {
            get
            {
                checkReader();
                stream.Position = offset + 362;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 362;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d5.2", (decimal)value);
            }
        }

        public byte limit_down_opt_scale
        {
            get
            {
                stream.Position = offset + 362;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal adm_lim
        {
            get
            {
                checkReader();
                stream.Position = offset + 367;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 367;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte adm_lim_scale
        {
            get
            {
                stream.Position = offset + 367;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal adm_lim_offmoney
        {
            get
            {
                checkReader();
                stream.Position = offset + 378;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 378;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte adm_lim_offmoney_scale
        {
            get
            {
                stream.Position = offset + 378;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public sbyte apply_adm_limit
        {
            get
            {
                checkReader();
                stream.Position = offset + 389;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 389;
                writer.Write(value);
            }
        }

        public decimal pctyield_coeff
        {
            get
            {
                checkReader();
                stream.Position = offset + 390;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 390;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte pctyield_coeff_scale
        {
            get
            {
                stream.Position = offset + 390;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public decimal pctyield_total
        {
            get
            {
                checkReader();
                stream.Position = offset + 401;
                return (decimal)P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 401;
                P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte pctyield_total_scale
        {
            get
            {
                stream.Position = offset + 401;
                return P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }

        public string exec_name
        {
            get
            {
                checkReader();
                stream.Position = offset + 412;
                return P2TypeParser.ParseCXX(reader, 1);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 412;
                P2TypeComposer.ComposeCXX(writer, value, 1);
            }
        }
    }
}