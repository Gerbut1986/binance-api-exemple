﻿using MultiTerminal.Connections.Details.Binance;
using MultiTerminal.Connections.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;

namespace MultiTerminal.Connections
{
    internal class BinanceFutureClient : BaseCryptoClient
    {
        DateTime lastBalanceRequestTime = DateTime.MinValue;
        decimal lastBalance = 0;
        int ReconectCount = 0;
        Timer timerResetReconectCount;
        private HttpClient _httpClient;
        readonly long recvWindow = 5000;
        static long dTime = 0; // разница между временем сервера и клиента
        readonly Dictionary<string, WebSocketData> SubscribeList = new Dictionary<string, WebSocketData>();

        public BinanceFutureClient(IConnectorLogger logger, ManualResetEvent cancelToken, BinanceFutureConnectionModel model) : base(logger, cancelToken, model)
        {
        }

        internal override decimal GetBalance()
        {
            if ((DateTime.UtcNow - lastBalanceRequestTime).TotalSeconds >= 5)
            {
                lastBalance = GetBalance("USDT").Result;
                lastBalanceRequestTime = DateTime.UtcNow;
            }
            return lastBalance;
        }

        internal async Task<decimal> GetBalance(string Asset)
        {
            decimal balance = 0;
            try
            {
                if (AccountTradeType == AccountTradeType.SPOT)
                {
                    AccountInfo AI = await CallAsync<AccountInfo>(EndPoints.AccountInformationFuture, $"recvWindow={recvWindow}");
                    balance = AI.Balances.Single(b => b.Asset == Asset).Free;
                }
                else if (AccountTradeType == AccountTradeType.MARGIN)
                {
                    BinanceMarginAccountDetails details = await CallAsync<BinanceMarginAccountDetails>(EndPoints.QueryCrossMarginAccountDetails, $"recvWindow={recvWindow}");
                    balance = details.userAssets.Single(ua => ua.asset == Asset).free;
                }
            }
            catch (Exception e)
            {
                logger.LogError(ViewId + $" Balance Error: {e.Message}");
            }

            return balance;
        }

