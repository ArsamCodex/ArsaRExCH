// wwwroot/tradingview.js
function initializeTradingViewChart(symbol) {
    if (typeof TradingView === "undefined") {
        console.error("TradingView is not defined. Please ensure the library is loaded correctly.");
        return;
    }

    new TradingView.widget({
        "container_id": "chart-container", // Make sure this matches the div ID
        "width": "100%",
        "height": "600px",
        "symbol": symbol,
        "interval": "D", // Daily interval
        "timezone": "Etc/UTC",
        "theme": "Light",
        "style": "1",
        "locale": "en",
        "toolbar_bg": "#f1f3f6",
        "enable_publishing": false,
        "allow_symbol_change": true,
        "autosize": true
    });
}

function waitForTradingViewAndInitialize(symbol) {
    const checkLibraryLoaded = setInterval(() => {
        if (typeof TradingView !== 'undefined') {
            clearInterval(checkLibraryLoaded);
            initializeTradingViewChart(symbol);
        }
    }, 100); // Check every 100ms
}
