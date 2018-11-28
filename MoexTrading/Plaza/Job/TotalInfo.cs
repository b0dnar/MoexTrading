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

            string streamInfo = "FORTS_FUTINFO_REPL", tableTools = "fut_sess_contents", streamCommonn = "FORTS_FUTCOMMON_REPL", tableCandles = "common", streamGlass = "FORTS_FUTAGGR50_REPL", streamTrade = "FORTS_FUTTRADE_REPL", tableTime = "heartbeat";
            string strConnectInfo = "p2repl://" + streamInfo + ";tables=" + tableTools;
            string strConnectCommon = "p2repl://" + streamCommonn + ";tables=" + tableCandles;
            string strConnectGlass = "p2repl://" + streamGlass;
            string strConnectTime = "p2ordbook://FORTS_ORDLOG_REPL;snapshot=FORTS_ORDBOOK_REPL";

            CGate.Open("ini=/Plaza/bin/cgate.ini;key=11111111");
            CGate.LogInfo("test .Net log.");
            Connection conn = new Connection("p2tcp://127.0.0.1:4001;app_name=TotalInfo");

            Listener listInfo = new Listener(conn, strConnectInfo);
            listInfo.Handler += new Listener.MessageHandler(MessageHandlerTools);

            Listener listCommon = new Listener(conn, strConnectCommon);
            listCommon.Handler += new Listener.MessageHandler(MessageHandlerCandles);

            Listener listGlass = new Listener(conn, strConnectGlass);
            listGlass.Handler += new Listener.MessageHandler(MessageHandlerGlass);

            Listener listTime = new Listener(conn, strConnectTime);
            listTime.Handler += new Listener.MessageHandler(MessageHandlerTime);

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

                        if (listGlass.State == State.Closed)
                        {
                            listGlass.Open("");
                        }
                        else if (listGlass.State == State.Error)
                        {
                            listGlass.Close();
                        }

                        if (listTime.State == State.Closed)
                        {
                            listTime.Open("");
                        }
                        else if (listTime.State == State.Error)
                        {
                            listTime.Close();
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
            listGlass.Close();
            listGlass.Dispose();
            listTime.Close();
            listTime.Dispose();
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
                    common com = new common(replmsg.Data);
                    int id = com.isin_id;

                    var dataTik = APIMongo.GetCandlesTikById(id, ElementMongo.NameTableCandlesOnTik);

                    if (com.price > 0)
                    {
                        if (dataTik == null)
                        {
                            DataCandlesTik data = new DataCandlesTik();
                            data.Id = id;
                            data.ArrayCandles.Add(com.price);

                            APIMongo.SetCandles(data.ToBsonDocument(), ElementMongo.NameTableCandlesOnTik);
                        }
                        else
                        {
                            int countElement = dataTik.ArrayCandles.Count;

                            if (dataTik.ArrayCandles[countElement - 1] != com.price)
                            {
                                dataTik.ArrayCandles.Add(com.price);
                                dataTik.ArrayMax.Add(dataTik.ArrayCandles[countElement - 1] > dataTik.ArrayCandles[countElement] ? dataTik.ArrayCandles[countElement - 1] : dataTik.ArrayCandles[countElement]);
                                dataTik.ArrayMin.Add(dataTik.ArrayCandles[countElement - 1] > dataTik.ArrayCandles[countElement] ? dataTik.ArrayCandles[countElement] : dataTik.ArrayCandles[countElement - 1]);
                                dataTik.ArrayTime.Add(com.mod_time_ns);

                                APIMongo.UpdateCandlesTik(dataTik, ElementMongo.NameTableCandlesOnTik);
                            }
                        }
                    }

                    if (com.close_price > 0)
                    {
                        Price price = new Price();
                        price.Max = com.max_price;
                        price.Min = com.min_price;
                        price.Open = com.open_price;
                        price.Close = com.close_price;
                        price.Time = com.mod_time;

                        var dataDay = APIMongo.GetCandlesDayById(id, ElementMongo.NameTableCandlesOnDays).Result;

                        if (dataDay == null)
                        {
                            dataDay = new DataCandlesDay();

                            dataDay.Id = id;
                            dataDay.ArrayPrices.Add(price);

                            APIMongo.SetCandles(dataDay.ToBsonDocument(), ElementMongo.NameTableCandlesOnDays);
                        }
                        else
                        {
                            dataDay.ArrayPrices.Add(price);

                            APIMongo.UpdateCandlesDay(dataDay, ElementMongo.NameTableCandlesOnDays);
                        }
                    }

                    if (com.cur_kotir_scale > 0)
                    {
                        var curKotir = new DataKotirovka();
                        curKotir.Id = id;
                        curKotir.Value = com.cur_kotir_scale;

                        var dataKotir = APIMongo.GetKotirovkaById(id).Result;

                        if (dataKotir == null)
                        {
                            APIMongo.SetKotirovka(curKotir.ToBsonDocument());
                        }
                        else
                        {
                            if (dataKotir.Value == curKotir.Value)
                                return 0;

                            curKotir.Diference = com.trend_scale;
                            curKotir.Percent = curKotir.Diference * 100 / curKotir.Value;

                            APIMongo.UpdateKotirovka(curKotir);
                        }
                    }
                }

                return 0;
            }
            catch (CGateException e)
            {
                return (int)e.ErrCode;
            }
        }

        private static int MessageHandlerGlass(Connection conn, Listener listener, Message msg)
        {
            try
            {
                if (msg.Type == MessageType.MsgStreamData)
                {
                    StreamDataMessage replmsg = (StreamDataMessage)msg;
                    orders_aggr glass = new orders_aggr(replmsg.Data);
                    int id = glass.isin_id;

                    if (glass.volume == 0 || glass.price == 0)
                        return 0;

                    var data = APIMongo.GetGlassById(id);

                    Glass gl = new Glass();
                    gl.Price = glass.price;
                    gl.Volum = glass.volume;
                    gl.Dir = glass.dir;

                    if (data == null)
                    {
                        data = new DataGlass();
                        data.Id = id;
                        data.ArrayGlass.Add(gl);

                        APIMongo.SetGlass(data.ToBsonDocument());
                    }
                    else
                    {
                        const int maxElem = 50;
                        int countElement = data.ArrayGlass.Count;

                        if (countElement == maxElem)
                            data.ArrayGlass.RemoveAt(0);

                        data.ArrayGlass.Add(gl);

                        APIMongo.UpdateGlass(data);
                    }
                }

                return 0;
            }
            catch (CGateException e)
            {
                return (int)e.ErrCode;
            }
        }

        private static int MessageHandlerTime(Connection conn, Listener listener, Message msg)
        {
            try
            {
                if (msg.Type == MessageType.MsgStreamData)
                {
                    StreamDataMessage replmsg = (StreamDataMessage)msg;
                    if (replmsg.MsgName.Equals("orders"))
                    {
                        orders_log order = new orders_log(replmsg.Data);
                    }



                    heartbeat tablServTime = new heartbeat(replmsg.Data);

                    var time = tablServTime.server_time;
                    var temp = new DateTime(2018, 1, 1);

                    if (time.Equals(temp))
                        return 0;

                    DataServer.Time = time;
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