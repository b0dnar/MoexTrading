﻿using System;
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
            string streamInfo = "FORTS_FUTINFO_REPL", tableTools = "fut_sess_contents", streamCommonn = "FORTS_FUTCOMMON_REPL", tableCandles = "common", streamGlass = "FORTS_FUTAGGR50_REPL", streamDeal = "FORTS_DEALS_REPL", tableTime = "heartbeat";
            string strConnectInfo = "p2repl://" + streamInfo + ";tables=" + tableTools;
            string strConnectCommon = "p2repl://" + streamCommonn + ";tables=" + tableCandles;
            string strConnectGlass = "p2repl://" + streamGlass;
            string strConnectTime = "p2repl://" + streamDeal + ";tables=" + tableTime;

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

                    var oldData = APIMongo.GetDataById<DataTools>(id, ElementMongo.NameTableTools);
                    if (oldData == null)
                    {
                        var data = new DataTools { Id = id, Name = tool.isin };
                        APIMongo.SetData(data.ToBsonDocument(), ElementMongo.NameTableTools);
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
                if (msg.Type != MessageType.MsgStreamData)
                    return 0;

                StreamDataMessage replmsg = (StreamDataMessage)msg;
                common com = new common(replmsg.Data);
                int id = com.isin_id;

                var dataTik = APIMongo.GetDataById<DataCandlesTik>(id, ElementMongo.NameTableCandlesOnTik);

                if (com.price_scale > 0)
                {
                    if (dataTik == null)
                    {
                        DataCandlesTik data = new DataCandlesTik();
                        data.Id = id;
                        data.ArrayCandles.Add(com.price_scale);

                        APIMongo.SetData(data.ToBsonDocument(), ElementMongo.NameTableCandlesOnTik);
                    }
                    else
                    {
                        int countElement = dataTik.ArrayCandles.Count;

                        if (dataTik.ArrayCandles[countElement - 1] != com.price_scale)
                        {
                            dataTik.ArrayCandles.Add(com.price_scale);
                            dataTik.ArrayMax.Add(dataTik.ArrayCandles[countElement - 1] > dataTik.ArrayCandles[countElement] ? dataTik.ArrayCandles[countElement - 1] : dataTik.ArrayCandles[countElement]);
                            dataTik.ArrayMin.Add(dataTik.ArrayCandles[countElement - 1] > dataTik.ArrayCandles[countElement] ? dataTik.ArrayCandles[countElement] : dataTik.ArrayCandles[countElement - 1]);
                            dataTik.ArrayTime.Add(com.mod_time_ns);

                            APIMongo.UpdateData<DataCandlesTik>(dataTik, ElementMongo.NameTableCandlesOnTik);
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

                    var dataDay = APIMongo.GetDataById<DataCandlesDay>(id, ElementMongo.NameTableCandlesOnDays);

                    if (dataDay == null)
                    {
                        dataDay = new DataCandlesDay();

                        dataDay.Id = id;
                        dataDay.ArrayPrices.Add(price);

                        APIMongo.SetData(dataDay.ToBsonDocument(), ElementMongo.NameTableCandlesOnDays);
                    }
                    else
                    {
                        dataDay.ArrayPrices.Add(price);

                        APIMongo.UpdateData<DataCandlesDay>(dataDay, ElementMongo.NameTableCandlesOnDays);
                    }
                }

                if (com.cur_kotir_scale > 0)
                {
                    var curKotir = new DataKotirovka();
                    curKotir.Id = id;
                    curKotir.Value = com.cur_kotir_scale;

                    var dataKotir = APIMongo.GetDataById<DataKotirovka>(id, ElementMongo.NameTableKotirovka);

                    if (dataKotir == null)
                    {
                        APIMongo.SetData(curKotir.ToBsonDocument(), ElementMongo.NameTableKotirovka);
                    }
                    else
                    {
                        if (dataKotir.Value == curKotir.Value)
                            return 0;

                        curKotir.Diference = com.trend_scale;
                        curKotir.Percent = curKotir.Diference * 100 / curKotir.Value;

                        APIMongo.UpdateData<DataKotirovka>(curKotir, ElementMongo.NameTableKotirovka);
                    }
                }

                if(com.best_buy_scale > 0 || com.best_sell_scale > 0)
                {
                    DataDeal deal = new DataDeal();
                    deal.Id = id;
                    deal.Buy = com.best_buy_scale;
                    deal.Sell = com.best_sell_scale;

                    var dataDeal = APIMongo.GetDataById<DataDeal>(id, ElementMongo.NameTableDeal);
                    if (dataDeal == null)
                        APIMongo.SetData(deal.ToBsonDocument(), ElementMongo.NameTableDeal);
                    else if (dataDeal.Buy != deal.Buy || dataDeal.Sell != deal.Sell)
                        APIMongo.UpdateData(deal, ElementMongo.NameTableDeal);
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

                    var data = APIMongo.GetDataById<DataGlass>(id, ElementMongo.NameTableGlass);

                    Glass gl = new Glass();
                    gl.Price = glass.price;
                    gl.Volum = glass.volume;
                    gl.Dir = glass.dir;

                    if (data == null)
                    {
                        data = new DataGlass();
                        data.Id = id;
                        data.ArrayGlass.Add(gl);

                        APIMongo.SetData(data.ToBsonDocument(), ElementMongo.NameTableGlass);
                    }
                    else
                    {
                        const int maxElem = 50;
                        int countElement = data.ArrayGlass.Count;

                        if (countElement == maxElem)
                            data.ArrayGlass.RemoveAt(0);

                        data.ArrayGlass.Add(gl);

                        APIMongo.UpdateData<DataGlass>(data, ElementMongo.NameTableGlass);
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
                    heartbeat tablServTime = new heartbeat(replmsg.Data);

                    DataServer.Time = tablServTime.server_time;
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