using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoexTrading.Plaza.Schemes
{
    partial class part
    {
        public const int table_index = 0;
        private System.IO.UnmanagedMemoryStream stream;
        private int offset;
        private System.IO.BinaryReader reader;
        private System.IO.BinaryWriter writer;
        public part()
        {
        }
        public part(System.IO.UnmanagedMemoryStream stream_)
        {
            Data = stream_;
            offset = 0;
        }
        public part(System.IO.UnmanagedMemoryStream stream_, int offset_)
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






        public string client_code
        {
            get
            {
                checkReader();
                stream.Position = offset + 24;
                return ru.micexrts.cgate.P2TypeParser.ParseCXX(reader, 7);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 24;
                ru.micexrts.cgate.P2TypeComposer.ComposeCXX(writer, value, 7);
            }
        }






        public decimal money_free
        {
            get
            {
                checkReader();
                stream.Position = offset + 32;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 32;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte money_free_scale
        {
            get
            {
                stream.Position = offset + 32;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public decimal money_blocked
        {
            get
            {
                checkReader();
                stream.Position = offset + 47;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 47;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte money_blocked_scale
        {
            get
            {
                stream.Position = offset + 47;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public decimal vm_reserve
        {
            get
            {
                checkReader();
                stream.Position = offset + 62;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 62;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte vm_reserve_scale
        {
            get
            {
                stream.Position = offset + 62;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public decimal fee
        {
            get
            {
                checkReader();
                stream.Position = offset + 77;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 77;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte fee_scale
        {
            get
            {
                stream.Position = offset + 77;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public decimal balance_money
        {
            get
            {
                checkReader();
                stream.Position = offset + 92;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 92;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte balance_money_scale
        {
            get
            {
                stream.Position = offset + 92;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public decimal coeff_go
        {
            get
            {
                checkReader();
                stream.Position = offset + 107;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 107;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d16.5", (decimal)value);
            }
        }

        public byte coeff_go_scale
        {
            get
            {
                stream.Position = offset + 107;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public sbyte limits_set
        {
            get
            {
                checkReader();
                stream.Position = offset + 118;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 118;
                writer.Write(value);
            }
        }






        public decimal money_old
        {
            get
            {
                checkReader();
                stream.Position = offset + 119;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 119;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte money_old_scale
        {
            get
            {
                stream.Position = offset + 119;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public decimal money_amount
        {
            get
            {
                checkReader();
                stream.Position = offset + 134;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 134;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte money_amount_scale
        {
            get
            {
                stream.Position = offset + 134;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public decimal money_pledge_amount
        {
            get
            {
                checkReader();
                stream.Position = offset + 149;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 149;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte money_pledge_amount_scale
        {
            get
            {
                stream.Position = offset + 149;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public decimal vm_intercl
        {
            get
            {
                checkReader();
                stream.Position = offset + 164;
                return (decimal)ru.micexrts.cgate.P2TypeParser.ParseBCDAsDecimal(reader, stream);
            }
            set
            {
                checkWriter();
                stream.Position = offset + 164;
                ru.micexrts.cgate.P2TypeComposer.ComposeDecimalAsBCD(writer, "d26.2", (decimal)value);
            }
        }

        public byte vm_intercl_scale
        {
            get
            {
                stream.Position = offset + 164;
                return ru.micexrts.cgate.P2TypeParser.ParseBCDAsScale(reader, stream);
            }
        }






        public sbyte is_auto_update_limit
        {
            get
            {
                checkReader();
                stream.Position = offset + 179;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 179;
                writer.Write(value);
            }
        }






        public sbyte no_fut_discount
        {
            get
            {
                checkReader();
                stream.Position = offset + 180;
                return reader.ReadSByte();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 180;
                writer.Write(value);
            }
        }






        public int num_clr_2delivery
        {
            get
            {
                checkReader();
                stream.Position = offset + 184;
                return reader.ReadInt32();
            }
            set
            {
                checkWriter();
                stream.Position = offset + 184;
                writer.Write(value);
            }
        }




    }
}