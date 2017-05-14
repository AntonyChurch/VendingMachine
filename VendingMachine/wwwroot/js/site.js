// Write your Javascript code.
var sessionId = $("#SessionId").val();
var displayText = $("#display-text");
var depositButton = $("#deposit-button");
var coinWeight = $("#coin-weight");
var coinSize = $("#coin-size");

var itemSelect = function(){
    var itemId = $(this).data('id');
    var body = "sessionId=" + sessionId  + "&";
    body += "itemId=" + itemId;

    $.ajax({
        method: "post",
        url: "/Home/BuyProduct",
        data: body
    })
    .done(function(result){
        if(result.canAfford){
            //Redirect with params
            window.location.href = "/Home/Index?change=" + result.balance + "&itemId=" + itemId;
        }
        else{
            displayText.text(result.displayText);
        }
    });
};

var coinWeightReady = false;
var coinWeightChange = function(){
    var currentValue = $(this).val();
    var valueFloat = parseFloat(currentValue);
    console.log(valueFloat);
    if(!isNaN(valueFloat)){
        coinWeightReady = true;
        if(coinSizeReady){
            turnOnDepositCoinButton();
        }
        else{
            turnOffDepositCoinButton();
        }
    }
    else{
        coinWeightReady = false;
        turnOffDepositCoinButton();
    }
};

var coinSizeReady = false;
var coinSizeChange = function(){
    var currentValue = $(this).val();
    var valueFloat = parseFloat(currentValue);
    console.log(valueFloat);
    if(!isNaN(valueFloat)){
        coinSizeReady = true;
        if(coinWeightReady){
            turnOnDepositCoinButton();
        }
        else{
            turnOffDepositCoinButton();
        }
    }
    else{
        coinSizeReady = false;
        turnOffDepositCoinButton();
    }
};

var turnOnDepositCoinButton = function(){
    depositButton.prop('disabled', false);
};

var turnOffDepositCoinButton = function(){
    depositButton.prop('disabled', true);
};

var depositButtonClick = function(){
    var weight = coinWeight.val();
    var size = coinSize.val();

    var body = "coinWeight=" + weight + "&";
    body += "coinSize=" + size + "&";
    body += "sessionId=" + sessionId;

    $.ajax({
        method: "post",
        url: "/Home/InsertCoin",
        data: body
    })
    .done(function(result){
        displayText.text(result.newTotalString);
        //Reset the coin inputs
        coinWeight.val("");
        coinSize.val("");
    });
};

$(document).ready(function(){
    turnOffDepositCoinButton();

    $(".item-select-button").click(itemSelect);

    coinWeight.change(coinWeightChange);
    coinSize.change(coinSizeChange);    

    depositButton.click(depositButtonClick);
});