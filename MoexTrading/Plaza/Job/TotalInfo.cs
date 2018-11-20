using System;
using System.Collections.Generic;
using System.Web;
using ru.micexrts.cgate;
using ru.micexrts.cgate.message;
using MoexTrading.Plaza.Schemes;

namespace MoexTrading.Plaza.Job
{
    public static class TotalInfo
    {
        private static bool bExit = false;

        public static void Run()
        {
            string streamName = "FORTS_FUTINFO_REPL";
            string table = "fut_instruments" ;
            
            CGate.Open("ini=/Plaza/bin/cgate.ini;key=11111111");
            CGate.LogInfo("test .Net log.");
            Connection conn = new Connection("p2tcp://127.0.0.1:4001;app_name=TotalInfo");
            Listener listener = new Listener(conn, "p2repl://" + streamName + "; tables=" + table);//FORTS_FUTAGGR50_REPL");//FORTS_FUTINFO_REPL;tables=fut_instruments");//FORTS_FUTCOMMON_REPL;tables=common");
            listener.Handler += new Listener.MessageHandler(MessageHandlerClient);
            // RunRead();
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

                    }
                }
                catch (CGateException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            conn.Close();
            listener.Close();
            listener.Dispose();
            conn.Dispose();
            CGate.Close();
        }

        // This callback is what typically message callback looks like
        // Processes messages that are important to Plaza-2 datastream lifecycle
        private static int MessageHandlerClient(Connection conn, Listener listener, Message msg)
        {
            try
            {
                if(msg.Type == MessageType.MsgStreamData)
                {
                    fut_instruments _Instruments = new fut_instruments(msg.Data);
                  //  var d = new DataInfo { InstrumentID = Convert.ToInt32(msgB.isin_id), InstrumentName = msgB.isin };
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