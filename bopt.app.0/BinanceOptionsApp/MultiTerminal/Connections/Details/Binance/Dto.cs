using Newtonsoft.Json;
using System.Collections.Generic;

namespace MultiTerminal.Connections.Details.Binance
{
    #region Testnet models:
    public class BalanceTestnet
    {
        [JsonProperty("accountAlias")]
        public string AccountAlias { get; set; }
        [JsonProperty("asset")]
        public string Asset { get; set; }
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
        [JsonProperty("crossWalletBalance")]
        public decimal CrossWalletBalance { get; set; }
        [JsonProperty("crossUnPnl")]
        public decimal CrossUnPnl { get; set; }
        [JsonProperty("availableBalance")]
        public decimal AvailableBalance { get; set; }
        [JsonProperty("maxWithdrawAmount")]
        public decimal MaxWithdrawAmount { get; set; }
        [JsonProperty("marginAvailable")]
        public bool MarginAvailable { get; set; }
        [JsonProperty("updateTime")]
        public long UpdateTime { get; set; }
    }

    #endregion
    public class AccountInfo
    {
        [JsonProperty("makerCommission")]
        public int MakerCommission { get; set; }
        [JsonProperty("takerCommission")]
        public int TakerCommission { get; set; }
        [JsonProperty("buyerCommission")]
        public int BuyerCommission { get; set; }
        [JsonProperty("sellerCommission")]
        public int SellerCommission { get; set; }
        [JsonProperty("canTrade")]
        public bool CanTrade { get; set; }
        [JsonProperty("canWithdraw")]
        public bool CanWithdraw { get; set; }
        [JsonProperty("canDeposit")]
        public bool CanDeposit { get; set; }
        [JsonProperty("balances")]
        public IEnumerable<BinanceBalance> Balances { get; set; }
    }
    public class BinanceBalance
    {
        [JsonProperty("asset")]
        public string Asset { get; set; }
        [JsonProperty("free")]
        public decimal Free { get; set; }
        [JsonProperty("locked")]
        public decimal Locked { get; set; }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class RateLimit
    {
        public string rateLimitType { get; set; }
        public string interval { get; set; }
        public int limit { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class BinanceFilter
    {
        public string filterType { get; set; }
        public string minPrice { get; set; }
        public string maxPrice { get; set; }
        public string tickSize { get; set; }
        public string minQty { get; set; }
        public string maxQty { get; set; }
        public string stepSize { get; set; }
        public string minNotional { get; set; }
        public int? limit { get; set; }
        public int? maxNumAlgoOrders { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class BinanceSymbol
    {
        public string symbol { get; set; }
        public string status { get; set; }
        public string baseAsset { get; set; }
        public int baseAssetPrecision { get; set; }
        public string quoteAsset { get; set; }
        public int quotePrecision { get; set; }
        public List<string> orderTypes { get; set; }
        public bool icebergAllowed { get; set; }
        public List<BinanceFilter> filters { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class ExchangeInfo
    {
        public string timezone { get; set; }
        public long serverTime { get; set; }
        public List<RateLimit> rateLimits { get; set; }
        public List<object> exchangeFilters { get; set; }
        public List<BinanceSymbol> symbols { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class BinanceFill
    {
        public string price { get; set; }
        public string qty { get; set; }
        public string commission { get; set; }
        public string commissionAsset { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class BinanceNewOrder
    {
        [JsonProperty("symbol", Required = Required.Default)]
        public string symbol { get; set; }
        [JsonProperty("orderId", Required = Required.Default)]
        public long orderId { get; set; }
        [JsonProperty("clientOrderId", Required = Required.Default)]
        public string clientOrderId { get; set; }
        [JsonProperty("transactTime", Required = Required.Default)]
        public long transactTime { get; set; }
        [JsonProperty("price", Required = Required.Default)]
        public string price { get; set; }
        [JsonProperty("origQty", Required = Required.Default)]
        public string origQty { get; set; }
        [JsonProperty("executedQty", Required = Required.Default)]
        public string executedQty { get; set; }
        [JsonProperty("cummulativeQuoteQty", Required = Required.Default)]
        public string cummulativeQuoteQty { get; set; }
        [JsonProperty("status", Required = Required.Default)]
        public string status { get; set; }
        [JsonProperty("timeInForce", Required = Required.Default)]
        public string timeInForce { get; set; }
        [JsonProperty("type", Required = Required.Default)]
        public string type { get; set; }
        [JsonProperty("side", Required = Required.Default)]
        public string side { get; set; }
        [JsonProperty("fills", Required = Required.Default)]
        public List<BinanceFill> fills { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class BinanceTestnetNewOrder
    {
        [JsonProperty("clientOrderId", Required = Required.Default)]
        public string clientOrderId { get; set; }
        [JsonProperty("cumQty", Required = Required.Default)]
        public string cumQty { get; set; }
        [JsonProperty("cumQuote", Required = Required.Default)]
        public string cumQuote { get; set; }
        [JsonProperty("executedQty", Required = Required.Default)]
        public string executedQty { get; set; }
        [JsonProperty("orderId", Required = Required.Default)]
        public long orderId { get; set; }
        [JsonProperty("avgPrice", Required = Required.Default)]
        public string avgPrice { get; set; }
        [JsonProperty("origQty", Required = Required.Default)]
        public string origQty { get; set; }
        [JsonProperty("price", Required = Required.Default)]
        public string price { get; set; }
        [JsonProperty("reduceOnly", Required = Required.Default)]
        public bool reduceOnly { get; set; }
        [JsonProperty("side", Required = Required.Default)]
        public string side { get; set; }
        [JsonProperty("positionSide", Required = Required.Default)]
        public string positionSide { get; set; }
        [JsonProperty("status", Required = Required.Default)]
        public string status { get; set; }
        [JsonProperty("stopPrice", Required = Required.Default)]
        public string stopPrice { get; set; }
        [JsonProperty("closePosition", Required = Required.Default)]
        public bool closePosition { get; set; }
        [JsonProperty("symbol", Required = Required.Default)]
        public string symbol { get; set; }
        [JsonProperty("timeInForce", Required = Required.Default)]
        public string timeInForce { get; set; }
        [JsonProperty("type", Required = Required.Default)]
        public string type { get; set; }
        [JsonProperty("origtype", Required = Required.Default)]
        public string origtype { get; set; }
        [JsonProperty("activatePrice", Required = Required.Default)]
        public string activatePrice { get; set; }
        [JsonProperty("priceRate", Required = Required.Default)]
        public string priceRate { get; set; }
        [JsonProperty("updateTime", Required = Required.Default)]
        public long updateTime { get; set; }
        [JsonProperty("workingType", Required = Required.Default)]
        public string workingType { get; set; }
        [JsonProperty("priceProtect", Required = Required.Default)]
        public bool priceProtect { get; set; }       
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class BinanceServerTime
    {
        public long serverTime { get; set; }

    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class BinanceListenKey
    {
        public string listenKey { get; set; }

    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class BinanceUserAsset
    {
        public string asset { get; set; }
        public decimal borrowed { get; set; }
        public decimal free { get; set; }
        public decimal interest { get; set; }
        public decimal locked { get; set; }
        public decimal netAsset { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class BinanceMarginAccountDetails
    {
        public bool borrowEnabled { get; set; }
        public decimal marginLevel { get; set; }
        public decimal totalAssetOfBtc { get; set; }
        public decimal totalLiabilityOfBtc { get; set; }
        public decimal totalNetAssetOfBtc { get; set; }
        public bool tradeEnabled { get; set; }
        public bool transferEnabled { get; set; }
        public List<BinanceUserAsset> userAssets { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class BinanceMarginNewOrder
    {
        [JsonProperty("symbol", Required = Required.Default)]
        public string symbol { get; set; }
        [JsonProperty("orderId", Required = Required.Default)]
        public long orderId { get; set; }
        [JsonProperty("clientOrderId", Required = Required.Default)]
        public string clientOrderId { get; set; }
        [JsonProperty("transactTime", Required = Required.Default)]
        public long transactTime { get; set; }
        [JsonProperty("price", Required = Required.Default)]
        public string price { get; set; }
        [JsonProperty("origQty", Required = Required.Default)]
        public string origQty { get; set; }
        [JsonProperty("executedQty", Required = Required.Default)]
        public string executedQty { get; set; }
        [JsonProperty("cummulativeQuoteQty", Required = Required.Default)]
        public string cummulativeQuoteQty { get; set; }
        [JsonProperty("status", Required = Required.Default)]
        public string status { get; set; }
        [JsonProperty("timeInForce", Required = Required.Default)]
        public string timeInForce { get; set; }
        [JsonProperty("type", Required = Required.Default)]
        public string type { get; set; }
        [JsonProperty("side", Required = Required.Default)]
        public string side { get; set; }
        [JsonProperty("marginBuyBorrowAmount", Required = Required.Default)]
        public int marginBuyBorrowAmount { get; set; }
        [JsonProperty("marginBuyBorrowAsset", Required = Required.Default)]
        public string marginBuyBorrowAsset { get; set; }
        [JsonProperty("isIsolated", Required = Required.Default)]
        public bool isIsolated { get; set; }
        [JsonProperty("fills", Required = Required.Default)]
        public List<BinanceFill> fills { get; set; }
    }
}
