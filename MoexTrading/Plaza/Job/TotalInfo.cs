using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Bson;
using ru.micexrts.cgate;
using ru.micexrts.cgate.message;
using MoexTrading.Plaza.Schemes;
using MoexTrading.Models;


namespace MoexTrading.Plaza.Job
{



    public static class TotalInfo
    {
        private static bool bExit = false;

        public static void Run()
        {

            string streamInfo = "FORTS_FUTINFO_REPL", tableTools = "fut_sess_contents", streamCommonn = "FORTS_FUTCOMMON_REPL", tableCandles = "common";
            string strConnectInfo = "p2repl://" + streamInfo + ";tables=" + tableTools;
            string strConnectCommon = "p2repl://" + streamCommonn + ";tables=" + tableCandles;


            CGate.Open("ini=/Plaza/bin/cgate.ini;key=11111111");
            CGate.LogInfo("test .Net log.");
            Connection conn = new Connection("p2tcp://127.0.0.1:4001;app_name=TotalInfo");

            Listener listInfo = new Listener(conn, strConnectInfo);//FORTS_FUTAGGR50_REPL");//FORTS_FUTINFO_REPL;tables=fut_instruments");//FORTS_FUTCOMMON_REPL;tables=common");
            listInfo.Handler += new Listener.MessageHandler(MessageHandlerTools);

            Listener listCommon = new Listener(conn, strConnectCommon);
            listCommon.Handler += new Listener.MessageHandler(MessageHandlerCandles);
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

                        if (listInfo.State == State.Closed)
                        {
                            listInfo.Open("");
                        }
                        else if (listInfo.State == State.Error)
                        {
                            listInfo.Close();
                        }

                        if (listCommon.State == State.Closed)
                        {
                            listCommon.Open("");
                        }
                        else if (listCommon.State == State.Error)
                        {
                            listCommon.Close();
                        }
                    }
                }
                catch (CGateException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            conn.Close();
            listInfo.Close();
            listInfo.Dispose();
            listCommon.Close();
            listCommon.Dispose();
            conn.Dispose();
            CGate.Close();
        }

        // This callback is what typically message callback looks like
        // Processes messages that are important to Plaza-2 datastream lifecycle
        private static int MessageHandlerTools(Connection conn, Listener listener, Message msg)
        {
            try
            {
                if (msg.Type == MessageType.MsgStreamData)
                {
                    StreamDataMessage replmsg = (StreamDataMessage)msg;
                    fut_sess_contents tool = new fut_sess_contents(replmsg.Data);
                    int id = tool.isin_id;

                    var oldData = APIMongo.GetToolsById(id).Result;
                    if (oldData == null)
                    {
                        var data = new DataTools { Id = id, Name = tool.isin };
                        APIMongo.SetTools(data.ToBsonDocument());
                    }
                }

                return 0;
            }
            catch (CGateException e)
            {
                return (int)e.ErrCode;
            }
        }

        private static int MessageHandlerCandles(Connection conn, Listener listener, Message msg)
        {
            try
            {
                if (msg.Type == MessageType.MsgStreamData)
                {
                    StreamDataMessage replmsg = (StreamDataMessage)msg;
                    common tool = new common(replmsg.Data);
                    int id = tool.isin_id;

                    var dataTik = APIMongo.GetCandlesTikById(id, ElementMongo.NameTableCandlesOnTik).Result;

                    if(tool.price_scale > 0)
                    {
                        if (dataTik == null)
                        {
                            DataCandlesTik data = new DataCandlesTik();
                            data.Id = id;
                            data.ArrayCandles.Add(tool.price_scale);

                            APIMongo.SetCandles(data.ToBsonDocument(), ElementMongo.NameTableCandlesOnTik);
                        }
                        else
                        {
                            int countElement = dataTik.ArrayCandles.Count;

                            if (dataTik.ArrayCandles[countElement - 1] != tool.price_scale)
                            {
                                dataTik.ArrayCandles.Add(tool.price_scale);
                                dataTik.ArrayMax.Add(tool.max_price_scale);
                                dataTik.ArrayMin.Add(tool.min_price_scale);
                                dataTik.ArrayTime.Add(tool.deal_time);

                                APIMongo.UpdateCandlesTik(dataTik, ElementMongo.NameTableCandlesOnTik);
                            }
                        }
                    }
                    

                    var dataDay = APIMongo.GetCandlesDayById(id, ElementMongo.NameTableDataCandlesOnDays).Result;

                    if (tool.close_price_scale == 0)
                        return 0;

                    Price price;

                    price.Max = tool.max_price_scale;
                    price.Min = tool.min_price_scale;
                    price.Open = tool.open_price_scale;
                    price.Close = tool.close_price_scale;
                    price.Time = tool.deal_time;

                    if (dataDay == null)
                    {
                        dataDay = new DataCandlesDay();

                        dataDay.Id = id;
                        dataDay.ArrayPrices.Add(price);

                        APIMongo.SetCandles(dataDay.ToBsonDocument(), ElementMongo.NameTableDataCandlesOnDays);
                    }
                    else
                    {
                        dataDay.ArrayPrices.Add(price);

                        APIMongo.UpdateCandlesDay(dataDay, ElementMongo.NameTableDataCandlesOnDays);
                    }
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