using System;
using System.IO;
using ru.micexrts.cgate;

namespace MoexTrading.Plaza.Schemes
{
    public class orders_log
    {
        public const int table_index = 0;
        private System.IO.UnmanagedMemoryStream stream;
        private int offset;
        private System.IO.BinaryReader reader;
        private System.IO.BinaryWriter writer;
        public orders_log()
        {
        }
        public orders_log(System.IO.UnmanagedMemoryStream stream_)
        {
            Data = stream_;
            offset = 0;
        }
        public orders_log(System.IO.UnmanagedMemoryStream stream_, int offset_)
        {
            Data = stream_;
            offset = offset_;
        }
        public System.IO.UnmanagedMemoryStream Data
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
                reader = new System.IO.BinaryReader(stream);
        }
        private void checkWriter()
        {
            if (writer == null)
                writer = new System.IO.BinaryWriter(stream);
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






        public long id_ord
        {
            get
            {
                checkReader();
                stream.Position = offset + 24;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 24;
                writer.Write(value);
            }
        }






        public int sess_id
        {
            get
            {
                checkReader();
                stream.Position = offset + 32;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 32;
                writer.Write(value);
            }
        }






        public int isin_id
        {
            get
            {
                checkReader();
                stream.Position = offset + 36;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 36;
                writer.Write(value);
            }
        }






        public long xamount
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






        public long xamount_rest
        {
            get
            {
                checkReader();
                stream.Position = offset + 48;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 48;
                writer.Write(value);
            }
        }






        public long id_deal
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






        public long xstatus
        {
            get
            {
                checkReader();
                stream.Position = offset + 64;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 64;
                writer.Write(value);
            }
        }






        public decimal price
        {
            get
            {
                checkReader();
                stream.Position = offset + 72;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 72;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte price_scale
        {
            get
            {
                stream.Position = offset + 72;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public System.DateTime moment
        {
            get
            {
                checkReader();
                stream.Position = offset + 84;
                return ru.micexrts.cgate.P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 84;
                ru.micexrts.cgate.P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }






        public ulong moment_ns
        {
            get
            {
                checkReader();
                stream.Position = offset + 96;
                return reader.ReadUInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 96;
                writer.Write(value);
            }
        }






        public sbyte dir
        {
            get
            {
                checkReader();
                stream.Position = offset + 104;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 104;
                writer.Write(value);
            }
        }






        public sbyte action
        {
            get
            {
                checkReader();
                stream.Position = offset + 105;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 105;
                writer.Write(value);
            }
        }






        public decimal deal_price
        {
            get
            {
                checkReader();
                stream.Position = offset + 106;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 106;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte deal_price_scale
        {
            get
            {
                stream.Position = offset + 106;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public string client_code
        {
            get
            {
                checkReader();
                stream.Position = offset + 117;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 7);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 117;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 7);
            }
        }






        public string login_from
        {
            get
            {
                checkReader();
                stream.Position = offset + 125;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 20);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 125;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 20);
            }
        }






        public string comment
        {
            get
            {
                checkReader();
                stream.Position = offset + 146;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 20);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 146;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 20);
            }
        }






        public sbyte hedge
        {
            get
            {
                checkReader();
                stream.Position = offset + 167;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 167;
                writer.Write(value);
            }
        }






        public sbyte trust
        {
            get
            {
                checkReader();
                stream.Position = offset + 168;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 168;
                writer.Write(value);
            }
        }






        public int ext_id
        {
            get
            {
                checkReader();
                stream.Position = offset + 172;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 172;
                writer.Write(value);
            }
        }






        public string broker_to
        {
            get
            {
                checkReader();
                stream.Position = offset + 176;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 7);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 176;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 7);
            }
        }






        public string broker_to_rts
        {
            get
            {
                checkReader();
                stream.Position = offset + 184;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 7);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 184;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 7);
            }
        }






