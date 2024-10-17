window.initializeTradingViewChart = (dotNetHelper) => {
    new TradingView.widget({
        "width": 980,
        "height": 610,
        "symbol": "BINANCE:BTCUSDT",
        "interval": "D",
        "timezone": "Etc/UTC",
        "theme": "light",
        "style": "1",
        "locale": "en",
        "toolbar_bg": "#f1f3f6",
        "enable_publishing": false,
        "allow_symbol_change": true,
        "container_id": "tradingview-chart",
        "datafeed": new Datafeed(dotNetHelper)
    });
};

class Datafeed {
    constructor(dotNetHelper) {
        this.dotNetHelper = dotNetHelper;
        this.price = null;
        this.startPriceTracking();
    }

    startPriceTracking() {
        setInterval(() => {
            // Fetch the current BTC price (you can use any price API)
            fetch("https://api.binance.com/api/v3/ticker/price?symbol=BTCUSDT")
                .then(response => response.json())
                .then(data => {
                    const currentPrice = data.price;
                    if (this.price !== currentPrice) {
                        this.price = currentPrice;
                        this.dotNetHelper.invokeMethodAsync('UpdatePrice', currentPrice);
                    }
                });
        }, 5000); // Update every 5 seconds
    }
}
