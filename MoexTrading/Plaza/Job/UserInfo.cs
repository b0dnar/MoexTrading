using System;
using MongoDB.Bson;
using ru.micexrts.cgate;
using ru.micexrts.cgate.message;
using MoexTrading.Plaza.Schemes;
using MoexTrading.Models;

namespace MoexTrading.Plaza.Job
{
    public class UserInfo
    {
        private static bool bExit = false;

        public static void Run()
        {
            string streamAccount = "FORTS_PART_REPL", tableAccount = "part";
            string strConnectAccount = "p2repl://" + streamAccount + ";tables=" + tableAccount;

            CGate.Open("ini=/Plaza/bin/user.ini;key=11111111");
            CGate.LogInfo("test .Net log.");
            Connection conn = new Connection("p2tcp://127.0.0.1:4001;app_name=UserInfo");

            Listener listAccount = new Listener(conn, strConnectAccount);
            listAccount.Handler += new Listener.MessageHandler(MessageHandlerAccount);

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
    }
}