mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
    console.log("Hello, world!");
  },

  GetPlayerData: function () {
    myGameInstance.SendMessage('Yandex', 'SetName', player.getName());
    myGameInstance.SendMessage('Yandex', 'SetPhoto', player.getPhoto("large"));
  },

  RateGame: function(){
    ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
  },



  SaveExtern: function(date){
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
  },

  LoadExtern: function(){
    player.getData().then(_date =>{
      const myJSON = JSON.stringify(_date);
      myGameInstance.SendMessage('GameManager', 'SetPlayerInfo', myJSON);
    }); 
  },

  SetToLeaderboard : function(value){
    ysdk.getLeaderboards()
      .then(lb => {
        lb.setLeaderboardScore('Money3', value);
      });
  },

  ShowAdv : function(){
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          myGameInstance.SendMessage("GameManager", "UnPauseAudi");
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
        } 
        })
  },

  AddMoneyExtern : function(){
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
          myGameInstance.SendMessage("GameManager", "PauseAudi");
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage("GameManager", "AddMoney");
        },
        onClose: () => {
          console.log('Video ad closed.');
          myGameInstance.SendMessage("GameManager", "UnPauseAudi");
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
  },

  GetLang: function () {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
  },

});