        #region Order's actions:
        public override OrderCloseResult Close(string symbol, string orderId, decimal price, decimal volume, OrderSide side, int slippage, OrderType type, int lifetimeMs)
        {
            OrderSide closeSide = side == OrderSide.Buy ? OrderSide.Sell : OrderSide.Buy;
            var openResult = Open(symbol, price, volume, closeSide, 0, slippage, 0, type, lifetimeMs);

            return new OrderCloseResult()
            {
                ClosePrice = openResult.OpenPrice,
                ExecutionTime = openResult.ExecutionTime,
                Error = openResult.Error
            };
        }
        public override bool OrderDelete(string id, string symbol, OrderType type, OrderSide side, decimal lot, decimal price)
        {
            return false;
        }
        public override OrderModifyResult Modify(string symbol, string orderId, OrderSide side, decimal slPrice, decimal tpPrice)
        {
            return new OrderModifyResult() { Error = "Not implemented" };
        }
        public override OrderOpenResult Open(string symbol, decimal price, decimal lot, OrderSide side, int magic, int slippage, int track, OrderType type, int lifetimeMs)
        {
            try
            {
                DateTime begin = DateTime.UtcNow;
                OrderInformation order = null;

                string sPrice = lot.ToString(CultureInfo.InvariantCulture);
                string sAmount = lot.ToString(CultureInfo.InvariantCulture);
                string sSide = (side == OrderSide.Buy) ? "BUY" : "SELL";

                var args = "";
                if (type == OrderType.Limit)
                {
                    args = $"symbol={symbol}&side={sSide}&type=LIMIT&timeInForce=FOK&quantity={sAmount}&price={sPrice}&recvWindow={recvWindow}";
                }
                else
                    args = $"symbol={symbol}&side={sSide}&type=MARKET&quantity={sAmount}&recvWindow={recvWindow}";

                if (AccountTradeType == AccountTradeType.SPOT)
                {
                    BinanceNewOrder o = CallAsync<BinanceNewOrder>(EndPoints.NewOrder, args).Result;

                    if (o.status == "FILLED")
                    {
                        string Id = o.orderId.ToString();
                        decimal OpenPrice = decimal.Parse(o.price, CultureInfo.InvariantCulture);
                        decimal Volume = decimal.Parse(o.executedQty, CultureInfo.InvariantCulture);

                        if (OpenPrice == 0 && o.fills != null)
                        {
                            decimal sumAmount = 0;
                            decimal sumAmountPrice = 0;

                            foreach (BinanceFill f in o.fills)
                            {
                                decimal p = decimal.Parse(f.price, CultureInfo.InvariantCulture);
                                decimal q = decimal.Parse(f.qty, CultureInfo.InvariantCulture);
                                sumAmount += q;
                                sumAmountPrice += q * p;
                            }

                            OpenPrice = sumAmountPrice / sumAmount;
                        }

                        order = GetPosition(symbol);
                        if (order == null)
                        {
                            order = new OrderInformation
                            {
                                Id = symbol,
                                Symbol = symbol,
                                Side = side,
                                OpenTime = DateTime.UtcNow
                            };
                            AddPosition(order);
                        }
                        if (order.Side != side)
                        {
                            RemovePosition(order);
                        }
                        order.OpenPrice = OpenPrice;
                        order.Volume = Volume;
                    }
                    else
                    {
                        return new OrderOpenResult() { Error = $"Order status = {o.status}" };
                    }

                }
                if (AccountTradeType == AccountTradeType.MARGIN)
                {
                    BinanceMarginNewOrder o = CallAsync<BinanceMarginNewOrder>(EndPoints.MarginAccountNewOrder, args).Result;

                    if (o.status == "FILLED")
                    {
                        string Id = o.orderId.ToString();
                        decimal OpenPrice = decimal.Parse(o.price, CultureInfo.InvariantCulture);
                        decimal Volume = decimal.Parse(o.executedQty, CultureInfo.InvariantCulture);

                        if (OpenPrice == 0 && o.fills != null)
                        {
                            decimal sumAmount = 0;
                            decimal sumAmountPrice = 0;

                            foreach (BinanceFill f in o.fills)
                            {
                                decimal p = decimal.Parse(f.price, CultureInfo.InvariantCulture);
                                decimal q = decimal.Parse(f.qty, CultureInfo.InvariantCulture);
                                sumAmount += q;
                                sumAmountPrice += q * p;
                            }

                            OpenPrice = sumAmountPrice / sumAmount;
                        }

                        order = GetPosition(symbol);
                        if (order == null)
                        {
                            order = new OrderInformation
                            {
                                Id = symbol,
                                Symbol = symbol,
                                Side = side,
                                OpenTime = DateTime.UtcNow
                            };
                            AddPosition(order);
                        }
                        if (order.Side != side)
                        {
                            RemovePosition(order);
                        }
                        order.OpenPrice = OpenPrice;
                        order.Volume = Volume;
                    }
                }

                if (order != null)
                {
                    return new OrderOpenResult()
                    {
                        Id = order.Id,
                        ExecutionTime = DateTime.UtcNow - begin,
                        OpenPrice = order.OpenPrice
                    };
                }

                return new OrderOpenResult() { Error = "Timeout error." };
            }
            catch (Exception e)
            {
                while (e.InnerException != null) e = e.InnerException;
                return new OrderOpenResult() { Error = e.Message };
            }
        }
        #endregion

        public override void Subscribe(string symbol, string id)
        {
            var quote = new TickEventArgs
            {
                Symbol = symbol,
                SymbolId = id
            };

            WebSocketData data = new WebSocketData
            {
                Active = true,
                quote = quote
            };

            if (SubscribeList.ContainsKey(quote.Symbol))
            {
                SubscribeList[quote.Symbol] = data;

            }
            else
            {
                SubscribeList.Add(quote.Symbol, data);
            }

            Subscribe(data);
        }