        public string broker_from_rts
        {
            get
            {
                checkReader();
                stream.Position = offset + 192;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 7);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 192;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 7);
            }
        }






        public System.DateTime date_exp
        {
            get
            {
                checkReader();
                stream.Position = offset + 200;
                return ru.micexrts.cgate.P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 200;
                ru.micexrts.cgate.P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }






        public long id_ord1
        {
            get
            {
                checkReader();
                stream.Position = offset + 212;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 212;
                writer.Write(value);
            }
        }






        public System.DateTime local_stamp
        {
            get
            {
                checkReader();
                stream.Position = offset + 220;
                return ru.micexrts.cgate.P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 220;
                ru.micexrts.cgate.P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }






        public int aspref
        {
            get
            {
                checkReader();
                stream.Position = offset + 232;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 232;
                writer.Write(value);
            }
        }




    }

    public class user_deal
    {
        public const int table_index = 4;
        private System.IO.UnmanagedMemoryStream stream;
        private int offset;
        private System.IO.BinaryReader reader;
        private System.IO.BinaryWriter writer;
        public user_deal()
        {
        }
        public user_deal(System.IO.UnmanagedMemoryStream stream_)
        {
            Data = stream_;
            offset = 0;
        }
        public user_deal(System.IO.UnmanagedMemoryStream stream_, int offset_)
        {
            Data = stream_;
            offset = offset_;
        }
        public System.IO.UnmanagedMemoryStream Data
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
                reader = new System.IO.BinaryReader(stream);
        }
        private void checkWriter()
        {
            if (writer == null)
                writer = new System.IO.BinaryWriter(stream);
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






        public long id_deal
        {
            get
            {
                checkReader();
                stream.Position = offset + 32;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 32;
                writer.Write(value);
            }
        }






        public long id_deal_multileg
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






        public long id_repo
        {
            get
            {
                checkReader();
                stream.Position = offset + 48;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 48;
                writer.Write(value);
            }
        }






        public long xpos
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






        public long xamount
        {
            get
            {
                checkReader();
                stream.Position = offset + 64;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 64;
                writer.Write(value);
            }
        }






        public long id_ord_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 72;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 72;
                writer.Write(value);
            }
        }






        public long id_ord_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 80;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 80;
                writer.Write(value);
            }
        }






        public decimal price
        {
            get
            {
                checkReader();
                stream.Position = offset + 88;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 88;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte price_scale
        {
            get
            {
                stream.Position = offset + 88;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public System.DateTime moment
        {
            get
            {
                checkReader();
                stream.Position = offset + 100;
                return ru.micexrts.cgate.P2TypeParser.ParseTimeAsDate(reader);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 100;
                ru.micexrts.cgate.P2TypeComposer.ComposeDateAsTime(writer, value);
            }
        }






        public ulong moment_ns
        {
            get
            {
                checkReader();
                stream.Position = offset + 112;
                return reader.ReadUInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 112;
                writer.Write(value);
            }
        }






        public sbyte nosystem
        {
            get
            {
                checkReader();
                stream.Position = offset + 120;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 120;
                writer.Write(value);
            }
        }






        public long xstatus_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 124;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 124;
                writer.Write(value);
            }
        }






        public long xstatus_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 132;
                return reader.ReadInt64();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 132;
                writer.Write(value);
            }
        }






        public int ext_id_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 140;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 140;
                writer.Write(value);
            }
        }






        public int ext_id_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 144;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 144;
                writer.Write(value);
            }
        }






        public string code_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 148;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 7);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 148;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 7);
            }
        }






        public string code_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 156;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 7);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 156;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 7);
            }
        }






        public string comment_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 164;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 20);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 164;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 20);
            }
        }






        public string comment_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 185;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 20);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 185;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 20);
            }
        }






        public sbyte trust_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 206;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 206;
                writer.Write(value);
            }
        }






        public sbyte trust_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 207;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 207;
                writer.Write(value);
            }
        }






        public sbyte hedge_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 208;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 208;
                writer.Write(value);
            }
        }






        public sbyte hedge_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 209;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 209;
                writer.Write(value);
            }
        }






        public decimal fee_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 210;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 210;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte fee_buy_scale
        {
            get
            {
                stream.Position = offset + 210;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public decimal fee_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 225;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 225;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte fee_sell_scale
        {
            get
            {
                stream.Position = offset + 225;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public string login_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 240;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 20);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 240;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 20);
            }
        }






        public string login_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 261;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 20);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 261;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 20);
            }
        }






        public string code_rts_buy
        {
            get
            {
                checkReader();
                stream.Position = offset + 282;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 7);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 282;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 7);
            }
        }






        public string code_rts_sell
        {
            get
            {
                checkReader();
                stream.Position = offset + 290;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 7);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 290;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 7);
            }
        }




    }
}