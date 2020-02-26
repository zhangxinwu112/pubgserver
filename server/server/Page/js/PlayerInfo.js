var playerUIApp = new Vue({
            el: '#playerUIApp',
            data: {
				isShowPlayInfo:false,
				
				playerScoreList: 
				    [{
				    	"id": 13,
				    	"userId": 55,
				    	"bulletCount": 80,
				    	"lifeValue": 228,
				    	"fightScore": 35,
				    	"userName": "天涯"
				    }, {
				    	"id": 14,
				    	"userId": 56,
				    	"bulletCount": 80,
				    	"lifeValue": 0,
				    	"fightScore": 35,
				    	"userName": "天涯1"
				    }, {
				    	"id": 15,
				    	"userId": 57,
				    	"bulletCount": 80,
				    	"lifeValue": 0,
				    	"fightScore": 35,
				    	"userName": "天涯0"
				    }]
			
            },
            mounted() {
				
				
            },
            methods: {
               ClosePlayInfoWindow()
               {
					playerUIApp.isShowPlayInfo = false;
               },
				
            }
        })
		
	function GetRoomLifeInfoByUser(data)
	{
		var _json = JSON.stringify(data);
		 var o = JSON.parse(_json);
		 playerUIApp.playerScoreList = o;
	}
	
		
		
		