        public void Subscribe(WebSocketData data)
        {
            var wsDepth =
            new WebSocket($"wss://fstream.binance.com:9443/ws/{data.quote.Symbol.ToLower()}@depth");

            wsDepth.MessageReceived += (s, e) =>
            {
                try
                {
                    JObject depth = (JObject)JsonConvert.DeserializeObject(e.Message);
                    try
                    {
                        decimal b = decimal.Parse((string)((JArray)depth["a"])[0][0], CultureInfo.InvariantCulture);
                        data.quote.Bid = b;
                    }
                    catch { }
                    try
                    {
                        decimal a = decimal.Parse((string)((JArray)depth["b"])[0][0], CultureInfo.InvariantCulture);
                        data.quote.Ask = a;
                    }
                    catch { }

                    OnTick(data.quote);
                }
                catch { }
            };
            wsDepth.Closed += (s, e) =>
            {
                // logger.LogInfo($"{ViewId}: {data.quote.Symbol} - Websocket closed");

                if (!SubscribeList.ContainsKey(data.quote.Symbol)) return;

                if (SubscribeList[data.quote.Symbol].Active)
                {
                    Task.Delay(5000).ContinueWith(_ =>
                    {

                        ReconectCount++;
                        if (ReconectCount > 5)
                        {
                            //    logger.LogWarning(ViewId + " Can not open WebSocket");
                            return;
                        }
                        //  logger.LogInfo(ViewId + " Reconect to WebSocket #");

                        // если в течении минуты нет реконектов, то сбрасываем счётчик реконектов
                        if (timerResetReconectCount != null) { timerResetReconectCount.Dispose(); }
                        timerResetReconectCount = new Timer(__ => { ReconectCount = 0; timerResetReconectCount.Dispose(); }, null, 60 * 1000, 5000);

                        Subscribe(data);
                    });
                }
            };
            wsDepth.Error += (s, e) =>
            {
                logger.LogError($"{ViewId}: {data.quote.Symbol} - {e.Exception.Message}");
            };

            data.webSocket = wsDepth;
            wsDepth.Open();
        }

        #region Try to connect to Binance Options:
        async Task GetOptions()
        {
            //var socket = new WebSocket("wss://stream.binance.com:9443/ws/fapi/v3/eoptions/btcusdt");

            using (var ws = new WebSocketSharp.WebSocket("wss://nbstream.binance.com/eoptions/ws"))
            {
                ws.OnMessage += async (sender, e) =>
                await HandleWebSocketOptions(e.Data);
                ws.OnError += (sender, e) =>
                    Console.WriteLine($"WebSocket Error: {e.Message}");
                ws.OnOpen += (sender, e) =>
                    OnOpen(ws);

                ws.Connect();

                Console.WriteLine("WebSocket connected.");

                await Task.Delay(5000); // Delay for 5 seconds (adjust as needed)
                Console.ReadKey();
                ws.Close();
            }
        }

        void OnOpen(WebSocketSharp.WebSocket ws)
        {
            Console.WriteLine($"Origin: {ws.Origin}\nEmitOnPing: {ws.EmitOnPing}");
        }

        void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public async Task HandleWebSocketOptions(string json)
        {
            //var depthUpdate = JsonConvert.DeserializeObject<BinanceLentaModel>(json);
            //WriteToTxtFile(json, lentaPath);
            await Task.Run(() =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(json);
            });
        }
        #endregion
        public override void Unsubscribe(string symbol, string id)
        {
            if (SubscribeList.ContainsKey(symbol))
            {
                SubscribeList[symbol].Active = false;
                SubscribeList[symbol].webSocket.Close();
                SubscribeList.Remove(symbol);
            }
        }

        public override void Start()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", Key);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _IsLoggedIn = true;

