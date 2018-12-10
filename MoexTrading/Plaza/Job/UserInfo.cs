using System;
using MongoDB.Bson;
using ru.micexrts.cgate;
using ru.micexrts.cgate.message;
using MoexTrading.Plaza.Schemes;
using MoexTrading.Models;

namespace MoexTrading.Plaza.Job
{
    public static class UserInfo
    {
        private static bool bExit = false;
        private static bool bExitPub = false;
        private static Connection conn;

        public static void Run()
        {
            string streamAccount = "FORTS_PART_REPL", tableAccount = "part", streamTrade = "FORTS_FUTTRADE_REPL", tableTrade = "orders_log";
            string strConnectAccount = "p2repl://" + streamAccount + ";tables=" + tableAccount;
            string strConnectTrade = "p2repl://" + streamTrade + ";tables=" + tableTrade;

            CGate.Open("ini=/Plaza/bin/user.ini;key=11111111");
            CGate.LogInfo("test .Net log.");
            conn = new Connection("p2tcp://127.0.0.1:4001;app_name=UserInfo");

            Listener listAccount = new Listener(conn, strConnectAccount);
            listAccount.Handler += new Listener.MessageHandler(MessageHandlerAccount);

            Listener listTrade = new Listener(conn, strConnectTrade);
            listTrade.Handler += new Listener.MessageHandler(MessageHandlerTrade);

            while (!bExit)
            {
                try
                {
                    State state = conn.State;
                    if (state == State.Error)
                    {
                        conn.Close();
                    }
                    else if (state == State.Closed)
                    {
                        conn.Open("");
                    }
                    else if (state == State.Active)
                    {
                        ErrorCode result = conn.Process(1000);

                        if (result != ErrorCode.Ok && result != ErrorCode.TimeOut)
                        {
                            CGate.LogError(String.Format("Warning: connection state request failed: {0}", CGate.GetErrorDesc(result)));
                        }

                        if (listAccount.State == State.Closed)
                        {
                            listAccount.Open("");
                        }
                        else if (listAccount.State == State.Error)
                        {
                            listAccount.Close();
                        }

                        if (listTrade.State == State.Closed)
                        {
                            listTrade.Open("");
                        }
                        else if (listTrade.State == State.Error)
                        {
                            listTrade.Close();
                        }
                    }
                }
                catch (CGateException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            conn.Close();
            listAccount.Close();
            listAccount.Dispose();
            listTrade.Close();
            listTrade.Dispose();
            conn.Dispose();
            CGate.Close();
        }

        private static int MessageHandlerAccount(Connection conn, Listener listener, Message msg)
        {
            try
            {
                if (msg.Type == MessageType.MsgStreamData)
                {
                    StreamDataMessage replmsg = (StreamDataMessage)msg;
                    part p = new part(replmsg.Data);
                    //int id = tool.isin_id;

                    //var oldData = APIMongo.GetToolsById(id).Result;
                    //if (oldData == null)
                    //{
                    //    var data = new DataTools { Id = id, Name = tool.isin };
                    //    APIMongo.SetTools(data.ToBsonDocument());
                    //}
                }

                return 0;
            }
            catch (CGateException e)
            {
                return (int)e.ErrCode;
            }
        }

        private static int MessageHandlerTrade(Connection conn, Listener listener, Message msg)
        {
            try
            {
                if (msg.Type == MessageType.MsgStreamData)
                {
                    StreamDataMessage replmsg = (StreamDataMessage)msg;
                    orders_log p = new orders_log(replmsg.Data);
                    //int id = tool.isin_id;

                    //var oldData = APIMongo.GetToolsById(id).Result;
                    //if (oldData == null)
                    //{
                    //    var data = new DataTools { Id = id, Name = tool.isin };
                    //    APIMongo.SetTools(data.ToBsonDocument());
                    //}
                }

                return 0;
            }
            catch (CGateException e)
            {
                return (int)e.ErrCode;
            }
        }

        public static void Publish()
        {
            int counter = 0;
            Publisher publisher = new Publisher(conn, "p2mq://FORTS_SRV;category=FORTS_MSG;name=srvlink;timeout=5000");
            Listener listener = new Listener(conn, "p2mqreply://;ref=srvlink");
            listener.Handler += new Listener.MessageHandler(ClientMessageCallback);

            while (!bExitPub)
            {
                try
                {
                    State state = conn.State;
                    if (state == State.Error)
                    {
                        conn.Close();
                    }
                    else if (state == State.Closed)
                    {
                        conn.Open("");
                    }
                    else if (state == State.Opening)
                    {
                        ErrorCode result = conn.Process(0);
                        if (result != ErrorCode.Ok && result != ErrorCode.TimeOut)
                        {
                            CGate.LogError(String.Format("Warning: connection state request failed: {0}", CGate.GetErrorDesc(result)));
                        }
                    }
                    else if (state == State.Active)
                    {
                        ErrorCode result = conn.Process(0);
                        if (result != ErrorCode.Ok && result != ErrorCode.TimeOut)
                        {
                            CGate.LogError(String.Format("Warning: connection state request failed: {0}", CGate.GetErrorDesc(result)));
                        }

                        if (listener.State == State.Closed)
                        {
                            listener.Open("");
                        }
                        else if (listener.State == State.Error)
                        {
                            listener.Close();
                        }
                        if (publisher.State == State.Closed)
                        {
                            publisher.Open("");
                        }
                        else if (publisher.State == State.Error)
                        {
                            publisher.Close();
                        }
                        if (listener.State == State.Active && publisher.State == State.Active)
                        {
                            SendOrder(publisher, counter++);
                            System.Threading.Thread.Sleep(2000);
                        }
                    }
                }
                catch (CGateException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            listener.Close();
            listener.Dispose();
            publisher.Close();
            publisher.Dispose();
//            conn.Dispose();
  //          CGate.Close();
        }

        private static void SendOrder(Publisher pub, int counter)
        {
            SchemeDesc schemeDesc = pub.Scheme;
            MessageDesc messageDesc = schemeDesc.Messages[0]; //AddMM
            FieldDesc fieldDesc = messageDesc.Fields[3];

            Message sendMessage = pub.NewMessage(MessageKeyType.KeyName, "FutAddOrder");

            DataMessage smsg = (DataMessage)sendMessage;
            smsg.UserId = (uint)counter;
            smsg["broker_code"].set("FZ00");
            smsg["isin"].set("Si-12.18");
            smsg["client_code"].set("U99");
            smsg["type"].set(1);
            smsg["dir"].set(1);
            smsg["amount"].set(1);
            smsg["price"].set("67000");

            pub.Post(sendMessage, PublishFlag.NeedReply);
            sendMessage.Dispose();
        }

        private static int ClientMessageCallback(Connection conn, Listener listener, Message msg)
        {
            try
            {
                if (msg.Type == MessageType.MsgData)
                {
                    var str = ((DataMessage)msg).Data;
                    bExitPub = true;
                }

                return 0;
            }
            catch (CGateException e)
            {
                return (int)e.ErrCode;
            }
        }
    }
}