            StartUserDataStream();
        }

        private async void StartUserDataStream()
        {
            try
            {
                BinanceEndPoint ep = (AccountTradeType == AccountTradeType.SPOT)
                    ? EndPoints.SpotCreateListenKeyFuture : EndPoints.MarginCreateListenKey;
                BinanceListenKey o = await CallAsync<BinanceListenKey>(ep);
                if (!string.IsNullOrEmpty(o.listenKey))
                {
                    OnLoggedIn();
                }
                else
                {
                    // logger.LogError(ViewId + $" listenKey is null");
                }

                try
                {
                    decimal balance = GetBalance();
                    //var ob = GetOrderBook("BTCUSDT");
                    logger.LogInfo(ViewId + $" {AccountTradeType} Balance: {balance} USDT");
                }
                catch (Exception e)
                {
                    logger.LogError(ViewId + $" Margin Balance Error: {e.Message}");
                }
            }
            catch (Exception e)
            {
                logger.LogError(ViewId + $" {e.Message}");
            }
        }

        public override void Stop(bool wait)
        {
            _IsLoggedIn = false;
            OnLoggedOut();
        }

        #region API

        public async Task<T> CallAsync<T>(BinanceEndPoint endpoint, string parameters = null, bool firstCall = true)
        {
            string finalEndpoint = endpoint.Value + (string.IsNullOrWhiteSpace(parameters) ? "" : $"?{parameters}");

            if (endpoint.isSigned)
            {
                string p = parameters + (!string.IsNullOrWhiteSpace(parameters) ? "&timestamp=" : "timestamp=") + GenerateTimeStamp(DateTime.Now.ToUniversalTime());
                var signature = GenerateSignature(Secret, p);
                finalEndpoint = $"{endpoint.Value}?{p}&signature={signature}";
            }

            string urlPath = "https://fapi.binance.com" + finalEndpoint;
            var path = new Uri(urlPath);

            HttpResponseMessage response;
            if (endpoint.Method == "POST")
                response = await _httpClient.PostAsync(path, null);
            else if (endpoint.Method == "GET")
                response = await _httpClient.GetAsync(path);
            else if (endpoint.Method == "PUT")
                response = await _httpClient.PutAsync(path, null);
            else if (endpoint.Method == "DELETE")
                response = await _httpClient.DeleteAsync(path);
            else
                throw new Exception($"Unknown type of request: endpoint.Method = \"{endpoint.Method}\"");

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


                return JsonConvert.DeserializeObject<T>(result);
            }

            if (response.StatusCode == HttpStatusCode.GatewayTimeout)
            {
                throw new Exception("Api Request Timeout.");
            }

            var e = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //"{\"code\":-1021,\"msg\":\"Timestamp for this request was 1000ms ahead of the server's time.\"}"
            var eCode = 0;
            string eMsg = "";
            if (e.IsValidJson())
            {
                try
                {
                    var i = JObject.Parse(e);

                    eCode = i["code"]?.Value<int>() ?? 0;
                    eMsg = i["msg"]?.Value<string>();
                }
                catch { }
            }

            if (firstCall && eCode == -1021)
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                long clientTime = (long)(DateTime.Now.ToUniversalTime() - epoch).TotalMilliseconds;
                BinanceServerTime sTime = await CallAsync<BinanceServerTime>(EndPoints.CheckServerTime);
                dTime = clientTime - sTime.serverTime;

                long clientTime2 = (long)(DateTime.Now.ToUniversalTime() - epoch).TotalMilliseconds;
                if ((clientTime2 - clientTime) < 1000)
                    return await CallAsync<T>(endpoint, parameters, false);
                else
                    throw new Exception(string.Format("Api Error Code: {0} Message: {1}", eCode, eMsg));
            }
            else
                throw new Exception(string.Format("Api Error Code: {0} Message: {1}", eCode, eMsg));
        }
        public string GenerateTimeStamp(DateTime baseDateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return ((long)(baseDateTime.ToUniversalTime() - epoch).TotalMilliseconds - dTime).ToString();
        }
        public string GenerateSignature(string apiSecret, string message)
        {
            var key = Encoding.UTF8.GetBytes(apiSecret);
            string stringHash;
            using (var hmac = new HMACSHA256(key))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                stringHash = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }

            return stringHash;
        }
        #endregion
    }
}
